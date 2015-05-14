using DPUruNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace LectorApp
{
    class Lector
    {
        public Reader lector;
        Form1 forma;

        public Lector(Form1 forma)
        {
            this.forma = forma;
        }

        public void inicializar()
        {
            try
            {
                ReaderCollection lectores = ReaderCollection.GetReaders();
                this.lector = lectores.FirstOrDefault();
                lector.Open(Constants.CapturePriority.DP_PRIORITY_EXCLUSIVE);
                lector.GetStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string inscribirHuella(Sockets socket)
        {
            DataResult<Fmd> enrollmentResult = Enrollment.CreateEnrollmentFmd(
                Constants.Formats.Fmd.ANSI,
                CaptureAndExtractFmd(5, socket)
            );

            Fmd fmd = enrollmentResult.Data;

            return Fmd.SerializeXml(fmd);
            //return JsonConvert.SerializeObject(fmd);
        }

        IEnumerable<Fmd> CaptureAndExtractFmd(int lecturas, Sockets socket)
        {
            int no_lecturas = 0;
            while (no_lecturas < lecturas)
            {
                if (lector.Status.Status.Equals(Constants.ReaderStatuses.DP_STATUS_READY))
                {
                    // Capture a fingerprint.
                    CaptureResult captureResult = lector.Capture(
                        Constants.Formats.Fid.ANSI,
                        Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT,
                        -1,
                        lector.Capabilities.Resolutions[0]
                    );

                    Bitmap bmp = new Bitmap(150, 150);
                    foreach (Fid.Fiv fiv in captureResult.Data.Views)
                    {
                        bmp = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                    }

                    System.IO.MemoryStream stream = new System.IO.MemoryStream();
                    bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] imageBytes = stream.ToArray();

                    string base64String = Convert.ToBase64String(imageBytes);
                    socket.mandarMensaje(5, base64String);
                    
                    no_lecturas++;
                    socket.mandarMensaje(3, "Lectura capturada");
                    // !!! Check for errors, use ‘yield return null; or break;’ to stop.
                    yield return FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI).Data;
                }
            }
        }

        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }
    }
}
