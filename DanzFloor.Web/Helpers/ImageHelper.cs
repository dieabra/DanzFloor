using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Helpers
{
    public class ImageHelper
    {
        public static byte[] ResizeImage(byte[] imageBytes, int? height = null, int? width = null, string mode = "", int quality = 80)
        {
            if (height != null || width != null)
            {
                string z = "";
                string y = "";
                string x = "";
                string scale = "";
                string q = "";

                if (height != null && mode != "max")
                    y = "maxheight=" + height + ";";
                if (width != null && mode != "max")
                    x = "maxwidth=" + width + ";";
                if (mode == "max")
                {
                    y = "h=" + height + ";";
                    x = "w=" + width + ";";
                }
                if (mode == "crop")
                    scale = "scale=both;";
                if (!string.IsNullOrEmpty(mode))
                    z = "mode=" + mode + ";";

                q = "quality=" + quality + ";format=jpg;";

                MemoryStream stream = new MemoryStream();
                try
                {
                    ImageResizer.ImageJob i = new ImageResizer.ImageJob(imageBytes, stream, new ImageResizer.Instructions(x + y + z + scale + q));
                    i.Build();
                    return stream.ToArray();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return imageBytes;
        }
    }
}