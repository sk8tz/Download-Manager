using System;

namespace Download_Manager
{
    public class FileInformation
    {
        public long contentLength;
        private static readonly double KB = 1024.0, MB = KB * 1024.0, GB = MB * 1024.0, TB = GB * 1024.0;
        private static double _size;
        public string downloadLink, saveTo, type, name, size;

        public FileInformation(string downloadLink, string saveTo)
        {
            this.downloadLink = Uri.UnescapeDataString(downloadLink);
            this.saveTo = saveTo;

            try
            {
                DownloadManager.GetFileInformation(ref contentLength, ref type, ref name, this.downloadLink);

                size = FormatSize(contentLength);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string FormatSize(long contentLength)
        {
            if (contentLength == 0.0)
            {
                return "0.0 Byte(s)";
            }

            string unit = "Bytes";

            try
            {
                _size = contentLength * 1.0;
            }
            catch
            {

            }

            if (_size > TB)
            {
                _size /= TB;
                unit = "TB";
            }
            else if (_size > GB)
            {
                _size /= GB;
                unit = "GB";
            }
            else if (_size > MB)
            {
                _size /= MB;
                unit = "MB";
            }
            else if (_size > KB)
            {
                _size /= KB;
                unit = "KB";
            }

            return string.Format("{0:.##}", _size) + " " + unit;
        }
    }
}