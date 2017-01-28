using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Download_Manager
{
    public class DownloadManager
    {
        private long totalBytesRead = 0;
        private static readonly string userAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2224.3 Safari/537.36";

        private FileMode fileMode = FileMode.Create;

        private FileInformation fileInformation;
        private ToolStripProgressBar progressBarStatus;
        private Segment[] segments;
        private Thread[] threads;
        private static readonly Regex regexURL = new Regex("(\\w+):\\/\\/([\\w.]+\\/?)\\S*"), regexFileName = new Regex("attachment;filename=(?<filename>.*)");

        public DownloadManager(FileInformation fileInformation, ToolStripProgressBar progressBarStatus)
        {
            this.fileInformation = fileInformation;
            this.progressBarStatus = progressBarStatus;

            segments = Segment.CalculateSegments(Form.countSegments, fileInformation.contentLength);
        }

        private void UpdateProgress(long totalBytesRead, long contentLength)
        {
            Action action;
            
            progressBarStatus.ProgressBar.Parent.Invoke(action = () =>
            {
                try
                {
                    progressBarStatus.Value = Convert.ToInt32((totalBytesRead * 100) / contentLength);
                }
                catch
                {

                }
            });
        }

        public void Start(object _index)
        {
            int index = (int)_index;

            byte[] buffer = new byte[524288];
            int bytesRead;

            HttpWebRequest httpWebRequest = GetHttpWebRequest(segments[index].StartPoint, segments[index].EndPoint, fileInformation.downloadLink);
            
            try
            {
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    Debugger.Log(10, "test", "\n" + index + ") Size = " + httpWebResponse.ContentLength + "\nStart = " + segments[index].StartPoint + "\nEND = " + segments[index].EndPoint + "\nTotal Size = " + fileInformation.contentLength + "\n\n");

                    using (Stream responseStream = httpWebResponse.GetResponseStream())
                    {
                        if (!Directory.Exists(fileInformation.saveTo + "\\Temp\\" + fileInformation.name))
                        {
                            Directory.CreateDirectory(fileInformation.saveTo + "\\Temp\\" + fileInformation.name);
                        }

                        using (FileStream fileStream = new FileStream(fileInformation.saveTo + "\\Temp\\" + fileInformation.name + "\\" + (index + 1), fileMode, FileAccess.Write, FileShare.None))
                        {
                            while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                totalBytesRead += bytesRead;

                                fileStream.Write(buffer, 0, bytesRead);
                                UpdateProgress(totalBytesRead, fileInformation.contentLength);

                                Thread.Sleep(segments.Length);
                            }
                        }
                    }
                }
            }
            catch (WebException webException)
            {
                Debugger.Log(0, "WebException", index + ") " + webException.Message);
            }

            threads[index] = null;

            Debugger.Log(0, "WebException", "total = " + totalBytesRead + "\nLen = " + fileInformation.contentLength + "\nDifference = " + (fileInformation.contentLength - totalBytesRead));

            AppendSegments();
        }

        public void Start()
        {
            threads = new Thread[segments.Length];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ParameterizedThreadStart(Start));
                threads[i].Start(i);
            }
        }

        private void AppendSegments()
        {
            int countCompletedThreads = 0;

            for (int i = 0; i < threads.Length; i++)       // checking if all threads have finished downloading...
            {
                if (threads[i] == null)
                {
                    countCompletedThreads++;
                }
            }

            if (countCompletedThreads != threads.Length)
            {
                return;
            }

            byte[] buffer = new byte[524288];
            int bytesRead;

            totalBytesRead = 0;

            using (FileStream fileOutputStream = new FileStream(fileInformation.saveTo + "\\" + fileInformation.name, FileMode.Append, FileAccess.Write))
            {
                for (int i = 0; i < segments.Length; i++)
                {
                    using (FileStream fileInputStream = new FileStream(fileInformation.saveTo + "\\Temp\\" + fileInformation.name + "\\" + (i + 1), FileMode.Open, FileAccess.Read))
                    {
                        while ((bytesRead = fileInputStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            Debugger.Log(0, "Append", "Appending files... ");

                            totalBytesRead += bytesRead;
                            fileOutputStream.Write(buffer, 0, bytesRead);
                            fileOutputStream.Flush();
                            UpdateProgress(totalBytesRead, fileInformation.contentLength);
                        }
                    }
                }
            }

            if (Directory.Exists(fileInformation.saveTo + "\\Temp\\" + fileInformation.name))
            {
                Directory.Delete(fileInformation.saveTo + "\\Temp\\" + fileInformation.name, true);
            }
        }

        private static HttpWebRequest GetHttpWebRequest(string method, string requestUriString)
        {
            HttpWebRequest httpWebRequest = null;

            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            httpWebRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.Method = method;
            httpWebRequest.ReadWriteTimeout = 30000;
            httpWebRequest.Proxy = null;

            return httpWebRequest;
        }

        private static HttpWebRequest GetHttpWebRequest(long rangeFrom, long rangeTo, string requestUriString)
        {
            HttpWebRequest httpWebRequest = null;

            try
            {
                httpWebRequest = GetHttpWebRequest("GET", requestUriString);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            httpWebRequest.AddRange(rangeFrom, rangeTo);

            return httpWebRequest;
        }

        public static bool IsURL(string input)
        {
            if (string.IsNullOrEmpty(regexURL.Match(input).ToString()))
            {
                return false;
            }

            return true;
        }

        // doesnot work perfectly and not optimized...
        public static void GetFileInformation(ref long contentLength, ref string fileType, ref string fileName, string downloadLink)
        {
            string responseUri;

            HttpWebRequest httpWebRequest = null;
            WebHeaderCollection webHeaderCollection = null;

            try
            {
                httpWebRequest = GetHttpWebRequest("HEAD", downloadLink);

                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    contentLength = httpWebResponse.ContentLength;

                    if (contentLength < 1)
                    {
                        throw new Exception("Remote server returned invalid file size");
                    }

                    fileType = httpWebResponse.ContentType;
                    responseUri = httpWebResponse.ResponseUri.ToString();
                    webHeaderCollection = httpWebResponse.Headers;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            fileName = webHeaderCollection["Content-Disposition"];

            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = regexFileName.Match(fileName).Groups["filename"].ToString();

                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = Path.GetFileName(responseUri);
                }
            }
            else
            {
                fileName = Path.GetFileName(downloadLink);

                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = "Unavailable";
                }
            }

            fileName = fileName.Replace("%20", " ");

            if (fileName.Contains("?"))
            {
                fileName = fileName.Substring(0, fileName.IndexOf('?'));
            }
        }
    }
}