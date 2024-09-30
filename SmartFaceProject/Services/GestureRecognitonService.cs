using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartFaceProject.Services
{
    public class GestureRecognitionService
    {
        private CascadeClassifier gestureClassifier;

        public GestureRecognitionService()
        {
            gestureClassifier = new CascadeClassifier("hand_cascade.xml"); // Jestlər üçün xüsusi cascade

            try
            {
                gestureClassifier = new CascadeClassifier("hand_cascade.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fayl yüklənərkən xəta: {ex.Message}");
            }
        }

        public bool DetectPerfectGesture(Mat frame)
        {
            var gestures = gestureClassifier.DetectMultiScale(frame);

            foreach (var gesture in gestures)
            {
                if (IsPerfectGesture(gesture))
                {
                    return true;
                }
            }
            return false;
        }


        public bool DetectPerfectGesture(OpenCvSharp.Mat frame)
        {
            throw new NotImplementedException();
        }

        private bool IsPerfectGesture(Rectangle gesture)
        {
            return true;
        }

        private bool IsPerfectGesture(Rect gesture)
        {
            // Burada Perfect jestini müəyyən etmək üçün qaydalar yazılacaq
            return true; // Məsələn, müəyyən sahə daxilindədirsə
        }
    }
}
