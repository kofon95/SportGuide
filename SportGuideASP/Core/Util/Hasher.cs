using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SportGuideASP.Core.Util
{
    public static class Hasher
    {
        public static string GetMD5Hash(string text)
        {
            return GetHash(MD5.Create(), text);
        }

        private static string GetHash(HashAlgorithm algorithm, string text)
        {
            using (algorithm)
            {
                byte[] bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter
                                .ToString(bytes)
                                .Replace("-", "")
                                .ToLower();
            }
        }
    }

    public class FileUploader
    {
        private Controller _ctrl;
        public FileUploader(Controller ctrl)
        {
            _ctrl = ctrl;
        }

        public string[] SaveImagesWithResizingGetFileNames(string directoryName, int maxWidth, int maxHeight)
        {
            var files = _ctrl.Request.Files;
            string[] savedPathFiles = new string[files.Count];

            for (int i = 0; i < files.Count; i++)
            {
                string fileName = GetRandomFileName(files[i].FileName);
                string fullFileName = Path.Combine(_ctrl.Server.MapPath(directoryName), fileName);
                var tmpName = fullFileName + "_tmp";
                files[i].SaveAs(tmpName);
                savedPathFiles[i] = fileName;

                try
                {
                    SaveThumbnail(tmpName, fullFileName, maxWidth, maxHeight);
                }
                catch (OutOfMemoryException e)
                {
                    // clear added images
                    for (int j = 0; j < i; j++)
                        File.Delete(savedPathFiles[j]);
                    StaticData.Log.Warn(e, files[i].FileName);
                    throw new BadImageFormatException($"Image does not valid image", files[i].FileName, e);
                }
                finally
                {
                    File.Delete(tmpName);
                }
            }

            return savedPathFiles;
        }

        private static Random _random = new Random();

        private static string GetRandomFileName(string filename)
        {
            StringBuilder sb = new StringBuilder(Path.GetFileNameWithoutExtension(filename), filename.Length + 20);
            sb
                .Replace(" ", "_")
                .Append('_')
                .Append(DateTime.Now.Ticks)
                .Append('_')
                .Append(_random.Next(1000))
                .Append(Path.GetExtension(filename));

            return sb.ToString();
        }

        internal void DeleteFiles(params string[] fileNames)
        {
            for (int i = 0; i < fileNames.Length; i++)
            {
                string fileName = _ctrl.Server.MapPath(fileNames[i]);
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
        }

        private static void SaveThumbnail(string source, string newFileName, int maxWidth, int maxHeight)
        {
            using (Image img = Image.FromFile(source))
            {
                var width = img.Width;
                var height = img.Height;
                if (width > maxWidth || height > maxHeight)
                {
                    double ratio = width > height ?
                        (double)width / maxWidth :
                        (double)height / maxHeight;
                    width = (int)Math.Round(width / ratio);
                    height = (int)Math.Round(height / ratio);
                }

                using (var bitmap = new Bitmap(width, height))
                {
                    using (var g = Graphics.FromImage(bitmap))
                    {
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        g.DrawImage(img, 0, 0, width, height);
                    }

                    bitmap.Save(newFileName, ImageFormat.Jpeg);
                }
            }
        }
    }
}