using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WSTankFarm.Models;
using WSTankFarm.Security;
namespace WSTankFarm.Controllers
{
   
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        BDD BD = new BDD();



        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            if (identity.IsAuthenticated)
            {
                return Ok(new DataResult { Status = true, Mensaje = "Usuario Autentificado" });
            }
            else
            {
                return Ok(new DataResult { Status = false, Mensaje = "Usuario No Autentificado" });
            }
        }
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);


            //TODO: This code is only for demo - extract method in new class & validate correctly in your application !!
            var passs = login.Password = DecryptStringAES(login.Password);
            var user = login.Username = DecryptStringAES(login.Username);
            var respuestaValida = ValidarLogin(user, passs);


           
            //var isAdminValid = (login.Username == "admin" && login.Password == "123456");
            if (respuestaValida.UserName != null)
            {
                var rolename = "Administrator";
                respuestaValida.Token = TokenGenerator.GenerateTokenJwt(login.Username, rolename);
                return Ok(respuestaValida);
            }
            else
            {
                return Ok( respuestaValida);
            }

            // Unauthorized access 
            //return Unauthorized();
        }



        public static string DecryptStringAES(string cipherText)
        {
            var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
            var iv = Encoding.UTF8.GetBytes("8080808080808080");

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }
        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings 
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform. 
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption. 
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream 
                                // and place them in a string. 
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }

        [HttpGet]
        public UsuariosSystema ValidarLogin(string UserName, string Password)
        {
            var respuesta = false;
            DataResponse data = new DataResponse();
            List<UsuariosSystema> lstUsuarios = new List<UsuariosSystema>();
            UsuariosSystema ObjUsuarios = new UsuariosSystema();

            List<Roles> lstRoles = new List<Roles>();
            Roles ObjRoles = new Roles();

            string sSql;
            System.Data.DataTable dT;
           
            try
            {


                sSql = "SELECT * from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "] where UserName=@UserName and Password=@Password and Status='0' ";


                var ArrParametros1 = new List<SqlParameter>();

                ArrParametros1.Add(new SqlParameter { ParameterName = "@UserName", Value = UserName });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Password", Value = Password });


                dT = BD.GetSQL(sSql, "Local", ArrParametros1);

                if (dT.Rows.Count > 0)
                {
                    foreach (DataRow dR1 in dT.Rows)
                    {

                        if (UserName == dR1["UserName"].ToString() && Password == dR1["Password"].ToString())
                        {
                            respuesta = true;
                            ObjUsuarios.ID = Convert.ToInt32(dR1["ID"]);
                            ObjUsuarios.Nombre = dR1["Nombre"].ToString();
                            ObjUsuarios.LastName = dR1["LastName"].ToString();
                            ObjUsuarios.SecondLastName = dR1["SecondLastName"].ToString();
                            ObjUsuarios.UserName = dR1["UserName"].ToString();
                            //ObjUsuarios.Password = dR1["Password"].ToString();
                            ObjUsuarios.IdRol = Convert.ToInt32(dR1["IdRol"]);
                            ObjUsuarios.IdIdioma = Convert.ToInt32(dR1["IdIdioma"]);
                            ObjUsuarios.Telefono = dR1["Telefono"].ToString();
                            ObjUsuarios.Correo = dR1["Correo"].ToString();
                            ObjUsuarios.Status = dR1["Status"].ToString();
                            ObjUsuarios.Compania = dR1["Compania"].ToString();
                            //ObjUsuarios.Token = Guid.NewGuid().ToString();

                            if (ObjUsuarios.IdRol.ToString() != "" & ObjUsuarios.IdRol.ToString() != "---")
                            {
                                string SqlR = "SELECT * from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaRol + "] where ID=@ID";

                                var ArrParametros2 = new List<SqlParameter>();

                                ArrParametros2.Add(new SqlParameter { ParameterName = "@ID", Value = ObjUsuarios.IdRol.ToString() });

                                dT = BD.GetSQL(SqlR, "Local", ArrParametros2);
                                foreach (DataRow dR in dT.Rows)
                                {
                                    ObjRoles.ID = dR["ID"];
                                    ObjRoles.Rol = dR["Rol"];
                                    ObjRoles.Customers = dR["Customers"];
                                    ObjRoles.Users = dR["Users"];
                                    ObjRoles.Vehicles = dR["Vehicles"];
                                    ObjRoles.Drivers = dR["Drivers"];
                                    ObjRoles.Department = dR["Department"];
                                    ObjRoles.Brand = dR["Brand"];
                                    ObjRoles.Model = dR["Model"];
                                    ObjRoles.CostCenter = dR["CostCenter"];
                                    ObjRoles.Perfiles = dR["Perfiles"];
                                    ObjRoles.Configuration = dR["Configuration"];
                                    ObjRoles.Status = dR["Status"];
                                    ObjUsuarios.Roles = ObjRoles;
                                    //lstUsuarios.Add(ObjUsuarios);
                                }
                            }
                        }
                        else
                        {

                            respuesta = false;
                        }
                        
                    }


                    data.lstUsuarios = lstUsuarios;
                    data.Message = "Usuario y contraseña correctos";
                    data.Status = "SUCCESSFUL";

                }
                else
                {
                    data.lstUsuarios = lstUsuarios;
                    data.Message = "Usuario y contraseña incorrectos o usuario Inactivo, favor de verificar";
                    data.Status = "FOUND";

                }

                return ObjUsuarios;
                //return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                return null;
            }

        }
        

      
        

    }
    public class DataResult
    {
        public Boolean Status { get; set; }
        public string Mensaje { get; set; } = "";
        public string Token { get; set; } = "";
        public string Username { get; set; } = "";

    }
}