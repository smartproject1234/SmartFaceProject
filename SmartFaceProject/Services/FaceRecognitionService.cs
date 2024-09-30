using OpenCvSharp;
using OpenCvSharp.Extensions; // Bu namespace-i əlavə et
using System.Drawing;

namespace SmartFaceProject.Services
{
    public class FaceRecognitionService
    {
        private CascadeClassifier faceClassifier;

        public FaceRecognitionService()
        {
            faceClassifier = new CascadeClassifier("C:\\Users\\vüsal\\source\\repos\\SmartFaceProject\\SmartFaceProject\\Resources\\haarcascade_frontalface_default.xml"); // Haar Cascade üz tanıma üçün istifadə olunur
        }

        public bool CompareFaces(Mat cameraFrame, Bitmap uploadedImage)
        {
            // Üzləri tap
            var cameraFaces = faceClassifier.DetectMultiScale(cameraFrame);
            var uploadedFace = faceClassifier.DetectMultiScale(BitmapConverter.ToMat(uploadedImage));

            // Üzlər uyğun gəlirsə, davam etdir
            if (cameraFaces.Length > 0 && uploadedFace.Length > 0)
            {
                return true;
            }
            return false;
        }
    }
}
