using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WSTankFarm.Controllers
{
    public class DispenserController : Controller
    {
        // ESTOS WS LOS UTILIZA LA APLICACION DE ESCRITORIO Y NO SE REQUIERE QUE TENGAN PERMISOS DE TOKEN
        BDD BD = new BDD();
        
        [HttpPost]
        public JsonResult wsInsertarLogs(string Descripcion = "")
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {



                string sSelect = "";
                sSelect = "USE [" + GlobalesLocal.BDLocal + "] INSERT INTO[dbo].[Logs]([Fecha],[Descripcion])VALUES(GETDATE(),@Descripcion)";
                var Sparam1 = new SqlParameter();
                var ArrParametros1 = new List<SqlParameter>();
                Sparam1.ParameterName = "@Descripcion";
                Sparam1.Value = Descripcion;
                ArrParametros1.Add(Sparam1);


                DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local");
                if (DataAfected > 0)
                {

                    data.Message = "SUCCESSFUL INSERTION";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED INSERTION OF LOGS";
                    data.Status = "ERROR";
                }





                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Insertar LOG en la base de datos local", "wsInsertarLogs");
                //EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Insertar un LOG en la base de datos local", "Ha ocurrido un error en la consulta al Insertar un LOG en la base de datos local", "", "", "", "");
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }



        }


        //gera
        [HttpGet]
        public JsonResult GetChoferes(string codcli = "0", string tag = "0", string all = "")
        {
            List<Choferes> lstChoferes = new List<Choferes>();
            DataTable dT = new DataTable();
            DataTable dTDrivers = new DataTable();
            DataTable dTPerfil = new DataTable();
            DataTable dtEmailManager = new DataTable();
            DataResponse data = new DataResponse();
            var ArrParametros1 = new List<SqlParameter>();
            try
            {
                string sSelect = "";
                string selectCustomer = "";
                string selectManagerCorreo = "";
                string selectCentroDepartamento = "";
                string consultaPerfiles = "";
                if (codcli != "0")
                {

                    sSelect = "SELECT * FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] CG INNER JOIN [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "]  TF  ON CG.codcli = TF.IdCliente  and TF.Tag=CG.tag and Status<>-1 where codcli= @codcli order by TF.ID desc";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codcli", Value = codcli });
                }
                else
                {
                    if (tag != "0")
                    {
                        sSelect = "SELECT * FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] CG INNER JOIN [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "]  TF  ON CG.codcli = TF.IdCliente  and TF.Tag=CG.tag and Status<>-1 where TF.Tag=@tag order by TF.ID desc";
                        ArrParametros1.Add(new SqlParameter { ParameterName = "@tag", Value = tag });
                    }
                    else
                    {
                        if (all == "" || all == "0" || all == null)
                        {
                            sSelect = "SELECT * FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] CG INNER JOIN [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "]  TF  ON CG.codcli = TF.IdCliente and TF.Tag=CG.tag and Status=0 and CG.codest=0 order by TF.ID desc";
                        }
                        else
                        {
                            sSelect = "SELECT * FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] CG INNER JOIN [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "]  TF  ON CG.codcli = TF.IdCliente and TF.Tag=CG.tag";
                        }


                    }

                }






                dT = BD.GetSQL(sSelect, "ControlGas", ArrParametros1);
                var columnas = dT.Columns;
                //return Json(dT.Rows, JsonRequestBehavior.AllowGet);
                foreach (DataRow dR1 in dT.Rows)
                {

                    Choferes ObjChofer = new Choferes();

                    ObjChofer.ID = Convert.ToInt32(dR1["ID"].ToString());
                    ObjChofer.Nombre = dR1["Nombre"].ToString();
                    ObjChofer.Telefono = dR1["Telefono"].ToString();
                    ObjChofer.IdIdioma = Convert.ToInt32(dR1["IdIdioma"].ToString());
                    ObjChofer.Gerente = dR1["Gerente"].ToString();
                    ObjChofer.Correo = dR1["Correo"].ToString().ToUpper();
                    ObjChofer.Perfil = dR1["Perfil"].ToString();
                    ObjChofer.codcli = Convert.ToInt32(dR1["codcli"].ToString());

                    ObjChofer.nrocho = Convert.ToInt32(dR1["nrocho"].ToString());
                    ObjChofer.den = dR1["den"].ToString();
                    ObjChofer.diacar = Convert.ToInt32(dR1["diacar"].ToString());
                    ObjChofer.hraini = Convert.ToInt32(dR1["hraini"].ToString());
                    ObjChofer.hrafin = Convert.ToInt32(dR1["hrafin"].ToString());
                    ObjChofer.tag = dR1["tag"].ToString();
                    ObjChofer.codest = Convert.ToInt32(dR1["codest"].ToString());

                    ObjChofer.Status = dR1["Status"].ToString();
                    ObjChofer.LastName = dR1["LastName"].ToString();
                    ObjChofer.SecondLastName = dR1["SecondLastName"].ToString();
                    ObjChofer.Departamento = dR1["Departamento"].ToString();
                    ObjChofer.CentroCostos = dR1["CentroCostos"].ToString();

                    var ArrParametros2 = new List<SqlParameter>();

                    selectCustomer = "select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where cod=@cod";
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@cod", Value = ObjChofer.codcli });
                    dTDrivers = BD.GetSQL(selectCustomer, "ControlGas", ArrParametros2);
                    foreach (DataRow Driv in dTDrivers.Rows)
                    {
                        ObjChofer.Company = Driv["den"].ToString();

                    }
                    var ArrParametros3 = new List<SqlParameter>();
                    selectCentroDepartamento = "select (select Departamento from Departamento where ID= [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "] .Departamento )as DepartamentoName, (select nameCentro from CentroCostos where id= [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "] .CentroCostos )as CentroCostosName from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "]  where id=@id";
                    ArrParametros3.Add(new SqlParameter { ParameterName = "@id", Value = ObjChofer.ID });
                    dTDrivers = BD.GetSQL(selectCentroDepartamento, "Local", ArrParametros3);
                    foreach (DataRow Driv in dTDrivers.Rows)
                    {
                        ObjChofer.DepartamentoName = Driv["DepartamentoName"].ToString().ToUpper();
                        ObjChofer.CentroCostosName = Driv["CentroCostosName"].ToString().ToUpper();

                    }
                    var ArrParametros4 = new List<SqlParameter>();
                    selectManagerCorreo = "select Correo FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "]  where replace(ltrim(rtrim(@Gerente)),' ','' ) = NameComplete";
                    ArrParametros4.Add(new SqlParameter { ParameterName = "@Gerente", Value = ObjChofer.Gerente });
                    dtEmailManager = BD.GetSQL(selectManagerCorreo, "ControlGas", ArrParametros4);

                    //Aqui se obtienen los datos del perfil 

                    //consultaPerfiles = "SELECT Odometro,Departamentos FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "] where ID = '" + ObjChofer.Perfil + "'";
                    //dTPerfil = BD.GetSQL(consultaPerfiles, "Local");
                    //foreach (DataRow perfiles in dTPerfil.Rows)
                    //{
                    //    ObjChofer.Departamentos = perfiles["Departamentos"].ToString().ToUpper();
                    //    ObjChofer.Odometro = perfiles["Odometro"].ToString().ToUpper();

                    //}


                    foreach (DataRow Driv in dtEmailManager.Rows)
                    {

                        if (Driv["Correo"].ToString() == "" || Driv["Correo"].ToString() == null || string.IsNullOrEmpty(Driv["Correo"].ToString()))
                        {
                            ObjChofer.ManagerEmail = "--";
                        }
                        else
                        {
                            ObjChofer.ManagerEmail = Driv["Correo"].ToString();
                        }

                    }
                    lstChoferes.Add(ObjChofer);
                }

                if (lstChoferes.Count > 0)
                {
                    data.lstChoferes = lstChoferes;
                    data.Message = "Drivers";
                    data.Status = "OK";
                }
                else
                {
                    data.lstChoferes = lstChoferes;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Error en la consulta no se pueden obtener los choferes", "GetChoferes");
                //EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Error en la consulta no se pueden obtener los choferes", "Error en la consulta no se pueden obtener los choferes", "", "", "", "");
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }



        }

        [HttpGet]
        public string ValidarHorarioChofer(string Tag = "0")
        {
            string result = "";
            string ConcatHoraMin = "";
            string DiaObtenido;
            int[] Dias = new int[7] { 1, 2, 4, 8, 16, 32, 64 };
            string[] NombresDias = new string[7] { "LUNES", "MARTES", "MIERCOLES", "JUEVES", "VIERNES", "SABADO", "DOMINGO" };
            List<string> ListaDeDias = new List<string>();

            DataResponse data = new DataResponse();
            Choferes ObjChoferes = new Choferes();
            List<Choferes> lstChoferes = new List<Choferes>();
            System.Data.DataTable dT;
            DateTime fechaActual = DateTime.Now;

            string mes2 = fechaActual.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();
            string diaActual = fechaActual.ToString("dddd", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();
            string hora = fechaActual.ToString("HH", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();
            string minutos = fechaActual.ToString("mm", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();
            ConcatHoraMin = hora + minutos;
            string sSelect = "";
            if (Tag == "0")
            {

            }
            else
            {
                sSelect = "SELECT[nrocho] as NumeroChofer,[diacar] as DiasCarro,[hraini] as HoraInicio ,[hrafin] as HoraFin, ChoferesCG.[tag] as Tag,[codest] as Status,[logusu] as Usuario, [logfch] as FechaRegistro, [Nombre] as NombreCompleto,[Telefono],[IdIdioma] as Idioma,[Gerente],[Correo] from[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] as ChoferesCG inner join[" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "]  as ChoferesTF on ChoferesCG.tag=ChoferesTF.Tag  WHERE ChoferesTF.tag =@Tag";

                var ArrParametros1 = new List<SqlParameter>();

                ArrParametros1.Add(new SqlParameter { ParameterName = "@Tag", Value = Tag });

                dT = BD.GetSQL(sSelect, "ControlGas", ArrParametros1);

                foreach (DataRow dR1 in dT.Rows)
                {

                    ObjChoferes.Nombre = dR1["NombreCompleto"].ToString();
                    ObjChoferes.nrocho = Convert.ToInt32(dR1["NumeroChofer"].ToString());
                    ObjChoferes.tag = dR1["Tag"].ToString();
                    ObjChoferes.diacar = Convert.ToInt32(dR1["DiasCarro"].ToString());
                    ObjChoferes.hraini = Convert.ToInt32(dR1["HoraInicio"].ToString());
                    ObjChoferes.hrafin = Convert.ToInt32(dR1["HoraFin"].ToString());

                    lstChoferes.Add(ObjChoferes);
                }
                for (int i = 0; i < Dias.Length; i++)
                {
                    //suma = suma + Dias[i];
                    decimal num = Convert.ToInt32(ObjChoferes.diacar) / Dias[i];
                    if (num % 2 == 0)
                    {

                    }
                    else
                    {
                        if (num >= 1)
                        {
                            ListaDeDias.Add(NombresDias[i].ToUpper());
                        }

                    }
                }
                string[] ArrayDias = ListaDeDias.ToArray();
                if (ArrayDias.Contains(diaActual))
                {
                    if (Convert.ToInt32(ObjChoferes.hraini) == 0)
                    {
                        if (Convert.ToInt32(ConcatHoraMin) > Convert.ToInt32(ObjChoferes.hrafin))
                        {
                            result = "No es posible despachar combustible al chofer " + ObjChoferes.Nombre.ToString() + " por que se encuentra fuera de horario";
                        }
                        else
                        {
                            result = "Ready";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(ConcatHoraMin) < Convert.ToInt32(ObjChoferes.hraini))
                        {
                            result = "No es posible despachar combustible al chofer " + ObjChoferes.Nombre.ToString() + " por que se encuentra fuera de horario";
                        }
                        else
                        {
                            if (Convert.ToInt32(ConcatHoraMin) > Convert.ToInt32(ObjChoferes.hrafin))
                            {
                                result = "No es posible despachar combustible al chofer " + ObjChoferes.Nombre.ToString() + " por que se encuentra fuera de horario";
                            }
                            else
                            {
                                result = "Ready";
                            }


                        }
                    }

                    if (Convert.ToInt32(ObjChoferes.hrafin) == 0)
                    {
                        if (Convert.ToInt32(ConcatHoraMin) < Convert.ToInt32(ObjChoferes.hraini))
                        {
                            result = "No es posible despachar combustible al chofer " + ObjChoferes.Nombre.ToString() + " por que se encuentra fuera de horario";
                        }
                        else
                        {
                            result = "Ready";
                        }
                    }
                    if (Convert.ToInt32(ObjChoferes.hrafin) == 0 && Convert.ToInt32(ObjChoferes.hraini) == 0)
                    {
                        result = "Ready";
                    }
                }
                else
                {
                    result = "No es posible despachar combustible al chofer " + ObjChoferes.Nombre.ToString() + " por que se encuentra fuera de horario";
                }

            }
            return result;
        }

        [HttpGet]
        public JsonResult GetVehiculos(string id = "0", string tag = "0", string all = "")

        {
            List<Vehiculo> lstVehiculos = new List<Vehiculo>();
            DataTable dT = new DataTable();
            DataTable dTPerfil = new DataTable();
            DataTable dT1 = new DataTable();

            DataTable dTDepart = new DataTable();
            DataTable dT2 = new DataTable();
            DataResponse data = new DataResponse();
            //**********************************************************************
            var ArrParametros = new List<SqlParameter>();
            //**********************************************************************
            try
            {
                string sSelect = "";
                string selectCustomer = "";
                string consultaPerfiles = "";
                string consultaDepartamento = "";
                //**********************************************************************
                ArrParametros = new List<SqlParameter>();
                if (id != "0")
                {
                    //OLD-select tipoPerfil from [TANKFARM].[dbo].[Perfiles]  where ID = [WS1].[dbo].[ClientesVehiculos].[tagex2]) as profile,
                    //sSelect = "SELECT " + "(select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where cod = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[codcli]" + ") as company,    " + "(select tipoPerfil from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "]  where ID = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[tagex2]" + ") as profile,[codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp],(select MarcDesc from  [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] where marca.MarcID=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvemp ) as marca ,[cnvobs],(select ModelDesc from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where modelo.ModelId=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvobs ) as modelo ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim] FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] WHERE codcli ='" + id + "' order by tar desc";
                    sSelect = "SELECT " + "(select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where cod = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[codcli]" + ") as company,    " + "(select tipoPerfil from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "]  where ID = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[tagex2]" + ") as profile,[codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp],(select MarcDesc from  [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] where marca.MarcID=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvemp ) as marca ,[cnvobs],(select ModelDesc from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where modelo.ModelId=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvobs ) as modelo ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim] FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] WHERE codcli =@CodCli order by tar desc";
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodCli", Value = id });
                    //OLD-sSelect = "SELECT * FROM ["+ GlobalesCorporativo.BDCorporativoModerno +"].[dbo].["+ GlobalesCorporativo.TablaClientesVehiculos +"] WHERE codcli =" + id;
                }
                else
                {

                    if (tag != "0")
                    {
                        //sSelect = "SELECT " + "(select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where cod = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[codcli]" + ") as company,    " + "(select tipoPerfil from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "]  where ID = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[tagex2]" + ") as profile,[codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp],(select MarcDesc from  [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] where marca.MarcID=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvemp ) as marca ,[cnvobs],(select ModelDesc from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where modelo.ModelId=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvobs ) as modelo ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim] FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] WHERE tag ='" + tag + "' order by tar desc";
                        sSelect = "SELECT " + "(select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where cod = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[codcli]" + ") as company,    " + "(select tipoPerfil from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "]  where ID = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[tagex2]" + ") as profile,[codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp],(select MarcDesc from  [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] where marca.MarcID=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvemp ) as marca ,[cnvobs],(select ModelDesc from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where modelo.ModelId=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvobs ) as modelo ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim] FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] WHERE tag =@Tag order by tar desc";
                        ArrParametros.Add(new SqlParameter { ParameterName = "@Tag", Value = tag });
                        //OLD-sSelect = "SELECT " + "(select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where cod = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[codcli]" + ") as company, [codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp],(select MarcDesc from  [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] where marca.MarcID=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvemp ) as marca ,[cnvobs],(select ModelDesc from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where modelo.ModelId=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvobs ) as modelo ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim] FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]  WHERE  tag ='" + tag + "'";
                        //OLD-sSelect = "SELECT * FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] WHERE  tag ='" + tag + "'";
                    }
                    else
                    {
                        if (all == "" || all == "0" || all == null)
                        {
                            //************************************************************
                            //AQUI NO SE LEE NINGUN PARAMETRO EL FILTRO ESTA QUEMADO EST=1
                            sSelect = "SELECT " + "(select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where cod = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[codcli]" + ") as company,    " + "(select tipoPerfil from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "]  where ID = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[tagex2]" + ") as profile,[codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp],(select MarcDesc from  [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] where marca.MarcID=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvemp ) as marca ,[cnvobs],(select ModelDesc from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where modelo.ModelId=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvobs ) as modelo ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim] FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] WHERE  est=1 order by tar desc;";
                            //************************************************************
                            //sSelect = "SELECT " + "(select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where cod = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[codcli]" + ") as company, [codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp],(select MarcDesc from  [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] where marca.MarcID=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvemp ) as marca ,[cnvobs],(select ModelDesc from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where modelo.ModelId=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvobs ) as modelo ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim] FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] where est=1";
                            //sSelect = "SELECT * FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] where est=1";
                        }
                        else
                        {
                            //************************************************************
                            //AQUI NO SE LEE NINGUN PARAMETRO 
                            sSelect = "SELECT " + "(select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where cod = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[codcli]" + ") as company,    " + "(select tipoPerfil from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "]  where ID = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[tagex2]" + ") as profile,[codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp],(select MarcDesc from  [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] where marca.MarcID=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvemp ) as marca ,[cnvobs],(select ModelDesc from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where modelo.ModelId=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvobs ) as modelo ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim] FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] order by tar desc ";
                            //************************************************************
                            //sSelect = "SELECT " + "(select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where cod = " + "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].[codcli]" + ") as company, [codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp],(select MarcDesc from  [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] where marca.MarcID=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvemp ) as marca ,[cnvobs],(select ModelDesc from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where modelo.ModelId=[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "].cnvobs ) as modelo ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim] FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]";
                            //sSelect = "SELECT * FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]";
                        }


                    }


                }



                dT = BD.GetSQL(sSelect, "ControlGas", ArrParametros);
                //**********************************************************************
                var columnas = dT.Columns;
                //return Json(dT.Rows, JsonRequestBehavior.AllowGet);
                foreach (DataRow dR1 in dT.Rows)
                {

                    Vehiculo ObjVehiculo = new Vehiculo();
                    ObjVehiculo.company = dR1["company"].ToString();
                    ObjVehiculo.profileName = dR1["profile"].ToString();
                    ObjVehiculo.codcli = Convert.ToInt32(dR1["codcli"].ToString());
                    ObjVehiculo.nroveh = Convert.ToInt32(dR1["nroveh"].ToString());
                    ObjVehiculo.tar = Convert.ToInt32(dR1["tar"].ToString());
                    ObjVehiculo.plc = dR1["plc"].ToString();

                    if (dR1["marca"].ToString() != "" && dR1["marca"].ToString() != null)
                    {

                        ObjVehiculo.den = dR1["marca"].ToString().Trim() + "/" + dR1["modelo"].ToString().Trim();

                    }
                    else
                    {
                        ObjVehiculo.den = "No information";
                    }

                    ObjVehiculo.rsp = dR1["rsp"].ToString();
                    ObjVehiculo.grp = dR1["grp"].ToString();
                    ObjVehiculo.diacar = Convert.ToInt32(dR1["diacar"].ToString());
                    ObjVehiculo.hraini = Convert.ToInt32(dR1["hraini"].ToString());
                    ObjVehiculo.hrafin = Convert.ToInt32(dR1["hrafin"].ToString());
                    ObjVehiculo.carmax = Convert.ToInt32(dR1["carmax"].ToString());
                    ObjVehiculo.candia = Convert.ToInt32(dR1["candia"].ToString());
                    ObjVehiculo.cansem = Convert.ToInt32(dR1["cansem"].ToString());
                    ObjVehiculo.canmes = Convert.ToInt32(dR1["canmes"].ToString());
                    ObjVehiculo.acudia = Convert.ToInt32(dR1["acudia"].ToString());
                    ObjVehiculo.acusem = Convert.ToInt32(dR1["acusem"].ToString());
                    ObjVehiculo.acumes = Convert.ToInt32(dR1["acumes"].ToString());
                    ObjVehiculo.ultcar = Convert.ToInt32(dR1["ultcar"].ToString());
                    ObjVehiculo.ultodm = Convert.ToInt32(dR1["ultodm"].ToString());
                    ObjVehiculo.codgas = Convert.ToInt32(dR1["codgas"].ToString());
                    ObjVehiculo.codprd = Convert.ToInt32(dR1["codprd"].ToString());
                    ObjVehiculo.debsdo = ToDouble(dR1["debsdo"].ToString());
                    ObjVehiculo.debfch = Convert.ToInt32(dR1["debfch"].ToString());
                    ObjVehiculo.debnro = Convert.ToInt32(dR1["debnro"].ToString());
                    ObjVehiculo.debcan = ToDouble(dR1["debcan"].ToString());
                    ObjVehiculo.nip = Convert.ToInt32(dR1["nip"].ToString());
                    ObjVehiculo.ptosdo = ToDouble(dR1["ptosdo"].ToString());
                    ObjVehiculo.ptofch = Convert.ToInt32(dR1["ptofch"].ToString());
                    ObjVehiculo.ptocan = ToDouble(dR1["ptocan"].ToString());
                    ObjVehiculo.premto = ToDouble(dR1["premto"].ToString());
                    ObjVehiculo.prepgo = ToDouble(dR1["prepgo"].ToString());
                    ObjVehiculo.prefid = ToDouble(dR1["prefid"].ToString());
                    ObjVehiculo.cnvemp = dR1["cnvemp"].ToString();
                    ObjVehiculo.cnvobs = dR1["cnvobs"].ToString();
                    ObjVehiculo.cnvfch = Convert.ToInt32(dR1["cnvfch"].ToString());
                    ObjVehiculo.manobs = dR1["manobs"].ToString();
                    ObjVehiculo.manper = Convert.ToInt32(dR1["manper"].ToString());
                    ObjVehiculo.manult = Convert.ToInt32(dR1["manult"].ToString());
                    ObjVehiculo.rut = dR1["rut"].ToString();
                    ObjVehiculo.tag = dR1["tag"].ToString();
                    ObjVehiculo.vto = Convert.ToInt32(dR1["vto"].ToString());
                    ObjVehiculo.limtur = Convert.ToInt32(dR1["limtur"].ToString());
                    ObjVehiculo.ulttur = Convert.ToInt32(dR1["ulttur"].ToString());
                    ObjVehiculo.acutur = Convert.ToInt32(dR1["acutur"].ToString());
                    ObjVehiculo.limprd = Convert.ToInt32(dR1["limprd"].ToString());
                    ObjVehiculo.acuprd = Convert.ToInt32(dR1["acuprd"].ToString());
                    ObjVehiculo.crefch = Convert.ToInt32(dR1["crefch"].ToString());
                    ObjVehiculo.crenro = Convert.ToInt32(dR1["crenro"].ToString());
                    ObjVehiculo.crecan = ToDouble(dR1["crecan"].ToString());
                    ObjVehiculo.crefch2 = Convert.ToInt32(dR1["crefch2"].ToString());
                    ObjVehiculo.crenro2 = Convert.ToInt32(dR1["crenro2"].ToString());
                    ObjVehiculo.crecan2 = ToDouble(dR1["crecan2"].ToString());
                    ObjVehiculo.debfch2 = Convert.ToInt32(dR1["debfch2"].ToString());
                    ObjVehiculo.debnro2 = Convert.ToInt32(dR1["debnro2"].ToString());
                    ObjVehiculo.debcan2 = ToDouble(dR1["debcan2"].ToString());
                    ObjVehiculo.est = Convert.ToInt32(dR1["est"].ToString());
                    ObjVehiculo.niplog = dR1["niplog"].ToString();
                    ObjVehiculo.logusu = Convert.ToInt32(dR1["logusu"].ToString());
                    ObjVehiculo.logfch = dR1["logfch"].ToString();
                    ObjVehiculo.lognew = dR1["lognew"].ToString();
                    ObjVehiculo.tagadi = dR1["tagadi"].ToString();
                    ObjVehiculo.ctapre = dR1["ctapre"].ToString();
                    ObjVehiculo.nropat = dR1["nropat"].ToString();
                    ObjVehiculo.nroeco = dR1["nroeco"].ToString();
                    ObjVehiculo.hraini2 = Convert.ToInt32(dR1["hraini2"].ToString());
                    ObjVehiculo.hrafin2 = Convert.ToInt32(dR1["hrafin2"].ToString());
                    ObjVehiculo.hraini3 = Convert.ToInt32(dR1["hraini3"].ToString());
                    ObjVehiculo.hrafin3 = Convert.ToInt32(dR1["hrafin3"].ToString());
                    ObjVehiculo.aju = Convert.ToInt32(dR1["aju"].ToString());
                    ObjVehiculo.ptodebacu = ToDouble(dR1["ptodebacu"].ToString());
                    ObjVehiculo.ptodebfch = Convert.ToInt32(dR1["ptodebfch"].ToString());
                    ObjVehiculo.ptocreacu = ToDouble(dR1["ptocreacu"].ToString());
                    ObjVehiculo.ptocrefch = Convert.ToInt32(dR1["ptocrefch"].ToString());
                    ObjVehiculo.ptovenacu = ToDouble(dR1["ptovenacu"].ToString());
                    ObjVehiculo.ptovenfch = Convert.ToInt32(dR1["ptovenfch"].ToString());
                    ObjVehiculo.tagex1 = dR1["tagex1"].ToString();
                    ObjVehiculo.tagex2 = dR1["tagex2"].ToString();
                    ObjVehiculo.tagex3 = dR1["tagex3"].ToString();
                    ObjVehiculo.ultcan = ToDouble(dR1["ultcan"].ToString());
                    ObjVehiculo.datvar = Convert.ToInt32(dR1["datvar"].ToString());
                    ObjVehiculo.catprd = dR1["catprd"].ToString();
                    ObjVehiculo.catuni = dR1["catuni"].ToString();
                    ObjVehiculo.dialim = dR1["dialim"].ToString();


                    /* selectCustomer = "select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where cod='" + ObjVehiculo.codcli + "'";
                     dT1 = BD.GetSQL(selectCustomer, "ControlGas");
                     foreach (DataRow Veh in dT1.Rows)
                     {
                         ObjVehiculo.company = Veh["den"];

                     }*/

                    //Aqui se obtienen los datos del perfil 
                    //**********************************************************************
                    ArrParametros = new List<SqlParameter>();
                    //consultaPerfiles = "SELECT Odometro,Departamentos FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "] where ID = '" + ObjVehiculo.tagex2 + "'";
                    consultaPerfiles = "SELECT Odometro,Departamentos FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "] where ID = @ID";
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ID", Value = ObjVehiculo.tagex2 });
                    dTPerfil = BD.GetSQL(consultaPerfiles, "Local", ArrParametros);
                    //**********************************************************************
                    foreach (DataRow perfiles in dTPerfil.Rows)
                    {
                        ObjVehiculo.Departamentos = perfiles["Departamentos"].ToString();
                        ObjVehiculo.ValidacionOdometro = perfiles["Odometro"].ToString();

                    }
                    var depa = ObjVehiculo.Departamentos.Split(',');

                    for (int i = 0; i < depa.Length - 1; i++)
                    {
                        //**********************************************************************
                        ArrParametros = new List<SqlParameter>();
                        //consultaDepartamento = "SELECT ID,Departamento FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaDepartamento + "] where ID = '" + depa[i] + "'";
                        consultaDepartamento = "SELECT ID,Departamento FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaDepartamento + "] where ID = @ID";
                        ArrParametros.Add(new SqlParameter { ParameterName = "@ID", Value = depa[i] });
                        dTDepart = BD.GetSQL(consultaDepartamento, "Local", ArrParametros);
                        //**********************************************************************
                        foreach (DataRow perfiles in dTDepart.Rows)
                        {
                            ObjVehiculo.NombreDepartamento += perfiles["Departamento"].ToString() + ",";


                        }

                    }
                    lstVehiculos.Add(ObjVehiculo);
                    if (tag != "0")
                    {
                        break;
                    }
                }

                if (lstVehiculos.Count > 0)
                {
                    data.lstVehicles = lstVehiculos;
                    data.Message = "VEHICLE(S) FOUND";
                    data.Status = "OK";
                }
                else
                {
                    data.lstVehicles = lstVehiculos;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al obtener los vehiculos", "GetVehiculos");
                //EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al obtener los vehiculos", "Ha ocurrido un error en la consulta al obtener los vehiculos", "", "", "", "");
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }



        }
        [HttpGet]
        public string ValidarHorarioVehiculo(string Tag = "0")
        {
            string result = "";
            string ConcatHoraMin = "";
            string DiaObtenido;
            int[] Dias = new int[7] { 1, 2, 4, 8, 16, 32, 64 };
            string[] NombresDias = new string[7] { "LUNES", "MARTES", "MIERCOLES", "JUEVES", "VIERNES", "SABADO", "DOMINGO" };
            List<string> ListaDeDias = new List<string>();

            DataResponse data = new DataResponse();
            Vehiculo ObjVehiculo = new Vehiculo();
            List<Vehiculo> lstVehiculo = new List<Vehiculo>();
            System.Data.DataTable dT;
            DateTime fechaActual = DateTime.Now;

            string mes2 = fechaActual.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();
            string diaActual = RemoveDiacritics(fechaActual.ToString("dddd", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper());
            string hora = fechaActual.ToString("HH", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();
            string minutos = fechaActual.ToString("mm", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();
            ConcatHoraMin = hora + minutos;
            string sSelect = "";
            if (Tag != "0")
            {
                sSelect = "select* from[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] where tag  = @Tag";
            }

            var ArrParametros1 = new List<SqlParameter>();

            ArrParametros1.Add(new SqlParameter { ParameterName = "@Tag", Value = Tag });

            dT = BD.GetSQL(sSelect, "ControlGas", ArrParametros1);

            foreach (DataRow dR1 in dT.Rows)
            {

                ObjVehiculo.plc = dR1["plc"].ToString();
                ObjVehiculo.nroveh = Convert.ToInt32(dR1["nroveh"].ToString());
                ObjVehiculo.tag = dR1["tag"].ToString();
                ObjVehiculo.diacar = Convert.ToInt32(dR1["diacar"].ToString());
                ObjVehiculo.hraini = Convert.ToInt32(dR1["hraini"].ToString());
                ObjVehiculo.hrafin = Convert.ToInt32(dR1["hrafin"].ToString());

                lstVehiculo.Add(ObjVehiculo);
            }
            for (int i = 0; i < Dias.Length; i++)
            {
                //suma = suma + Dias[i];
                decimal num = Convert.ToInt32(ObjVehiculo.diacar) / Dias[i];
                if (num % 2 == 0)
                {

                }
                else
                {
                    if (num >= 1)
                    {
                        ListaDeDias.Add(NombresDias[i].ToUpper());
                    }

                }
            }
            string[] ArrayDias = ListaDeDias.ToArray();
            if (ArrayDias.Contains(diaActual))
            {
                if (Convert.ToInt32(ObjVehiculo.hraini) == 0)
                {
                    if (Convert.ToInt32(ConcatHoraMin) > Convert.ToInt32(ObjVehiculo.hrafin))
                    {
                        result = "No es posible despachar combustible al vehiculo  número" + ObjVehiculo.nroveh.ToString() + " por que se encuentra fuera de horario";
                    }
                    else
                    {
                        result = "Ready";
                    }
                }
                else
                {
                    if (Convert.ToInt32(ConcatHoraMin) < Convert.ToInt32(ObjVehiculo.hraini))
                    {
                        result = "No es posible despachar combustible al vehiculo  número " + ObjVehiculo.nroveh.ToString() + " por que se encuentra fuera de horario";
                    }
                    else
                    {
                        if (Convert.ToInt32(ConcatHoraMin) > Convert.ToInt32(ObjVehiculo.hrafin))
                        {
                            result = "No es posible despachar combustible al vehiculo  número " + ObjVehiculo.nroveh.ToString() + " por que se encuentra fuera de horario";
                        }
                        else
                        {
                            result = "Ready";
                        }


                    }
                }

                if (Convert.ToInt32(ObjVehiculo.hrafin) == 0)
                {
                    if (Convert.ToInt32(ConcatHoraMin) < Convert.ToInt32(ObjVehiculo.hraini))
                    {
                        result = "No es posible despachar combustible al vehiculo  número " + ObjVehiculo.nroveh.ToString() + " por que se encuentra fuera de horario";
                    }
                    else
                    {
                        result = "Ready";
                    }
                }


                if (Convert.ToInt32(ObjVehiculo.hrafin) == 0 && Convert.ToInt32(ObjVehiculo.hraini) == 0)
                {
                    result = "Ready";
                }

            }
            else
            {
                result = "No es posible despachar combustible al vehiculo  número " + ObjVehiculo.nroveh.ToString() + " por que se encuentra fuera de horario";
            }
            return result;
        }

        //Este ws es utilizado para actualizar el odometro  de un vehiculo base de datos de control Gas
        [HttpPost]
        public JsonResult WsUpdateOdometro(string tag, string odometro)
        {

            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {
                string sSelect = "";
                sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "]  UPDATE [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]SET [ultodm] =@odometro  WHERE tag=@tag";

                var ArrParametros1 = new List<SqlParameter>();

                ArrParametros1.Add(new SqlParameter { ParameterName = "@tag", Value = tag });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@odometro", Value = odometro });

                DataAfected = BD.SetSQL(ArrParametros1, sSelect, "ControlGas");

                if (DataAfected > 0)
                {

                    data.Message = "SUCCESSFUL UPGRADE ODOMETER";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED UPGRADE ODOMETER";
                    data.Status = "ERROR";
                }
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar el odometro en control gas", "WsUpdateOdometro");
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }



        }

        // Este metodo es utilizado para obtener la configuracion guardada en la tabla configuracion
        [HttpGet]
        public JsonResult GetConfiguracion(string ID = "0")
        {
            List<Configuracion> lstConfiguracion = new List<Configuracion>();
            DataTable dT = new DataTable();
            DataTable dTRol = new DataTable();
            DataTable dTUser = new DataTable();
            DataResponse data = new DataResponse();
            Configuracion ObjConfiguracion = new Configuracion();
            try
            {
                string sSelect = "";
                string SelectManager = "";
                string SelectUser = "";
                int IDRol = 0;
                if (ID != "0")
                {
                    sSelect = "SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaConfiguracion + "] where ID=@ID";
                }
                else
                {
                    sSelect = "SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaConfiguracion + "]";
                }
                var ArrParametros1 = new List<SqlParameter>();
                ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = ID });
                dT = BD.GetSQL(sSelect, "Local", ArrParametros1);
                var columnas = dT.Columns;
                //return Json(dT.Rows, JsonRequestBehavior.AllowGet);
                foreach (DataRow dR1 in dT.Rows)
                {
                    ObjConfiguracion.ID = dR1["ID"].ToString();
                    ObjConfiguracion.IPSERVIDOR = dR1["IPSERVIDOR"].ToString();
                    ObjConfiguracion.IPCONTROLGAS1 = dR1["IPCONTROLGAS1"].ToString();
                    ObjConfiguracion.IPCONTROLGAS2 = dR1["IPCONTROLGAS2"].ToString();
                    ObjConfiguracion.IPPLC = dR1["IPPLC"].ToString();
                    ObjConfiguracion.REPORTESPOR = dR1["REPORTESPOR"].ToString();
                    ObjConfiguracion.DESTINATARIOS = dR1["DESTINATARIOS"].ToString();
                    ObjConfiguracion.USER_ACOUNT = dR1["USER_ACOUNT"].ToString();
                    ObjConfiguracion.PASSWORD = dR1["PASSWORD"].ToString();
                    ObjConfiguracion.SERVER_SMTP = dR1["SERVER_SMTP"].ToString();
                    ObjConfiguracion.PORT = dR1["PORT"].ToString();
                    //*******************----------------------------------------------------------------*******************
                    ObjConfiguracion.FECHA = Convert.ToDateTime(dR1["FECHA"]).ToString("dd/MM/yyyy");
                    SelectManager = "select ID,Rol from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaRol + "] where Rol='SYSTEM MANAGER'";
                    var ArrParametros2 = new List<SqlParameter>();
                    dTRol = BD.GetSQL(SelectManager, "Local", ArrParametros2);
                    foreach (DataRow Rol in dTRol.Rows)
                    {
                        IDRol = Convert.ToInt32(Rol["ID"]);
                        break;
                    }
                    if (IDRol != 0)
                    {
                        var ArrParametros3 = new List<SqlParameter>();
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@IdRol", Value = IDRol });
                        SelectUser = "select Nombre,Telefono,Correo from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "]where IdRol=@IdRol";
                        dTUser = BD.GetSQL(SelectUser, "Local", ArrParametros3);
                        foreach (DataRow User in dTUser.Rows)
                        {
                            ObjConfiguracion.MANAGER = User["Nombre"].ToString();
                            ObjConfiguracion.TELEFONO = User["Telefono"].ToString();
                            ObjConfiguracion.CORREO = User["Correo"].ToString();
                            break;
                        }
                    }
                    else
                    {
                        ObjConfiguracion.MANAGER = "N/A";
                        ObjConfiguracion.TELEFONO = "N/A";
                        ObjConfiguracion.CORREO = "N/A";
                    }
                    lstConfiguracion.Add(ObjConfiguracion);
                }
                if (lstConfiguracion.Count > 0)
                {
                    data.lstConfiguracion = lstConfiguracion;
                    data.Message = "SUCCESSFUL CONFIGURACION";
                    data.Status = "OK";
                }
                else
                {
                    data.lstConfiguracion = lstConfiguracion;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Obtener los datos de la configuracion", "GetConfiguracion");
                //EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Obtener los datos de la configuracion", "Ha ocurrido un error en la consulta al Obtener los datos de la configuracion", "", "", "", "");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public string ValidaVolumenVehiculo(string Tag = "0", decimal Vol = 0)
        {
            if (Tag == "0")
            {
                return "Indique el identificador del vehículo";
            }
            if (Vol == 0)
            {
                return "Indique el volumen a despachar para el vehículo";
            }
            string sSelect = "";
            System.Data.DataTable dT;

            sSelect = "select * from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] where tag  =@Tag";

            var ArrParametros1 = new List<SqlParameter>();

            ArrParametros1.Add(new SqlParameter { ParameterName = "@Tag", Value = Tag });

            dT = BD.GetSQL(sSelect, "ControlGas", ArrParametros1);

            if (dT.Rows.Count > 0)
            {
                DataRow dR = dT.Rows[0];
                if (Convert.ToDecimal(dR["carmax"]) < Vol && Convert.ToDecimal(dR["carmax"]) > 0)
                {
                    return "El volumen solicitado excede la carga máxima autorizada para el vehículo";
                }
                if (Convert.ToDecimal(dR["acudia"]) + Vol > Convert.ToDecimal(dR["candia"]) && Convert.ToDecimal(dR["candia"]) > 0)
                {
                    return "El vehículo excede la carga máxima por día";
                }
                if (Convert.ToDecimal(dR["acusem"]) + Vol > Convert.ToDecimal(dR["cansem"]) && Convert.ToDecimal(dR["cansem"]) > 0)
                {
                    return "El vehículo excede la carga máxima por semana";
                }
                if (Convert.ToDecimal(dR["acumes"]) + Vol > Convert.ToDecimal(dR["canmes"]) && Convert.ToDecimal(dR["canmes"]) > 0)
                {
                    return "El vehículo excede la carga máxima por mes";
                }

                return "true";
            }
            else
            {
                return "Vehículo no localizado";
            }
        }

        //juan
        // Este metodo es utilizado para actualizar un  DESPACHO en la base de datos de tank farm
        [HttpPost]
        public JsonResult WsUpdateDespachoControlGas(Despacho despacho)
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {
                string sSelect = "";
                sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "]  UPDATE [dbo].[Despachos] SET " +
                    "[nrobom] = @nrobom  ,[nrotur] = @nrotur ,[codprd] = @codprd ,[codcli] = @codcli ,[nroveh] = @nroveh ," +
                    "[tar] = @tar ,[odm] = @odm ,[codisl] = @codisl ,[nrocte] = @nrocte ,[fchcte] = @fchcte ,[mtogto] = @mtogto ," +
                    "[rut] = @rut ,[cho] = @cho ,[pto] = @pto ,[codres] = @codres ,[graprd] = @graprd ,[nroarc] = @nroarc ," +
                    "[nrofac] = @nrofac ,[gasfac] = @gasfac ,[nroedc] = @nroedc ,[chkedc] = @chkedc ,[pre] = @pre ,[niv] = @niv ," +
                    "[nrocho] = @nrocho ,[tiptrn] = @tiptrn ,[logmsk] = @logmsk ,[logusu] = @logusu ,[logexp] = '' ,[datref] = @datref ," +
                    "[satuid] = @satuid ,[satrfc] = @satrfc WHERE nrotrn=@nrotrn";
                var ArrParametros1 = new List<SqlParameter>();
                ArrParametros1.Add(new SqlParameter { ParameterName = "@nrotrn", Value = despacho.nrotrn });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@nrobom", Value = despacho.nrobom });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@nrotur", Value = despacho.nrotur });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@codprd", Value = despacho.codprd });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@codcli", Value = despacho.codcli });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@nroveh", Value = despacho.nroveh });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@tar", Value = despacho.tar });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@odm", Value = despacho.odm });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@codisl", Value = despacho.codisl });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@nrocte", Value = despacho.nrocte });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@fchcte", Value = despacho.fchcte });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@mtogto", Value = despacho.mtogto });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@rut", Value = despacho.rut });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@cho", Value = despacho.cho });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@pto", Value = despacho.pto });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@codres", Value = despacho.codres });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@graprd", Value = despacho.graprd });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@nroarc", Value = despacho.nroarc });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@nrofac", Value = despacho.nrofac });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@gasfac", Value = despacho.gasfac });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@nroedc", Value = despacho.nroedc });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@chkedc", Value = despacho.chkedc });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@pre", Value = despacho.pre });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@niv", Value = despacho.niv });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@nrocho", Value = despacho.nrocho });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@tiptrn", Value = despacho.tiptrn });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@logmsk", Value = despacho.logmsk });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@logusu", Value = despacho.logusu });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@datref", Value = despacho.datref });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@satuid", Value = despacho.satuid });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@satrfc", Value = despacho.satrfc });

                DataAfected = BD.SetSQL(ArrParametros1, sSelect, "ControlGas");
                if (DataAfected > 0)
                {

                    data.Message = "SUCCESSFUL INSERTION";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED INSERTION OF DESPACHOS";
                    data.Status = "ERROR";
                }
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar un depacho en control gas", "WsUpdateDespachoControlGas");
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }



        }

        public string RemoveDiacritics(string input)
        {
            string stFormD = input.Normalize(NormalizationForm.FormD);
            int len = stFormD.Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[i]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[i]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static double ToDouble(string Value)
        {
            if (Value == null)
            {
                return 0;
            }
            else
            {
                double OutVal;
                double.TryParse(Value, out OutVal);

                if (double.IsNaN(OutVal) || double.IsInfinity(OutVal))
                {
                    return 0;
                }
                return OutVal;
            }
        }

        public static void AgregaLog(string Valor, string Metodo)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = "C:\\Logs\\";
            try
            {
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
            }
            catch (Exception ex)
            {
                // handle them here
            }
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("dd-MM-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(System.DateTime.Today.ToString("[dd-MM-yyyy HH:mm]") + "-" + "METODO: " + Metodo + "-- Desc: " + Valor);
            log.Close();
        }


    }
}