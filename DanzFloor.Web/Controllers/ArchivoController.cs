using DanzFloor.Web.Datos;
using DanzFloor.Web.Models;
using DanzFloor.Web.Models.Enum;
using System;
using System.Linq;
using System.IO;
using System.Web.Mvc;
using System.Web;
using DanzFloor.Web.Helpers;

namespace DanzFloor.Web.Controllers
{
    public class ArchivoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
                
        public ActionResult Eliminar(Guid archivoID)
        {
            new Repositorio<Archivo>(db).Eliminar(new Repositorio<Archivo>(db).Traer(archivoID));

            return Json(new
            {
                Resultado = Resultado.OK,
                Mensaje = ""
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Create(Guid ArchivoConfiguracionId)
        {
            bool isSavedSuccessfully = true;

            string fName = SetFile(out isSavedSuccessfully);
            var nombre = fName.Split('.').First();
            var extension = fName.Split('.').Last();

            if (isSavedSuccessfully)
            {
                var archivo = new Archivo()
                {
                    Nombre = nombre,
                    Contenido = extension,
                    Descripcion = fName,
                    Titulo = fName,
                    ArchivoConfiguracion = new Repositorio<ArchivoConfiguracion>(db).Traer(ArchivoConfiguracionId)
                };

                new Repositorio<Archivo>(db).Crear(archivo);

                return Json(new { Message = archivo.Id });
            }
            else
                throw new Exception("Error al cargar la imagen en File Manager.");
        }

        private string SetFile(out bool isSavedSuccessfully)
        {
            isSavedSuccessfully = true;
            string fName = "";
            string fExtension = "";
            Guid id = Guid.Empty;

            byte[] data = new byte[] { };

            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];

                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                data = target.ToArray();

                fName = file.FileName;
                if (file.ContentType.Contains("/"))
                    fExtension = file.ContentType.Split('/').Last();
                else
                    fExtension = file.ContentType;

                if (file != null && file.ContentLength > 0)
                {
                    ws.filemanager.Service1SoapClient client = new ws.filemanager.Service1SoapClient();
                    client.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                    bool error = false;

                    id = client.SetFile(data, fName, out error);

                }

            }
                

            return id.ToString() + "." + fExtension;
        }
        
        public FileResult GetFileByName(string name)
        {
            ws.filemanager.Service1SoapClient client = new ws.filemanager.Service1SoapClient();

            var file = client.GetFileByName(name);
            
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }

        public FileResult ObtenerArchivo(Guid id)
        {
            var archivo = new Repositorio<Archivo>(db).Traer(id);
            var name = archivo.Nombre;
            byte[] byteArchivo;

            ws.filemanager.Service1SoapClient client = new ws.filemanager.Service1SoapClient();
            
            if (Guid.TryParse(name, out id))
                byteArchivo = client.GetFile(id);
            else
                byteArchivo = client.GetFileByName(name);


            Response.Headers.Add("Etag", "\"" + name.ToLower().Split('-').First() + ":0\"");
            Response.Cache.SetExpires(DateTime.Now.AddHours(24));
            Response.Cache.SetLastModified(new DateTime(2017, 01, 02));
            Response.Cache.SetCacheability(HttpCacheability.Public);

            return File(byteArchivo, System.Net.Mime.MediaTypeNames.Application.Octet, "Archivo." + archivo.Contenido);
        }

        public ActionResult ObtenerImagen(string name, string height = null, string width = null, string mode = "crop", int quality = 100)
        {
            ws.filemanager.Service1SoapClient client = new ws.filemanager.Service1SoapClient();

            Guid id = Guid.Empty;
            byte[] imageData;

            if (Guid.TryParse(name, out id))
                imageData = client.GetFile(id);
            else
                imageData = client.GetFileByName(name);

            if (imageData == null)
                return null;

            int? heightValue = null;
            int? widthValue = null;
            if (height != null)
                heightValue = Convert.ToInt32(height);
            if (width != null)
                widthValue = Convert.ToInt32(width);

            var modificado = ImageHelper.ResizeImage(imageData, heightValue, widthValue, mode, quality);

            Response.Headers.Add("Etag", "\"" + name.ToLower().Split('-').First() + height + width + mode + quality + ":0\"");
            Response.Cache.SetExpires(DateTime.Now.AddHours(24));
            Response.Cache.SetLastModified(new DateTime(2017, 01, 02));
            Response.Cache.SetCacheability(HttpCacheability.Public);

            return File(modificado, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }
        
        public ActionResult ObtenerImagenPorId(string id)
        {
            string height = null;
            string width = null;
            string mode = null;
            int? quality = 100;
            string archivoId = id;

            if (id.Contains("|"))
            {
                try
                {
                    var split = id.Split('|');
                    
                    for (int i = 1; i < split.Length; i++)
                    {
                        var str = split[i].Split('-');
                        if (str[0] == "height")
                            height = str[1];
                        if (str[0] == "width")
                            width = str[1];
                        if (str[0] == "mode")
                            mode = str[1];
                        if (str[0] == "quality")
                            quality = Convert.ToInt32(str[1]);
                    }
                    archivoId = split[0];
                }
                catch (Exception ex) {
                    var error = new Exception("Error split ObtenerImagenPorId, imagen: " + id, ex);
                    Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(error));
                }
            }
            var guid = new Guid(archivoId);
            var name = new Repositorio<Archivo>(db).Traer(guid).Nombre;

           return ObtenerImagen(name, height, width, mode, quality.Value);
        }
    }
}
