

using Market.Services.IServices;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Market.Services
{
    public class ImageProcessingService : IImageProcessingService
    {
        public void CropImage(string imageFilePath, string savePath, int initialX, int initialY, int width, int height)
        {
            Bitmap? src = new Bitmap(imageFilePath);

            Bitmap? target = new Bitmap(width, height);

            Rectangle destinationRect = new Rectangle(0, 0, width, height);
            RectangleF sourceRect = new RectangleF(initialX, initialY, width, height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, destinationRect, sourceRect, GraphicsUnit.Point);
                target.Save(savePath, ImageFormat.Jpeg);
            }
        }

        public void InsertTextIntoImage(string imageFilePath, string savePath, string text, PointF location, string fontName, float fontSize)
        {
            Bitmap newBitmap;

            using (var bitmap = (Bitmap)Image.FromFile(imageFilePath))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font font = new Font(fontName, fontSize))
                    {
                        graphics.DrawString(text.ToUpper(), font, Brushes.Black, location);
                    }
                }
                newBitmap = new Bitmap(bitmap);
            }

            newBitmap.Save(savePath, ImageFormat.Jpeg);
            newBitmap.Dispose();
        }


        private static IFormFile ConvertToFormFile(Stream stream, string fileName)
        {
            // Create a memory stream from the provided stream
            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            memoryStream.Position = 0;

            // Create an IFormFile instance
            var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "imageFile", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg" // Set the content type accordingly
            };

            return formFile;
        }

    }
}
