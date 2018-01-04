using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Postal;
using DanzFloor.Web.Datos;
using DanzFloor.Web.Helpers;
using DanzFloor.Web.Models;
using DanzFloor.Web.Models.Dominio;
using DanzFloor.Web.Models.ViewModels;
using DanzFloor.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using DanzFloor.Web.Models.ViewModels.Frontend;
using DanzFloor.Web.Models.Dominio.Usuarios;
using DanzFloor.Web.Models.Autenticacion;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using DanzFloor.Web.Models.Enum;

namespace DanzFloor.Web.Controllers
{
    public class BaseCuentaController: BaseController
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        
        #region Start
        public ApplicationSignInManager _signInManager;
        public ApplicationUserManager _userManager;


        public BaseCuentaController()
        {
        }

        public BaseCuentaController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        #endregion
        
        public bool RegisterUser(RegisterViewModel model)
        {
            try
            {
                var db = new ApplicationDbContext();
                var user = new ApplicationUser(model);
                
                var usuario = new Usuario();
                usuario.UsuarioApplicacion = user;
                usuario.Nombre = model.Name;
                usuario.Apellido = model.Lastname;
                usuario.Email = model.Email;
                usuario.FechaNacimiento = DateTime.Now;
                usuario.Sexo = new Repositorio<Sexo>(db).TraerTodos().First();

                new Repositorio<Usuario>(db).Crear(usuario);
                model.Password = model.ConfirmPassword = usuario.UsuarioApplicacion.Id.Substring(0,4) + GenerarRandomPass();
                UserManager.AddPassword(usuario.UsuarioApplicacion.Id, model.Password);

                try
                {
                    //Enviar Mail Usuario Generado
                    dynamic em = new Email("UsuarioGenerado");
                    em.To = model.Email;
                    em.Nombre = user.Name;
                    em.Link = WebConfigurationManager.AppSettings["Core"];
                    em.Password = model.Password;
                    em.Send();
                    //END Mail
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }

                if (model.UserRolesSelected != null && model.UserRolesSelected.Count > 0)
                    model.UserRolesSelected.ToList().ForEach(rol => UserManager.AddToRole(user.Id, model.Perfiles.First(x => x.Value == rol).Text));

                return true;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return false;
            }

        }

        public bool RestaurarPassword(Guid usuarioid)
        {
            var persona = new Repositorio<Persona>(db).Traer(usuarioid);
            var appUser = UserManager.FindById(persona.UsuarioApplicacion.Id);

            var password = appUser.Id.ToString().Substring(0, 4) + GenerarRandomPass();
            UserManager.RemovePassword(appUser.Id);
            UserManager.AddPassword(appUser.Id, password);
            

            try
            {
                //Enviar Mail Usuario Generado
                dynamic em = new Email("RestaurarPassword");
                em.To = appUser.Email;
                em.Nombre = appUser.Name;
                em.Link = WebConfigurationManager.AppSettings["Core"];
                em.Password = password;
                em.Send();
                //END Mail
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return true;
        }

        public bool ReenviarMailBienvenida(Guid usuarioid)
        {
            var persona = new Repositorio<Persona>(db).Traer(usuarioid);
            var appUser = UserManager.FindById(persona.UsuarioApplicacion.Id);

            var password = appUser.Id.ToString().Substring(0, 4) + GenerarRandomPass();
            UserManager.RemovePassword(appUser.Id);
            UserManager.AddPassword(appUser.Id, password);
            
            try
            {
                //Enviar Mail Usuario Generado
                dynamic em = new Email("UsuarioGeneradoDisculpas");
                em.To = appUser.Email;
                em.Nombre = appUser.Name;
                em.Link = WebConfigurationManager.AppSettings["Core"];
                em.Password = password;
                em.Send();
                //END Mail
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return true;
        }

        public string GenerarRandomPass()
        {
            string letras = "abcdefghijklmnopqrstuvwxyz";
            string numeros = "1234567890";
            string caracteres = "$%#@!&";
            Random random = new Random();

            int letraPosition = random.Next(0, letras.Length - 1);
            random = new Random();
            int mayusPosition = random.Next(0, letras.Length - 1);
            int numeroPosition = random.Next(0, numeros.Length - 1);
            int charPosition = random.Next(0, caracteres.Length - 1);

            string pass = letras[letraPosition] +letras[mayusPosition].ToString().ToUpper() + numeros[numeroPosition].ToString() + caracteres[charPosition];
            return pass;
        }

        public bool RegisterUser(RegisterFrontViewModel model)
        {
            try
            {
                var db = new ApplicationDbContext();
                var user = new ApplicationUser(model);

                var usuario = new Usuario();
                usuario.UsuarioApplicacion = user;
                usuario.Nombre = model.Name;
                usuario.Apellido = model.Lastname;
                usuario.Email = model.Email;
                usuario.FechaNacimiento = DateTime.Now;
                usuario.Sexo = new Repositorio<Sexo>(db).TraerTodos().First();
                
                new Repositorio<Usuario>(db).Crear(usuario);
                model.Password = model.ConfirmPassword = usuario.UsuarioApplicacion.Id.Substring(0, 4) + GenerarRandomPass();
                UserManager.AddPassword(usuario.UsuarioApplicacion.Id, model.Password);

                if (model.UserRolesSelected != null && model.UserRolesSelected.Count > 0)
                    model.UserRolesSelected.ToList().ForEach(rol => UserManager.AddToRole(user.Id, model.Perfiles.First(x => x.Value == rol).Text));

                try
                {
                    //Enviar Mail Usuario Generado
                    dynamic em = new Email("UsuarioGenerado");
                    em.To = model.Email;
                    em.Nombre = user.Name;
                    em.Link = WebConfigurationManager.AppSettings["Core"];
                    em.Password = model.Password;
                    em.Send();
                    //END Mail
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }

                return true;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return false;
            }

        }

        public bool ModificarApplicationUser(RegisterViewModel usuarioModificado)
        {
            try
            {
                var appUser = UserManager.FindById(usuarioModificado.IdUsuarioApplicacion);

                if (usuarioModificado.ResetPassword)
                {
                    UserManager.RemovePassword(appUser.Id);
                    UserManager.AddPassword(appUser.Id, usuarioModificado.Password);
                }

                appUser.Email = usuarioModificado.Email;
                appUser.Name = usuarioModificado.Name;
                appUser.Lastname = usuarioModificado.Lastname;

                if (usuarioModificado.UserRolesSelected != null)
                {
                    UserManager.RemoveFromRoles(appUser.Id, UserManager.GetRoles(appUser.Id).ToArray());

                    UserManager.Update(appUser);

                    List<string> rolesAAgregar = new List<string>();

                    usuarioModificado.UserRolesSelected.ToList()
                        .ForEach(roleId =>
                                rolesAAgregar.Add(usuarioModificado.Perfiles.First(perf => perf.Value == roleId).Text)
                            );

                    UserManager.AddToRoles(appUser.Id, rolesAAgregar.ToArray());
                }

                UserManager.Update(appUser);

                return true;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return false;
            }
        }


        public bool ModificarApplicationUser(RegisterFrontViewModel usuarioModificado)
        {
            try
            {
                var appUser = UserManager.FindById(usuarioModificado.IdUsuarioApplicacion);

                if (usuarioModificado.ResetPassword)
                {
                    UserManager.RemovePassword(appUser.Id);
                    UserManager.AddPassword(appUser.Id, usuarioModificado.Password);
                }

                appUser.Email = usuarioModificado.Email;
                appUser.Name = usuarioModificado.Name;
                appUser.Lastname = usuarioModificado.Lastname;

                if (usuarioModificado.UserRolesSelected != null)
                {
                    UserManager.RemoveFromRoles(appUser.Id, UserManager.GetRoles(appUser.Id).ToArray());

                    UserManager.Update(appUser);

                    List<string> rolesAAgregar = new List<string>();

                    usuarioModificado.UserRolesSelected.ToList()
                        .ForEach(roleId =>
                                rolesAAgregar.Add(usuarioModificado.Perfiles.First(perf => perf.Value == roleId).Text)
                            );

                    UserManager.AddToRoles(appUser.Id, rolesAAgregar.ToArray());
                }

                UserManager.Update(appUser);

                return true;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return false;
            }
        }

        [Authorize(Roles = RoleConst.Administrador + ", " + RoleConst.Colaborador)]
        public ActionResult IndexColaborador()
        {
            var db = new ApplicationDbContext();
            
            return View(new Repositorio<Colaborador>(db).TraerTodos());
        }

        [Authorize(Roles = RoleConst.Administrador + ", " + RoleConst.Colaborador)]
        public ActionResult CreateColaborador()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador + ", " + RoleConst.Colaborador)]
        public ActionResult CreateColaborador(RegisterViewModel model)
        {
            try
            {
                var db = new ApplicationDbContext();
                if(new Repositorio<Persona>(db).TraerTodos().Any(p=>p.Email == model.Email))
                {
                    ModelState.AddModelError("", "Ya existe un usuario con ese mail: " + model.Email);
                    return View(model);  
                }

                var user = new ApplicationUser(model);

                var usuario = new Colaborador();
                usuario.UsuarioApplicacion = user;
                usuario.Nombre = model.Name;
                usuario.Apellido = model.Lastname;
                usuario.Email = model.Email;
                usuario.FechaNacimiento = DateTime.Now;
                usuario.Sexo = new Repositorio<Sexo>(db).TraerTodos().First();

                new Repositorio<Colaborador>(db).Crear(usuario);

                model.Password = model.ConfirmPassword = usuario.UsuarioApplicacion.Id.Substring(0, 4) + GenerarRandomPass();
                UserManager.AddPassword(usuario.UsuarioApplicacion.Id, model.Password);
                //UserManager.AddPassword(usuario.UsuarioApplicacion.Id, model.Password);

                try
                {
                    //Enviar Mail Usuario Generado
                    dynamic em = new Email("UsuarioGenerado");
                    em.To = model.Email;
                    em.Nombre = user.Name;
                    em.Link = WebConfigurationManager.AppSettings["Core"];
                    em.Password = model.Password;
                    em.Send();
                    //END Mail
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }

                if (model.UserRolesSelected != null && model.UserRolesSelected.Count > 0)
                    model.UserRolesSelected.ToList().ForEach(rol => UserManager.AddToRole(user.Id, model.Perfiles.First(x => x.Value == rol).Text));

                return RedirectToAction("IndexColaborador");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

                return View(model);
            }

        }

        [HttpGet]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult DeleteColaborador(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return null;// new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var venue = new Repositorio<Colaborador>(db).Traer(id.Value);
                if (venue == null)
                    return null;
                else
                    new Repositorio<Colaborador>(db).Eliminar(venue);

                return Json(new
                {
                    Resultado = Resultado.OK,
                    Mensaje = ""
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Resultado = Resultado.Error,
                    Mensaje = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }



        [AllowCrossSiteJson]
        [System.Web.Mvc.AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (returnUrl != null)
                    CacheHelper.InsertKey(CacheHelper.GetNotLoggedUserKey(), returnUrl);
                if (string.IsNullOrEmpty(returnUrl))
                    ViewBag.ReturnUrl = "/Home/Index";
                else
                    ViewBag.ReturnUrl = returnUrl;

                ViewBag.urlFacebookRedirect =
                    "https://www.facebook.com/v2.9/dialog/oauth?%20client_id=" + SocialIds.FacebookAppId + "&redirect_uri=" +
                    WebConfigurationManager.AppSettings["Core"] +
                    WebConfigurationManager.AppSettings["urlFacebookRedirect"] +
                    "&scope=email";
                ViewBag.urlInstagramRedirect = "https://api.instagram.com/oauth/authorize/?client_id=" + SocialIds.InstagramClientId + "&redirect_uri=" + WebConfigurationManager.AppSettings["Core"] + WebConfigurationManager.AppSettings["urlInstagramRedirect"] + "&response_type=code";
                ViewBag.urlSpotifyRedirect = "https://accounts.spotify.com/authorize/?client_id=" + SocialIds.SpotifyClientId + "&redirect_uri=" + WebConfigurationManager.AppSettings["Core"] + WebConfigurationManager.AppSettings["urlSpotifyRedirect"] + "&response_type=code&scope=user-read-email";

            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult ReturnFrom(RedSocial tipo, string code)
        {
            var goTo = CacheHelper.GetKey(CacheHelper.GetNotLoggedUserKey());
            try
            {
                using (WebClient client = new WebClient())
                {
                    ISocialUser user;
                    byte[] response;
                    switch (tipo)
                    {
                        case RedSocial.Google:
                            throw new Exception("Loggeo incorrecto con Google");
                            break;
                        case RedSocial.Instagram:
                            response =
                            client.UploadValues("https://api.instagram.com/oauth/access_token", new NameValueCollection()
                            {
                               { "client_id", SocialIds.InstagramClientId },
                               { "client_secret", SocialIds.InstagramClientSecret },
                               { "grant_type", SocialIds.InstagramGrantType },
                               { "redirect_uri", (WebConfigurationManager.AppSettings["Core"] + WebConfigurationManager.AppSettings["urlInstagramRedirect"])  },
                               { "code", code },
                            });

                            InstagramResponseValidationCode responseI = JsonConvert.DeserializeObject<InstagramResponseValidationCode>(System.Text.Encoding.UTF8.GetString(response));
                            user = responseI.user;
                            ((InstagramUser)user).Token = responseI.access_token;

                            break;
                        case RedSocial.FaceBook:
                            response = client.UploadValues("https://graph.facebook.com/v2.9/oauth/access_token", new NameValueCollection()
                            {
                                { "client_id", SocialIds.FacebookAppId },
                                { "redirect_uri", WebConfigurationManager.AppSettings["Core"] + WebConfigurationManager.AppSettings["urlFacebookRedirect"] },
                                { "client_secret", SocialIds.FacebookSecret },
                                { "code", code }
                            });

                            FacebookResponseValidationCode obj = JsonConvert.DeserializeObject<FacebookResponseValidationCode>(System.Text.Encoding.UTF8.GetString(response));

                            //No se por que no anda de esta forma
                            //byte[] userResponse = client.UploadValues("https://graph.facebook.com/v2.9/me", new NameValueCollection()
                            //{
                            //    { "fields", "email,id,first_name,last_name,name,picture.type(large)" },
                            //    { "access_token", obj.access_token }
                            //});

                            //FacebookUser fbUser = JsonConvert.DeserializeObject<FacebookUser>(System.Text.Encoding.UTF8.GetString(response));
                            //user = fbUser;
                            using (HttpClient cliente = new HttpClient())
                            {
                                var mess = cliente.GetStringAsync("https://graph.facebook.com/me?fields=email,name,picture.type(large),id&access_token=" + obj.access_token);
                                user = JsonConvert.DeserializeObject<FacebookUser>(mess.Result);
                                ((FacebookUser)user).Token = obj.access_token;
                            }
                            break;
                        case RedSocial.Spotify:
                            SpotifyResponseValidationCode responseS;
                            using (HttpClient cliente = new HttpClient())
                            {
                                Dictionary<string, string> postData = new Dictionary<string, string>();
                                postData.Add("grant_type", "authorization_code");
                                postData.Add("code", code);
                                postData.Add("redirect_uri", WebConfigurationManager.AppSettings["Core"] + WebConfigurationManager.AppSettings["urlSpotifyRedirect"]);
                                postData.Add("client_id", SocialIds.SpotifyClientId);
                                postData.Add("client_secret", SocialIds.SpotifySecret);
                                var content = new FormUrlEncodedContent(postData);
                                var httpResponse = cliente.PostAsync("https://accounts.spotify.com/api/token", content).Result.Content.ReadAsStringAsync().Result;
                                responseS = JsonConvert.DeserializeObject<SpotifyResponseValidationCode>(httpResponse);
                            }
                            using (HttpClient cliente = new HttpClient())
                            {
                                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseS.access_token);

                                var mess = cliente.GetAsync("https://api.spotify.com/v1/me").Result.Content.ReadAsStringAsync();
                                user = JsonConvert.DeserializeObject<SpotifyUser>(mess.Result);
                                ((SpotifyUser)user).accessToken = responseS.access_token;
                            }
                            break;
                        case RedSocial.SoundCloud:
                            throw new Exception("Loggeo incorrecto con SoundCloud");
                            break;
                        default:
                            throw new Exception("Loggeo incorrecto con Random");
                            break;
                    }


                    if (ExisteUsuarioSocial(user.GetId()))
                    {
                        //existe el user, loguearlo
                        var id = user.GetId();
                        var userSocial = new Repositorio<UsuarioSocial>(new ApplicationDbContext()).TraerTodos().Single(u => u.IdSocial == id);
                        var usuarios = new Repositorio<Clubber>(db).TraerTodos()
                          .Where(r => r.CredencialesSociales.Any(c => c.IdSocial == userSocial.IdSocial));

                        SignInManager.SignIn(usuarios.First().UsuarioApplicacion, true, true);

                        if (string.IsNullOrEmpty(goTo))
                            return RedirectToAction("Index", "Frontend");
                        else
                            return RedirectToLocal(goTo.ToString());
                    }
                    else
                    {
                        //no existe el user -> crearlo
                        var nuevo = new RegisterFrontViewModel();
                        nuevo.Name = user.GetName();
                        nuevo.IdSocial = user.GetId();
                        nuevo.Email = string.IsNullOrWhiteSpace(user.GetEmail()) ? user.GetName().Replace(" ", "") + "@danzfloor.com.ar" : user.GetEmail();

                        var clubberRepo = new Repositorio<Clubber>(db);
                        if (clubberRepo.TraerTodos().Any(u => u.UsuarioApplicacion.UserName == nuevo.Email))
                        {
                            var clubber = clubberRepo.TraerTodos().First(u => u.UsuarioApplicacion.UserName == nuevo.Email);
                            var userSocial = new UsuarioSocial
                            {
                                AccessToken = user.GetAccessToken(),
                                Apellido = "",
                                Email = user.GetEmail(),
                                Nombre = user.GetName(),
                                IdSocial = user.GetId(),
                                RedSocial = user is FacebookUser ? RedSocial.FaceBook : user is SpotifyUser ? RedSocial.Spotify : RedSocial.Instagram,
                            };

                            clubber.CredencialesSociales = new List<Models.Dominio.UsuarioSocial>();
                            clubber.CredencialesSociales.Add(userSocial);
                            clubberRepo.GuardarCambios();



                            SignInManager.SignIn(clubber.UsuarioApplicacion, true, true);

                            return RedirectToAction("Index", "Frontend");
                        }

                        var appUser = new ApplicationUser
                        {
                            UserName = nuevo.Email,
                            Email = nuevo.Email,
                            Name = nuevo.Name,
                            Lastname = "",
                            TokenFechaVencimiento = DateTime.Now.AddDays(7),
                        };


                        var usuario = new Clubber();
                        usuario.UsuarioApplicacion = appUser;
                        usuario.Nombre = nuevo.Name;
                        usuario.Apellido = "";
                        usuario.Email = nuevo.Email;
                        usuario.FechaNacimiento = DateTime.Now;
                        usuario.Sexo = new Repositorio<Sexo>(db).TraerTodos().First();

                        if (!string.IsNullOrWhiteSpace(user.GetId()))
                        {
                            var userSocial = new UsuarioSocial
                            {
                                Apellido = "",
                                Email = nuevo.Email,
                                Nombre = nuevo.Name,
                                IdSocial = nuevo.IdSocial,
                                RedSocial = RedSocial.Instagram,
                            };

                            usuario.CredencialesSociales = new List<Models.Dominio.UsuarioSocial>();
                            usuario.CredencialesSociales.Add(userSocial);
                        }
                        new Repositorio<Usuario>(db).Crear(usuario);
                        UserManager.AddPassword(usuario.UsuarioApplicacion.Id, "qweQWE123!#");

                        UserManager.AddToRole(appUser.Id, RoleConst.Clubber);

                        SignInManager.SignIn(appUser, true, true);

                        return RedirectToAction("Index", "Frontend");
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [AllowAnonymous]
        public ActionResult Registrar(string nombre, string idSocial)
        {
            var user = new RegisterFrontViewModel();
            user.Name = nombre;
            user.IdSocial = idSocial;
            if (string.IsNullOrWhiteSpace(idSocial))
            {
                user.Password = "qweQWE123!#";
                user.ConfirmPassword = "qweQWE123!#";
            }
            return View("Registrar", user);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registrar(RegisterFrontViewModel model)
        {
            string ContraseñaError = "La contraseña debe tener al menos: <br>- 6 caracteres</br>- 1 minúscula</br>- 1 mayúscula</br>- 1 símbolo (ej: $, % o !)</br>- 1 número";
            try
            {
                if (string.IsNullOrWhiteSpace(model.Email))
                {
                    ViewBag.Error = "No ha completado el mail";
                    return View("Registrar", model);
                }
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    ViewBag.Error = "No ha completado el nombre";
                    return View("Registrar", model);
                }
                if (string.IsNullOrWhiteSpace(model.IdSocial))
                {
                    if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.ConfirmPassword))
                    {
                        ViewBag.Error = "No ha completado la contraseña y su confirmación";
                        return View("Registrar", model);
                    }
                    if (model.Password != model.ConfirmPassword)
                    {
                        ViewBag.Error = "La contraseña y su confirmación no son iguales";
                        return View("Registrar", model);
                    }
                    if (model.Password.Length < 6)
                    {
                        ViewBag.Error = ContraseñaError;
                        return View("Registrar", model);
                    }
                    if (!model.Password.Any(c => char.IsLower(c)))
                    {
                        ViewBag.Error = ContraseñaError;
                        return View("Registrar", model);
                    }
                    if (!model.Password.Any(c => char.IsUpper(c)))
                    {
                        ViewBag.Error = ContraseñaError;
                        return View("Registrar", model);
                    }
                    if (!model.Password.Any(c => char.IsNumber(c)))
                    {
                        ViewBag.Error = ContraseñaError;
                        return View("Registrar", model);
                    }
                    //Regex RgxUrl = new Regex("[^a-z0-9]");
                    if (model.Password.All(Char.IsLetterOrDigit))
                    {
                        ViewBag.Error = ContraseñaError;
                        return View("Registrar", model);
                    }
                }
                else
                {
                    model.Password = "qweQWE123!#";
                    model.ConfirmPassword = "qweQWE123!#";
                }
                if (new Repositorio<Usuario>(db).TraerTodos().Any(u => u.UsuarioApplicacion.UserName == model.Email))
                {
                    ViewBag.Error = "El email ingresado ya se encuentra en uso";
                    return View("Registrar", model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Lastname = "",
                    TokenFechaVencimiento = DateTime.Now.AddDays(7),
                };


                var usuario = new Clubber();
                usuario.UsuarioApplicacion = user;
                usuario.Nombre = model.Name;
                usuario.Apellido = "";
                usuario.Email = model.Email;
                usuario.FechaNacimiento = DateTime.Now;
                usuario.Sexo = new Repositorio<Sexo>(db).TraerTodos().First();

                if (!string.IsNullOrWhiteSpace(model.IdSocial))
                {
                    var userSocial = new UsuarioSocial
                    {
                        Apellido = "",
                        Email = model.Email,
                        Nombre = model.Name,
                        IdSocial = model.IdSocial,
                        RedSocial = RedSocial.Instagram,
                    };

                    usuario.CredencialesSociales = new List<Models.Dominio.UsuarioSocial>();
                    usuario.CredencialesSociales.Add(userSocial);
                }
                new Repositorio<Usuario>(db).Crear(usuario);
                UserManager.AddPassword(usuario.UsuarioApplicacion.Id, model.Password);

                UserManager.AddToRole(user.Id, RoleConst.Clubber);

                SignInManager.SignIn(user, true, true);

                return RedirectToAction("Index", "Frontend");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ha ocurrido un error!";
                return View("Registrar", model);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                model.MostrarPopUpLoginFront = true;
                return View(model);
            }
            
            var usuario = new Repositorio<Persona>(db).TraerTodos().FirstOrDefault(x => x.Email.Contains(model.Email));


            if (usuario == null)
            {
                model.MostrarPopUpLoginFront = true;
                ModelState.AddModelError("", "El usuario o la contraseña son incorrectos.");
                return View(model);
            }
            else
            {
                SignInStatus resultado;

                if (usuario.Email.Contains("@sapore-di-pane.com.ar"))
                {
                    resultado = SignInManager.PasswordSignIn(usuario.Email, model.Password, model.RememberMe, shouldLockout: false);
                }
                else
                {
                    resultado = SignInManager.PasswordSignIn(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                }

                if (resultado != SignInStatus.Success)
                {
                    model.MostrarPopUpLoginFront = true;
                    ModelState.AddModelError("", "El usuario o la contraseña son incorrectos.");
                    return View(model);
                }

                if (usuario.UsuarioApplicacion.Id.Substring(0, 4) == model.Password.Substring(0, 4))
                {
                    return RedirectToAction("RecuperarPassword");
                }

                if (usuario is Colaborador)
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction("Index", "Frontend");
            }
        } 

        [AllowCrossSiteJson]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Cuenta");
        }

        [AllowCrossSiteJson]
        [System.Web.Mvc.Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [AllowCrossSiteJson]
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        [System.Web.Mvc.Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        [AllowCrossSiteJson]
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [AllowCrossSiteJson]
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult EnviarMailRecuperarPassword(ForgotPasswordViewModel model)
        {
            var email = model.Email;
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Error = "Por favor complete el email";
                return View("ForgotPassword", model);
            }
            ApplicationDbContext db = new ApplicationDbContext();
            UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = um.FindByEmail(email);

            if (user != null)
            {
                try
                {
                    user.Token = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);
                    user.TokenFechaVencimiento = DateTime.Now.AddDays(1);
                    um.Update(user);
                    dynamic em = new Email("EnviarMailRecuperarPassWord");
                    em.To = email;
                    em.Nombre = user.Name;
                    em.Link = WebConfigurationManager.AppSettings["Core"] + WebConfigurationManager.AppSettings["RecuperarPasswordLink"].Replace("[[token]]", user.Token.ToString()).Replace("[[usuarioId]]", user.Id.ToString()).Replace("&amp;", "&");
                    em.Send();
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Hubo un error al enviar el email";
                    return View("ForgotPassword", model);
                }
            }
            else
            {
                ViewBag.Error = "El mail no se corresponde con ningun usuario";
                return View("ForgotPassword", model);
            }

            ViewBag.Success = true;
            return View("ForgotPassword", model);
        }

        [System.Web.Mvc.AllowAnonymous]
        public ActionResult RecuperarPassword(string usuarioid = null, Guid? token = null)
        {
            if (User.Identity.IsAuthenticated && usuarioid == null && token == null)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                user.Token = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);
                user.TokenFechaVencimiento = DateTime.Now.AddDays(5);
                UserManager.Update(user);
                
                var model = new RecuperarPasswordViewModel();
                model.Token = user.Token;
                model.UsuarioId = user.Id;
                return View(model);
            }
            else {
                var model = new RecuperarPasswordViewModel();
                model.Token = token.Value;
                model.UsuarioId = usuarioid;
                return View(model);
            }
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpPost]
        public ActionResult RecuperarPassword(RecuperarPasswordViewModel model)
        {
            var user = UserManager.FindById(model.UsuarioId);
            if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.ConfirmPassword))
            {
                model.Error = "No ha completado los datos solicitados";
                return View("RecuperarPassword", model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                model.Error = "La contraseña y su confirmación no son iguales";
                return View("RecuperarPassword", model);
            }
            if (model.Password.Length < 6)
            {
                model.Error = "La contraseña debe tener al menos 6 caracteres";
                return View("RecuperarPassword", model);
            }
            if (!model.Password.Any(c => char.IsLower(c)))
            {
                model.Error = "La contraseña debe contener al menos una minúscula";
                return View("RecuperarPassword", model);
            }
            if (!model.Password.Any(c => char.IsUpper(c)))
            {
                model.Error = "La contraseña debe contener al menos una mayúscula";
                return View("RecuperarPassword", model);
            }
            if (!model.Password.Any(c => char.IsNumber(c)))
            {
                model.Error = "La contraseña debe contener al menos un número";
                return View("RecuperarPassword", model);
            }
            Regex RgxUrl = new Regex("[^a-z0-9]");
            if (model.Password.All(Char.IsLetterOrDigit))
            {
                model.Error = "La contraseña debe contener al menos un caracter (ejemplos: '!', '#', '-')";
                return View("RecuperarPassword", model);
            }


            if (user != null)
            {
                if (user.Token.Equals(model.Token) && (DateTime.Now <= user.TokenFechaVencimiento))
                {
                    try
                    {
                        UserManager.PasswordHasher = new PasswordHasher();
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);

                        UserManager.Update(user);




                        model.Error = null;
                        ViewBag.Success = true;
                        return View("RecuperarPassword", model);
                    }
                    catch (Exception ex)
                    {
                        model.Error = "Ha ocurrido un error";
                        return View("RecuperarPassword", model);
                    }
                }
                else
                {
                    model.Error = "El token no se corresponde con ningun usuario o el mismo ha expirado";
                    return View("RecuperarPassword", model);
                }
            }
            else
            {
                model.Error = "El id no se corresponde con ningun usuario";
                return View("RecuperarPassword", model);
            }

        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Cuenta", new { ReturnUrl = returnUrl, provider = provider }));
        }
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl, string provider)
        {
            var goTo = CacheHelper.GetKey(CacheHelper.GetNotLoggedUserKey());

            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (ExisteUsuarioSocial(loginInfo.Email))
            {
                //existe el user, loguearlo
                var userSocial = new Repositorio<UsuarioSocial>(new ApplicationDbContext()).TraerTodos().Single(u => u.IdSocial == loginInfo.Email);
                var usuarios = new Repositorio<Clubber>(db).TraerTodos()
                  .Where(r => r.CredencialesSociales.Any(c => c.IdSocial == userSocial.IdSocial));

                SignInManager.SignIn(usuarios.First().UsuarioApplicacion, true, true);

                if (string.IsNullOrEmpty(goTo))
                    return RedirectToAction("Index", "Frontend");
                else
                    return RedirectToLocal(goTo.ToString());
            }
            else
            {
                //no existe el user -> crearlo
                var user = new ApplicationUser
                {
                    UserName = loginInfo.Email,
                    Email = loginInfo.Email,
                    Name = loginInfo.DefaultUserName,
                    Lastname = "",
                    TokenFechaVencimiento = DateTime.Now.AddDays(7),
                };

                var userSocial = new UsuarioSocial
                {
                    Apellido = "",
                    Email = loginInfo.Email,
                    Nombre = loginInfo.DefaultUserName,
                    IdSocial = loginInfo.Email,
                    RedSocial = provider == "Google" ? RedSocial.Google : RedSocial.FaceBook,
                };

                var usuario = new Clubber();
                usuario.UsuarioApplicacion = user;
                usuario.Nombre = loginInfo.DefaultUserName;
                usuario.Apellido = "";
                usuario.Email = loginInfo.Email;
                usuario.FechaNacimiento = DateTime.Now;
                usuario.Sexo = new Repositorio<Sexo>(db).TraerTodos().First();

                usuario.CredencialesSociales = new List<Models.Dominio.UsuarioSocial>();
                usuario.CredencialesSociales.Add(userSocial);

                new Repositorio<Usuario>(db).Crear(usuario);
                UserManager.AddPassword(usuario.UsuarioApplicacion.Id, "qweQWE123!#");

                UserManager.AddToRole(user.Id, RoleConst.Clubber);

                SignInManager.SignIn(user, true, true);

                return RedirectToAction("Index", "Frontend");


            }
        }

        public bool ExisteUsuarioSocial(string codigo)
        {
            return new Repositorio<UsuarioSocial>(new ApplicationDbContext()).TraerTodos().Any(u => u.IdSocial == codigo);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";


        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        public ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }
        #endregion
    }
}