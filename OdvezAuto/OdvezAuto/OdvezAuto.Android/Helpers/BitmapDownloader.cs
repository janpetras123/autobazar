using System;
using System.Net;
using Android.Graphics;

namespace OdvezAuto.Droid.Helpers
{
    public class BitmapDownloader
    {
        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                try
                {
                    byte[] imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }
                catch (WebException e)
                {
                    //nieco je s internetom
                }
                catch (ArgumentException)
                {
                    //nespravna adresa na obrazok
                }
            }

            return imageBitmap;
        }
    }
}