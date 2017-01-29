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
        private byte[] buffer = new byte[524288];       // 5*1024 KB = 512 KB...
        private int bytesRead;
        private long totalBytesRead = 0;
        private static readonly string userAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2224.3 Safari/537.36";
        private string[] paths = new string[2];         // paths[0] = temporary download path and, paths[1] = actual download path...

        private FileMode fileMode = FileMode.Create;

        private FileInformation fileInformation;
        private Controls controls;
        private Segment[] segments;
        private Thread[] threads;
        private static readonly Regex regexURL = new Regex("(\\w+):\\/\\/([\\w.]+\\/?)\\S*"), regexFileName = new Regex("attachment;filename=(?<filename>.*)");

        public DownloadManager(FileInformation fileInformation, Controls controls)
        {
            this.fileInformation = fileInformation;
            this.controls = controls;

            paths[0] = fileInformation.saveTo + "\\Temporary Files\\" + fileInformation.name;
            paths[1] = fileInformation.saveTo + "\\" + fileInformation.name;

            segments = Segment.CalculateSegments(Form.countSegments, fileInformation.contentLength);
        }

        public void UpdateReceived(long totalBytesRead)
        {
            controls.UpdateReceived(FileInformation.FormatSize(totalBytesRead) + " of " + fileInformation.size);
        }

        private void UpdateProgressBarValue(long totalBytesRead, long contentLength)
        {
            controls.UpdateProgressBarValue(Convert.ToInt32((totalBytesRead * 100) / contentLength));
        }

        private void UpdateProgressBarStatusValue(long totalBytesRead, long contentLength)
        {
            controls.UpdateProgressBarStatusValue(Convert.ToInt32((totalBytesRead * 100) / contentLength));
        }

        public void Start(object _index)        // the exception handling procedure will be changed...
        {
            byte[] buffer = new byte[524288];       // 5*1024 KB = 512 KB...
            int index = (int)_index, bytesRead;

            HttpWebRequest httpWebRequest;

            try
            {
                httpWebRequest = GetHttpWebRequest(segments[index].StartPoint, segments[index].EndPoint, fileInformation.downloadLink);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            
            try
            {
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream responseStream = httpWebResponse.GetResponseStream())
                    {
                        if (!Directory.Exists(paths[0]))
                        {
                            Directory.CreateDirectory(paths[0]);
                        }

                        using (FileStream fileStream = new FileStream(paths[0] + "\\" + (index + 1), fileMode, FileAccess.Write, FileShare.None))
                        {
                            controls.UpdateStatus("Downloading");

                            while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                totalBytesRead += bytesRead;

                                fileStream.Write(buffer, 0, bytesRead);

                                UpdateReceived(totalBytesRead);
                                UpdateProgressBarValue(totalBytesRead, fileInformation.contentLength);

                                if (Form.state == Form.State.Paused || Form.state == Form.State.Error)        // pausing condition...
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            if (Form.state != Form.State.Paused && Form.state != Form.State.Error)        // when paused...
            {
                threads[index] = null;

                AppendSegments();
            }
        }

        public void Start()
        {
            threads = new Thread[segments.Length];

            try
            {
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i] = new Thread(new ParameterizedThreadStart(Start));
                    threads[i].Start(i);
                }
            }
            catch (Exception exception)
            {
                Form.state = Form.State.Error;

                throw exception;
            }
        }

        private void AppendSegments()       // this method will be modified...
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

            controls.UpdateStatus("Appending segments");

            totalBytesRead = 0;

            using (FileStream fileOutputStream = new FileStream(paths[1], FileMode.Append, FileAccess.Write))
            {
                for (int i = 0; i < segments.Length; i++)
                {
                    using (FileStream fileInputStream = new FileStream(paths[0] + "\\" + (i + 1), FileMode.Open, FileAccess.Read))
                    {
                        while ((bytesRead = fileInputStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            totalBytesRead += bytesRead;

                            fileOutputStream.Write(buffer, 0, bytesRead);
                            fileOutputStream.Flush();
                            UpdateProgressBarStatusValue(totalBytesRead, fileInformation.contentLength);
                        }
                    }
                }
            }

            if (Directory.Exists(paths[0]))
            {
                Directory.Delete(paths[0], true);
            }

            controls.UpdateStatus("Downloading complete");
        }

        /*
         * exceptions handled properly...
         */
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

        /*
         * exceptions handled properly...
         */
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