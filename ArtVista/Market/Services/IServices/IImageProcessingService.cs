using System.Drawing;

namespace Market.Services.IServices
{
    public interface IImageProcessingService
    {
        void CropImage(string imageFilePath, string savePath, int initialX, int initialY, int width, int height);
        void InsertTextIntoImage(string imageFilePath, string savePath, string text, PointF location, string fontName, float fontSize);
    }
}
