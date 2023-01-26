using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ResourceService.Services
{
    public class Utility
    {
        private static Utility _object;
        public static Utility Instance
        {
            get
            {
                if(_object == null)
                {
                    _object = new Utility();
                }
                return _object;
            }
        }
        private Utility()
        {

        }

        public void ResizeImage(string path, int width, int height,
            double mBytes)
        {
            if (width > 0 || height > 0)
            {
                ResizeImage(path, width, height);
            }

            if (mBytes > 0)
            {
                DownSizeImage(path, mBytes);
            }
        }

        private void DownSizeImage(string path, double mByte)
        {
            Bitmap open = new Bitmap(path);

            try
            {
                open = new Bitmap(path);
                if (HasTransparency(open))
                {
                    open.Dispose();
                }
                else
                {
                    mByte = mByte * 1024 * 1024;
                    int currentSize = open.Width * open.Height * 4;
                    if (currentSize > mByte)
                    {
                        double rate = (double)currentSize / mByte;
                        int width = (int)(open.Width / rate);
                        int height = (int)(open.Height / rate);
                        open.Dispose();

                        using (var result = new Bitmap(width, height))
                        {
                            using (var input = new Bitmap(path))
                            {
                                using (Graphics g = Graphics.FromImage(result))
                                {
                                    g.DrawImage(input, 0, 0, width, height);
                                    g.Dispose();
                                }
                                input.Dispose();
                            }

                            var ici = ImageCodecInfo.GetImageEncoders().FirstOrDefault();
                            var eps = new EncoderParameters(1);
                            eps.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                            result.Save(path, ici, eps);
                            result.Dispose();
                        }
                    }
                    else
                    {
                        open.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                open.Dispose();
                throw ex;
            }
        }

        private void ResizeImage(string path, int width, int height)
        {
            Bitmap open = new Bitmap(path);

            try
            {
                if (width <= 0)
                {
                    width = open.Width;
                }
                if (height <= 0)
                {
                    height = open.Height;
                }

                if (HasTransparency(open))
                {
                    open.Dispose();
                }
                else if (width < open.Width || height < open.Height)
                {
                    if (width > 0 && width < open.Width && height > 0 && height < open.Height)
                    {
                        if (width / open.Width < height / open.Height)
                        {
                            height = (int)Math.Round((double)open.Height * width / open.Width);
                        }
                        else if (width / open.Width > height / open.Height)
                        {
                            width = (int)Math.Round((double)open.Width * height / open.Height);
                        }
                    }
                    else if (width > 0 && width < open.Width)
                    {
                        height = (int)Math.Round((double)open.Height * width / open.Width);
                        if (height > open.Height)
                        {
                            height = open.Height;
                            width = (int)Math.Round((double)open.Width * height / open.Height);
                        }
                    }
                    else if (height > 0 && height < open.Height)
                    {
                        width = (int)Math.Round((double)open.Width * height / open.Height);
                        if (width > open.Width)
                        {
                            width = open.Width;
                            height = (int)Math.Round((double)open.Height * width / open.Width);
                        }
                    }

                    open.Dispose();

                    using (var result = new Bitmap(width, height))
                    {
                        using (var input = new Bitmap(path))
                        {
                            using (Graphics g = Graphics.FromImage(result))
                            {
                                g.DrawImage(input, 0, 0, width, height);
                                g.Dispose();
                            }
                            input.Dispose();
                        }

                        var ici = ImageCodecInfo.GetImageEncoders().FirstOrDefault();
                        var eps = new EncoderParameters(1);
                        eps.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                        result.Save(path, ici, eps);
                        result.Dispose();
                    }
                }
                else
                {
                    open.Dispose();
                }
            }
            catch (Exception ex)
            {
                open.Dispose();
                throw ex;
            }
        }

        private bool HasTransparency(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    var pixel = image.GetPixel(i, j);
                    if (pixel.A != 255)
                        return true;
                }
            }

            return false;
        }
    }
}
