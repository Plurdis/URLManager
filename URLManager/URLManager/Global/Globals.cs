using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using URLManager.Windows;

namespace URLManager.Global
{
    public static class Globals
    {
        public static Stack<LinkSelectWindow> lsWdws = new Stack<LinkSelectWindow>();


        public static ImageSource GetIcon(string fileName)
        {
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(fileName);
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                            icon.Handle,
                            new Int32Rect(0, 0, icon.Width, icon.Height),
                            BitmapSizeOptions.FromEmptyOptions());
            }
            catch (FileNotFoundException)
            {
                return null;
                throw;
            }
            
        }


        public static ImageSource GetResourcesIcon(string filename)
        {
            string path = "/URLManager;component/Resources/CategoryIcon/" + filename;

            return new BitmapImage(new Uri(path, UriKind.Relative));
        }



        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        public static ImageSource ToImageSource(this Icon icon)
        {
            Bitmap bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            if (!DeleteObject(hBitmap))
            {
                //throw new Win32Exception();

                return null;
            }

            return wpfBitmap;
        }



        public static ImageSource GetImageFromURL(string urllink)
        {
            urllink = GetURLLink(urllink);

            Uri url = new Uri(urllink);
            WebRequest request = (HttpWebRequest)WebRequest.Create("http://" + url.Host + "/favicon.ico");

            Bitmap bm = new Bitmap(32, 32);
            MemoryStream memStream;
            try
            {
                using (Stream response = request.GetResponse().GetResponseStream())
                {
                    memStream = new MemoryStream();
                    byte[] buffer = new byte[1024];
                    int byteCount;

                    do
                    {
                        byteCount = response.Read(buffer, 0, buffer.Length);
                        memStream.Write(buffer, 0, byteCount);
                    } while (byteCount > 0);
                }

                bm = new Bitmap(Image.FromStream(memStream));

                if (bm != null)
                {
                    Icon ic = Icon.FromHandle(bm.GetHicon());
                    return ic.ToImageSource();
                }


                return null;
            }
            catch (Exception)
            {
                return null;
            }
            
        }


        public static string GetURLLink(string checkurl)
        {
            if (checkurl.StartsWith("https://")) return "http://" + checkurl.Substring(8);
            if (checkurl.StartsWith("http://")) return checkurl;
            return "http://" + checkurl;
        }

        public static string[] GetResourceNames(string path)        {            var asm = Assembly.GetEntryAssembly();            string resourceName = $"{asm.GetName().Name}.g.resources";            using (var stream = asm.GetManifestResourceStream(resourceName))            {                using (var reader = new System.Resources.ResourceReader(stream))                {                    var d = reader                        .Cast<DictionaryEntry>()                        .Select(entry => (string)entry.Key)                        .Where(key => key.StartsWith(path, StringComparison.OrdinalIgnoreCase))                        .Select(key => $"pack://application:,,,/{asm.GetName().Name};component/{key}")                        .ToList();                    d.Sort();                    return d.ToArray();
                }            }        }

    }
}
