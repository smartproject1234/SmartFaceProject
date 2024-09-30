using OpenCvSharp;
using SmartFaceProject.Services;
using System.Drawing;
using System.Windows.Forms;
using System.Windows;
using System;

namespace SmartFaceProject
{
    public partial class MainForm : Form
    {
        private VideoCapture capture;
        private FaceRecognitionService faceRecognitionService;
        private GestureRecognitionService gestureRecognitionService;
        private Bitmap uploadedImage;

        public MainForm()
        {
            InitializeComponent();
            faceRecognitionService = new FaceRecognitionService();
            gestureRecognitionService = new GestureRecognitionService();
        }

        private void btnStartCamera_Click(object sender, EventArgs e)
        {
            capture = new VideoCapture(0); // Kameranı aç
            if (!capture.IsOpened())
            {
                System.Windows.MessageBox.Show("Kamera açılmadı!");
                return;
            }
            System.Windows.Forms.Application.Idle += ProcessFrame;
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            using (var frame = new Mat())
            {
                capture.Read(frame);
                if (!frame.Empty())
                {
                    // Üz tanıma
                    bool isFaceMatch = faceRecognitionService.CompareFaces(frame, uploadedImage);
                    if (!isFaceMatch)
                    {
                        System.Windows.MessageBox.Show("Üz uyğun deyil! Proqram bağlanır.");
                        System.Windows.Forms.Application.Exit();
                    }

                    // Jest tanıma
                    bool isPerfectGesture = gestureRecognitionService.DetectPerfectGesture(frame);
                    if (isPerfectGesture)
                    {
                        System.Windows.MessageBox.Show("Perfect jesti tapıldı!");
                    }

                    // Kameradan görüntünü göstər
                    pictureBox.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                }
            }
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|.jpg;.jpeg;*.png;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    uploadedImage = new Bitmap(openFileDialog.FileName); // Yüklənmiş şəkli saxlamaq
                }
            }
        }
    }

}
