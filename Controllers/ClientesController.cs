using DocumentFormat.OpenXml.Spreadsheet;
using MailKit.Security;
using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SpreadsheetLight;
using SpreadsheetLight.Drawing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace WSTankFarm.Controllers
{
    [Authorize]
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        BDD BD = new BDD();
        



        //juan
        // Este metodo es utilizado para actualizar un  DESPACHO en la base de datos de tank farm
        
        [HttpPost] [Route("WsUpdateDespachoControlGas")]
        public IHttpActionResult WsUpdateDespachoControlGas(Despacho despacho)
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
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar un depacho en control gas", "WsUpdateDespachoControlGas");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar un deapacho en control gas", "Ha ocurrido un error en la consulta al actualizar un despacho en control gas", "", "", "", "");
                return Ok(mensaje);
            }



        }
        [HttpGet]
        [Route("GetDespachosFiltros")]
        public IHttpActionResult  GetDespachosFiltros(string LastConsum = "", string Producto = "", string Estacion = "", string Company = "", string DriverTag = "", string VehicleTag = "", string Department = "", string Cost = "", string StartDate = "", string EndDate = "", string EST = "")
        {
            List<Despacho> lstDespachos = new List<Despacho>();
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            List<double> ArrayMagna = new List<double>();
            List<double> ArrayDieselTF = new List<double>();
            List<string> ArrayDieselTGF = new List<string>();
            List<string> ArrayGasolinaTGF = new List<string>();
            List<string> ArrayFechasTF = new List<string>();
            List<string> ArrayFechasGasTGF = new List<string>();
            List<string> ArrayFechasDieselTGF = new List<string>();
            List<int> ArrayTrnTGF = new List<int>();
            List<int> ArrayTrnTF = new List<int>();
            List<string> ArrayFechasGeneral = new List<string>();
            Despacho ObjDespacho = new Despacho();
            try
            {
                string sSelect = "SELECT top 2000 [xHoraDespacho],[xTagVehiculo] ,[Departamento], [CentroCostos],[xResposableVehiculo] ,[xMarca] ,[xModelo] ,[xNombreChofer] ,[xTagChofer] ,[xGerente] ,[nrotrn] ,[codgas] ,[fchtrn] ,[hratrn] ," +
                    "[fchcor] ,[nrotur] ,[codisl] ,[nrobom] ,[graprd] ,[codprd] ,[codcli] ,[nroveh] ,[tar] ,[odm] ,[rut] ,[nrocho] ,[cho] ,[codres] ,[tiptrn] ,[nrocte] ,[fchcte] ,[nrofac] ,[gasfac] ,[nroedc] ,[chkedc] ,[nroarc] ,[pto] ,[pre] ," +
                    "[can] ,[mto] ,[mtogto] ,[niv] ,[xEstacion] ,[xFecha] ,[xFechaCorte] ,[xTurno] ,[xCorte] ,[xDespacho] ,[xProducto] ,[xProductoUni] ,[xCliente] ,[xChofer] ,[xDespachador] ,[xPlacas] ,[datref] ,[satrfc] ,[satuid] FROM" +
                    " [" + GlobalesLocal.BDLocal + "].[dbo].[VDespachosAll]";
                var ArrParametros1 = new List<SqlParameter>();

                string condicion = "";
                if (LastConsum != "" && LastConsum != "0")
                {
                    condicion += "xFecha>=DATEADD(DAY,-@LastConsum,GETDATE())";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@LastConsum", Value = LastConsum });
                }





                if (Producto != "" && Producto != "0")
                {
                    if (condicion.Length > 0)
                    {
                        condicion += "and ";
                    }
                    condicion += "rtrim(ltrim(xProducto))=@Producto";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Producto", Value = Producto });
                }


                if (Estacion != "" && Estacion != "0")
                {
                    if (condicion.Length > 0)
                    {
                        condicion += "and ";
                    }
                    condicion += "codgas=@Estacion";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Estacion", Value = Estacion });
                }

                if (Company != "" && Company != "0")
                {
                    if (condicion.Length > 0)
                    {
                        condicion += "and ";
                    }
                    condicion += "xCliente=@Company";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Company", Value = Company });
                }

                if (DriverTag != "" && DriverTag != "0")
                {
                    if (condicion.Length > 0)
                    {
                        condicion += "and ";
                    }
                    condicion += "xTagChofer=@DriverTag";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@DriverTag", Value = DriverTag });
                }

                if (VehicleTag != "" && VehicleTag != "0")
                {
                    if (condicion.Length > 0)
                    {
                        condicion += "and ";
                    }
                    condicion += "xTagVehiculo=@VehicleTag";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@VehicleTag", Value = VehicleTag });

                }

                if (Department != "" && Department != "0")
                {
                    if (condicion.Length > 0)
                    {
                        condicion += "and ";
                    }
                    condicion += "Departamento=@Department";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Department", Value = Department });

                }

                if (Cost != "" && Cost != "0")
                {
                    if (condicion.Length > 0)
                    {
                        condicion += "and ";
                    }
                    condicion += "CentroCostos=@Cost";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Cost", Value = Cost });

                }

                if (StartDate != "" && StartDate != "0" && EndDate != "" && EndDate != "0")
                {
                    if (condicion.Length > 0)
                    {
                        condicion += "and ";
                    }
                    condicion += "xFecha >= CONVERT(datetime,@StartDate,121) and xFecha <=CONVERT(datetime,@EndDate,121)";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@StartDate", Value = StartDate });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@EndDate", Value = EndDate });

                }

                if (condicion.Length > 0)
                {
                    sSelect += " WHERE " + condicion + "";
                }


                sSelect += "ORDER BY  nrotrn desc ";

                if (sSelect != "")
                {
                    dT = BD.GetSQL(sSelect, "ControlGas", ArrParametros1);
                    var columnas = dT.Columns;

                    foreach (DataRow dR1 in dT.Rows)
                    {
                        ObjDespacho = new Despacho();

                        ObjDespacho.xTagVehiculo = dR1["xTagVehiculo"].ToString();
                        ObjDespacho.xDepartamento = dR1["Departamento"].ToString();
                        //ObjDespacho.xResposableVehiculo = dR1["xResposableVehiculo"].ToString();
                        //ObjDespacho.xMarca = dR1["xMarca"].ToString();
                        //ObjDespacho.xModelo = dR1["xModelo"].ToString();
                        ObjDespacho.xNombreChofer = dR1["xNombreChofer"].ToString();
                        ObjDespacho.xTagChofer = dR1["xTagChofer"].ToString();
                        ObjDespacho.xGerente = dR1["xGerente"].ToString();

                        //ObjDespacho.nrotrn = Convert.ToInt32(dR1["nrotrn"].ToString());
                        ObjDespacho.codgas = Convert.ToInt32(dR1["codgas"].ToString());
                        ObjDespacho.fchtrn = Convert.ToInt32(dR1["fchtrn"].ToString());
                        ObjDespacho.hratrn = Convert.ToInt32(dR1["hratrn"].ToString());
                        //ObjDespacho.fchcor = Convert.ToInt32(dR1["fchcor"].ToString());
                        //ObjDespacho.nrotur = Convert.ToInt32(dR1["nrotur"].ToString());
                        //ObjDespacho.codisl = Convert.ToInt32(dR1["codisl"].ToString());
                        //ObjDespacho.nrobom = Convert.ToInt32(dR1["nrobom"].ToString());
                        //ObjDespacho.graprd = Convert.ToInt32(dR1["graprd"].ToString());
                        ObjDespacho.codprd = Convert.ToInt32(dR1["codprd"].ToString());
                        ObjDespacho.codcli = Convert.ToInt32(dR1["codcli"].ToString());
                        //ObjDespacho.nroveh = Convert.ToInt32(dR1["nroveh"].ToString());
                        //ObjDespacho.tar = Convert.ToInt32(dR1["tar"].ToString());
                        //ObjDespacho.odm = Convert.ToInt32(dR1["odm"].ToString());
                        //ObjDespacho.rut = dR1["rut"].ToString();
                        // ObjDespacho.nrocho = Convert.ToInt32(dR1["nrocho"].ToString());
                        //ObjDespacho.cho = dR1["cho"].ToString();
                        //ObjDespacho.codres = Convert.ToInt32(dR1["codres"].ToString());
                        //ObjDespacho.tiptrn = Convert.ToInt32(dR1["tiptrn"].ToString());
                        //ObjDespacho.nrocte = Convert.ToInt32(dR1["nrocte"].ToString());
                        //ObjDespacho.fchcte = Convert.ToInt32(dR1["fchcte"].ToString());
                        //ObjDespacho.nrofac = Convert.ToInt32(dR1["nrofac"].ToString());
                        //ObjDespacho.gasfac = Convert.ToInt32(dR1["gasfac"].ToString());
                        //ObjDespacho.nroedc = Convert.ToInt32(dR1["nroedc"].ToString());
                        //ObjDespacho.chkedc = Convert.ToInt32(dR1["chkedc"].ToString());
                        //ObjDespacho.nroarc = Convert.ToInt32(dR1["nroarc"].ToString());
                        //ObjDespacho.pto = ToDouble(dR1["pto"].ToString());
                        //ObjDespacho.pre = ToDouble(dR1["pre"].ToString());
                        ObjDespacho.can = ToDouble(dR1["can"].ToString());
                        //ObjDespacho.mto = ToDouble(dR1["mto"].ToString());
                        //ObjDespacho.mtogto = ToDouble(dR1["mtogto"].ToString());
                        //ObjDespacho.niv = Convert.ToInt32(dR1["niv"].ToString());
                        ObjDespacho.xEstacion = dR1["xEstacion"].ToString();

                        DateTime fechaG = Convert.ToDateTime(dR1["xFecha"]);
                        var datevalueG = fechaG.ToString("yyyy-MM-dd");

                        ObjDespacho.xFecha = datevalueG;
                        ObjDespacho.xHoraDespacho = dR1["xHoraDespacho"].ToString();
                        //ObjDespacho.xFechaCorte = dR1["xFechaCorte"].ToString();
                        //ObjDespacho.xTurno = dR1["xTurno"].ToString();
                        //ObjDespacho.xCorte = dR1["xCorte"].ToString();
                        //ObjDespacho.xDespacho = dR1["xDespacho"].ToString();
                        ObjDespacho.xProducto = dR1["xProducto"].ToString();
                        //ObjDespacho.xProductoUni = dR1["xProductoUni"].ToString();
                        ObjDespacho.xCliente = dR1["xCliente"].ToString();
                        ObjDespacho.xChofer = dR1["xChofer"].ToString();
                        //ObjDespacho.xDespachador = dR1["xDespachador"].ToString();
                        ObjDespacho.xPlacas = dR1["xPlacas"].ToString();
                        //ObjDespacho.datref = dR1["datref"].ToString();
                        //ObjDespacho.satrfc = dR1["satrfc"].ToString();
                        //ObjDespacho.satuid = dR1["satuid"].ToString();
                        ObjDespacho.xCentroCostos = dR1["CentroCostos"].ToString();


                        DateTime fecha = Convert.ToDateTime(dR1["xFecha"]);
                        var datevalue = fecha.ToString("yyyy-MM-dd");

                        //DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                        //String dy = datevalue.Day.ToString();
                        //String mn = datevalue.Month.ToString();
                        //String yy = datevalue.Year.ToString();
                        //string strMonthName = dy + "-" + mfi.GetMonthName(Convert.ToInt32(mn)).ToString().Substring(0, 3) + "-" + yy;



                        if (dR1["xProducto"].ToString() != "" && dR1["xProducto"].ToString() != null)
                        {
                            if (dR1["xProducto"].ToString().Replace(" ", "").Trim() == "Diesel")
                            {
                                if (EST == "TF")
                                {
                                    ArrayDieselTF.Add(ObjDespacho.can);
                                    ArrayFechasTF.Add(datevalue);
                                    //ArrayTrnTF.Add(ObjDespacho.nrotrn);
                                    lstDespachos.Add(ObjDespacho);
                                }


                            }
                        }

                        if (dR1["xProducto"].ToString().Replace(" ", "").Trim() == "Diesel")
                        {
                            if (EST == "TGF")
                            {
                                ArrayDieselTGF.Add(ObjDespacho.can.ToString());
                                ArrayFechasDieselTGF.Add(datevalue);
                                //ArrayTrnTGF.Add(ObjDespacho.nrotrn);
                                //ArrayFechasGeneral.Add(datevalue);
                                lstDespachos.Add(ObjDespacho);


                            }


                        }
                        if (dR1["xProducto"].ToString().Replace(" ", "").Trim() == "Regular")
                        {
                            if (EST == "TGF")
                            {

                                ArrayGasolinaTGF.Add(ObjDespacho.can.ToString());
                                ArrayFechasGasTGF.Add(datevalue);
                                //ArrayTrnTGF.Add(ObjDespacho.nrotrn);
                                ///ArrayFechasGeneral.Add(datevalue);
                                lstDespachos.Add(ObjDespacho);


                            }

                        }




                        ObjDespacho.ArrayGasolinaTGF = ArrayGasolinaTGF;
                        ObjDespacho.ArrayDieselTF = ArrayDieselTF;
                        ObjDespacho.ArrayDieselTGF = ArrayDieselTGF;
                        ObjDespacho.ArrayFechasTF = ArrayFechasTF;
                        ObjDespacho.ArrayFechasGasTGF = ArrayFechasGasTGF;
                        ObjDespacho.ArrayFechasDieselTGF = ArrayFechasDieselTGF;
                        //ObjDespacho.ArrayTrnTGF = ArrayTrnTGF;
                        //ObjDespacho.ArrayTrnTF = ArrayTrnTF;
                        //ObjDespacho.ArrayFechasGeneral = ArrayFechasGeneral;

                    }





                }


                if (lstDespachos.Count > 0)
                {
                    data.lstDespachos = lstDespachos;
                    data.Message = "Despachos";
                    data.Status = "OK";
                }
                else
                {
                    data.lstDespachos = lstDespachos;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                var json = Ok(data);
                
                return json;



            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Error en la consulta no se pueden obtener los Despachos", "GetDespachosFiltros");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Error en la consulta no se pueden obtener los despachos de la vista", "Error en la consulta no se pueden obtener los despachos de la vista", "", "", "", "");
                return Ok(mensaje);
            }



        }
        //juan
        //// Este metodo es utilizado para insertar un nuevo DESPACHO en la base de datos de tank farm
        [HttpPost] [Route("WsInsertarDespachoControlGas")]
        public IHttpActionResult  WsInsertarDespachoControlGas(Despacho despacho)
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {
                string sSelect = "";
                sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] " + "INSERT INTO [dbo].[Despachos] ([nrotrn] ,[codgas] ,[nrobom] ,[fchtrn] ,[hratrn] ,[fchcor] ,[nrotur] ,[codprd] ,[can] ,[mto] ,[codcli] ,[nroveh] ," +
                    "[tar] ,[odm] ,[codisl] ,[nrocte] ,[fchcte] ,[mtogto] ,[rut] ,[cho] ,[pto] ,[codres] ,[graprd] ,[nroarc] ,[nrofac] ,[gasfac] ,[nroedc] ,[chkedc] ,[pre] ,[niv] ,[nrocho] ,[tiptrn] ,[logmsk] ,[logusu] ,[lognew] ," +
                    "[logfch] ,[logexp] ,[datref] ,[satuid] ,[satrfc]) VALUES ( (select isnull(max(nrotrn),0)+1 from Despachos), @codgas , @nrobom , convert(int,getdate()), " +
                    "REPLACE(CONVERT(NVARCHAR(5),CONVERT(TIME,GETDATE())),':',''), convert(int,getdate()), @nrotur , @codprd , @can , @mto , " +
                    "@codcli , @nroveh , @tar , @odm , @codisl , @nrocte , @fchcte , " +
                    "@mtogto , @rut , @cho , @pto , @codres , @graprd , @nroarc , " +
                    "@nrofac , @gasfac , @nroedc , @chkedc , @pre , @niv , @nrocho , @tiptrn , " +
                    "@logmsk , @logusu , GETDATE(), GETDATE(), '', @datref , @satuid , @satrfc )";
                var ArrParametros1 = new List<SqlParameter>();
                ArrParametros1.Add(new SqlParameter { ParameterName = "@codgas", Value = despacho.codgas });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@nrobom", Value = despacho.nrobom });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@nrotur", Value = despacho.nrotur });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@codprd", Value = despacho.codprd });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@can", Value = despacho.can });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@mto", Value = despacho.mto });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@codcli", Value = despacho.codcli });
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
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Insertar un deapacho en control gas", "WsInsertarDespachoControlGas");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Insertar un deapacho en control gas", "Ha ocurrido un error en la consulta al Insertar un deapacho en control gas", "", "", "", "");
                return Ok(mensaje);
            }



        }

        //juan
        // Este metodo es utilizado para insertar un nuevo PERFIL en la base de datos de tank farm
        [HttpPost] [Route("WsInsertarPerfil")]
        public IHttpActionResult  WsInsertarPerfil(Perfiles perfiles)
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {

                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {

                    string sSelect = "";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(ID+1) AS id " +
                        "FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "])IF @MAXID >0 SELECT" +
                        " @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "])  " +
                        " ELSE set @MAXID=1 " + "INSERT INTO [dbo].[" + GlobalesLocal.TablaPerfiles + "] (ID,[namePerfil] ," +
                        "[codcli], [Unlimited],[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[codgas] ,[codprd] ,[fecha] ," +
                        "[status] ,[tipoPerfil],[Departamentos],[Odometro]) VALUES (@MAXID, @namePerfil , @codcli ," +
                        " @Unlimited , @diacar , @hraini ,  @hrafin , " +
                        "@carmax , @candia , @cansem , @canmes , " +
                        "@codgas , @codprd , GETDATE(), 0, @tipoPerfil ," +
                        " @Departamentos , @Odometro ) ";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@namePerfil", Value = perfiles.namePerfil ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codcli", Value = perfiles.codcli ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Unlimited", Value = perfiles.Unlimited ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diacar", Value = perfiles.diacar ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@hraini", Value = perfiles.hraini ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@hrafin", Value = perfiles.hrafin ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@carmax", Value = perfiles.carmax ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@candia", Value = perfiles.candia ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cansem", Value = perfiles.cansem ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@canmes", Value = perfiles.canmes ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codgas", Value = perfiles.codgas ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codprd", Value = perfiles.codprd ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tipoPerfil", Value = perfiles.tipoPerfil ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Departamentos", Value = perfiles.Departamentos ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Odometro", Value = perfiles.Odometro ?? (object)DBNull.Value });

                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local", perfiles, "WsInsertarPerfil");
                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF PERFILES";
                        data.Status = "ERROR";
                    }



                }





                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {
                    string sSelect = "";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(ID+1) AS id " +
                        "FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "])IF @MAXID >0 SELECT" +
                        " @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "])  " +
                        " ELSE set @MAXID=1 " + "INSERT INTO [dbo].[" + GlobalesLocal.TablaPerfiles + "] (ID,[namePerfil] ," +
                        "[codcli], [Unlimited],[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[codgas] ,[codprd] ,[fecha] ," +
                        "[status] ,[tipoPerfil],[Departamentos],[Odometro]) VALUES (@MAXID, @namePerfil , @codcli ," +
                        " @Unlimited , @diacar , @hraini ,  @hrafin , " +
                        "@carmax , @candia , @cansem , @canmes , " +
                        "@codgas , @codprd , GETDATE(), 0, @tipoPerfil ," +
                        " @Departamentos , @Odometro ) ";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@namePerfil", Value = perfiles.namePerfil ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codcli", Value = perfiles.codcli ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Unlimited", Value = perfiles.Unlimited ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diacar", Value = perfiles.diacar ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@hraini", Value = perfiles.hraini ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@hrafin", Value = perfiles.hrafin ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@carmax", Value = perfiles.carmax ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@candia", Value = perfiles.candia ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cansem", Value = perfiles.cansem ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@canmes", Value = perfiles.canmes ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codgas", Value = perfiles.codgas ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codprd", Value = perfiles.codprd ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tipoPerfil", Value = perfiles.tipoPerfil ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Departamentos", Value = perfiles.Departamentos ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Odometro", Value = perfiles.Odometro ?? (object)DBNull.Value });
                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local");
                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF PERFILES";
                        data.Status = "ERROR";
                    }




                }



                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Insertar un perfil en la base de datos local", "WsInsertarPerfil");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Insertar un perfil en la base de datos local", "Ha ocurrido un error en la consulta al Insertar un perfil en la base de datos local", "", "", "", "");
                return Ok(mensaje);
            }



        }



        [HttpGet]
        [Route("GetPerfiles")]
        public IHttpActionResult  GetPerfiles(string id = "0", string all = "", string TipoPerfil = "")
        {
            System.Data.DataTable s = new System.Data.DataTable();
            List<Perfiles> lstPerfiles = new List<Perfiles>();
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            try
            {
                string sSelect = "";
                var ArrParametros1 = new List<SqlParameter>();


                if (id != "0")
                {
                    sSelect = "SELECT ID,[namePerfil] ,[codcli] ,[Unlimited] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ," +
                        "[codgas] ,[codprd] ,[fecha] ,[status] ,[tipoPerfil] ,Clientes.den as xNombreCliente,Gasolineras.den as xProducto ," +
                        "Productos.den xEstacion,Departamentos,Odometro FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "]" +
                        " LEFT JOIN 	[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]	ON	" +
                        "Clientes.cod = Perfiles.codcli LEFT JOIN	[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Gasolineras	ON	" +
                        "Gasolineras.cod = Perfiles.codgas LEFT JOIN	[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Productos ON	" +
                        "Productos.cod = Perfiles.codprd where  status=0 and  and  ID=@ID order by namePerfil asc";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = id });
                }
                else
                {
                    if (TipoPerfil != "" && all == "undefined")
                    {
                        if (TipoPerfil == "VEHICLE")
                        {
                            sSelect = "SELECT ID,[namePerfil] ,[codcli] ,[Unlimited] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes]" +
                                " ,[codgas] ,[codprd] ,[fecha] ,[status] ,[tipoPerfil] ,Clientes.den as xNombreCliente,Gasolineras.den as xProducto ," +
                                "Productos.den xEstacion,Departamentos,Odometro FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "] " +
                                "LEFT JOIN 	[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Clientes	ON	Clientes.cod = Perfiles.codcli LEFT JOIN" +
                                "	[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Gasolineras	ON	Gasolineras.cod = Perfiles.codgas LEFT JOIN	" +
                                "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Productos ON	Productos.cod = Perfiles.codprd where status=0 and" +
                                " tipoPerfil=@TipoPerfil OR tipoPerfil='VEHICLE-PRODUCTION' and status=0 order by namePerfil asc";
                            ArrParametros1.Add(new SqlParameter { ParameterName = "@TipoPerfil", Value = TipoPerfil });
                        }
                        else
                        {
                            sSelect = "SELECT ID,[namePerfil] ,[codcli] ,[Unlimited] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[codgas] ,[codprd] ," +
                                "[fecha] ,[status] ,[tipoPerfil] ,Clientes.den as xNombreCliente,Gasolineras.den as xProducto ,Productos.den xEstacion,Departamentos,Odometro FROM " +
                                "[" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "] LEFT JOIN 	" +
                                "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Clientes	ON	Clientes.cod = Perfiles.codcli LEFT JOIN	" +
                                "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Gasolineras	ON	Gasolineras.cod = Perfiles.codgas LEFT JOIN	" +
                                "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Productos ON	Productos.cod = Perfiles.codprd where  " +
                                "tipoPerfil=@TipoPerfil and status=0  order by namePerfil asc";
                            ArrParametros1.Add(new SqlParameter { ParameterName = "@TipoPerfil", Value = TipoPerfil });
                        }

                    }
                    else
                    {
                        if (all == "enabled")
                        {
                            sSelect = "SELECT ID,[namePerfil] ,[codcli] ,[Unlimited] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] " +
                                ",[codgas] ,[codprd] ,[fecha] ,[status] ,[tipoPerfil] ,Clientes.den as xNombreCliente,Gasolineras.den as xProducto ,Productos.den xEstacion" +
                                ",Departamentos,Odometro FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "] LEFT JOIN " +
                                "	[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Clientes	ON	Clientes.cod = Perfiles.codcli LEFT JOIN" +
                                "	[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Gasolineras	ON	Gasolineras.cod = Perfiles.codgas LEFT JOIN" +
                                "	[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Productos ON	Productos.cod = Perfiles.codprd  where   status=0 order by " +
                                "namePerfil asc";
                        }
                        else if (all == "all")
                        {
                            if (TipoPerfil == "VEHICLE")
                            {
                                sSelect = "SELECT ID,[namePerfil] ,[codcli] ,[Unlimited] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[codgas] ," +
                                    "[codprd] ,[fecha] ,[status] ,[tipoPerfil] ,Clientes.den as xNombreCliente,Gasolineras.den as xProducto ,Productos.den xEstacion,Departamentos," +
                                    "Odometro FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "] LEFT JOIN 	" +
                                    "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Clientes	ON	Clientes.cod = Perfiles.codcli LEFT JOIN	" +
                                    "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Gasolineras	ON	Gasolineras.cod = Perfiles.codgas LEFT JOIN	" +
                                    "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Productos	ON	Productos.cod = Perfiles.codprd where  " +
                                    " tipoPerfil=@TipoPerfil OR tipoPerfil='VEHICLE-PRODUCTION' order by namePerfil asc";
                                ArrParametros1.Add(new SqlParameter { ParameterName = "@TipoPerfil", Value = TipoPerfil });
                            }
                            else
                            {
                                sSelect = "SELECT ID,[namePerfil] ,[codcli] ,[Unlimited] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[codgas] ," +
                                    "[codprd] ,[fecha] ,[status] ,[tipoPerfil] ,Clientes.den as xNombreCliente,Gasolineras.den as xProducto ,Productos.den xEstacion," +
                                    "Departamentos,Odometro FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaPerfiles + "] LEFT JOIN " +
                                    "	[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Clientes	ON	Clientes.cod = Perfiles.codcli LEFT JOIN" +
                                    "	[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Gasolineras	ON	Gasolineras.cod = Perfiles.codgas LEFT JOIN	" +
                                    "[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].Productos	ON	Productos.cod = Perfiles.codprd where  " +
                                    "tipoPerfil=@TipoPerfil order by namePerfil asc";
                                ArrParametros1.Add(new SqlParameter { ParameterName = "@TipoPerfil", Value = TipoPerfil });
                            }


                        }
                    }

                }






                dT = BD.GetSQL(sSelect, "Local", ArrParametros1);
                var columnas = dT.Columns;
                //return Ok(dT.Rows);
                foreach (DataRow dR1 in dT.Rows)
                {

                    Perfiles ObjPerfiles = new Perfiles();
                    ObjPerfiles.ID = Convert.ToInt32(dR1["ID"].ToString());
                    ObjPerfiles.namePerfil = dR1["namePerfil"].ToString();
                    ObjPerfiles.codcli = dR1["codcli"].ToString();
                    ObjPerfiles.Unlimited = dR1["Unlimited"].ToString();
                    ObjPerfiles.diacar = dR1["diacar"].ToString();
                    ObjPerfiles.hraini = dR1["hraini"].ToString();
                    ObjPerfiles.hrafin = dR1["hrafin"].ToString();
                    ObjPerfiles.carmax = dR1["carmax"].ToString();
                    ObjPerfiles.candia = dR1["candia"].ToString();
                    ObjPerfiles.cansem = dR1["cansem"].ToString();
                    ObjPerfiles.canmes = dR1["canmes"].ToString();

                    ObjPerfiles.codgas = dR1["codgas"].ToString();
                    ObjPerfiles.codprd = dR1["codprd"].ToString();
                    ObjPerfiles.fecha = dR1["fecha"].ToString();
                    ObjPerfiles.status = dR1["status"].ToString();
                    ObjPerfiles.tipoPerfil = dR1["tipoPerfil"].ToString();

                    ObjPerfiles.xNombreCliente = dR1["xNombreCliente"].ToString();
                    ObjPerfiles.xProducto = dR1["xProducto"].ToString();
                    ObjPerfiles.xEstacion = dR1["xEstacion"].ToString();
                    ObjPerfiles.Departamentos = dR1["Departamentos"].ToString();
                    ObjPerfiles.Odometro = dR1["Odometro"].ToString();

                    var depa = ObjPerfiles.Departamentos.Split(',');

                    for (int i = 0; i < depa.Length - 1; i++)
                    {
                        DataTable dTDepart = new DataTable();
                        string consultaDepartamento = "SELECT ID,Departamento FROM " +
                            "[" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaDepartamento + "] where ID = @ID";
                        var ArrParametros2 = new List<SqlParameter>();
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@ID", Value = depa[i] });

                        dTDepart = BD.GetSQL(consultaDepartamento, "Local", ArrParametros2);
                        foreach (DataRow perfiles in dTDepart.Rows)
                        {
                            ObjPerfiles.NombreDepartamento += perfiles["Departamento"].ToString() + ",";


                        }

                    }







                    lstPerfiles.Add(ObjPerfiles);
                }

                if (lstPerfiles.Count > 0)
                {
                    data.lstPerfiles = lstPerfiles;
                    data.Message = "PERFILES(S) FOUND";
                    data.Status = "OK";
                }
                else
                {
                    data.lstPerfiles = lstPerfiles;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al obtener los perfiles en la bd local", "GetPerfiles");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al obtener obtener los perfiles en la bd local", "Ha ocurrido un error en la consulta al obtener los perfiles en la bd local", "", "", "", "");
                return Ok(mensaje);
            }



        }
        //juan
        // Este metodo es utilizado para actualizar un  DESPACHO en la base de datos de tank farm
        [HttpPost] [Route("WsUpdatePerfil")]
        public IHttpActionResult  WsUpdatePerfil(Perfiles perfiles)
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {

                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {

                    string sSelect = "";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "]  UPDATE [dbo].[" + GlobalesLocal.TablaPerfiles + "] SET" +
                        " [namePerfil] = @namePerfil ,[codcli] = @codcli ,[Unlimited] = @Unlimited   ," +
                        "[diacar] = @diacar  ,[hraini] = @hraini  ,[hrafin] = @hrafin  ," +
                        "[carmax] = @carmax  ,[candia] = @candia  ,[cansem] = @cansem  ," +
                        "[canmes] = @canmes  ,[codgas] = @codgas  ,[codprd] = @codprd  ," +
                        "[fecha] = GETDATE(),[Departamentos]= @Departamentos ,[Odometro]= @Odometro  " +
                        "WHERE ID=@ID ";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@namePerfil", Value = perfiles.namePerfil ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codcli", Value = perfiles.codcli ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Unlimited", Value = perfiles.Unlimited ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diacar", Value = perfiles.diacar ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@hraini", Value = perfiles.hraini ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@hrafin", Value = perfiles.hrafin ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@carmax", Value = perfiles.carmax ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@candia", Value = perfiles.candia ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cansem", Value = perfiles.cansem ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@canmes", Value = perfiles.canmes ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codgas", Value = perfiles.codgas ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codprd", Value = perfiles.codprd ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Departamentos", Value = perfiles.Departamentos ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Odometro", Value = perfiles.Odometro ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = perfiles.ID });

                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local", perfiles, "WsUpdatePerfil");
                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF PERFILES";
                        data.Status = "ERROR";
                    }

                }

                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {

                    string sSelect = "";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "]  UPDATE [dbo].[" + GlobalesLocal.TablaPerfiles + "] SET" +
                        " [namePerfil] = @namePerfil ,[codcli] = @codcli ,[Unlimited] = @Unlimited   ," +
                        "[diacar] = @diacar  ,[hraini] = @hraini  ,[hrafin] = @hrafin  ," +
                        "[carmax] = @carmax  ,[candia] = @candia  ,[cansem] = @cansem  ," +
                        "[canmes] = @canmes  ,[codgas] = @codgas  ,[codprd] = @codprd  ," +
                        "[fecha] = GETDATE(),[Departamentos]= @Departamentos ,[Odometro]= @Odometro  " +
                        "WHERE ID=@ID ";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@namePerfil", Value = perfiles.namePerfil ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codcli", Value = perfiles.codcli ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Unlimited", Value = perfiles.Unlimited ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diacar", Value = perfiles.diacar ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@hraini", Value = perfiles.hraini ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@hrafin", Value = perfiles.hrafin ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@carmax", Value = perfiles.carmax ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@candia", Value = perfiles.candia ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cansem", Value = perfiles.cansem ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@canmes", Value = perfiles.canmes ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codgas", Value = perfiles.codgas ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codprd", Value = perfiles.codprd ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Departamentos", Value = perfiles.Departamentos ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Odometro", Value = perfiles.Odometro ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = perfiles.ID });
                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local");
                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF PERFILES";
                        data.Status = "ERROR";
                    }

                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar un PERFIL en la BD local", "WsUpdatePerfil");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar  un PERFIL en la BD local", "Ha ocurrido un error en la consulta al actualizar un PERFIL en la BD local", "", "", "", "");
                return Ok(mensaje);
            }



        }

        // Este metodo es utilizado para insertar un nuevo centro de costos  en la base de datos de tank farm
        //juan
        [HttpPost] [Route("WsInsertarCentroCostos")]
        public IHttpActionResult  WsInsertarCentroCostos(CentroCostos centroCostos)
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {

                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {

                    string sSelect = "";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(id+1) AS id " +
                        "FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaCentroCostos + "])IF @MAXID >0 " +
                        "SELECT @MAXID=(SELECT MAX(id+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaCentroCostos + "]) " +
                        "  ELSE set @MAXID=1 INSERT INTO [dbo].[" + GlobalesLocal.TablaCentroCostos + "] (id,idDepartamento ,nameCentro,status) " +
                        "VALUES (@MAXID,@idDepartamento, @nameCentro,0) ";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@idDepartamento", Value = centroCostos.idDepartamento });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@nameCentro", Value = centroCostos.nameCentro });

                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local", centroCostos, "WsInsertarCentroCostos");
                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF COST CENTER";
                        data.Status = "ERROR";
                    }
                }


                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {
                    string sSelect = "";
                    var ArrParametros1 = new List<SqlParameter>();

                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(id+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaCentroCostos + "])IF @MAXID >0 SELECT @MAXID=(SELECT MAX(id+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaCentroCostos + "])   ELSE set @MAXID=1 INSERT INTO [dbo].[" + GlobalesLocal.TablaCentroCostos + "] (id,idDepartamento ,nameCentro,status) VALUES (@MAXID,'" + centroCostos.idDepartamento + "', '" + centroCostos.nameCentro + "',0) ";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@idDepartamento", Value = centroCostos.idDepartamento });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@nameCentro", Value = centroCostos.nameCentro });
                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local");
                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF COST CENTER";
                        data.Status = "ERROR";
                    }


                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Insertar un centro de costos en la base de datos local", "WsInsertarCentroCostos");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Insertar un centro de costos en la base de datos local", "Ha ocurrido un error en la consulta al Insertar un centro de costos en la base de datos local", "", "", "", "");
                return Ok(mensaje);
            }



        }

        //juan
        [HttpGet] [Route("GetCentroCostos")]
        public IHttpActionResult  GetCentroCostos(string idDepartamento = "0", string all = "")
        {
            System.Data.DataTable s = new System.Data.DataTable();
            List<CentroCostos> lstCentroCostos = new List<CentroCostos>();
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            try
            {
                string sSelect = "";
                var ArrParametros1 = new List<SqlParameter>();

                if ( all == "" || all == null && idDepartamento != "0" && idDepartamento != "")
                {
                    sSelect = "SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaCentroCostos + "] where " +
                        "idDepartamento=@idDepartamento and Status<>-1 order by id desc";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@idDepartamento", Value = idDepartamento ?? (object)DBNull.Value });
                }
                else
                {
                    if (all == "" || all == "0" || all == null)
                    {
                        sSelect = "  SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaCentroCostos + "]" +
                            " where Status=0 order by status order by id desc";
                    }
                    else
                    {
                        sSelect = "  SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaCentroCostos + "]where" +
                            " idDepartamento=@idDepartamento order by id desc";
                        ArrParametros1.Add(new SqlParameter { ParameterName = "@idDepartamento", Value = idDepartamento ?? (object)DBNull.Value });
                    }


                }





                dT = BD.GetSQL(sSelect, "Local", ArrParametros1);
                var columnas = dT.Columns;
                //return Ok(dT.Rows);
                foreach (DataRow dR1 in dT.Rows)
                {

                    CentroCostos ObjCentroCostos = new CentroCostos();
                    ObjCentroCostos.id = Convert.ToInt32(dR1["id"].ToString());
                    ObjCentroCostos.idDepartamento = dR1["idDepartamento"].ToString();
                    ObjCentroCostos.nameCentro = dR1["nameCentro"].ToString();
                    ObjCentroCostos.status = dR1["status"].ToString();


                    lstCentroCostos.Add(ObjCentroCostos);
                }

                if (lstCentroCostos.Count > 0)
                {
                    data.lstCentroCostos = lstCentroCostos;
                    data.Message = "CENTRO COSTOS(S) FOUND";
                    data.Status = "OK";
                }
                else
                {
                    data.lstCentroCostos = lstCentroCostos;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al obtener CENTROS DE COSTOS en la bd local", "GetCentroCostos");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al obtener obtener CENTROS DE COSTOS en la bd local", "Ha ocurrido un error en la consulta al obtener CENTROS DE COSTOS en la bd local", "", "", "", "");
                return Ok(mensaje);
            }



        }

        // Este metodo es utilizado para actualizar un  DESPACHO en la base de datos de tank farm
        [HttpPost] [Route("WsUpdateCentroCostos")]
        public IHttpActionResult  WsUpdateCentroCostos(CentroCostos centroCostos)
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {

                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {

                    string sSelect = "";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "]  UPDATE [dbo].[" + GlobalesLocal.TablaCentroCostos + "] SET" +
                        " [idDepartamento] = @idDepartamento ,[nameCentro] = @nameCentro " +
                        "  WHERE id=@ID";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@idDepartamento", Value = centroCostos.idDepartamento ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@nameCentro", Value = centroCostos.nameCentro ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = centroCostos.id });


                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local", centroCostos, "WsUpdateCentroCostos");
                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF COST CENTER";
                        data.Status = "ERROR";
                    }
                }


                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {

                    string sSelect = "";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "]  UPDATE [dbo].[" + GlobalesLocal.TablaCentroCostos + "] SET" +
                       " [idDepartamento] = @idDepartamento ,[nameCentro] = @nameCentro " +
                       "  WHERE id=@ID";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@idDepartamento", Value = centroCostos.idDepartamento ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@nameCentro", Value = centroCostos.nameCentro ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = centroCostos.id });
                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local");
                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF COST CENTER";
                        data.Status = "ERROR";
                    }

                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar un CENTRO DE COSTOS en la BD local", "WsUpdateCentroCostos");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar  UN CENTRO DE COSTOS  en la BD local", "Ha ocurrido un error en la consulta al actualizar un  CENTRO DE COSTOS en la BD local", "", "", "", "");
                return Ok(mensaje);
            }



        }

        [HttpGet] [Route("GetUserID")]
        public IHttpActionResult  GetUserID(DatosUsuario user)
        {
            try
            {
                var RFID = "56326074";
                user.ID = "2903";
                user.Status = "enabled";
                user.Email = "Juan.Quijano@BMW.com";
                user.Deparment = "FM-Building";

                //user.RFID = "x0023 x0123 x1234 x2456";
                user.UserName = "Jose Luis Montes";

                if (RFID == user.RFID)
                {
                    return Ok(user);
                }
                else
                {
                    var mensaje = new Mensajes();
                    mensaje.Mensaje = "User was not found, Please try a valid User";
                    mensaje.Status = "Error";
                    return Ok(mensaje);
                }

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                return Ok(mensaje);
            }



        }
        [HttpGet] [Route("GetCarID")]
        public IHttpActionResult  GetCarID(DatosVehiculo car)
        {
            try
            {
                var Tag = "639399745430";
                car.ID = "2903";
                car.Status = "enabled";
                //user.RFID = "x0023 x0123 x1234 x2456";
                car.CarName = "BMW Serie 3 - 2020 ";

                if (Tag == car.Tag)
                {

                    return Ok(car);
                }
                else
                {
                    var mensaje = new Mensajes();
                    mensaje.Mensaje = "The Car was not found, Please try a valid Car";
                    mensaje.Status = "Error";
                    return Ok(mensaje);
                }

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                return Ok(mensaje);
            }



        }


        //juan
        [HttpGet]
        [Route("GetClientes")]
        public IHttpActionResult  GetClientes(string id = "0", string all = "")
        {

            System.Data.DataTable s = new System.Data.DataTable();
            List<Cliente> lstClientes = new List<Cliente>();
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            try
            {
                string sSelect = "";
                var ArrParametros1 = new List<SqlParameter>();
                if (id != "0")
                {

                    sSelect = "select  * from  [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "] where cod=@ID order by cod desc";

                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = id });

                }
                else
                {
                    if (all == "" || all == "0" || all == null)
                    {
                        sSelect = "select * from  [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  where codest=0 order by cod desc";
                    }
                    else
                    {
                        sSelect = "select * from  [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "] order by cod desc";
                    }
                }






                dT = BD.GetSQL(sSelect, "ControlGas", ArrParametros1);
                var columnas = dT.Columns;
                //return Ok(dT.Rows);
                foreach (DataRow dR1 in dT.Rows)
                {

                    Cliente ObjPerfiles = new Cliente();
                    ObjPerfiles.cod = dR1["cod"].ToString();
                    ObjPerfiles.den = dR1["den"].ToString();
                    ObjPerfiles.dom = dR1["dom"].ToString();
                    ObjPerfiles.col = dR1["col"].ToString();
                    ObjPerfiles.del = dR1["del"].ToString();
                    ObjPerfiles.ciu = dR1["ciu"].ToString();
                    ObjPerfiles.est = dR1["est"].ToString();
                    ObjPerfiles.tel = dR1["tel"].ToString();
                    ObjPerfiles.fax = dR1["fax"].ToString();
                    ObjPerfiles.rfc = dR1["rfc"].ToString();
                    ObjPerfiles.tipval = ToDouble(dR1["tipval"].ToString());
                    ObjPerfiles.mtoasg = ToDouble(dR1["mtoasg"].ToString());
                    ObjPerfiles.mtodis = ToDouble(dR1["mtodis"].ToString());
                    ObjPerfiles.mtorep = ToDouble(dR1["mtorep"].ToString());
                    ObjPerfiles.cndpag = ToDouble(dR1["cndpag"].ToString());
                    ObjPerfiles.diarev = ToDouble(dR1["diarev"].ToString());
                    ObjPerfiles.horrev = dR1["horrev"].ToString();
                    ObjPerfiles.diapag = ToDouble(dR1["diapag"].ToString());
                    ObjPerfiles.horpag = dR1["horpag"].ToString();
                    ObjPerfiles.cto = dR1["cto"].ToString();
                    ObjPerfiles.obs = dR1["obs"].ToString();
                    ObjPerfiles.codext = dR1["codext"].ToString();
                    ObjPerfiles.datcon = ToDouble(dR1["datcon"].ToString());
                    ObjPerfiles.codpos = dR1["codpos"].ToString();
                    ObjPerfiles.pto = ToDouble(dR1["pto"].ToString());
                    ObjPerfiles.ptosdo = ToDouble(dR1["ptosdo"].ToString());
                    ObjPerfiles.debsdo = ToDouble(dR1["debsdo"].ToString());
                    ObjPerfiles.cresdo = ToDouble(dR1["cresdo"].ToString());
                    ObjPerfiles.fmtexp = ToDouble(dR1["fmtexp"].ToString());
                    ObjPerfiles.arcexp = dR1["arcexp"].ToString();
                    ObjPerfiles.polcor = ToDouble(dR1["polcor"].ToString());
                    ObjPerfiles.ultcor = ToDouble(dR1["ultcor"].ToString());
                    ObjPerfiles.debnro = ToDouble(dR1["debnro"].ToString());
                    ObjPerfiles.crenro = ToDouble(dR1["crenro"].ToString());
                    ObjPerfiles.debglo = ToDouble(dR1["debglo"].ToString());
                    ObjPerfiles.codtip = ToDouble(dR1["codtip"].ToString());
                    ObjPerfiles.codzon = ToDouble(dR1["codzon"].ToString());
                    ObjPerfiles.codgrp = ToDouble(dR1["codgrp"].ToString());
                    ObjPerfiles.codest = ToDouble(dR1["codest"].ToString());
                    ObjPerfiles.logusu = ToDouble(dR1["logusu"].ToString());
                    ObjPerfiles.logfch = dR1["logfch"].ToString();
                    ObjPerfiles.lognew = dR1["lognew"].ToString();
                    ObjPerfiles.lognew = dR1["lognew"].ToString();
                    ObjPerfiles.correo = dR1["correo"].ToString();
                    ObjPerfiles.dattik = ToDouble(dR1["dattik"].ToString());
                    ObjPerfiles.ptodebacu = ToDouble(dR1["ptodebacu"].ToString());
                    ObjPerfiles.ptodebfch = ToDouble(dR1["ptodebfch"].ToString());
                    ObjPerfiles.ptocreacu = ToDouble(dR1["ptocreacu"].ToString());
                    ObjPerfiles.ptocrefch = ToDouble(dR1["ptocrefch"].ToString());
                    ObjPerfiles.ptovenacu = ToDouble(dR1["ptovenacu"].ToString());
                    ObjPerfiles.ptovenfch = ToDouble(dR1["ptovenfch"].ToString());
                    ObjPerfiles.domnroext = dR1["domnroext"].ToString();
                    ObjPerfiles.domnroint = dR1["domnroint"].ToString();
                    ObjPerfiles.datvar = ToDouble(dR1["datvar"].ToString());
                    ObjPerfiles.nroctapag = dR1["nroctapag"].ToString();
                    ObjPerfiles.tipopepag = ToDouble(dR1["tipopepag"].ToString());
                    ObjPerfiles.cveest = dR1["cveest"].ToString();
                    ObjPerfiles.cvetra = dR1["cvetra"].ToString();
                    ObjPerfiles.geodat = dR1["geodat"].ToString();
                    ObjPerfiles.geolat = ToDouble(dR1["geolat"].ToString());
                    ObjPerfiles.geolng = ToDouble(dR1["geolng"].ToString());
                    ObjPerfiles.taxext = ToDouble(dR1["taxext"].ToString());
                    ObjPerfiles.taxextid = dR1["taxextid"].ToString();
                    ObjPerfiles.bcomn1cod = ToDouble(dR1["bcomn1cod"].ToString());
                    ObjPerfiles.bcomn1den = dR1["bcomn1den"].ToString();
                    ObjPerfiles.bcomn1cta = dR1["bcomn1cta"].ToString();
                    ObjPerfiles.bcomn2cod = ToDouble(dR1["bcomn2cod"].ToString());
                    ObjPerfiles.bcomn2den = dR1["bcomn2den"].ToString();
                    ObjPerfiles.bcomn2cta = ToDouble(dR1["bcomn2cta"].ToString());
                    ObjPerfiles.bcome1cod = ToDouble(dR1["bcome1cod"].ToString());
                    ObjPerfiles.bcome1den = dR1["bcome1den"].ToString();
                    ObjPerfiles.bcome1cta = dR1["bcome1cta"].ToString();
                    ObjPerfiles.bcome2cod = ToDouble(dR1["bcome2cod"].ToString());
                    ObjPerfiles.bcome2den = dR1["bcome2den"].ToString();
                    ObjPerfiles.bcome2cta = dR1["bcome2cta"].ToString();
                    ObjPerfiles.perfis = ToDouble(dR1["perfis"].ToString());
                    ObjPerfiles.perfisnom = dR1["perfisnom"].ToString();
                    ObjPerfiles.perfisapp = dR1["perfisapp"].ToString();
                    ObjPerfiles.perfisapm = dR1["perfisapm"].ToString();
                    ObjPerfiles.curp = dR1["curp"].ToString();
                    ObjPerfiles.codrefban = ToDouble(dR1["codrefban"].ToString());
                    ObjPerfiles.paisat = dR1["paisat"].ToString();
                    ObjPerfiles.satuso = dR1["satuso"].ToString();



                    lstClientes.Add(ObjPerfiles);
                }

                if (lstClientes.Count > 0)
                {
                    data.lstClients = lstClientes;
                    data.Message = "CUSTOMER(S) FOUND";
                    data.Status = "OK";
                }
                else
                {
                    data.lstClients = lstClientes;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al obtener los clientes en control gas", "GetClientes");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al obtener los clientes en control gas", "Ha ocurrido un error en la consulta al obtener los clientes en control gas", "", "", "", "");
                return Ok(mensaje);
            }



        }


        //juan
        // Este metodo es utilizado para insertar un nuevo CLIENTE en la base de datos de tank farm
        [HttpPost] [Route("WsInsertarClienteControlGas")]
        public IHttpActionResult  WsInsertarClienteControlGas(Cliente cliente)
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {
                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    string sSelect = "";

                    sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(cod+1) AS id FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "])" +
                        "IF @MAXID >0 SELECT @MAXID=(SELECT MAX(cod+1) AS id FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "])   ELSE set @MAXID=1;  INSERT INTO [dbo].[" + GlobalesCorporativo.TablaClientes + "]" +
                        "  ([cod] ,[den] ,[dom] ,[col] ,[del] ,[ciu] ,[est] ,[tel] ,[fax] ,[rfc] ,[tipval] ,[mtoasg] ,[mtodis] ,[mtorep] ,[cndpag] ,[diarev] ,[horrev] ,[diapag] ,[horpag] ,[cto] ,[obs] ,[codext] ,[datcon] ,[codpos] ,[pto] ,[ptosdo] ,[debsdo] ,[cresdo] ," +
                        "[fmtexp] ,[arcexp] ,[polcor] ,[ultcor] ,[debnro] ,[crenro] ,[debglo] ,[codtip] ,[codzon] ,[codgrp] ,[codest] ,[logusu] ,[logfch] ,[lognew] ,[pai] ,[correo] ,[dattik] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ," +
                        "[ptovenfch] ,[domnroext] ,[domnroint] ,[datvar] ,[nroctapag] ,[tipopepag] ,[cveest] ,[cvetra] ,[geodat] ,[geolat] ,[geolng] ,[taxext] ,[taxextid] ,[bcomn1cod] ,[bcomn1den] ,[bcomn1cta] ,[bcomn2cod] ,[bcomn2den] ,[bcomn2cta] ,[bcome1cod] ," +
                        "[bcome1den] ,[bcome1cta] ,[bcome2cod] ,[bcome2den] ,[bcome2cta] ,[perfis] ,[perfisnom] ,[perfisapp] ,[perfisapm] ,[curp] ,[codrefban] ,[paisat] ,[satuso])" +
                        " VALUES (@MAXID,@den  ,@dom  ,@col  ,@del  ,@ciu  ,@est  ,@tel  ," +
                        "@fax  ,@rfc  ,@tipval  ,@mtoasg  ,@mtodis  ,@mtorep  ,@cndpag  ," +
                        "@diarev  ,@horrev  ,@diapag  @horpag  ,@cto  ,@obs  ," +
                        "(SELECT MAX(cod+1) AS id FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  ) ,@datcon +,@codpos +," +
                        "@pto ,@ptosdo ,@debsdo ,@cresdo ,@fmtexp ,@arcexp ,@polcor ," +
                        "@ultcor ,@debnro ,@crenro ,@debglo ,@codtip ,@codzon ,@codgrp ," +
                        "@codest ,@logusu ,GETDATE() ,GETDATE() ,@pai ,@correo @dattik ,@ptodebacu ," +
                        "@ptodebfch ,@ptocreacu ,@ptocrefch ,@ptovenacu ,@ptovenfch ,@domnroext ," +
                        "@domnroint ,@datvar @nroctapag  ,@tipopepag ,@cveest ,@cvetra ,@geodat ," +
                        "@geolat ,@geolng ,@taxext ,@taxextid ,@bcomn1cod ,@bcomn1den ,@bcomn1cta ," +
                        "@bcomn2cod ,@bcomn2den ,@bcomn2cta ,@bcome1cod ,@bcome1den ,@bcome1cta ," +
                        "@bcome2cod,  @bcome2den  ,@bcome2cta  ,@perfis ,@perfisnom ,@perfisapp  ," +
                        "@perfisapm  ,@curp  ,@codrefban ,@paisat  ,@satuso )";

                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@den", Value = cliente.den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@col", Value = cliente.col ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@del", Value = "" ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ciu", Value = cliente.ciu ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@est", Value = cliente.est ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tel", Value = cliente.tel ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@fax", Value = cliente.fax ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@rfc", Value = cliente.rfc ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tipval", Value = cliente.tipval });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtoasg", Value = cliente.mtoasg });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtodis", Value = cliente.mtodis });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtorep", Value = cliente.mtorep });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cndpag", Value = cliente.cndpag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diarev", Value = cliente.diarev });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@horrev", Value = cliente.horrev ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diapag", Value = cliente.diapag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@horpag", Value = cliente.horpag ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cto", Value = cliente.cto ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@obs", Value = cliente.obs ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@dom", Value = cliente.dom ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@datcon", Value = cliente.datcon });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codpos", Value = cliente.codpos ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptosdo", Value = cliente.ptosdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@pto", Value = cliente.pto });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debsdo", Value = cliente.debsdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cresdo", Value = cliente.cresdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@fmtexp", Value = cliente.fmtexp });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@arcexp", Value = cliente.arcexp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@polcor", Value = cliente.polcor });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ultcor", Value = cliente.ultcor });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debnro", Value = cliente.debnro });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@crenro", Value = cliente.crenro });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debglo", Value = cliente.debglo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codtip", Value = cliente.codtip });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codzon", Value = cliente.codzon });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codgrp", Value = cliente.codgrp });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codest", Value = cliente.codest });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@logusu", Value = cliente.logusu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@pai", Value = cliente.pai ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@correo", Value = cliente.correo ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@dattik", Value = cliente.dattik });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptodebacu", Value = cliente.ptodebacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptodebfch", Value = cliente.ptodebfch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptocreacu", Value = cliente.ptocreacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptocrefch", Value = cliente.ptocrefch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptovenacu", Value = cliente.ptovenacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptovenfch", Value = cliente.ptovenfch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@domnroext", Value = cliente.domnroext ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@domnroint", Value = cliente.domnroint ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@datvar", Value = cliente.datvar });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@nroctapag", Value = cliente.nroctapag ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tipopepag", Value = cliente.tipopepag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cveest", Value = cliente.cveest ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cvetra", Value = cliente.cvetra ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geodat", Value = cliente.geodat ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geolat", Value = cliente.geolat });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geolng", Value = cliente.geolng });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@taxext", Value = cliente.taxext });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@taxextid", Value = cliente.taxextid ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1cod", Value = cliente.bcomn1cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1den", Value = cliente.bcomn1den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1cta", Value = cliente.bcomn1cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2cod", Value = cliente.bcomn2cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2den", Value = cliente.bcomn2den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2cta", Value = cliente.bcomn2cta });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1cod", Value = cliente.bcome1cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1den", Value = cliente.bcome1den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1cta", Value = cliente.bcome1cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2cod", Value = cliente.bcome2cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2den", Value = cliente.bcome2den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2cta", Value = cliente.bcome2cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfis", Value = cliente.perfis });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisnom", Value = cliente.perfisnom ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisapp", Value = cliente.perfisapp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisapm", Value = cliente.perfisapm ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@curp", Value = cliente.curp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codrefban", Value = cliente.codrefban });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@paisat", Value = cliente.paisat ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@satuso", Value = cliente.satuso ?? (object)DBNull.Value });
                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "ControlGas", cliente, "WsInsertarClienteControlGas");

                    if (DataAfected > 0)
                    {

                        // Task oTask = new Task();
                        // oTask.Start();
                        // await.oTask;
                        //SendWS(cliente, "WsInsertarClienteControlGas");
                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";

                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF CLIENTES";
                        data.Status = "ERROR";
                    }

                }
                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {
                    string sSelect = "";
                    sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(cod+1) AS id FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "])IF @MAXID >0 SELECT @MAXID=(SELECT MAX(cod+1) AS id FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "])   ELSE set @MAXID=1;  INSERT INTO [dbo].[" + GlobalesCorporativo.TablaClientes + "]  ([cod] ,[den] ,[dom] ,[col] ,[del] ,[ciu] ,[est] ,[tel] ,[fax] ,[rfc] ,[tipval] ,[mtoasg] ,[mtodis] ,[mtorep] ,[cndpag] ,[diarev] ,[horrev] ,[diapag] ,[horpag] ,[cto] ,[obs] ,[codext] ,[datcon] ,[codpos] ,[pto] ,[ptosdo] ,[debsdo] ,[cresdo] ,[fmtexp] ,[arcexp] ,[polcor] ,[ultcor] ,[debnro] ,[crenro] ,[debglo] ,[codtip] ,[codzon] ,[codgrp] ,[codest] ,[logusu] ,[logfch] ,[lognew] ,[pai] ,[correo] ,[dattik] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[domnroext] ,[domnroint] ,[datvar] ,[nroctapag] ,[tipopepag] ,[cveest] ,[cvetra] ,[geodat] ,[geolat] ,[geolng] ,[taxext] ,[taxextid] ,[bcomn1cod] ,[bcomn1den] ,[bcomn1cta] ,[bcomn2cod] ,[bcomn2den] ,[bcomn2cta] ,[bcome1cod] ,[bcome1den] ,[bcome1cta] ,[bcome2cod] ,[bcome2den] ,[bcome2cta] ,[perfis] ,[perfisnom] ,[perfisapp] ,[perfisapm] ,[curp] ,[codrefban] ,[paisat] ,[satuso])" +
                        " VALUES (@MAXID,@den  ,@dom  ,@col  ,@del  ,@ciu  ,@est  ,@tel  ," +
                        "@fax  ,@rfc  ,@tipval  ,@mtoasg  ,@mtodis  ,@mtorep  ,@cndpag  ," +
                        "@diarev  ,@horrev  ,@diapag,  @horpag  ,@cto  ,@obs," +
                        "(SELECT MAX(cod+1) AS id FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "]  ) ,@datcon,@codpos," +
                        "@pto ,@ptosdo ,@debsdo ,@cresdo ,@fmtexp ,@arcexp ,@polcor ," +
                        "@ultcor ,@debnro ,@crenro ,@debglo ,@codtip ,@codzon ,@codgrp ," +
                        "@codest ,@logusu ,GETDATE() ,GETDATE() ,@pai ,@correo, @dattik ,@ptodebacu ," +
                        "@ptodebfch ,@ptocreacu ,@ptocrefch ,@ptovenacu ,@ptovenfch ,@domnroext ," +
                        "@domnroint ,@datvar, @nroctapag  ,@tipopepag ,@cveest ,@cvetra ,@geodat ," +
                        "@geolat ,@geolng ,@taxext ,@taxextid ,@bcomn1cod ,@bcomn1den ,@bcomn1cta ," +
                        "@bcomn2cod ,@bcomn2den ,@bcomn2cta ,@bcome1cod ,@bcome1den ,@bcome1cta ," +
                        "@bcome2cod,  @bcome2den  ,@bcome2cta  ,@perfis ,@perfisnom ,@perfisapp  ," +
                        "@perfisapm  ,@curp  ,@codrefban ,@paisat  ,@satuso )";

                    var ArrParametros1 = new List<SqlParameter>();




                    ArrParametros1.Add(new SqlParameter { ParameterName = "@den", Value = cliente.den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@col", Value = cliente.col ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@del", Value = "" ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ciu", Value = cliente.ciu ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@est", Value = cliente.est ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tel", Value = cliente.tel ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@fax", Value = cliente.fax ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@rfc", Value = cliente.rfc ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tipval", Value = cliente.tipval });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtoasg", Value = cliente.mtoasg });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtodis", Value = cliente.mtodis });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtorep", Value = cliente.mtorep });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cndpag", Value = cliente.cndpag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diarev", Value = cliente.diarev });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@horrev", Value = cliente.horrev ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diapag", Value = cliente.diapag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@horpag", Value = cliente.horpag ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cto", Value = cliente.cto ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@obs", Value = cliente.obs ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@dom", Value = cliente.dom ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@datcon", Value = cliente.datcon });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codpos", Value = cliente.codpos ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptosdo", Value = cliente.ptosdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@pto", Value = cliente.pto });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debsdo", Value = cliente.debsdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cresdo", Value = cliente.cresdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@fmtexp", Value = cliente.fmtexp });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@arcexp", Value = cliente.arcexp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@polcor", Value = cliente.polcor });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ultcor", Value = cliente.ultcor });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debnro", Value = cliente.debnro });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@crenro", Value = cliente.crenro });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debglo", Value = cliente.debglo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codtip", Value = cliente.codtip });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codzon", Value = cliente.codzon });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codgrp", Value = cliente.codgrp });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codest", Value = cliente.codest });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@logusu", Value = cliente.logusu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@pai", Value = cliente.pai ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@correo", Value = cliente.correo ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@dattik", Value = cliente.dattik });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptodebacu", Value = cliente.ptodebacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptodebfch", Value = cliente.ptodebfch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptocreacu", Value = cliente.ptocreacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptocrefch", Value = cliente.ptocrefch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptovenacu", Value = cliente.ptovenacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptovenfch", Value = cliente.ptovenfch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@domnroext", Value = cliente.domnroext ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@domnroint", Value = cliente.domnroint ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@datvar", Value = cliente.datvar });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@nroctapag", Value = cliente.nroctapag ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tipopepag", Value = cliente.tipopepag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cveest", Value = cliente.cveest ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cvetra", Value = cliente.cvetra ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geodat", Value = cliente.geodat ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geolat", Value = cliente.geolat });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geolng", Value = cliente.geolng });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@taxext", Value = cliente.taxext });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@taxextid", Value = cliente.taxextid ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1cod", Value = cliente.bcomn1cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1den", Value = cliente.bcomn1den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1cta", Value = cliente.bcomn1cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2cod", Value = cliente.bcomn2cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2den", Value = cliente.bcomn2den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2cta", Value = cliente.bcomn2cta });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1cod", Value = cliente.bcome1cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1den", Value = cliente.bcome1den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1cta", Value = cliente.bcome1cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2cod", Value = cliente.bcome2cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2den", Value = cliente.bcome2den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2cta", Value = cliente.bcome2cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfis", Value = cliente.perfis });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisnom", Value = cliente.perfisnom ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisapp", Value = cliente.perfisapp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisapm", Value = cliente.perfisapm ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@curp", Value = cliente.curp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codrefban", Value = cliente.codrefban });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@paisat", Value = cliente.paisat ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@satuso", Value = cliente.satuso ?? (object)DBNull.Value });
                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "ControlGas");

                    if (DataAfected > 0)
                    {
                        if (WSInsertarClientesGasolineras(cliente.den) > 0)
                        {
                            data.Message = "SUCCESSFUL INSERTION";
                            data.Status = "OK";
                        }
                        else
                        {
                            data.Message = "FAILED INSERTION OF CLIENTES";
                            data.Status = "ERROR";
                        }


                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF CLIENTES";
                        data.Status = "ERROR";
                    }

                }
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Insertar los clientes en control gas", "WsInsertarClienteControlGas");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Insertar los clientes en control gas", "Ha ocurrido un error en la consulta al Insertar los clientes en control gas", "", "", "", "");
                return Ok(mensaje);
            }



        }
        [Route("WSInsertarClientesGasolineras")]
        public int WSInsertarClientesGasolineras(string Descripcion)
        {
            int DataAfected = 0;
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            string sSelect = "";

            string sSelectLocaL = "";

            sSelectLocaL = "SELECT * FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].dbo.[" + GlobalesCorporativo.TablaClientes + "] WHERE den = @den";
            var ArrParametros1 = new List<SqlParameter>();
            ArrParametros1.Add(new SqlParameter { ParameterName = "@den", Value = Descripcion });
            dT = BD.GetSQL(sSelectLocaL, "ControlGas", ArrParametros1);
            var codClie = "";
            foreach (DataRow dR1 in dT.Rows)
            {
                codClie = dR1["cod"].ToString();
            }

            sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] INSERT INTO [dbo].[" + GlobalesCorporativo.TablaClientesGasolineras + "]  ([codcli] ,[codgas] ,[codest] ,[logusu] ,[logfch])" +
                " VALUES (@codClie,0 ,0 ,1 ,GETDATE())";
            var ArrParametros2 = new List<SqlParameter>();
            ArrParametros2.Add(new SqlParameter { ParameterName = "@codClie", Value = codClie });
            DataAfected = BD.SetSQL(ArrParametros2, sSelect, "ControlGas");

            if (DataAfected > 0)
            {
                return DataAfected;


            }
            else
            {

                return DataAfected;
            }
        }
        public int SendWS(object Parametro, string NameWS = "")
        {
            var status = 0;
            var URLWS = ConfigurationManager.AppSettings["URLWS"];
            var client = new RestClient();
            var request = new RestRequest(URLWS + NameWS);
            if (NameWS == "WsStatusChangue")
            {
                request.Method = Method.GET;
            }
            else
            {
                request.Method = Method.POST;
            }

            //request.Timeout = 200;
            request.AddHeader("Accept", "application/json");
            request.AddObject(Parametro);
            var restResponse = client.Execute(request);
            var data = JsonConvert.DeserializeObject<DataResponse>(restResponse.Content);
            try
            {
                if (data != null)
                {
                    if (data.Message == "NO DATA FOUND" || data.Message == "FAILED INSERTION OF USER")
                    {

                        //PasarAlerta("Vehicle  was not found." + Environment.NewLine + " Please try an existing vehicle!", "Warning", "OK");

                        status = 0;
                    }
                    else
                    {
                        status = 1;


                    }
                }


            }
            catch (Exception e)
            {
                status = 0;
                AgregaLog("ERROR AL EJECUTAR UN WS DE SINCRONIZACION " + e.Message.ToString(), "SendWS");
                //PasarAlerta("There is no connection with the server" + Environment.NewLine, "WarningNotify", "OK");
                //return null;
                //throw;
            }
            return status;

        }



        // Este metodo es utilizado para actualizar un  CLIENTE en la base de datos de tank farm
        [HttpPost] [Route("WsUpdateClienteControlGas")]
        public IHttpActionResult  WsUpdateClienteControlGas(Cliente cliente)
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {
                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {

                    string sSelect = "";

                    sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] UPDATE [dbo].[" + GlobalesCorporativo.TablaClientes + "]  SET [den] =@den ,[dom] =@dom ," +
                        "[col] = @col ,[del] = @del ,[ciu] = @ciu ,[est] = @est ,[tel] = @tel ,[fax] = @fax ,[rfc] = @rfc ,[tipval] = @tipval ,[mtoasg] = @mtoasg ,[mtodis] = @mtodis ,[mtorep] = @mtorep, " +
                        "[cndpag] = @cndpag ,[diarev] =@diarev ,[horrev] = @horrev,[diapag] = @diapag ,[horpag] = @horpag,[cto] = @cto ,[obs] = @obs ,[datcon] = @datcon ,[codpos] = @codpos ,[pto] = @pto ," +
                        "[ptosdo] = @ptosdo ,[debsdo] = @debsdo ,[cresdo] = @cresdo ,[fmtexp] = @fmtexp,[arcexp] = @arcexp ,[polcor] =@polcor ,[ultcor] = @ultcor ,[debnro] = @debnro ,[crenro] = @crenro ,[debglo] = @debglo ," +
                        "[codtip] = @codtip ,[codzon] = @codzon ,[codgrp] = @codgrp ,[codest] = @codest ,[logusu] =@logusu ,[logfch] = GETDATE() ,[lognew] = GETDATE() ,[pai] = @pai ,[correo] = @correo ,[dattik] = @dattik ," +
                        "[ptodebacu] = @ptodebacu ," +
                        "[ptodebfch] = @ptodebfch ,[ptocreacu] = @ptocreacu ,[ptocrefch] = @ptocrefch ,[ptovenacu] = @ptovenacu ,[ptovenfch] = @ptovenfch ,[domnroext] = @domnroext ,[domnroint] = @domnroint ,[datvar] = @datvar ," +
                        "[nroctapag] = @nroctapag,[tipopepag] = @tipopepag ,[cveest] = @cveest ,[cvetra] = @cvetra," +
                        "[geodat] = @geodat ,[geolat] = @geolat ,[geolng] = @geolng ,[taxext] = @taxext ,[taxextid] = @taxextid ," +
                        "[bcomn1cod] = @bcomn1cod  ,[bcomn1den] = @bcomn1den ,[bcomn1cta] = @bcomn1cta ,[bcomn2cod] = @bcomn2cod  ," +
                        "[bcomn2den] = @bcomn2den  ,[bcomn2cta] = @bcomn2cta ,[bcome1cod] = @bcome1cod  ,[bcome1den] = @bcome1den ," +
                        "[bcome1cta] = @bcome1cta  ,[bcome2cod] = @bcome2cod  ,[bcome2den] = @bcome2den  ,[bcome2cta] = @bcome2cta  ," +
                        "[perfis] = @perfis ,[perfisnom] = @perfisnom ,[perfisapp] = @perfisapp  ,[perfisapm] = @perfisapm ," +
                        "[curp] = @curp  ,[codrefban] = @codrefban  ,[paisat] = @paisat  ,[satuso] = @satuso  WHERE cod =@cod ";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cod", Value = cliente.cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@den", Value = cliente.den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@col", Value = cliente.col ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@del", Value = "" ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ciu", Value = cliente.ciu ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@est", Value = cliente.est ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tel", Value = cliente.tel ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@fax", Value = cliente.fax ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@rfc", Value = cliente.rfc ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tipval", Value = cliente.tipval });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtoasg", Value = cliente.mtoasg });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtodis", Value = cliente.mtodis });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtorep", Value = cliente.mtorep });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cndpag", Value = cliente.cndpag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diarev", Value = cliente.diarev });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@horrev", Value = cliente.horrev ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diapag", Value = cliente.diapag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@horpag", Value = cliente.horpag ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cto", Value = cliente.cto ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@obs", Value = cliente.obs ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@dom", Value = cliente.dom ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@datcon", Value = cliente.datcon });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codpos", Value = cliente.codpos ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptosdo", Value = cliente.ptosdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@pto", Value = cliente.pto });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debsdo", Value = cliente.debsdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cresdo", Value = cliente.cresdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@fmtexp", Value = cliente.fmtexp });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@arcexp", Value = cliente.arcexp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@polcor", Value = cliente.polcor });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ultcor", Value = cliente.ultcor });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debnro", Value = cliente.debnro });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@crenro", Value = cliente.crenro });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debglo", Value = cliente.debglo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codtip", Value = cliente.codtip });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codzon", Value = cliente.codzon });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codgrp", Value = cliente.codgrp });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codest", Value = cliente.codest });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@logusu", Value = cliente.logusu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@pai", Value = cliente.pai ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@correo", Value = cliente.correo ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@dattik", Value = cliente.dattik });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptodebacu", Value = cliente.ptodebacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptodebfch", Value = cliente.ptodebfch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptocreacu", Value = cliente.ptocreacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptocrefch", Value = cliente.ptocrefch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptovenacu", Value = cliente.ptovenacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptovenfch", Value = cliente.ptovenfch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@domnroext", Value = cliente.domnroext ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@domnroint", Value = cliente.domnroint ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@datvar", Value = cliente.datvar });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@nroctapag", Value = cliente.nroctapag ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tipopepag", Value = cliente.tipopepag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cveest", Value = cliente.cveest ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cvetra", Value = cliente.cvetra ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geodat", Value = cliente.geodat ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geolat", Value = cliente.geolat });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geolng", Value = cliente.geolng });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@taxext", Value = cliente.taxext });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@taxextid", Value = cliente.taxextid ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1cod", Value = cliente.bcomn1cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1den", Value = cliente.bcomn1den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1cta", Value = cliente.bcomn1cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2cod", Value = cliente.bcomn2cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2den", Value = cliente.bcomn2den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2cta", Value = cliente.bcomn2cta });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1cod", Value = cliente.bcome1cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1den", Value = cliente.bcome1den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1cta", Value = cliente.bcome1cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2cod", Value = cliente.bcome2cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2den", Value = cliente.bcome2den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2cta", Value = cliente.bcome2cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfis", Value = cliente.perfis });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisnom", Value = cliente.perfisnom ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisapp", Value = cliente.perfisapp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisapm", Value = cliente.perfisapm ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@curp", Value = cliente.curp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codrefban", Value = cliente.codrefban });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@paisat", Value = cliente.paisat ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@satuso", Value = cliente.satuso ?? (object)DBNull.Value });

                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "ControlGas", cliente, "WsUpdateClienteControlGas");
                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED UPGRADE1 OF CLIENTES";
                        data.Status = "ERROR";
                    }

                }
                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {

                    string sSelect = "";

                    sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] UPDATE [dbo].[" + GlobalesCorporativo.TablaClientes + "]  SET [den] =@den ,[dom] =@dom ," +
                         "[col] = @col ,[del] = @del ,[ciu] = @ciu ,[est] = @est ,[tel] = @tel ,[fax] = @fax ,[rfc] = @rfc ,[tipval] = @tipval ,[mtoasg] = @mtoasg ,[mtodis] = @mtodis ,[mtorep] = @mtorep, " +
                         "[cndpag] = @cndpag ,[diarev] =@diarev ,[horrev] = @horrev,[diapag] = @diapag ,[horpag] = @horpag,[cto] = @cto ,[obs] = @obs ,[datcon] = @datcon ,[codpos] = @codpos ,[pto] = @pto ," +
                         "[ptosdo] = @ptosdo ,[debsdo] = @debsdo ,[cresdo] = @cresdo ,[fmtexp] = @fmtexp,[arcexp] = @arcexp ,[polcor] =@polcor ,[ultcor] = @ultcor ,[debnro] = @debnro ,[crenro] = @crenro ,[debglo] = @debglo ," +
                         "[codtip] = @codtip ,[codzon] = @codzon ,[codgrp] = @codgrp ,[codest] = @codest ,[logusu] =@logusu ,[logfch] = GETDATE() ,[lognew] = GETDATE() ,[pai] = @pai ,[correo] = @correo ,[dattik] = @dattik ," +
                         "[ptodebacu] = @ptodebacu ," +
                         "[ptodebfch] = @ptodebfch ,[ptocreacu] = @ptocreacu ,[ptocrefch] = @ptocrefch ,[ptovenacu] = @ptovenacu ,[ptovenfch] = @ptovenfch ,[domnroext] = @domnroext ,[domnroint] = @domnroint ,[datvar] = @datvar ," +
                         "[nroctapag] = @nroctapag,[tipopepag] = @tipopepag ,[cveest] = @cveest ,[cvetra] = @cvetra," +
                         "[geodat] = @geodat ,[geolat] = @geolat ,[geolng] = @geolng ,[taxext] = @taxext ,[taxextid] = @taxextid ," +
                         "[bcomn1cod] = @bcomn1cod  ,[bcomn1den] = @bcomn1den ,[bcomn1cta] = @bcomn1cta ,[bcomn2cod] = @bcomn2cod  ," +
                         "[bcomn2den] = @bcomn2den  ,[bcomn2cta] = @bcomn2cta ,[bcome1cod] = @bcome1cod  ,[bcome1den] = @bcome1den ," +
                         "[bcome1cta] = @bcome1cta  ,[bcome2cod] = @bcome2cod  ,[bcome2den] = @bcome2den  ,[bcome2cta] = @bcome2cta  ," +
                         "[perfis] = @perfis ,[perfisnom] = @perfisnom ,[perfisapp] = @perfisapp  ,[perfisapm] = @perfisapm ," +
                         "[curp] = @curp  ,[codrefban] = @codrefban  ,[paisat] = @paisat  ,[satuso] = @satuso  WHERE cod =@cod ";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cod", Value = cliente.cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@den", Value = cliente.den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@col", Value = cliente.col ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@del", Value = "" ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ciu", Value = cliente.ciu ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@est", Value = cliente.est ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tel", Value = cliente.tel ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@fax", Value = cliente.fax ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@rfc", Value = cliente.rfc ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tipval", Value = cliente.tipval });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtoasg", Value = cliente.mtoasg });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtodis", Value = cliente.mtodis });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@mtorep", Value = cliente.mtorep });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cndpag", Value = cliente.cndpag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diarev", Value = cliente.diarev });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@horrev", Value = cliente.horrev ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@diapag", Value = cliente.diapag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@horpag", Value = cliente.horpag ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cto", Value = cliente.cto ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@obs", Value = cliente.obs ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@dom", Value = cliente.dom ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@datcon", Value = cliente.datcon });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codpos", Value = cliente.codpos ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptosdo", Value = cliente.ptosdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@pto", Value = cliente.pto });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debsdo", Value = cliente.debsdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cresdo", Value = cliente.cresdo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@fmtexp", Value = cliente.fmtexp });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@arcexp", Value = cliente.arcexp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@polcor", Value = cliente.polcor });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ultcor", Value = cliente.ultcor });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debnro", Value = cliente.debnro });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@crenro", Value = cliente.crenro });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@debglo", Value = cliente.debglo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codtip", Value = cliente.codtip });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codzon", Value = cliente.codzon });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codgrp", Value = cliente.codgrp });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codest", Value = cliente.codest });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@logusu", Value = cliente.logusu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@pai", Value = cliente.pai ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@correo", Value = cliente.correo ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@dattik", Value = cliente.dattik });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptodebacu", Value = cliente.ptodebacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptodebfch", Value = cliente.ptodebfch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptocreacu", Value = cliente.ptocreacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptocrefch", Value = cliente.ptocrefch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptovenacu", Value = cliente.ptovenacu });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ptovenfch", Value = cliente.ptovenfch });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@domnroext", Value = cliente.domnroext ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@domnroint", Value = cliente.domnroint ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@datvar", Value = cliente.datvar });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@nroctapag", Value = cliente.nroctapag ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@tipopepag", Value = cliente.tipopepag });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cveest", Value = cliente.cveest ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@cvetra", Value = cliente.cvetra ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geodat", Value = cliente.geodat ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geolat", Value = cliente.geolat });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@geolng", Value = cliente.geolng });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@taxext", Value = cliente.taxext });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@taxextid", Value = cliente.taxextid ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1cod", Value = cliente.bcomn1cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1den", Value = cliente.bcomn1den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn1cta", Value = cliente.bcomn1cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2cod", Value = cliente.bcomn2cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2den", Value = cliente.bcomn2den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcomn2cta", Value = cliente.bcomn2cta });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1cod", Value = cliente.bcome1cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1den", Value = cliente.bcome1den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome1cta", Value = cliente.bcome1cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2cod", Value = cliente.bcome2cod });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2den", Value = cliente.bcome2den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@bcome2cta", Value = cliente.bcome2cta ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfis", Value = cliente.perfis });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisnom", Value = cliente.perfisnom ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisapp", Value = cliente.perfisapp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@perfisapm", Value = cliente.perfisapm ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@curp", Value = cliente.curp ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@codrefban", Value = cliente.codrefban });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@paisat", Value = cliente.paisat ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@satuso", Value = cliente.satuso ?? (object)DBNull.Value });





                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "ControlGas");

                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED UPGRADE1 OF CLIENTES";
                        data.Status = "ERROR";
                    }
                }


                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar clientes en control gas", "WsUpdateClienteControlGas");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar clientes en control gas", "Ha ocurrido un error en la consulta al actualizar clientes en control gas", "", "", "", "");
                return Ok(mensaje);
            }



        }


        //juan
        // Este metodo es utilizado para deshabilitar  un  cliente del sistema 
        [HttpPost] [Route("WsDisabledCustomer")]
        public IHttpActionResult  WsDisabledCustomer(string cod = "0")
        {
            int DataAfectedLocal = 0;
            DataResponse data = new DataResponse();
            try
            {
                string sSelectLocaL = "";



                sSelectLocaL = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] Update [dbo].[" + GlobalesCorporativo.TablaClientes + "]  set codest='1' WHERE cod=@cod";
                var ArrParametros1 = new List<SqlParameter>();
                ArrParametros1.Add(new SqlParameter { ParameterName = "@cod", Value = cod });
                DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "ControlGas");

                if (DataAfectedLocal > 0)
                {
                    data.Message = "SUCCESSFUL DISABLED";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED DISABLED OF CUSTOMER";
                    data.Status = "ERROR";
                }



                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al deshabilitar un cliente", "WsDisabledCustomer");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al deshabilitar un cliente", "Ha ocurrido un error en la consulta al deshabilitar un cliente", "", "", "", "");
                return Ok(mensaje);
            }



        }


        // Este metodo es utilizado para insertar un nuevo departamento en la base de de datos local
        [HttpPost] [Route("WsInsertarDepartamentos")]
        public IHttpActionResult  WsInsertarDepartamentos(Departamentos departamentos)
        {

            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {


                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {

                    string sSelect = "";

                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaDepartamento + "])IF @MAXID >0 SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaDepartamento + "])   ELSE set @MAXID=1 INSERT INTO [dbo].[" + GlobalesLocal.TablaDepartamento + "] ([ID],[Departamento],[Status])VALUES(@MAXID, @Departamento,1)";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Departamento", Value = departamentos.Departamento });
                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local", departamentos, "WsInsertarDepartamentos");

                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF DEPARTMENT";
                        data.Status = "ERROR";
                    }


                }

                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {

                    string sSelect = "";
                    var d = "";

                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaDepartamento + "])IF @MAXID >0 SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaDepartamento + "])   ELSE set @MAXID=1 INSERT INTO [dbo].[" + GlobalesLocal.TablaDepartamento + "] ([ID],[Departamento],[Status])VALUES(@MAXID, @Departamento,1)";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Departamento", Value = departamentos.Departamento });
                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local");

                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF DEPARTMENT";
                        data.Status = "ERROR";
                    }


                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Insertar un departamento en la bd local", "WsInsertarDepartamentos");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Insertar un departamento en la bd local", "Ha ocurrido un error en la consulta al Insertar un departamento en la bd local", "", "", "", "");
                return Ok(mensaje);
            }



        }

        // Este metodo es utilizado para insertar un nuevo chofer en la base de datos local
        [HttpPost] [Route("WsInsertarChofer")]
        public IHttpActionResult  WsInsertarChofer(Choferes choferes)
        {
            var F = "";
            int DataAfectedLocal = 0;
            int DataAfectedTankFarm = 0;
            DataResponse data = new DataResponse();
            DataTable dT = new DataTable();
            //**********************************************************************

            //**********************************************************************

            try
            {


                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    string sSelectLocaL = "";
                    string sSelecTTankFarm = "";
                    string validartag = "";
                    var ArrParametros = new List<SqlParameter>();
                    //**********************************************************************
                    ArrParametros = new List<SqlParameter>();
                    //validartag = "select * from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where tag='" + choferes.tag + "'";
                    validartag = "select * from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where tag=@Tag";
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Tag", Value = choferes.tag ?? (object)DBNull.Value });
                    dT = BD.GetSQL(validartag, "ControlGas", ArrParametros);
                    //**********************************************************************

                    if (dT.Rows.Count > 0)
                    {
                        data.Message = "FAILED TAG ALREADY EXISTS";
                        data.Status = "DUPLICATE";
                        return Ok(data);

                    }
                    //**********************************************************************
                    var ArrParametros1 = new List<SqlParameter>();
                    //sSelecTTankFarm = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] INSERT INTO [dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] ([codcli] ,[nrocho] ,[den] ,[diacar] ,[hraini] ,[hrafin] ,[tag] ,[codest] ,[logusu] ,[logfch]) VALUES ('" + choferes.codcli + "', (SELECT nro=( CASE WHEN MAX(nrocho+1)>1 THEN (SELECT MAX(nrocho+1) as nrocho FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where codcli='" + choferes.codcli + "') ELSE 1 END) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where codcli='" + choferes.codcli + "'), '" + choferes.den + "', '" + choferes.diacar + "', '" + choferes.hraini + "', '" + choferes.hrafin + "', '" + choferes.tag + "', 0, 0, GETDATE())";
                    sSelecTTankFarm = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] INSERT INTO [dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] ([codcli] ,[nrocho] ,[den] ,[diacar] ,[hraini] ,[hrafin] ,[tag] ,[codest] ,[logusu] ,[logfch]) VALUES (@CodCli, (SELECT nro=( CASE WHEN MAX(nrocho+1)>1 THEN (SELECT MAX(nrocho+1) as nrocho FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where codcli=@CodCli) ELSE 1 END) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where codcli=@CodCli),@Den,@DiaCar,@HraIni,@HraFin,@Tag, 0, 0, GETDATE())";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@CodCli", Value = choferes.codcli });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Den", Value = choferes.den ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@DiaCar", Value = choferes.diacar });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@HraIni", Value = choferes.hraini });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@HraFin", Value = choferes.hrafin });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Tag", Value = choferes.tag ?? (object)DBNull.Value });
                    DataAfectedTankFarm = BD.SetSQL(ArrParametros1, sSelecTTankFarm, "ControlGas", choferes, "WsInsertarChofer");
                    //**********************************************************************

                    if (DataAfectedTankFarm > 0)
                    {
                        var nombreConcatenado = choferes.Nombre + choferes.LastName + choferes.SecondLastName;
                        nombreConcatenado = nombreConcatenado.Replace(" ", "");
                        //**********************************************************************
                        var ArrParametros2 = new List<SqlParameter>();
                        //sSelectLocaL = " USE [" + GlobalesLocal.BDLocal + "]  DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "])IF @MAXID >0 SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "])   ELSE set @MAXID=1 INSERT INTO [dbo].[" + GlobalesLocal.TablaChoferes + "]  ([ID],[Nombre] ,[Telefono] ,[IdCliente] ,[IdIdioma] ,[Gerente] ,[Correo], [Perfil] ,[Tag] ,[Status],[LastName], [SecondLastName], [Departamento], [CentroCostos],[NameComplete]) VALUES (@MAXID,'" + choferes.Nombre + "', '" + choferes.Telefono + "', '" + choferes.codcli + "', '" + choferes.IdIdioma + "', '" + choferes.Gerente + "', '" + choferes.Correo + "','" + choferes.Perfil + "', '" + choferes.tag + "', 0,'" + choferes.LastName + "','" + choferes.SecondLastName + "','" + choferes.Departamento + "','" + choferes.CentroCostos + "','"+nombreConcatenado+"')";
                        sSelectLocaL = " USE [" + GlobalesLocal.BDLocal + "]  DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "])IF @MAXID >0 SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "])   ELSE set @MAXID=1 INSERT INTO [dbo].[" + GlobalesLocal.TablaChoferes + "]  ([ID],[Nombre] ,[Telefono] ,[IdCliente] ,[IdIdioma] ,[Gerente] ,[Correo], [Perfil] ,[Tag] ,[Status],[LastName], [SecondLastName], [Departamento], [CentroCostos],[NameComplete]) VALUES (@MAXID,@Nombre,@Telefono,@IdCliente,@IdIdioma,@Gerente,@Correo,@Perfil,@Choferes,0,@LastName,@SecondLastName,@Departamento,@CentroCostos,@NombreConcatenado)";
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@Nombre", Value = choferes.Nombre ?? (object)DBNull.Value });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@Telefono", Value = choferes.Telefono ?? (object)DBNull.Value });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@IdCliente", Value = choferes.codcli });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@IdIdioma", Value = choferes.IdIdioma });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@Gerente", Value = choferes.Gerente ?? (object)DBNull.Value });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@Correo", Value = choferes.Correo ?? (object)DBNull.Value });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@Perfil", Value = choferes.Perfil ?? (object)DBNull.Value });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@Tag", Value = choferes.tag ?? (object)DBNull.Value });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@LastName", Value = choferes.LastName ?? (object)DBNull.Value });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@SecondLastName", Value = choferes.SecondLastName ?? (object)DBNull.Value });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@Departamento", Value = choferes.Departamento ?? (object)DBNull.Value });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@CentroCostos", Value = choferes.CentroCostos ?? (object)DBNull.Value });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@NameComplete", Value = nombreConcatenado ?? (object)DBNull.Value });
                        ArrParametros2.Add(new SqlParameter { ParameterName = "@NombreConcatenado", Value = nombreConcatenado ?? (object)DBNull.Value });
                        DataAfectedLocal = BD.SetSQL(ArrParametros2, sSelectLocaL, "Local");
                        //**********************************************************************

                        if (DataAfectedLocal > 0)
                        {
                            //SendWS(choferes, "WsInsertarChofer");
                            data.Message = "SUCCESSFUL INSERTION";
                            data.Status = "OK";
                        }
                        else
                        {

                            data.Message = "FAILED INSERTION OF DRIVER";
                            data.Status = "ERROR";
                        }

                    }
                    else
                    {

                    }
                }

                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {
                    string sSelectLocaL = "";
                    string sSelecTTankFarm = "";
                    string validartag = "";
                    //**********************************************************************
                    var ArrParametros3 = new List<SqlParameter>();
                    //validartag = "select * from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where tag='" + choferes.tag + "'";
                    validartag = "select * from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where tag=@Tag";
                    ArrParametros3.Add(new SqlParameter { ParameterName = "@Tag", Value = choferes.tag ?? (object)DBNull.Value });
                    dT = BD.GetSQL(validartag, "ControlGas", ArrParametros3);
                    //**********************************************************************
                    if (dT.Rows.Count > 0)
                    {
                        data.Message = "FAILED TAG ALREADY EXISTS";
                        data.Status = "DUPLICATE";
                        return Ok(data);

                    }
                    //**********************************************************************
                    var ArrParametros4 = new List<SqlParameter>();
                    //sSelecTTankFarm = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] INSERT INTO [dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] ([codcli] ,[nrocho] ,[den] ,[diacar] ,[hraini] ,[hrafin] ,[tag] ,[codest] ,[logusu] ,[logfch]) VALUES ('" + choferes.codcli + "', (SELECT nro=( CASE WHEN MAX(nrocho+1)>1 THEN (SELECT MAX(nrocho+1) as nrocho FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where codcli='" + choferes.codcli + "') ELSE 1 END) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where codcli='" + choferes.codcli + "'), '" + choferes.den + "', '" + choferes.diacar + "', '" + choferes.hraini + "', '" + choferes.hrafin + "', '" + choferes.tag + "', 0, 0, GETDATE())";
                    sSelecTTankFarm = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] INSERT INTO [dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] ([codcli] ,[nrocho] ,[den] ,[diacar] ,[hraini] ,[hrafin] ,[tag] ,[codest] ,[logusu] ,[logfch]) VALUES (@CodCli, (SELECT nro=( CASE WHEN MAX(nrocho+1)>1 THEN (SELECT MAX(nrocho+1) as nrocho FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where codcli=@CodCli) ELSE 1 END) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] where codcli=@CodCli), @Den, @DiaCar, @HraIni,@HraFin,@Tag, 0, 0, GETDATE())";
                    //[codcli] ,[nrocho] ,[den] ,[diacar] ,[hraini] ,[hrafin] ,[tag] ,[codest] ,[logusu] ,[logfch]

                    ArrParametros4.Add(new SqlParameter { ParameterName = "@CodCli", Value = choferes.codcli });
                    ArrParametros4.Add(new SqlParameter { ParameterName = "@Den", Value = choferes.den ?? (object)DBNull.Value });
                    ArrParametros4.Add(new SqlParameter { ParameterName = "@DiaCar", Value = choferes.diacar });
                    ArrParametros4.Add(new SqlParameter { ParameterName = "@HraIni", Value = choferes.hraini });
                    ArrParametros4.Add(new SqlParameter { ParameterName = "@HraFin", Value = choferes.hrafin });
                    ArrParametros4.Add(new SqlParameter { ParameterName = "@Tag", Value = choferes.tag ?? (object)DBNull.Value });

                    DataAfectedTankFarm = BD.SetSQL(ArrParametros4, sSelecTTankFarm, "ControlGas");
                    //**********************************************************************

                    if (DataAfectedTankFarm > 0)
                    {
                        var nombreConcatenado = choferes.Nombre + choferes.LastName + choferes.SecondLastName;
                        nombreConcatenado = nombreConcatenado.Replace(" ", "");
                        //**********************************************************************
                        var ArrParametros5 = new List<SqlParameter>();
                        //sSelectLocaL = " USE [" + GlobalesLocal.BDLocal + "]  DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "])IF @MAXID >0 SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "])   ELSE set @MAXID=1 INSERT INTO [dbo].[" + GlobalesLocal.TablaChoferes + "]  ([ID],[Nombre] ,[Telefono] ,[IdCliente] ,[IdIdioma] ,[Gerente] ,[Correo], [Perfil] ,[Tag] ,[Status],[LastName], [SecondLastName], [Departamento], [CentroCostos],[NameComplete]) VALUES (@MAXID,'" + choferes.Nombre + "', '" + choferes.Telefono + "', '" + choferes.codcli + "', '" + choferes.IdIdioma + "', '" + choferes.Gerente + "', '" + choferes.Correo + "','" + choferes.Perfil + "', '" + choferes.tag + "', 0,'" + choferes.LastName + "','" + choferes.SecondLastName + "','" + choferes.Departamento + "','" + choferes.CentroCostos + "','" + nombreConcatenado + "')";
                        sSelectLocaL = " USE [" + GlobalesLocal.BDLocal + "]  DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "])IF @MAXID >0 SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "])   ELSE set @MAXID=1 INSERT INTO [dbo].[" + GlobalesLocal.TablaChoferes + "]  ([ID],[Nombre] ,[Telefono] ,[IdCliente] ,[IdIdioma] ,[Gerente] ,[Correo], [Perfil] ,[Tag] ,[Status],[LastName], [SecondLastName], [Departamento], [CentroCostos],[NameComplete]) VALUES (@MAXID,@Nombre,@Telefono,@IdCliente,@IdIdioma,@Gerente,@Correo,@Perfil,@Tag, 0,@LastName,@SecondLastName,@Departamento,@CentroCostos,@NombreConcatenado)";
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@Nombre", Value = choferes.Nombre ?? (object)DBNull.Value });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@Telefono", Value = choferes.Telefono ?? (object)DBNull.Value });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@IdCliente", Value = choferes.codcli });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@IdIdioma", Value = choferes.IdIdioma });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@Gerente", Value = choferes.Gerente ?? (object)DBNull.Value });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@Correo", Value = choferes.Correo ?? (object)DBNull.Value });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@Perfil", Value = choferes.Perfil ?? (object)DBNull.Value });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@Tag", Value = choferes.tag ?? (object)DBNull.Value });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@LastName", Value = choferes.LastName ?? (object)DBNull.Value });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@SecondLastName", Value = choferes.SecondLastName ?? (object)DBNull.Value });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@Departamento", Value = choferes.Departamento ?? (object)DBNull.Value });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@CentroCostos", Value = choferes.CentroCostos ?? (object)DBNull.Value });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@NameComplete", Value = nombreConcatenado ?? (object)DBNull.Value });
                        ArrParametros5.Add(new SqlParameter { ParameterName = "@NombreConcatenado", Value = nombreConcatenado ?? (object)DBNull.Value });


                        DataAfectedLocal = BD.SetSQL(ArrParametros5, sSelectLocaL, "Local");
                        //**********************************************************************
                        if (DataAfectedLocal > 0)
                        {

                            data.Message = "SUCCESSFUL INSERTION";
                            data.Status = "OK";
                        }
                        else
                        {

                            data.Message = "FAILED INSERTION OF DRIVER";
                            data.Status = "ERROR";
                        }

                    }
                    else
                    {

                    }
                }

                return Ok(data);

            }
            catch (Exception e)
            {

                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Insertar un chofer", "WsInsertarChofer");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Insertar un chofer", "Ha ocurrido un error en la consulta al Insertar un chofer", "", "", "", "");
                return Ok(mensaje);
            }



        }

        [Route("GetVehiculos")]
        public IHttpActionResult  GetVehiculos(string id = "0", string tag = "0", string all = "")

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
                //return Ok(dT.Rows);
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
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al obtener los vehiculos", "GetVehiculos");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al obtener los vehiculos", "Ha ocurrido un error en la consulta al obtener los vehiculos", "", "", "", "");
                return Ok(mensaje);
            }



        }

        //Este ws es utilizado para actualizar un vehiculo en la base de datos de control Gas
        [HttpPost] [Route("WsUpdateVehiculoControlGas")]
        public IHttpActionResult  WsUpdateVehiculoControlGas(Vehiculo vehiculos)
        {

            //**********************************************************************
            var ArrParametros = new List<SqlParameter>();
            //**********************************************************************

            try
            {
                DataResponse data = new DataResponse();

                List<Vehiculo> lstVehiculos = new List<Vehiculo>();


                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    int DataAfected = 0;

                    string sSelect = "";
                    //**********************************************************************
                    ArrParametros = new List<SqlParameter>();
                    //sSelect = "DECLARE @var1 varchar(30);Declare @res varchar(30);Declare @nro varchar(30);Declare @numve varchar(30);Declare @resuNumve varchar(30);(SELECT @numve= MAX(nroveh+1) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where codcli='" + vehiculos.codcli + "' )(select @var1=codcli from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where tag='" + vehiculos.tag + "') set @resuNumve=( SELECT CASE When @numve >0 Then @numve ELSE 1 END ) IF @var1='" + vehiculos.codcli + "' set @res='SI'ELSE set @res='NO'set @nro=(Select CASE WHEN @res='SI' THEN (select nroveh from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where tag='" + vehiculos.tag + "') Else (@resuNumve) END Designation) USE [" + GlobalesCorporativo.BDCorporativoModerno + "]  UPDATE [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]SET codcli='" + vehiculos.codcli + "', nroveh= @nro , [plc] = '" + vehiculos.plc + "' ,[den] = '" + vehiculos.den + "' ,[rsp] = '" + vehiculos.rsp + "' ,[grp] = '" + vehiculos.grp + "' ,[diacar] = '" + vehiculos.diacar + "' ,[hraini] = '" + vehiculos.hraini + "' ,[hrafin] = '" + vehiculos.hrafin + "' ,[carmax] = '" + vehiculos.carmax + "' ,[candia] = '" + vehiculos.candia + "' ,[cansem] = '" + vehiculos.cansem + "' ,[canmes] = '" + vehiculos.canmes + "' ,[acudia] = '" + vehiculos.acudia + "' ,[acusem] = '" + vehiculos.acusem + "' ,[acumes] = '" + vehiculos.acumes + "' ,[ultcar] = '" + vehiculos.ultcar + "' ,[ultodm] = '" + vehiculos.ultodm + "' ,[codgas] = '" + vehiculos.codgas + "' ,[codprd] = '" + vehiculos.codprd + "' ,[debsdo] = '" + vehiculos.debsdo + "' ,[debfch] = '" + vehiculos.debfch + "' ,[debnro] = '" + vehiculos.debnro + "' ,[debcan] = '" + vehiculos.debcan + "' ,[nip] = '" + vehiculos.nip + "' ,[ptosdo] = '" + vehiculos.ptosdo + "' ,[ptofch] = '" + vehiculos.ptofch + "' ,[ptocan] = '" + vehiculos.ptocan + "' ,[premto] = '" + vehiculos.premto + "' ,[prepgo] = '" + vehiculos.prepgo + "' ,[prefid] = '" + vehiculos.prefid + "' ,[cnvemp] = '" + vehiculos.cnvemp + "' ,[cnvobs] = '" + vehiculos.cnvobs + "' ,[cnvfch] = '" + vehiculos.cnvfch + "' ,[manobs] = '" + vehiculos.manobs + "' ,[manper] = '" + vehiculos.manper + "' ,[manult] = '" + vehiculos.manult + "' ,[rut] = '" + vehiculos.rut + "' ,[vto] = '" + vehiculos.vto + "' ,[limtur] = '" + vehiculos.limtur + "' ,[ulttur] = '" + vehiculos.ulttur + "' ,[acutur] = '" + vehiculos.acutur + "' ,[limprd] = '" + vehiculos.limprd + "' ,[acuprd] = '" + vehiculos.acuprd + "' ,[crefch] = '" + vehiculos.crefch + "' ,[crenro] = '" + vehiculos.crenro + "' ,[crecan] = '" + vehiculos.crecan + "' ,[crefch2] = '" + vehiculos.crefch2 + "' ,[crenro2] = '" + vehiculos.crenro2 + "' ,[crecan2] = '" + vehiculos.crecan2 + "' ,[debfch2] = '" + vehiculos.debfch2 + "' ,[debnro2] = '" + vehiculos.debnro2 + "' ,[debcan2] = '" + vehiculos.debcan2 + "' ,[est] = 1 ,[niplog] = GETDATE() ,[logusu] = 0 ,[logfch] = GETDATE() ,[lognew] = GETDATE() ,[tagadi] = '" + vehiculos.tagadi + "' ,[ctapre] = '" + vehiculos.ctapre + "' ,[nropat] = '" + vehiculos.nropat + "' ,[nroeco] = '" + vehiculos.nroeco + "' ,[hraini2] = '" + vehiculos.hraini2 + "' ,[hrafin2] = '" + vehiculos.hrafin2 + "' ,[hraini3] = '" + vehiculos.hraini3 + "' ,[hrafin3] = '" + vehiculos.hrafin3 + "' ,[aju] = '" + vehiculos.aju + "' ,[ptodebacu] = '" + vehiculos.ptodebacu + "' ,[ptodebfch] = '" + vehiculos.ptodebfch + "' ,[ptocreacu] = '" + vehiculos.ptocreacu + "' ,[ptocrefch] = '" + vehiculos.ptocrefch + "' ,[ptovenacu] = '" + vehiculos.ptovenacu + "' ,[ptovenfch] = '" + vehiculos.ptovenfch + "' ,[tagex1] = '" + vehiculos.tagex1 + "' ,[tagex2] = '" + vehiculos.tagex2 + "' ,[tagex3] = '" + vehiculos.tagex3 + "' ,[ultcan] = '" + vehiculos.ultcan + "' ,[datvar] = '" + vehiculos.datvar + "' ,[catprd] = '" + vehiculos.catprd + "' ,[catuni] = '" + vehiculos.catuni + "' ,[dialim] = '" + vehiculos.dialim + "' WHERE   [tag]='" + vehiculos.tag + "'";
                    sSelect = "DECLARE @var1 varchar(30);Declare @res varchar(30);Declare @nro varchar(30);Declare @numve varchar(30);Declare @resuNumve varchar(30);(SELECT @numve= MAX(nroveh+1) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where codcli=@CodCli )(select @var1=codcli from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where tag=@Tag) set @resuNumve=( SELECT CASE When @numve >0 Then @numve ELSE 1 END ) IF @var1=@CodCli set @res='SI'ELSE set @res='NO'set @nro=(Select CASE WHEN @res='SI' THEN (select nroveh from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where tag=@Tag) Else (@resuNumve) END Designation) USE [" + GlobalesCorporativo.BDCorporativoModerno + "]  UPDATE [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]SET codcli=@CodCli, nroveh= @nro , [plc] = @PLC ,[den] = @Den ,[rsp] = @RSP ,[grp] = @GRP ,[diacar] = @DiaCar ,[hraini] = @HraIni ,[hrafin] = @HraFin ,[carmax] = @CarMax ,[candia] = @CanDia ,[cansem] = @CanSem ,[canmes] = @CanMes ,[acudia] = @AcuDia ,[acusem] = @AcuSem ,[acumes] = @AcuMes ,[ultcar] =@UltCar ,[ultodm] = @UltOdm ,[codgas] = @CodGas ,[codprd] = @CodProd ,[debsdo] = @DebSdo ,[debfch] = @DebFch ,[debnro] = @DebNro ,[debcan] = @DebCan ,[nip] = @Nip ,[ptosdo] = @PtoSdo ,[ptofch] = @PtoFch ,[ptocan] = @PtoCan ,[premto] = @PreMto ,[prepgo] = @PrePgo ,[prefid] = @PrefID ,[cnvemp] = @CnvEmp ,[cnvobs] = @CnvObs ,[cnvfch] = @CnvFch ,[manobs] = @ManObs ,[manper] = @ManPer ,[manult] = @ManUlt ,[rut] = @Rut ,[vto] = @Vto ,[limtur] = @LimTur ,[ulttur] = @UltTur ,[acutur] = @AcuTur ,[limprd] = @LimPrd ,[acuprd] = @AcuPrd ,[crefch] = @CreFch ,[crenro] = @CreNro ,[crecan] = @CreCan ,[crefch2] = @CreFch2 ,[crenro2] = CreNro2 ,[crecan2] = @CreCan2 ,[debfch2] = DebFch2 ,[debnro2] = @DebNro2 ,[debcan2] = @DebCan2 ,[est] = 1 ,[niplog] = GETDATE() ,[logusu] = 0 ,[logfch] = GETDATE() ,[lognew] = GETDATE() ,[tagadi] = @TagAdi ,[ctapre] = @CtaPre ,[nropat] = @NroPat ,[nroeco] = @NroEco ,[hraini2] = @HraIni2 ,[hrafin2] = @HraFin2 ,[hraini3] = @HraIni3 ,[hrafin3] = @HraFin3 ,[aju] = @Aju ,[ptodebacu] = @PtoDebAcu ,[ptodebfch] = @PtoDebFch ,[ptocreacu] = @PtoCreAcu ,[ptocrefch] = @PtoCreFch ,[ptovenacu] = @PtoVenAcu ,[ptovenfch] = @PtoVenFch ,[tagex1] = @TagEx1 ,[tagex2] = @TagEx2 ,[tagex3] = @TagEx3 ,[ultcan] = @UltCan ,[datvar] = @DatVar ,[catprd] = @CatPrd ,[catuni] = @CatUni ,[dialim] = @DiaLim WHERE   [tag]=@Tag";

                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodCli", Value = vehiculos.codcli });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Tag", Value = vehiculos.tag ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PLC", Value = vehiculos.plc ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Den", Value = vehiculos.den ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@RSP", Value = vehiculos.rsp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@GRP", Value = vehiculos.grp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DiaCar", Value = vehiculos.diacar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni", Value = vehiculos.hraini });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin", Value = vehiculos.hrafin });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CarMax", Value = vehiculos.carmax });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanDia", Value = vehiculos.candia });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanSem", Value = vehiculos.cansem });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanMes", Value = vehiculos.canmes });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuDia", Value = vehiculos.acudia });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuSem", Value = vehiculos.acusem });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuMes", Value = vehiculos.acumes });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltCar", Value = vehiculos.ultcar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltOdm", Value = vehiculos.ultodm });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodGas", Value = vehiculos.codgas });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodProd", Value = vehiculos.codprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebSdo", Value = vehiculos.debsdo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebFch", Value = vehiculos.debfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebNro", Value = vehiculos.debnro });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebCan", Value = vehiculos.debcan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Nip", Value = vehiculos.nip });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoSdo", Value = vehiculos.ptosdo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoFch", Value = vehiculos.ptofch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCan", Value = vehiculos.ptocan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PreMto", Value = vehiculos.premto });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PrePgo", Value = vehiculos.prepgo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PrefID", Value = vehiculos.prefid });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvEmp", Value = vehiculos.cnvemp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvObs", Value = vehiculos.cnvobs ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvFch", Value = vehiculos.cnvfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManObs", Value = vehiculos.manobs ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManPer", Value = vehiculos.manper });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManUlt", Value = vehiculos.manult });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Rut", Value = vehiculos.rut ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Vto", Value = vehiculos.vto });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@LimTur", Value = vehiculos.limtur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltTur", Value = vehiculos.ulttur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuTur", Value = vehiculos.acutur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@LimPrd", Value = vehiculos.limprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuPrd", Value = vehiculos.acuprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreFch", Value = vehiculos.crefch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreNro", Value = vehiculos.crenro });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreCan", Value = vehiculos.crecan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreFch2", Value = vehiculos.crefch2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreNro2", Value = vehiculos.crenro2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreCan2", Value = vehiculos.crecan2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebFch2", Value = vehiculos.debfch2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebNro2", Value = vehiculos.debnro2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebCan2", Value = vehiculos.debcan2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagAdi", Value = vehiculos.tagadi ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CtaPre", Value = vehiculos.ctapre ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@NroPat", Value = vehiculos.nropat ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@NroEco", Value = vehiculos.nroeco ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni2", Value = vehiculos.hraini2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin2", Value = vehiculos.hrafin2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni3", Value = vehiculos.hraini3 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin3", Value = vehiculos.hrafin3 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Aju", Value = vehiculos.aju });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoDebAcu", Value = vehiculos.ptodebacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoDebFch", Value = vehiculos.ptodebfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCreAcu", Value = vehiculos.ptocreacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCreFch", Value = vehiculos.ptocrefch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoVenAcu", Value = vehiculos.ptovenacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoVenFch", Value = vehiculos.ptovenfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx1", Value = vehiculos.tagex1 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx2", Value = vehiculos.tagex2 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx3", Value = vehiculos.tagex3 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltCan", Value = vehiculos.ultcan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DatVar", Value = vehiculos.datvar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CatPrd", Value = vehiculos.catprd ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CatUni", Value = vehiculos.catuni ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DiaLim", Value = vehiculos.dialim ?? (object)DBNull.Value });

                    DataAfected = BD.SetSQL(ArrParametros, sSelect, "ControlGas", vehiculos, "WsUpdateVehiculoControlGas");
                    //**********************************************************************

                    if (DataAfected > 0)
                    {
                        //SendWS(vehiculos, "WsUpdateVehiculoControlGas");
                        data.Message = "SUCCESSFUL UPGRADE";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED UPGRADE OF VEHICLES";
                        data.Status = "ERROR";
                    }
                }

                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {
                    int DataAfected = 0;

                    string sSelect = "";
                    //**********************************************************************
                    ArrParametros = new List<SqlParameter>();
                    //sSelect = "DECLARE @var1 varchar(30);Declare @res varchar(30);Declare @nro varchar(30);Declare @numve varchar(30);Declare @resuNumve varchar(30);(SELECT @numve= MAX(nroveh+1) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where codcli='" + vehiculos.codcli + "' )(select @var1=codcli from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where tag='" + vehiculos.tag + "') set @resuNumve=( SELECT CASE When @numve >0 Then @numve ELSE 1 END ) IF @var1='" + vehiculos.codcli + "' set @res='SI'ELSE set @res='NO'set @nro=(Select CASE WHEN @res='SI' THEN (select nroveh from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where tag='" + vehiculos.tag + "') Else (@resuNumve) END Designation) USE [" + GlobalesCorporativo.BDCorporativoModerno + "]  UPDATE [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]SET codcli='" + vehiculos.codcli + "', nroveh= @nro , [plc] = '" + vehiculos.plc + "' ,[den] = '" + vehiculos.den + "' ,[rsp] = '" + vehiculos.rsp + "' ,[grp] = '" + vehiculos.grp + "' ,[diacar] = '" + vehiculos.diacar + "' ,[hraini] = '" + vehiculos.hraini + "' ,[hrafin] = '" + vehiculos.hrafin + "' ,[carmax] = '" + vehiculos.carmax + "' ,[candia] = '" + vehiculos.candia + "' ,[cansem] = '" + vehiculos.cansem + "' ,[canmes] = '" + vehiculos.canmes + "' ,[acudia] = '" + vehiculos.acudia + "' ,[acusem] = '" + vehiculos.acusem + "' ,[acumes] = '" + vehiculos.acumes + "' ,[ultcar] = '" + vehiculos.ultcar + "' ,[ultodm] = '" + vehiculos.ultodm + "' ,[codgas] = '" + vehiculos.codgas + "' ,[codprd] = '" + vehiculos.codprd + "' ,[debsdo] = '" + vehiculos.debsdo + "' ,[debfch] = '" + vehiculos.debfch + "' ,[debnro] = '" + vehiculos.debnro + "' ,[debcan] = '" + vehiculos.debcan + "' ,[nip] = '" + vehiculos.nip + "' ,[ptosdo] = '" + vehiculos.ptosdo + "' ,[ptofch] = '" + vehiculos.ptofch + "' ,[ptocan] = '" + vehiculos.ptocan + "' ,[premto] = '" + vehiculos.premto + "' ,[prepgo] = '" + vehiculos.prepgo + "' ,[prefid] = '" + vehiculos.prefid + "' ,[cnvemp] = '" + vehiculos.cnvemp + "' ,[cnvobs] = '" + vehiculos.cnvobs + "' ,[cnvfch] = '" + vehiculos.cnvfch + "' ,[manobs] = '" + vehiculos.manobs + "' ,[manper] = '" + vehiculos.manper + "' ,[manult] = '" + vehiculos.manult + "' ,[rut] = '" + vehiculos.rut + "' ,[vto] = '" + vehiculos.vto + "' ,[limtur] = '" + vehiculos.limtur + "' ,[ulttur] = '" + vehiculos.ulttur + "' ,[acutur] = '" + vehiculos.acutur + "' ,[limprd] = '" + vehiculos.limprd + "' ,[acuprd] = '" + vehiculos.acuprd + "' ,[crefch] = '" + vehiculos.crefch + "' ,[crenro] = '" + vehiculos.crenro + "' ,[crecan] = '" + vehiculos.crecan + "' ,[crefch2] = '" + vehiculos.crefch2 + "' ,[crenro2] = '" + vehiculos.crenro2 + "' ,[crecan2] = '" + vehiculos.crecan2 + "' ,[debfch2] = '" + vehiculos.debfch2 + "' ,[debnro2] = '" + vehiculos.debnro2 + "' ,[debcan2] = '" + vehiculos.debcan2 + "' ,[est] = 1 ,[niplog] = GETDATE() ,[logusu] = 0 ,[logfch] = GETDATE() ,[lognew] = GETDATE() ,[tagadi] = '" + vehiculos.tagadi + "' ,[ctapre] = '" + vehiculos.ctapre + "' ,[nropat] = '" + vehiculos.nropat + "' ,[nroeco] = '" + vehiculos.nroeco + "' ,[hraini2] = '" + vehiculos.hraini2 + "' ,[hrafin2] = '" + vehiculos.hrafin2 + "' ,[hraini3] = '" + vehiculos.hraini3 + "' ,[hrafin3] = '" + vehiculos.hrafin3 + "' ,[aju] = '" + vehiculos.aju + "' ,[ptodebacu] = '" + vehiculos.ptodebacu + "' ,[ptodebfch] = '" + vehiculos.ptodebfch + "' ,[ptocreacu] = '" + vehiculos.ptocreacu + "' ,[ptocrefch] = '" + vehiculos.ptocrefch + "' ,[ptovenacu] = '" + vehiculos.ptovenacu + "' ,[ptovenfch] = '" + vehiculos.ptovenfch + "' ,[tagex1] = '" + vehiculos.tagex1 + "' ,[tagex2] = '" + vehiculos.tagex2 + "' ,[tagex3] = '" + vehiculos.tagex3 + "' ,[ultcan] = '" + vehiculos.ultcan + "' ,[datvar] = '" + vehiculos.datvar + "' ,[catprd] = '" + vehiculos.catprd + "' ,[catuni] = '" + vehiculos.catuni + "' ,[dialim] = '" + vehiculos.dialim + "' WHERE   [tag]='" + vehiculos.tag + "'";
                    sSelect = "DECLARE @var1 varchar(30);Declare @res varchar(30);Declare @nro varchar(30);Declare @numve varchar(30);Declare @resuNumve varchar(30);(SELECT @numve= MAX(nroveh+1) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where codcli=@CodCli )(select @var1=codcli from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where tag=@Tag) set @resuNumve=( SELECT CASE When @numve >0 Then @numve ELSE 1 END ) IF @var1=@CodCli set @res='SI'ELSE set @res='NO'set @nro=(Select CASE WHEN @res='SI' THEN (select nroveh from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where tag=@Tag) Else (@resuNumve) END Designation) USE [" + GlobalesCorporativo.BDCorporativoModerno + "]  UPDATE [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]SET codcli=@CodCli, nroveh= @nro , [plc] = @PLC ,[den] = @Den ,[rsp] = @RSP, [grp] = @GRP,[diacar] = @DiaCar,[hraini] =@HraIni,[hrafin] = @HraFin,[carmax] = @CarMax,[candia] = @CanDia, [cansem] = @CanSem,[canmes] = @CanMes,[acudia] = @AcuDia ,[acusem] = @AcuSem ,[acumes] = @AcuMes ,[ultcar] = @UltCar ,[ultodm] = @UltOdm ,[codgas] = @CodGas ,[codprd] = @CodProd ,[debsdo] = @DebSdo ,[debfch] = @DebFch ,[debnro] = @DebNro ,[debcan] = @DebCan ,[nip] = @Nip ,[ptosdo] = @PtoSdo ,[ptofch] = @PtoFch ,[ptocan] = @PtoCan ,[premto] = @PreMto ,[prepgo] = @PrePgo ,[prefid] = @PrefID ,[cnvemp] = @CnvEmp ,[cnvobs] = @CnvObs ,[cnvfch] = @CnvFch ,[manobs] = @ManObs ,[manper] = @ManPer ,[manult] = @ManUlt ,[rut] = @Rut ,[vto] = @Vto ,[limtur] = @Limtur ,[ulttur] = @UltTur ,[acutur] = @AcuTur ,[limprd] = @LimPrd ,[acuprd] = @AcuPrd ,[crefch] = @CreFch ,[crenro] = @CreNro ,[crecan] = @CreCan ,[crefch2] = @CreFch2 ,[crenro2] = @CreNro2 ,[crecan2] = @CreCan2 ,[debfch2] = @DebFch2 ,[debnro2] = @DebNro2 ,[debcan2] = @DebCan2 ,[est] = 1 ,[niplog] = GETDATE() ,[logusu] = 0 ,[logfch] = GETDATE() ,[lognew] = GETDATE() ,[tagadi] = @TagAdi ,[ctapre] = @CtaPre ,[nropat] = @NroPat ,[nroeco] = @NroEco ,[hraini2] = @HraIni2 ,[hrafin2] = @HraFin2 ,[hraini3] = @HraIni3 ,[hrafin3] = @HraFin3 ,[aju] = @Aju ,[ptodebacu] = @PtoDebAcu ,[ptodebfch] = @PtoDebFch ,[ptocreacu] = @PtoCreAcu ,[ptocrefch] = @PtoCreFch ,[ptovenacu] = @PtoVenAcu ,[ptovenfch] = @PtoVenFch ,[tagex1] = @TagEx1 ,[tagex2] = @TagEx2 ,[tagex3] = @TagEx3 ,[ultcan] = @UltCan ,[datvar] = @DatVar ,[catprd] = @CatPrd ,[catuni] = @CatUni ,[dialim] = @DiaLim WHERE   [tag]=@Tag";

                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodCli", Value = vehiculos.codcli });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Tag", Value = vehiculos.tag ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PLC", Value = vehiculos.plc ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Den", Value = vehiculos.den ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@RSP", Value = vehiculos.rsp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@GRP", Value = vehiculos.grp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DiaCar", Value = vehiculos.diacar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni", Value = vehiculos.hraini });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin", Value = vehiculos.hrafin });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CarMax", Value = vehiculos.carmax });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanDia", Value = vehiculos.candia });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanSem", Value = vehiculos.cansem });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanMes", Value = vehiculos.canmes });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuDia", Value = vehiculos.acudia });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuSem", Value = vehiculos.acusem });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuMes", Value = vehiculos.acumes });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltCar", Value = vehiculos.ultcar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltOdm", Value = vehiculos.ultodm });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodGas", Value = vehiculos.codgas });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodProd", Value = vehiculos.codprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebSdo", Value = vehiculos.debsdo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebFch", Value = vehiculos.debfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebNro", Value = vehiculos.debnro });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebCan", Value = vehiculos.debcan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Nip", Value = vehiculos.nip });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoSdo", Value = vehiculos.ptosdo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoFch", Value = vehiculos.ptofch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCan", Value = vehiculos.ptocan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PreMto", Value = vehiculos.premto });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PrePgo", Value = vehiculos.prepgo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PrefID", Value = vehiculos.prefid });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvEmp", Value = vehiculos.cnvemp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvObs", Value = vehiculos.cnvobs ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvFch", Value = vehiculos.cnvfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManObs", Value = vehiculos.manobs ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManPer", Value = vehiculos.manper });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManUlt", Value = vehiculos.manult });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Rut", Value = vehiculos.rut ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Vto", Value = vehiculos.vto });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@LimTur", Value = vehiculos.limtur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltTur", Value = vehiculos.ulttur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuTur", Value = vehiculos.acutur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@LimPrd", Value = vehiculos.limprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuPrd", Value = vehiculos.acuprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreFch", Value = vehiculos.crefch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreNro", Value = vehiculos.crenro });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreCan", Value = vehiculos.crecan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreFch2", Value = vehiculos.crefch2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreNro2", Value = vehiculos.crenro2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreCan2", Value = vehiculos.crecan2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebFch2", Value = vehiculos.debfch2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebNro2", Value = vehiculos.debnro2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebCan2", Value = vehiculos.debcan2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagAdi", Value = vehiculos.tagadi ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CtaPre", Value = vehiculos.ctapre ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@NroPat", Value = vehiculos.nropat ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@NroEco", Value = vehiculos.nroeco ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni2", Value = vehiculos.hraini2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin2", Value = vehiculos.hrafin2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni3", Value = vehiculos.hraini3 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin3", Value = vehiculos.hrafin3 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Aju", Value = vehiculos.aju });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoDebAcu", Value = vehiculos.ptodebacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoDebFch", Value = vehiculos.ptodebfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCreAcu", Value = vehiculos.ptocreacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCreFch", Value = vehiculos.ptocrefch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoVenAcu", Value = vehiculos.ptovenacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoVenFch", Value = vehiculos.ptovenfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx1", Value = vehiculos.tagex1 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx2", Value = vehiculos.tagex2 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx3", Value = vehiculos.tagex3 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltCan", Value = vehiculos.ultcan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DatVar", Value = vehiculos.datvar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CatPrd", Value = vehiculos.catprd ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CatUni", Value = vehiculos.catuni ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DiaLim", Value = vehiculos.dialim ?? (object)DBNull.Value });




                    DataAfected = BD.SetSQL(ArrParametros, sSelect, "ControlGas");
                    //**********************************************************************

                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL UPGRADE";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED UPGRADE OF VEHICLES";
                        data.Status = "ERROR";
                    }
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar lo vehiculos en control gas", "WsUpdateVehiculoControlGas");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar lo vehiculos en control gas", "Ha ocurrido un error en la consulta al actualizar lo vehiculos en control gas", "", "", "", "");
                return Ok(mensaje);
            }



        }

        // Este metodo es utilizado para insertar un nuevo vehiculo en la base de datos de tank farm
        [HttpPost] [Route("WsInsertarVehiculoControlGas")]
        public IHttpActionResult  WsInsertarVehiculoControlGas(Vehiculo vehiculos)
        {

            int DataAfected = 0;
            DataResponse data = new DataResponse();
            //**********************************************************************
            var ArrParametros = new List<SqlParameter>();
            //**********************************************************************
            try
            {

                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    string sSelect = "";
                    var d = "";
                    //**********************************************************************
                    ArrParametros = new List<SqlParameter>();
                    //sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] INSERT INTO [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]([codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp] ,[cnvobs] ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim]) " +
                    //"VALUES ('" + vehiculos.codcli + "' , (SELECT nro=( CASE WHEN MAX(nroveh+1)>1 THEN (SELECT MAX(nroveh+1) as nroveh FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where codcli='" + vehiculos.codcli + "') ELSE 1 END) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] where codcli='" + vehiculos.codcli + "') ,(select COUNT( * )+1 from [" + GlobalesCorporativo.BDCorporativoModerno + "]. [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]) ,'" + vehiculos.plc + "' ,'" + vehiculos.den + "' ,'" + vehiculos.rsp + "' ,'" + vehiculos.grp + "' ,'" + vehiculos.diacar + "' ,'" + vehiculos.hraini + "' ,'" + vehiculos.hrafin + "' ,'" + vehiculos.carmax + "' ,'" + vehiculos.candia + "' ,'" + vehiculos.cansem + "' ,'" + vehiculos.canmes + "' ,'" + vehiculos.acudia + "' ,'" + vehiculos.acusem + "' ,'" + vehiculos.acumes + "' ,'" + vehiculos.ultcar + "' ,'" + vehiculos.ultodm + "' ,'" + vehiculos.codgas + "' ,'" + vehiculos.codprd + "' ,'" + vehiculos.debsdo + "' ,'" + vehiculos.debfch + "' ,'" + vehiculos.debnro + "' ,'" + vehiculos.debcan + "' ,'" + vehiculos.nip + "' ,'" + vehiculos.ptosdo + "' ,'" + vehiculos.ptofch + "' ,'" + vehiculos.ptocan + "' ,'" + vehiculos.premto + "' ,'" + vehiculos.prepgo + "' ,'" + vehiculos.prefid + "' ,'" + vehiculos.cnvemp + "' ,'" + vehiculos.cnvobs + "' ,'" + vehiculos.cnvfch + "' ,'" + vehiculos.manobs + "' ,'" + vehiculos.manper + "' ,'" + vehiculos.manult + "' ,'" + vehiculos.rut + "' ,'" + vehiculos.tag + "' ,'" + vehiculos.vto + "' ,'" + vehiculos.limtur + "' ,'" + vehiculos.ulttur + "' ,'" + vehiculos.acutur + "' ,'" + vehiculos.limprd + "' ,'" + vehiculos.acuprd + "' ,'" + vehiculos.crefch + "' ,'" + vehiculos.crenro + "' ,'" + vehiculos.crecan + "' ,'" + vehiculos.crefch2 + "' ,'" + vehiculos.crenro2 + "' ,'" + vehiculos.crecan2 + "' ,'" + vehiculos.debfch2 + "' ,'" + vehiculos.debnro2 + "' ,'" + vehiculos.debcan2 + "' ,1 ,GETDATE() ,0 ,GETDATE() ,GETDATE() ,'" + vehiculos.tagadi + "' ,'" + vehiculos.ctapre + "' ,'" + vehiculos.nropat + "' ,'" + vehiculos.nroeco + "' ,'" + vehiculos.hraini2 + "' ,'" + vehiculos.hrafin2 + "' ,'" + vehiculos.hraini3 + "' ,'" + vehiculos.hrafin3 + "' ,'" + vehiculos.aju + "' ,'" + vehiculos.ptodebacu + "' ,'" + vehiculos.ptodebfch + "' ,'" + vehiculos.ptocreacu + "' ,'" + vehiculos.ptocrefch + "' ,'" + vehiculos.ptovenacu + "' ,'" + vehiculos.ptovenfch + "' ,'" + vehiculos.tagex1 + "' ,'" + vehiculos.tagex2 + "' ,'" + vehiculos.tagex3 + "' ,'" + vehiculos.ultcan + "' ,'" + vehiculos.datvar + "' ,'" + vehiculos.catprd + "' ,'L' ,'" + vehiculos.dialim + "')";

                    sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] INSERT INTO [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]([codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp] ,[cnvobs] ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim]) " +
                    "VALUES (@CodCli , (SELECT nro=( CASE WHEN MAX(nroveh+1)>1 THEN (SELECT MAX(nroveh+1) as nroveh FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where codcli=@CodCli) ELSE 1 END) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] where codcli=@CodCli) ,(select COUNT( * )+1 from [" + GlobalesCorporativo.BDCorporativoModerno + "]. [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]) ,@PLC ,@Den ,@RSP ,@GRP ,@DiaCar ,@HraIni ,@HraFin ,@CarMax ,@CanDia ,@CanSem ,@CanMes ,@AcuDia ,@AcuSem ,@AcuMes ,@UltCar ,@UltOdm ,@CodGas ,@CodProd ,@DebSdo ,@DebFch ,@DebNro ,@DebCan ,@Nip ,@PtoSdo ,@PtoFch ,@PtoCan ,@PreMto ,@PrePgo ,@PrefID ,@CnvEmp ,@CnvObs ,@CnvFch ,@ManObs ,@ManPer ,@ManUlt ,@Rut ,@Tag ,@Vto ,@LimTur ,@UltTur ,@AcuTur ,@LimPrd ,@AcuPrd ,@CreFch ,@CreNro ,@CreCan ,@CreFch2 ,@CreNro2 ,@CreCan2 ,@DebFch2 ,@DebNro2 ,@DebCan2 ,1 ,GETDATE() ,0 ,GETDATE() ,GETDATE() ,@TagAdi ,@CtaPre ,@NroPat ,@NroEco ,@HraIni2,@HraFin2 ,@HraIni3 ,@HraFin3 ,@Aju ,@PtoDebAcu ,@PtoDebFch ,@PtoCreAcu ,@PtoCreFch ,@PtoVenAcu ,@PtoVenFch ,@TagEx1 ,@TagEx2 ,@TagEx3 ,@UltCan ,@DatVar ,@CatPrd ,'L' ,@DiaLim)";

                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodCli", Value = vehiculos.codcli });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PLC", Value = vehiculos.plc ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Den", Value = vehiculos.den ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@RSP", Value = vehiculos.rsp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@GRP", Value = vehiculos.grp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DiaCar", Value = vehiculos.diacar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni", Value = vehiculos.hraini });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin", Value = vehiculos.hrafin });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CarMax", Value = vehiculos.carmax });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanDia", Value = vehiculos.candia });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanSem", Value = vehiculos.cansem });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanMes", Value = vehiculos.canmes });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuDia", Value = vehiculos.acudia });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuSem", Value = vehiculos.acusem });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuMes", Value = vehiculos.acumes });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltCar", Value = vehiculos.ultcar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltOdm", Value = vehiculos.ultodm });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodGas", Value = vehiculos.codgas });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodProd", Value = vehiculos.codprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebSdo", Value = vehiculos.debsdo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebFch", Value = vehiculos.debfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebNro", Value = vehiculos.debnro });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebCan", Value = vehiculos.debcan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Nip", Value = vehiculos.nip });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoSdo", Value = vehiculos.ptosdo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoFch", Value = vehiculos.ptofch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCan", Value = vehiculos.ptocan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PreMto", Value = vehiculos.premto });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PrePgo", Value = vehiculos.prepgo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PrefID", Value = vehiculos.prefid });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvEmp", Value = vehiculos.cnvemp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvObs", Value = vehiculos.cnvobs ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvFch", Value = vehiculos.cnvfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManObs", Value = vehiculos.manobs ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManPer", Value = vehiculos.manper });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManUlt", Value = vehiculos.manult });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Rut", Value = vehiculos.rut ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Tag", Value = vehiculos.tag ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Vto", Value = vehiculos.vto });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@LimTur", Value = vehiculos.limtur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltTur", Value = vehiculos.ulttur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuTur", Value = vehiculos.acutur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@LimPrd", Value = vehiculos.limprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuPrd", Value = vehiculos.acuprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreFch", Value = vehiculos.crefch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreNro", Value = vehiculos.crenro });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreCan", Value = vehiculos.crecan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreFch2", Value = vehiculos.crefch2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreNro2", Value = vehiculos.crenro2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreCan2", Value = vehiculos.crecan2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebFch2", Value = vehiculos.debfch2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebNro2", Value = vehiculos.debnro2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebCan2", Value = vehiculos.debcan2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagAdi", Value = vehiculos.tagadi ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CtaPre", Value = vehiculos.ctapre ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@NroPat", Value = vehiculos.nropat ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@NroEco", Value = vehiculos.nroeco ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni2", Value = vehiculos.hraini2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin2", Value = vehiculos.hrafin2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni3", Value = vehiculos.hraini3 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin3", Value = vehiculos.hrafin3 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Aju", Value = vehiculos.aju });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoDebAcu", Value = vehiculos.ptodebacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoDebFch", Value = vehiculos.ptodebfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCreAcu", Value = vehiculos.ptocreacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCreFch", Value = vehiculos.ptocrefch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoVenAcu", Value = vehiculos.ptovenacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoVenFch", Value = vehiculos.ptovenfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx1", Value = vehiculos.tagex1 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx2", Value = vehiculos.tagex2 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx3", Value = vehiculos.tagex3 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltCan", Value = vehiculos.ultcan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DatVar", Value = vehiculos.datvar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CatPrd", Value = vehiculos.catprd ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DiaLim", Value = vehiculos.dialim ?? (object)DBNull.Value });

                    DataAfected = BD.SetSQL(ArrParametros, sSelect, "ControlGas", vehiculos, "WsInsertarVehiculoControlGas");
                    //**********************************************************************
                    if (DataAfected > 0)
                    {
                        // SendWS(vehiculos, "WsInsertarVehiculoControlGas");
                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF VEHICLES";
                        data.Status = "ERROR";
                    }
                }



                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {
                    string sSelect = "";
                    var d = "";
                    //**********************************************************************
                    ArrParametros = new List<SqlParameter>();
                    //sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] INSERT INTO [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]([codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp] ,[cnvobs] ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim]) " +
                    // "VALUES ('" + (vehiculos.codcli) + "' , (SELECT nro=( CASE WHEN MAX(nroveh+1)>1 THEN (SELECT MAX(nroveh+1) as nroveh FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where codcli='" + vehiculos.codcli + "') ELSE 1 END) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] where codcli='" + vehiculos.codcli + "') ,(select COUNT( * )+1 from [" + GlobalesCorporativo.BDCorporativoModerno + "]. [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]) ,'" + vehiculos.plc + "' ,'" + vehiculos.den + "' ,'" + vehiculos.rsp + "' ,'" + vehiculos.grp + "' ,'" + vehiculos.diacar + "' ,'" + vehiculos.hraini + "' ,'" + vehiculos.hrafin + "' ,'" + vehiculos.carmax + "' ,'" + vehiculos.candia + "' ,'" + vehiculos.cansem + "' ,'" + vehiculos.canmes + "' ,'" + vehiculos.acudia + "' ,'" + vehiculos.acusem + "' ,'" + vehiculos.acumes + "' ,'" + vehiculos.ultcar + "' ,'" + vehiculos.ultodm + "' ,'" + vehiculos.codgas + "' ,'" + vehiculos.codprd + "' ,'" + vehiculos.debsdo + "' ,'" + vehiculos.debfch + "' ,'" + vehiculos.debnro + "' ,'" + vehiculos.debcan + "' ,'" + vehiculos.nip + "' ,'" + vehiculos.ptosdo + "' ,'" + vehiculos.ptofch + "' ,'" + vehiculos.ptocan + "' ,'" + vehiculos.premto + "' ,'" + vehiculos.prepgo + "' ,'" + vehiculos.prefid + "' ,'" + vehiculos.cnvemp + "' ,'" + vehiculos.cnvobs + "' ,'" + vehiculos.cnvfch + "' ,'" + vehiculos.manobs + "' ,'" + vehiculos.manper + "' ,'" + vehiculos.manult + "' ,'" + vehiculos.rut + "' ,'" + vehiculos.tag + "' ,'" + vehiculos.vto + "' ,'" + vehiculos.limtur + "' ,'" + vehiculos.ulttur + "' ,'" + vehiculos.acutur + "' ,'" + vehiculos.limprd + "' ,'" + vehiculos.acuprd + "' ,'" + vehiculos.crefch + "' ,'" + vehiculos.crenro + "' ,'" + vehiculos.crecan + "' ,'" + vehiculos.crefch2 + "' ,'" + vehiculos.crenro2 + "' ,'" + vehiculos.crecan2 + "' ,'" + vehiculos.debfch2 + "' ,'" + vehiculos.debnro2 + "' ,'" + vehiculos.debcan2 + "' ,1 ,GETDATE() ,0 ,GETDATE() ,GETDATE() ,'" + vehiculos.tagadi + "' ,'" + vehiculos.ctapre + "' ,'" + vehiculos.nropat + "' ,'" + vehiculos.nroeco + "' ,'" + vehiculos.hraini2 + "' ,'" + vehiculos.hrafin2 + "' ,'" + vehiculos.hraini3 + "' ,'" + vehiculos.hrafin3 + "' ,'" + vehiculos.aju + "' ,'" + vehiculos.ptodebacu + "' ,'" + vehiculos.ptodebfch + "' ,'" + vehiculos.ptocreacu + "' ,'" + vehiculos.ptocrefch + "' ,'" + vehiculos.ptovenacu + "' ,'" + vehiculos.ptovenfch + "' ,'" + vehiculos.tagex1 + "' ,'" + vehiculos.tagex2 + "' ,'" + vehiculos.tagex3 + "' ,'" + vehiculos.ultcan + "' ,'" + vehiculos.datvar + "' ,'" + vehiculos.catprd + "' ,'L' ,'" + vehiculos.dialim + "')";

                    sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "] INSERT INTO [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]([codcli] ,[nroveh] ,[tar] ,[plc] ,[den] ,[rsp] ,[grp] ,[diacar] ,[hraini] ,[hrafin] ,[carmax] ,[candia] ,[cansem] ,[canmes] ,[acudia] ,[acusem] ,[acumes] ,[ultcar] ,[ultodm] ,[codgas] ,[codprd] ,[debsdo] ,[debfch] ,[debnro] ,[debcan] ,[nip] ,[ptosdo] ,[ptofch] ,[ptocan] ,[premto] ,[prepgo] ,[prefid] ,[cnvemp] ,[cnvobs] ,[cnvfch] ,[manobs] ,[manper] ,[manult] ,[rut] ,[tag] ,[vto] ,[limtur] ,[ulttur] ,[acutur] ,[limprd] ,[acuprd] ,[crefch] ,[crenro] ,[crecan] ,[crefch2] ,[crenro2] ,[crecan2] ,[debfch2] ,[debnro2] ,[debcan2] ,[est] ,[niplog] ,[logusu] ,[logfch] ,[lognew] ,[tagadi] ,[ctapre] ,[nropat] ,[nroeco] ,[hraini2] ,[hrafin2] ,[hraini3] ,[hrafin3] ,[aju] ,[ptodebacu] ,[ptodebfch] ,[ptocreacu] ,[ptocrefch] ,[ptovenacu] ,[ptovenfch] ,[tagex1] ,[tagex2] ,[tagex3] ,[ultcan] ,[datvar] ,[catprd] ,[catuni] ,[dialim]) " +
                     "VALUES (@CodCli , (SELECT nro=( CASE WHEN MAX(nroveh+1)>1 THEN (SELECT MAX(nroveh+1) as nroveh FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]where codcli=@CodCli) ELSE 1 END) FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] where codcli=@CodCli) ,(select COUNT( * )+1 from [" + GlobalesCorporativo.BDCorporativoModerno + "]. [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]) ,@PLC ,@Den ,@RSP,@GRP,@DiaCar,@HraIni,@HraFin,@CarMax,@CanDia,@CanSem,@CanMes,@AcuDia,@AcuSem,@AcuMes,@UltCar,@UltOdm,@CodGas,@CodProd,@DebSdo,@DebFch,@DebNro,@DebCan,@Nip,@PtoSdo,@PtoFch,@PtoCan,@PreMto,@PrePgo,@PrefID,@CnvEmp,@CnvObs,@CnvFch,@ManObs,@ManPer,@ManUlt,@Rut,@Tag,@Vto,@LimTur,@UltTur,@AcuTur,@LimPrd,@AcuPrd,@CreFch,@CreNro,@CreCan,@CreFch2,@CreNro2,@CreCan2,@DebFch2,@DebNro2,@DebCan2 ,1 ,GETDATE() ,0 ,GETDATE() ,GETDATE() ,@TagAdi,@CtaPre,@NroPat,@NroEco,@HraIni2,@HraFin2,@HraIni3,@HraFin3,@Aju,@PtoDebAcu,@PtoDebFch,@PtoCreAcu,@PtoCreFch,@PtoVenAcu,@PtoVenFch,@TagEx1,@TagEx2,@TagEx3,@UltCan,@DatVar,@CatPrd,'L',@DiaLim)";

                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodCli", Value = vehiculos.codcli });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PLC", Value = vehiculos.plc ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Den", Value = vehiculos.den ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@RSP", Value = vehiculos.rsp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@GRP", Value = vehiculos.grp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DiaCar", Value = vehiculos.diacar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni", Value = vehiculos.hraini });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin", Value = vehiculos.hrafin });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CarMax", Value = vehiculos.carmax });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanDia", Value = vehiculos.candia });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanSem", Value = vehiculos.cansem });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CanMes", Value = vehiculos.canmes });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuDia", Value = vehiculos.acudia });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuSem", Value = vehiculos.acusem });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuMes", Value = vehiculos.acumes });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltCar", Value = vehiculos.ultcar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltOdm", Value = vehiculos.ultodm });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodGas", Value = vehiculos.codgas });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CodProd", Value = vehiculos.codprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebSdo", Value = vehiculos.debsdo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebFch", Value = vehiculos.debfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebNro", Value = vehiculos.debnro });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebCan", Value = vehiculos.debcan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Nip", Value = vehiculos.nip });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoSdo", Value = vehiculos.ptosdo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoFch", Value = vehiculos.ptofch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCan", Value = vehiculos.ptocan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PreMto", Value = vehiculos.premto });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PrePgo", Value = vehiculos.prepgo });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PrefID", Value = vehiculos.prefid });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvEmp", Value = vehiculos.cnvemp ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvObs", Value = vehiculos.cnvobs ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CnvFch", Value = vehiculos.cnvfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManObs", Value = vehiculos.manobs ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManPer", Value = vehiculos.manper });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ManUlt", Value = vehiculos.manult });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Rut", Value = vehiculos.rut ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Tag", Value = vehiculos.tag ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Vto", Value = vehiculos.vto });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@LimTur", Value = vehiculos.limtur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltTur", Value = vehiculos.ulttur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuTur", Value = vehiculos.acutur });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@LimPrd", Value = vehiculos.limprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@AcuPrd", Value = vehiculos.acuprd });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreFch", Value = vehiculos.crefch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreNro", Value = vehiculos.crenro });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreCan", Value = vehiculos.crecan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreFch2", Value = vehiculos.crefch2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreNro2", Value = vehiculos.crenro2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CreCan2", Value = vehiculos.crecan2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebFch2", Value = vehiculos.debfch2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebNro2", Value = vehiculos.debnro2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DebCan2", Value = vehiculos.debcan2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagAdi", Value = vehiculos.tagadi ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CtaPre", Value = vehiculos.ctapre ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@NroPat", Value = vehiculos.nropat ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@NroEco", Value = vehiculos.nroeco ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni2", Value = vehiculos.hraini2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin2", Value = vehiculos.hrafin2 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraIni3", Value = vehiculos.hraini3 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@HraFin3", Value = vehiculos.hrafin3 });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Aju", Value = vehiculos.aju });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoDebAcu", Value = vehiculos.ptodebacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoDebFch", Value = vehiculos.ptodebfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCreAcu", Value = vehiculos.ptocreacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoCreFch", Value = vehiculos.ptocrefch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoVenAcu", Value = vehiculos.ptovenacu });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@PtoVenFch", Value = vehiculos.ptovenfch });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx1", Value = vehiculos.tagex1 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx2", Value = vehiculos.tagex2 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@TagEx3", Value = vehiculos.tagex3 ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@UltCan", Value = vehiculos.ultcan });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DatVar", Value = vehiculos.datvar });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@CatPrd", Value = vehiculos.catprd ?? (object)DBNull.Value });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@DiaLim", Value = vehiculos.dialim ?? (object)DBNull.Value });
                    DataAfected = BD.SetSQL(ArrParametros, sSelect, "ControlGas");
                    //**********************************************************************


                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF VEHICLES";
                        data.Status = "ERROR";
                    }

                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Insertar los vehiculos en cntrol gas", "WsInsertarVehiculoControlGas");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Insertar los vehiculos en control gas", "Ha ocurrido un error en la consulta al Insertar los vehiculos en control gas", "", "", "", "");
                return Ok(mensaje);
            }



        }

        // Este metodo es utilizado para deshabilitar  un  chofer del sistema
        [HttpPost] [Route("WsDisabledChofer")]
        public IHttpActionResult  WsDisabledChofer(string tag = "0")
        {
            int DataAfectedLocal = 0;
            int DataAfectedTankFarm = 0;
            DataResponse data = new DataResponse();
            //**********************************************************************
            var ArrParametros = new List<SqlParameter>();
            //**********************************************************************
            try
            {
                string sSelectLocaL = "";
                string sSelectTank = "";


                //**********************************************************************
                ArrParametros = new List<SqlParameter>();
                //sSelectLocaL = "Update [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] set codest='-1' WHERE tag='" + tag + "'";
                sSelectLocaL = "Update [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] set codest='-1' WHERE tag=@Tag";

                ArrParametros.Add(new SqlParameter { ParameterName = "@Tag", Value = tag });

                DataAfectedLocal = BD.SetSQL(ArrParametros, sSelectLocaL, "ControlGas");
                //**********************************************************************

                //**********************************************************************
                ArrParametros = new List<SqlParameter>();
                //sSelectTank = "Update [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "]  set Status='-1' WHERE tag='" + tag + "'";
                sSelectTank = "Update [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "]  set Status='-1' WHERE tag=@Tag";

                ArrParametros.Add(new SqlParameter { ParameterName = "@Tag", Value = tag });

                DataAfectedTankFarm = BD.SetSQL(ArrParametros, sSelectTank, "Local");
                //**********************************************************************

                if (DataAfectedLocal > 0 && DataAfectedTankFarm > 0)
                {
                    data.Message = "SUCCESSFUL DISABLED";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED DISABLED OF DRIVER";
                    data.Status = "ERROR";
                }



                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al deshabilitar un chofer", "WsDisabledChofer");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al deshabilitar un chofer", "Ha ocurrido un error en la consulta al deshabilitar un chofer", "", "", "", "");
                return Ok(mensaje);
            }



        }

        // Este metodo es utilizado para deshabilitar  un  vehicle del sistema 
        [HttpPost] [Route("WsDisabledVehicle")]
        public IHttpActionResult  WsDisabledVehicle(string tag = "0")
        {
            int DataAfectedLocal = 0;
            DataResponse data = new DataResponse();
            try
            {
                string sSelectLocaL = "";

                //**********************************************************************
                var ArrParametros = new List<SqlParameter>();

                //sSelectLocaL = "Update [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]set est='0' WHERE tag='" + tag + "'";
                sSelectLocaL = "Update [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]set est='0' WHERE tag=@Tag";

                ArrParametros.Add(new SqlParameter { ParameterName = "@Tag", Value = tag });

                DataAfectedLocal = BD.SetSQL(ArrParametros, sSelectLocaL, "ControlGas");
                //**********************************************************************
                if (DataAfectedLocal > 0)
                {
                    data.Message = "SUCCESSFUL DISABLED";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED DISABLED OF CUSTOMER";
                    data.Status = "ERROR";
                }



                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al deshabilitar un vehiculo", "WsDisabledVehicle");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al deshabilitar un vehiculo", "Ha ocurrido un error en la consulta al deshabilitar un vehiculo", "", "", "", "");
                return Ok(mensaje);
            }



        }



        // Este metodo es utilizado para dehabilitar un departamento en la base de de datos local
        [HttpPost] [Route("WsStatusChangue")]
        public IHttpActionResult  WsStatusChangue(ChangueStatus changuestatus)
        {
            int DataAfected = 0;
            //ChangueStatus changestatus = new ChangueStatus();
            DataResponse data = new DataResponse();

            //**********************************************************************
            var ArrParametros = new List<SqlParameter>();

            try
            {
                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    string sSelect = "";
                    var d = "";
                    if (changuestatus.ID != "" && changuestatus.ID != "0")
                    {
                        sSelect = $"UPDATE [dbo].{changuestatus.table} SET {changuestatus.nameStatus} = @Status WHERE {changuestatus.nameid} = @ID";


                        ArrParametros.Add(new SqlParameter { ParameterName = "@Status", Value = changuestatus.Status });
                        ArrParametros.Add(new SqlParameter { ParameterName = "@ID", Value = changuestatus.ID });


                    }
                    else
                    {
                        data.Message = "DATA EMPTY";
                        data.Status = "ERROR";
                        return Ok(data);
                    }


                    DataAfected = BD.SetSQL(ArrParametros, sSelect, changuestatus.BDD, changuestatus, "WsStatusChangue");
                    //**********************************************************************
                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL STATUS";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED STATUS OF " + changuestatus.table.ToUpper() + "";
                        data.Status = "ERROR";
                    }


                }


                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {

                    string sSelect = "";
                    var d = "";
                    if (changuestatus.ID != "" && changuestatus.ID != "0")
                    {
                        //**********************************************************************
                        ArrParametros = new List<SqlParameter>();
                        sSelect = $"UPDATE [dbo].{changuestatus.table} SET {changuestatus.nameStatus} = @Status WHERE  {changuestatus.nameid} = @ID";

                        ArrParametros.Add(new SqlParameter { ParameterName = "@Status", Value = changuestatus.Status });
                        ArrParametros.Add(new SqlParameter { ParameterName = "@ID", Value = changuestatus.ID });

                    }
                    else
                    {
                        data.Message = "DATA EMPTY";
                        data.Status = "ERROR";
                        return Ok(data);
                    }

                    DataAfected = BD.SetSQL(ArrParametros, sSelect, changuestatus.BDD);
                    //**********************************************************************

                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL STATUS";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED STATUS OF " + changuestatus.table.ToUpper() + "";
                        data.Status = "ERROR";
                    }

                }


                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al deshabilitar o habilitar  un " + changuestatus.table + " en la bd local", "WsStatusChangue");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al deshabilitar o habilitar un " + changuestatus.table + " en la bd local", "Ha ocurrido un error en la consulta al deshabilitar o habilitar un " + changuestatus.table + " en la bd local", "", "", "", "");
                return Ok(mensaje);
            }



        }


        // Este metodo es utilizado para actualizar un departamento en la base de de datos local
        [HttpPost] [Route("WsUpdateDepartamentos")]
        public IHttpActionResult  WsUpdateDepartamentos(Departamentos departamentos)
        {
            //**********************************************************************
            var ArrParametros = new List<SqlParameter>();

            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {

                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    string sSelect = "";
                    var d = "";

                    //sSelect = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.TablaDepartamento + "] SET [Departamento] = '" + departamentos.Departamento + "' WHERE ID = '" + departamentos.ID + "'";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.TablaDepartamento + "] SET [Departamento] = '@Departamento' WHERE ID = @ID";

                    ArrParametros.Add(new SqlParameter { ParameterName = "@Departamento", Value = departamentos.Departamento });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ID", Value = departamentos.ID });

                    DataAfected = BD.SetSQL(ArrParametros, sSelect, "Local", departamentos, "WsUpdateDepartamentos");
                    //**********************************************************************



                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL UPDATED";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED UPDATED OF DEPARTMENT";
                        data.Status = "ERROR";
                    }


                }



                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {
                    string sSelect = "";
                    var d = "";
                    ArrParametros = new List<SqlParameter>();
                    //sSelect = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.TablaDepartamento + "] SET [Departamento] = '" + departamentos.Departamento + "' WHERE ID = '" + departamentos.ID + "'";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.TablaDepartamento + "] SET [Departamento] = @Departamento WHERE ID = @ID";
                    ArrParametros.Add(new SqlParameter { ParameterName = "@Departamento", Value = departamentos.Departamento });
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ID", Value = departamentos.ID });
                    DataAfected = BD.SetSQL(ArrParametros, sSelect, "Local");
                    //**********************************************************************
                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL UPDATED";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED UPDATED OF DEPARTMENT";
                        data.Status = "ERROR";
                    }


                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar un departamento en la bd local", "WsUpdateDepartamentos");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar un departamento en la bd local", "Ha ocurrido un error en la consulta al actualizar un departamento en la bd local", "", "", "", "");
                return Ok(mensaje);
            }



        }

        //Este ws es para obtener los departamentos de la bd de datos local
        [HttpGet]
        [Route("GetDepartamentos")]
        public IHttpActionResult  GetDepartamentos(string id = "0", string all = "")
        {
            System.Data.DataTable s = new System.Data.DataTable();
            List<Departamentos> lstDep = new List<Departamentos>();
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();

            //**********************************************************************
            var ArrParametros = new List<SqlParameter>();

            try
            {
                string sSelect = "";
                if (id != "0")
                {
                    //sSelect = "select  * from [" + GlobalesLocal.BDLocal + "].[dbo]. [" + GlobalesLocal.TablaDepartamento + "] where ID=" + id+ "' order by Departamento asc";
                    sSelect = "select  * from [" + GlobalesLocal.BDLocal + "].[dbo]. [" + GlobalesLocal.TablaDepartamento + "] where ID=@ID order by Departamento asc";
                    ArrParametros.Add(new SqlParameter { ParameterName = "@ID", Value = id });
                }
                else
                {
                    if (all == "" || all == "0" || all == null)
                    {

                        //sSelect = "SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaDepartamento + "] where Status=1 order by Departamento asc";
                        sSelect = "SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaDepartamento + "] where Status=1 order by Departamento asc";
                    }
                    else
                    {
                        //sSelect = "SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaDepartamento + "] order by Departamento asc";
                        sSelect = "SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaDepartamento + "] order by Departamento asc";
                    }

                }

                dT = BD.GetSQL(sSelect, "Local", ArrParametros);
                //**********************************************************************

                var columnas = dT.Columns;
                //return Ok(dT.Rows);
                foreach (DataRow dR1 in dT.Rows)
                {

                    Departamentos ObjDepartamentos = new Departamentos();
                    ObjDepartamentos.ID = dR1["ID"].ToString();
                    ObjDepartamentos.Departamento = dR1["Departamento"].ToString();
                    ObjDepartamentos.Status = dR1["Status"].ToString();




                    lstDep.Add(ObjDepartamentos);
                }

                if (lstDep.Count > 0)
                {
                    data.lstDep = lstDep;
                    data.Message = "DEPARTMENTS(S) FOUND";
                    data.Status = "OK";
                }
                else
                {
                    data.lstDep = lstDep;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al obtener los departamentos de la base de datos local", "GetDepartamentos");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al obtener los departamentos de la base de datos local", "Ha ocurrido un error en la consulta al obtener los departamentos de la base de datos local", "", "", "", "");
                return Ok(mensaje);
            }



        }

        //gera

        // Este metodo es utilizado para actualizar un  chofer en la base de datos local y de tank farm
        [HttpPost] [Route("WsUpdateChofer")]
        public IHttpActionResult  WsUpdateChofer(Choferes choferes)
        {
            int DataAfectedLocal = 0;
            int DataAfectedTankFarm = 0;
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();

            try
            {


                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {

                    var ArrParametros1 = new List<SqlParameter>();
                    string sSelectLocaL = "";
                    string sSelecTTankFarm = "";
                    string validartag = "";

                    validartag = "select * from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "] where Tag=@Tag and ID<>@ID";

                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Tag", Value = choferes.tag ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = choferes.IDP });
                    dT = BD.GetSQL(validartag, "ControlGas", ArrParametros1);
                    if (dT.Rows.Count == 1)
                    {
                        data.Message = "FAILED TAG ALREADY EXISTS";
                        data.Status = "DUPLICATE";
                        return Ok(data);

                    }



                    var ArrParametros2 = new List<SqlParameter>();
                    sSelecTTankFarm = "use [" + GlobalesCorporativo.BDCorporativoModerno + "] UPDATE [dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] SET [codcli]= @codcli, [den]= @den ,[diacar]=@diacar ,[hraini]=@hraini ,[hrafin]=@hrafin ,[tag]=@tag ,[logusu]='0' ,[logfch]=GETDATE() WHERE   tag=@TagOld";

                    ArrParametros2.Add(new SqlParameter { ParameterName = "@codcli", Value = choferes.codcli });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@den", Value = choferes.den ?? (object)DBNull.Value });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@diacar", Value = choferes.diacar });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@hraini", Value = choferes.hrafin });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@hrafin", Value = choferes.hrafin });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@tag", Value = choferes.tag ?? (object)DBNull.Value });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@TagOld", Value = choferes.TagOld ?? (object)DBNull.Value });


                    DataAfectedTankFarm = BD.SetSQL(ArrParametros2, sSelecTTankFarm, "ControlGas", choferes, "WsUpdateChofer");


                    if (DataAfectedTankFarm > 0)
                    {
                        var ArrParametros3 = new List<SqlParameter>();
                        var nombreConcatenado = choferes.Nombre + choferes.LastName + choferes.SecondLastName;
                        nombreConcatenado = nombreConcatenado.Replace(" ", "");
                        sSelectLocaL = "  use [" + GlobalesLocal.BDLocal + "] update [dbo].[" + GlobalesLocal.TablaChoferes + "]  SET [Nombre]=@Nombre ,[Telefono]=@Telefono ,[IdCliente]=@codcli ,[IdIdioma]='1' ,[Gerente]=@Gerente ,[Correo]=@Correo,[Perfil]=@Perfil ,[Tag]=@tag,[LastName]=@LastName,[SecondLastName]=@SecondLastName,[Departamento]=@Departamento ,[CentroCostos]=@CentroCostos,[NameComplete]=@nombreConcatenado WHERE Tag=@TagOld";

                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Nombre", Value = choferes.Nombre ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Telefono", Value = choferes.Telefono ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@codcli", Value = choferes.codcli });

                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Gerente", Value = choferes.Gerente ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Correo", Value = choferes.Correo ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Perfil", Value = choferes.Perfil ?? (object)DBNull.Value });

                        ArrParametros3.Add(new SqlParameter { ParameterName = "@tag", Value = choferes.tag ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@LastName", Value = choferes.LastName ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@SecondLastName", Value = choferes.SecondLastName ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Departamento", Value = choferes.Departamento ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@CentroCostos", Value = choferes.CentroCostos ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@nombreConcatenado", Value = nombreConcatenado ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@TagOld", Value = choferes.TagOld ?? (object)DBNull.Value });

                        DataAfectedLocal = BD.SetSQL(ArrParametros3, sSelectLocaL, "Local");

                        if (DataAfectedLocal > 0)
                        {
                            //SendWS(choferes, "WsUpdateChofer");
                            data.Message = "SUCCESSFUL UPDATED";
                            data.Status = "OK";
                        }
                        else
                        {

                            data.Message = "FAILED INSERTION OF DRIVER";
                            data.Status = "ERROR";
                        }

                    }
                    else
                    {

                    }

                }



                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {

                    string sSelectLocaL = "";
                    string sSelecTTankFarm = "";
                    string validartag = "";
                    var ArrParametros1 = new List<SqlParameter>();

                    validartag = "select * from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaChoferes + "] where Tag=@tag and ID<>@ID";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Tag", Value = choferes.tag ?? (object)DBNull.Value });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = choferes.IDP });
                    dT = BD.GetSQL(validartag, "ControlGas", ArrParametros1);
                    if (dT.Rows.Count == 1)
                    {
                        data.Message = "FAILED TAG ALREADY EXISTS";
                        data.Status = "DUPLICATE";
                        return Ok(data);

                    }

                    var ArrParametros2 = new List<SqlParameter>();
                    sSelecTTankFarm = "use [" + GlobalesCorporativo.BDCorporativoModerno + "] UPDATE [dbo].[" + GlobalesCorporativo.TablaClientesChoferes + "] SET [codcli]= @codcli, [den]= @den ,[diacar]=@diacar ,[hraini]=@hraini ,[hrafin]=@hrafin ,[tag]=@tag ,[logusu]='0' ,[logfch]=GETDATE() WHERE   tag=@TagOld";

                    ArrParametros2.Add(new SqlParameter { ParameterName = "@codcli", Value = choferes.codcli });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@den", Value = choferes.den ?? (object)DBNull.Value });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@diacar", Value = choferes.diacar });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@hraini", Value = choferes.hrafin });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@hrafin", Value = choferes.hrafin });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@tag", Value = choferes.tag ?? (object)DBNull.Value });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@TagOld", Value = choferes.TagOld ?? (object)DBNull.Value });

                    DataAfectedTankFarm = BD.SetSQL(ArrParametros2, sSelecTTankFarm, "ControlGas");

                    if (DataAfectedTankFarm > 0)
                    {
                        var ArrParametros3 = new List<SqlParameter>();
                        var nombreConcatenado = choferes.Nombre + choferes.LastName + choferes.SecondLastName;
                        nombreConcatenado = nombreConcatenado.Replace(" ", "");
                        sSelectLocaL = "  use [" + GlobalesLocal.BDLocal + "] update [dbo].[" + GlobalesLocal.TablaChoferes + "] SET [Nombre]=@Nombre ,[Telefono]=@Telefono ,[IdCliente]=@codcli ,[IdIdioma]='1' ,[Gerente]=@Gerente ,[Correo]=@Correo,[Perfil]=@Perfil ,[Tag]=@tag,[LastName]=@LastName,[SecondLastName]=@SecondLastName,[Departamento]=@Departamento ,[CentroCostos]=@CentroCostos,[NameComplete]=@nombreConcatenado WHERE Tag=@TagOld";
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Nombre", Value = choferes.Nombre ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Telefono", Value = choferes.Telefono ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@codcli", Value = choferes.codcli });

                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Gerente", Value = choferes.Gerente ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Correo", Value = choferes.Correo ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Perfil", Value = choferes.Perfil ?? (object)DBNull.Value });

                        ArrParametros3.Add(new SqlParameter { ParameterName = "@tag", Value = choferes.tag ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@LastName", Value = choferes.LastName ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@SecondLastName", Value = choferes.SecondLastName ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@Departamento", Value = choferes.Departamento ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@CentroCostos", Value = choferes.CentroCostos ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@nombreConcatenado", Value = nombreConcatenado ?? (object)DBNull.Value });
                        ArrParametros3.Add(new SqlParameter { ParameterName = "@TagOld", Value = choferes.TagOld ?? (object)DBNull.Value });

                        DataAfectedLocal = BD.SetSQL(ArrParametros3, sSelectLocaL, "Local");

                        if (DataAfectedLocal > 0)
                        {
                            //SendWS(choferes, "WsUpdateChofer");
                            data.Message = "SUCCESSFUL UPDATED";
                            data.Status = "OK";
                        }
                        else
                        {

                            data.Message = "FAILED INSERTION OF DRIVER";
                            data.Status = "ERROR";
                        }

                    }
                    else
                    {

                    }
                }
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar choferes", "WsUpdateChofer");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar choferes", "Ha ocurrido un error en la consulta al actualizar choferes", "", "", "", "");
                return Ok(mensaje);
            }



        }

        //gera
        [Route("GetChoferes")]
        public IHttpActionResult  GetChoferes(string codcli = "0", string tag = "0", string all = "")
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
                //return Ok(dT.Rows);
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
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Error en la consulta no se pueden obtener los choferes", "GetChoferes");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Error en la consulta no se pueden obtener los choferes", "Error en la consulta no se pueden obtener los choferes", "", "", "", "");
                return Ok(mensaje);
            }



        }

        //gera
        // Este metodo es utilizado para insertar un nuevo chofer en la base de datos local
        [HttpPost] [Route("WsInsertarUsuario")]
        public IHttpActionResult  WsInsertarUsuario(UsuariosSystema userssystema)
        {
            int DataAfectedLocal = 0;
            DataResponse data = new DataResponse();
            try
            {

                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    var ArrParametros1 = new List<SqlParameter>();
                    string sSelectLocaL = "";



                    sSelectLocaL = "USE [" + GlobalesLocal.BDLocal + "] DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "])IF @MAXID >0 SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "])   ELSE set @MAXID=1  INSERT INTO [dbo].[" + GlobalesLocal.TablaUsuarios + "] ([ID],[Nombre] ,[UserName],[Password] ,[IdRol] ,[IdIdioma] ,[Telefono] ,[Correo] ,[Status],[Compania],[Departamento],[Manager],[LastName],[SecondLastName]) VALUES (@MAXID,@Nombre,@UserName,@Password, @IdRol,@IdIdioma,@Telefono,@Correo, @Status, @Compania,@Departamento,@Manager,@LastName,@SecondLastName)";
                    string hh = userssystema.Nombre;
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Nombre", Value = userssystema.Nombre });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@UserName", Value = userssystema.UserName });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Password", Value = userssystema.Password });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@IdRol", Value = userssystema.IdRol });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@IdIdioma", Value = userssystema.IdIdioma });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Telefono", Value = userssystema.Telefono });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Correo", Value = userssystema.Correo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Status", Value = userssystema.Status });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Compania", Value = userssystema.Compania });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Departamento", Value = userssystema.Departamento });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Manager", Value = userssystema.Manager });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@LastName", Value = userssystema.LastName });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@SecondLastName", Value = userssystema.SecondLastName });

                    DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local", userssystema, "WsInsertarUsuario");

                    if (DataAfectedLocal > 0)
                    {
                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF USER";
                        data.Status = "ERROR";
                    }

                }








                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {

                    string sSelectLocaL = "";
                    var ArrParametros1 = new List<SqlParameter>();


                    sSelectLocaL = "USE [" + GlobalesLocal.BDLocal + "] DECLARE @MAXID int; SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "])IF @MAXID >0 SELECT @MAXID=(SELECT MAX(ID+1) AS id FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "])   ELSE set @MAXID=1 INSERT INTO [dbo].[" + GlobalesLocal.TablaUsuarios + "] ([ID],[Nombre] ,[UserName],[Password] ,[IdRol] ,[IdIdioma] ,[Telefono] ,[Correo] ,[Status],[Compania],[Departamento],[Manager],[LastName],[SecondLastName]) VALUES (@MAXID,@Nombre,@UserName,@Password, @IdRol,@IdIdioma,@Telefono,@Correo, @Status, @Compania,@Departamento,@Manager,@LastName,@SecondLastName)";
                    string hh = userssystema.Nombre;
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Nombre", Value = userssystema.Nombre });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@UserName", Value = userssystema.UserName });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Password", Value = userssystema.Password });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@IdRol", Value = userssystema.IdRol });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@IdIdioma", Value = userssystema.IdIdioma });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Telefono", Value = userssystema.Telefono });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Correo", Value = userssystema.Correo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Status", Value = userssystema.Status });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Compania", Value = userssystema.Compania });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Departamento", Value = userssystema.Departamento });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Manager", Value = userssystema.Manager });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@LastName", Value = userssystema.LastName });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@SecondLastName", Value = userssystema.SecondLastName });
                    DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local");

                    if (DataAfectedLocal > 0)
                    {
                        data.Message = "SUCCESSFUL INSERTION";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERTION OF USER";
                        data.Status = "ERROR";
                    }

                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Insertar los usuarios", "WsInsertarUsuario");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Insertar los usuarios", "Ha ocurrido un error en la consulta al Insertar los usuarios", "", "", "", "");

                return Ok(mensaje);
            }



        }



        //gera
        // Este metodo es utilizado para actualizar  un  usuario del sistema en la base de datos local
        [HttpPost] [Route("WsUpdateUsuario")]
        public IHttpActionResult  WsUpdateUsuario(UsuariosSystema userssystema)
        {
            int DataAfectedLocal = 0;
            DataResponse data = new DataResponse();

            try
            {

                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    var ArrParametros1 = new List<SqlParameter>();
                    string sSelectLocaL = "";


                    sSelectLocaL = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.TablaUsuarios + "] SET [Nombre] = @Nombre,[Password] = @Password  ,[UserName]=@UserName,[IdRol] = @IdRol,[IdIdioma] = @IdIdioma ,[Telefono] = @Telefono ,[Correo] = @Correo ,[Status] = @Status,[Compania] = @Compania,[Departamento] = @Departamento ,[Manager] =@Manager,[LastName] =@LastName,[SecondLastName] = @SecondLastName  WHERE ID=@ID";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Nombre", Value = userssystema.Nombre });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Password", Value = userssystema.Password });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@UserName", Value = userssystema.UserName });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@IdRol", Value = userssystema.IdRol });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@IdIdioma", Value = userssystema.IdIdioma });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Telefono", Value = userssystema.Telefono });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Correo", Value = userssystema.Correo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Status", Value = userssystema.Status });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Compania", Value = userssystema.Compania });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Departamento", Value = userssystema.Departamento });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Manager", Value = userssystema.Manager });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@LastName", Value = userssystema.LastName });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@SecondLastName", Value = userssystema.SecondLastName });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = userssystema.ID });


                    DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local", userssystema, "WsUpdateUsuario");

                    if (DataAfectedLocal > 0)
                    {
                        data.Message = "SUCCESSFUL UPGRADE";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED UPGRADE OF USER";
                        data.Status = "ERROR";
                    }

                }


                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {
                    string sSelectLocaL = "";
                    var ArrParametros1 = new List<SqlParameter>();


                    sSelectLocaL = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.TablaUsuarios + "]SET [Nombre] = @Nombre,[Password] = @Password  ,[UserName]=@UserName,[IdRol] = @IdRol,[IdIdioma] = @IdIdioma ,[Telefono] = @Telefono ,[Correo] = @Correo ,[Status] = @Status,[Compania] = @Compania,[Departamento] = @Departamento ,[Manager] =@Manager,[LastName] =@LastName,[SecondLastName] = @SecondLastName  WHERE ID=@ID";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Nombre", Value = userssystema.Nombre });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Password", Value = userssystema.Password });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@UserName", Value = userssystema.UserName });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@IdRol", Value = userssystema.IdRol });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@IdIdioma", Value = userssystema.IdIdioma });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Telefono", Value = userssystema.Telefono });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Correo", Value = userssystema.Correo });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Status", Value = userssystema.Status });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Compania", Value = userssystema.Compania });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Departamento", Value = userssystema.Departamento });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Manager", Value = userssystema.Manager });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@LastName", Value = userssystema.LastName });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@SecondLastName", Value = userssystema.SecondLastName });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = userssystema.ID });
                    DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local");

                    if (DataAfectedLocal > 0)
                    {
                        data.Message = "SUCCESSFUL UPGRADE";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED UPGRADE OF USER";
                        data.Status = "ERROR";
                    }

                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar los usuarios", "WsUpdateUsuario");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar los usuarios", "Ha ocurrido un error en la consulta al actualizar los usuarios", "", "", "", "");
                return Ok(mensaje);
            }



        }





        //gera
        [HttpGet]
        [Route("GetUsuarios")]
       
        public IHttpActionResult  GetUsuarios(string ID = "0", string all = "")
        {
            List<UsuariosSystema> lstUsuarios = new List<UsuariosSystema>();
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            var ArrParametros1 = new List<SqlParameter>();
            try
            {
                string sSelect = "";
                if (ID != "0" && ID != "")
                {
                    sSelect = "SELECT [ID] ,[Nombre],[LastName],[SecondLastName] ,[UserName] ,[Password] ,[IdRol], (select Rol from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaRol + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].IdRol = [" + GlobalesLocal.BDLocal + "].[dbo].Rol.ID) as Rol ,[IdIdioma], (select Idioma from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaIdioma + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].IdIdioma = [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaIdioma + "].ID) as Idioma ,[Telefono] ,[Correo] ,[Status] ,[Compania], (select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].Compania =[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "].cod ) as CompaniaU ,[Departamento], (select Departamento from [" + GlobalesLocal.BDLocal + "].[dbo]. [" + GlobalesLocal.TablaDepartamento + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].Departamento = [" + GlobalesLocal.BDLocal + "].[dbo]. [" + GlobalesLocal.TablaDepartamento + "].ID) as DepartamentoU ,[Manager] FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "] where ID= @ID and Status=0 order by ID desc";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = ID });
                }
                else
                {
                    if (all == "" || all == "0" || all == null)
                    {

                        sSelect = "  SELECT  [ID] ,[Nombre],[LastName],[SecondLastName] ,[UserName] ,[Password] ,[IdRol], (select Rol from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaRol + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].IdRol = [" + GlobalesLocal.BDLocal + "].[dbo].Rol.ID) as Rol ,[IdIdioma], (select Idioma from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaIdioma + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].IdIdioma = [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaIdioma + "].ID) as Idioma ,[Telefono] ,[Correo] ,[Status] ,[Compania], (select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].Compania =[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "].cod ) as CompaniaU ,[Departamento], (select Departamento from [" + GlobalesLocal.BDLocal + "].[dbo]. [" + GlobalesLocal.TablaDepartamento + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].Departamento = [" + GlobalesLocal.BDLocal + "].[dbo]. [" + GlobalesLocal.TablaDepartamento + "].ID) as DepartamentoU ,[Manager] FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "] where Status=0 order by ID desc";
                    }
                    else
                    {
                        sSelect = " SELECT  [ID] ,[Nombre],[LastName],[SecondLastName] ,[UserName] ,[Password] ,[IdRol], (select Rol from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaRol + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].IdRol = [" + GlobalesLocal.BDLocal + "].[dbo].Rol.ID) as Rol ,[IdIdioma], (select Idioma from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaIdioma + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].IdIdioma = [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaIdioma + "].ID) as Idioma ,[Telefono] ,[Correo] ,[Status] ,[Compania], (select den from [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].Compania =[" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientes + "].cod ) as CompaniaU ,[Departamento], (select Departamento from [" + GlobalesLocal.BDLocal + "].[dbo]. [" + GlobalesLocal.TablaDepartamento + "] where [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "].Departamento = [" + GlobalesLocal.BDLocal + "].[dbo]. [" + GlobalesLocal.TablaDepartamento + "].ID) as DepartamentoU ,[Manager] FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaUsuarios + "] order by ID desc";
                    }



                }






                dT = BD.GetSQL(sSelect, "Local", ArrParametros1);
                var columnas = dT.Columns;
                //return Ok(dT.Rows);
                foreach (DataRow dR1 in dT.Rows)
                {

                    UsuariosSystema ObjUsuarios = new UsuariosSystema();


                    ObjUsuarios.ID = Convert.ToInt32(dR1["ID"]);
                    ObjUsuarios.Nombre = dR1["Nombre"].ToString();
                    ObjUsuarios.LastName = dR1["LastName"].ToString();
                    ObjUsuarios.SecondLastName = dR1["SecondLastName"].ToString();
                    ObjUsuarios.UserName = dR1["UserName"].ToString();
                    ObjUsuarios.Password = dR1["Password"].ToString();
                    ObjUsuarios.IdRol = Convert.ToInt32(dR1["IdRol"]);
                    ObjUsuarios.IdIdioma = Convert.ToInt32(dR1["IdIdioma"]);
                    ObjUsuarios.Telefono = dR1["Telefono"].ToString();
                    ObjUsuarios.Correo = dR1["Correo"].ToString();
                    ObjUsuarios.Compania = dR1["CompaniaU"].ToString();
                    ObjUsuarios.codcli = dR1["Compania"].ToString();
                    ObjUsuarios.Status = dR1["Status"].ToString();
                    ObjUsuarios.Idioma = dR1["Idioma"].ToString();
                    ObjUsuarios.Rol = dR1["Rol"].ToString();
                    ObjUsuarios.Departamento = dR1["DepartamentoU"].ToString();
                    ObjUsuarios.IdDep = dR1["Departamento"].ToString();
                    ObjUsuarios.Manager = dR1["Manager"].ToString();

                    lstUsuarios.Add(ObjUsuarios);
                }

                if (lstUsuarios.Count > 0)
                {
                    data.lstUsuarios = lstUsuarios;
                    data.Message = "USERS";
                    data.Status = "OK";
                }
                else
                {
                    data.lstUsuarios = lstUsuarios;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Obtener los usuarios", "GetUsuarios");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Obtener los usuarios", "Ha ocurrido un error en la consulta al Obtener los usuarios", "", "", "", "");
                return Ok(mensaje);
            }



        }


        // gera

        // Este metodo es utilizado para eliminar  un  usuario del sistema en la base de datos local
        [HttpPost] [Route("WsDeleteUsuario")]
        public IHttpActionResult  WsDeleteUsuario(string id = "0")
        {
            int DataAfectedLocal = 0;
            DataResponse data = new DataResponse();
            var ArrParametros1 = new List<SqlParameter>();
            try
            {
                string sSelectLocaL = "";



                sSelectLocaL = "USE [" + GlobalesLocal.BDLocal + "] Delete from  [dbo].[" + GlobalesLocal.TablaUsuarios + "]WHERE ID=@ID";
                ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = id });
                DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local");

                if (DataAfectedLocal > 0)
                {
                    data.Message = "SUCCESSFUL REMOVED";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED DELETE OF USER";
                    data.Status = "ERROR";
                }



                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al ELIMINAR un usuario", "WsDeleteUsuario");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al ELIMINAR un usuario", "Ha ocurrido un error en la consulta al ELIMINAR un usuario", "", "", "", "");
                return Ok(mensaje);
            }



        }



        //gera
        // Este metodo es utilizado para obtener roles
        [HttpGet] [Route("GetLanguague")]
        public IHttpActionResult  GetLanguague(string ID = "0")
        {
            List<Languague> lstLanguague = new List<Languague>();
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            var ArrParametros1 = new List<SqlParameter>();
            try
            {
                string sSelect = "";
                if (ID != "0")
                {
                    sSelect = "SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaIdioma + "] where ID=@ID";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = ID });
                }
                else
                {
                    sSelect = "  SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaIdioma + "]";
                }

                dT = BD.GetSQL(sSelect, "Local", ArrParametros1);
                var columnas = dT.Columns;
                //return Ok(dT.Rows);
                foreach (DataRow dR1 in dT.Rows)
                {

                    Languague ObjLanguague = new Languague();
                    ObjLanguague.ID = dR1["ID"];
                    ObjLanguague.Idioma = dR1["Idioma"];
                    ObjLanguague.Status = dR1["Status"];
                    lstLanguague.Add(ObjLanguague);
                }
                if (lstLanguague.Count > 0)
                {
                    data.lstLanguague = lstLanguague;
                    data.Message = "ROLES";
                    data.Status = "OK";
                }
                else
                {
                    data.lstLanguague = lstLanguague;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Obtener los idiomas", "GetLanguague");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Obtener los idiomas", "Ha ocurrido un error en la consulta al Obtener los idiomas", "", "", "", "");

                return Ok(mensaje);
            }



        }


        //gera
        // Este metodo es utilizado para insertar un nuevo ROl en la base de datos local
        [HttpPost] [Route("WsInsertarRol")]
        public IHttpActionResult  WsInsertarRol(Roles roles)
        {
            int DataAfectedLocal = 0;
            DataResponse data = new DataResponse();
            var ArrParametros1 = new List<SqlParameter>();
            try
            {
                string sSelectLocaL = "";

                sSelectLocaL = "USE [" + GlobalesLocal.BDLocal + "] INSERT INTO [dbo].[" + GlobalesLocal.TablaRol + "] ([Rol] ,[Status],[Users],[Customers],[Vehicles],[Drivers],[Department],[Brand],[Model],[CostCenter],[Perfiles],[Configuration]) VALUES (@Rol,@Status,@Users,@Customers,@Vehicles,@Drivers,@Department,@Brand,@Model,@CostCenter,@Perfiles,@Configuration)";
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Rol", Value = roles.Rol });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Users", Value = roles.Users });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Customers", Value = roles.Customers });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Vehicles", Value = roles.Vehicles});
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Drivers", Value = roles.Drivers});
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Department", Value = roles.Department });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Brand", Value = roles.Brand});
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Model", Value = roles.Model});
                ArrParametros1.Add(new SqlParameter { ParameterName = "@CostCenter", Value = roles.CostCenter});
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Perfiles", Value = roles.Perfiles});
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Configuration", Value = roles.Configuration});
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Status", Value = roles.Status});

                DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local");

                if (DataAfectedLocal > 0)
                {
                    data.Message = "SUCCESSFUL INSERTION";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED INSERTION OF ROL";
                    data.Status = "ERROR";
                }
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Insertar en roles", "WsInsertarRol");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Insertar en roles", "Ha ocurrido un error en la consulta al Insertar en roles", "", "", "", "");
                return Ok(mensaje);
            }



        }


        //gera
        // Este metodo es utilizado para actualizar  un  Rol del sistema en la base de datos local
        [HttpPost] [Route("WsUpdateRol")]
        public IHttpActionResult  WsUpdateRol(Roles roles)
            
        {
            
            int DataAfectedLocal = 0;
            DataResponse data = new DataResponse();
            var ArrParametros1 = new List<SqlParameter>();
            try
            {
                string sSelectLocaL = "";



                sSelectLocaL = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.TablaRol + "] SET [Rol] = @Rol ,Users = @Users,Customers = @Customers,Vehicles = @Vehicles,Drivers =  @Drivers,Department = @Department,Brand = @Brand,Model = @Model, CostCenter= @CostCenter,Perfiles=@Perfiles, Configuration = @Configuration, Status=@Status  where ID=@ID ";
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Rol", Value =roles.Rol });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Users", Value =roles.Users });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Customers", Value =roles.Customers });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Vehicles", Value =roles.Vehicles });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Drivers", Value =roles.Drivers });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Department", Value =roles.Department });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Brand", Value =roles.Brand });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Model", Value =roles.Model });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@CostCenter", Value =roles.CostCenter });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Perfiles", Value =roles.Perfiles });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Configuration", Value =roles.Configuration });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Status", Value =roles.Status });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value =roles.ID });

                DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local");

                if (DataAfectedLocal > 0)
                {
                    data.Message = "SUCCESSFUL UPGRADE ROL";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED UPGRADE OF ROLES";
                    data.Status = "ERROR";
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar los roles", "WsUpdateRol");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar los roles", "Ha ocurrido un error en la consulta al actualizar los roles", "", "", "", "");
                return Ok(mensaje);
            }



        }

        //gera
        // Este metodo es utilizado para dar de baja  un  Rol del sistema en la base de datos local
        [HttpPost] [Route("WsDeleteRol")]
        public IHttpActionResult  WsDeleteRol(string id)
        {
            int DataAfectedLocal = 0;
            DataResponse data = new DataResponse();
            var ArrParametros1 = new List<SqlParameter>();
            var Status = -1;
            try
            {
                string sSelectLocaL = "";



                sSelectLocaL = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.TablaRol + "] SET Status = @Status where ID=@id";
                ArrParametros1.Add(new SqlParameter { ParameterName = "@Status", Value = Status });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@id", Value = id });
                DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local");

                if (DataAfectedLocal > 0)
                {
                    data.Message = "SUCCESSFUL DELETE ROL";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED DELETE OF ROL";
                    data.Status = "ERROR";
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al eliminar un rol", "WsDeleteRol");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al eliminar un rol", "Ha ocurrido un error en la consulta al eliminar un rol", "", "", "", "");
                return Ok(mensaje);
            }



        }


        //gera

        // Este metodo es utilizado para obtener roles
        [HttpGet] [Route("GetRol")]
        public IHttpActionResult  GetRol(string ID = "0", string all = "")
        {
            List<Roles> lstRoles = new List<Roles>();
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            var ArrParametros1 = new List<SqlParameter>();
            var Status = 1;
            try
            {
                string sSelect = "";
                if (ID != "0")
                {
                    sSelect = "SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaRol + "] where ID= @ID and Status=1";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = ID });
                }
                else
                {
                    if (all == "" || all == "0" || all == null)
                    {
                        sSelect = "  SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaRol + "] where Status=1";
                    }
                    else
                    {
                        sSelect = "  SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaRol + "]";
                    }



                }




                dT = BD.GetSQL(sSelect, "Local", ArrParametros1);
                var columnas = dT.Columns;
                //return Ok(dT.Rows);
                foreach (DataRow dR1 in dT.Rows)
                {

                    Roles ObjUsuarios = new Roles();

                    ObjUsuarios.ID = dR1["ID"];
                    ObjUsuarios.Rol = dR1["Rol"];
                    ObjUsuarios.Users = dR1["Users"].ToString();
                    ObjUsuarios.Customers = dR1["Customers"].ToString();
                    ObjUsuarios.Vehicles = dR1["Vehicles"].ToString();
                    ObjUsuarios.Drivers = dR1["Drivers"].ToString();
                    ObjUsuarios.Department = dR1["Department"].ToString();
                    ObjUsuarios.Brand = dR1["Brand"].ToString();
                    ObjUsuarios.Model = dR1["Model"].ToString();
                    ObjUsuarios.CostCenter = dR1["CostCenter"].ToString();
                    ObjUsuarios.Perfiles = dR1["Perfiles"].ToString();
                    ObjUsuarios.Configuration = dR1["Configuration"].ToString();
                    ObjUsuarios.Status = dR1["Status"];



                    lstRoles.Add(ObjUsuarios);
                }

                if (lstRoles.Count > 0)
                {
                    data.lstRoles = lstRoles;
                    data.Message = "ROLES";
                    data.Status = "OK";
                }
                else
                {
                    data.lstRoles = lstRoles;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Obtener los Roles", "GetRol");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Obtener los Roles", "Ha ocurrido un error en la consulta al Obtener los Roles", "", "", "", "");

                return Ok(mensaje);
            }



        }


        //gera
        // Este metodo es utilizado para insertar un modelo de vehiculo en la base de de datos local
        [HttpPost] [Route("WsInsertModelo")]
        public IHttpActionResult  WsInsertModelo(modelo modelo)
        {

            int DataAfected = 0;
            DataResponse data = new DataResponse();

            try
            {

                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    var ArrParametros1 = new List<SqlParameter>();
                    string sSelect = "";
                    var d = "";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] INSERT INTO [dbo].[" + GlobalesLocal.Tablamodelo + "] ([ModelId] ,[ModelDesc] ,[MarcID],[Status]) VALUES ((select MAX(ModelId)+1 from [" + GlobalesLocal.Tablamodelo + "]), @ModelDesc ,@MarcID, 1)";

                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ModelDesc", Value = modelo.ModelDesc });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@MarcID", Value = modelo.MarcID });

                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local", modelo, "WsInsertModelo");

                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL UPDATED";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERT OF MODEL";
                        data.Status = "ERROR";
                    }
                }

                if (GlobalesCorporativo.StatusAPP == "HIJO")

                {

                    string sSelect = "";
                    var d = "";
                    var ArrParametros1 = new List<SqlParameter>();

                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] INSERT INTO [dbo].[" + GlobalesLocal.Tablamodelo + "] ([ModelId] ,[ModelDesc] ,[MarcID],[Status]) VALUES ((select MAX(ModelId)+1 from [" + GlobalesLocal.Tablamodelo + "]), @ModelDesc,@MarcID, 1)";
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ModelDesc", Value = modelo.ModelDesc });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@MarcID", Value = modelo.MarcID });

                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local");

                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFUL UPDATED";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED INSERT OF MODEL";
                        data.Status = "ERROR";
                    }
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al insertar un modelo de vehiculo en la bd local", "WsInsertModelo");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al insertar  un modelo de vehiculo en la bd local", "Ha ocurrido un error en la consulta al insertar un modelo de vehiculo en la bd local", "", "", "", "");
                return Ok(mensaje);
            }



        }
        //gera
        // Este metodo es utilizado para obtener roles
        [HttpGet] [Route("GetModelByBrand")]
        public IHttpActionResult  GetModelByBrand(string MarcID = "0", string all = "")
        {
            List<modelo> lstmodelo = new List<modelo>();
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            var ArrParametros1 = new List<SqlParameter>();
            try
            {
                string sSelect = "";
                if (MarcID != "0" && MarcID != "" && all == "" || all == null)
                {

                    sSelect = "SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where MarcID=@MarcID and Status<>@status";
                    var status = 0;
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@MarcID", Value = MarcID });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Status", Value = status });
                }
                else
                {
                    if (all == "" || all == "0" || all == null)
                    {
                        sSelect = "  SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where Status=1 order by Status";
                    }
                    else
                    {
                        sSelect = "  SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "]where MarcID=@MarcID  order by Status";

                        ArrParametros1.Add(new SqlParameter { ParameterName = "@MarcID", Value = MarcID });

                    }


                }





                dT = BD.GetSQL(sSelect, "Local", ArrParametros1);
                var columnas = dT.Columns;
                //return Ok(dT.Rows);
                foreach (DataRow dR1 in dT.Rows)
                {


                    modelo modelo = new modelo();

                    modelo.ModelId = dR1["ModelId"].ToString();
                    modelo.ModelDesc = dR1["ModelDesc"].ToString();
                    modelo.MarcID = dR1["MarcID"].ToString();
                    modelo.Status = dR1["Status"].ToString();
                    lstmodelo.Add(modelo);

                }

                if (lstmodelo.Count > 0)
                {
                    data.lstmodelo = lstmodelo;
                    data.Message = "MODELOS";
                    data.Status = "OK";
                }
                else
                {
                    data.lstmodelo = lstmodelo;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Obtener los Roles", "GetModelByBrand");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Obtener los Roles", "Ha ocurrido un error en la consulta al Obtener los Roles", "", "", "", "");

                return Ok(mensaje);
            }



        }




        //gera
        // Este metodo es utilizado para insertar una configuracion en la base de datos local
        [HttpPost] [Route("WsInsertarConfiguracion")]
        public IHttpActionResult  WsInsertarConfiguracion(Configuracion configuracion)
        {
            int DataAfectedLocal = 0;

            DataResponse data = new DataResponse();
            var ArrParametros1 = new List<SqlParameter>();
            try
            {
                string sSelectLocaL = "";

                sSelectLocaL = "USE [" + GlobalesLocal.BDLocal + "] INSERT INTO [dbo].[" + GlobalesLocal.TablaConfiguracion + "] ([IPSERVIDOR],[IPCONTROLGAS1],[IPCONTROLGAS2],[IPPLC],[REPORTESPOR],[FECHA]) VALUES(@IPSERVIDOR, @IPCONTROLGAS1,@IPCONTROLGAS2,@IPPLC,@REPORTESPOR, GETDATE())";
                ArrParametros1.Add(new SqlParameter { ParameterName = "@IPSERVIDOR", Value = configuracion.IPSERVIDOR ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@IPCONTROLGAS1", Value = configuracion.IPCONTROLGAS1 ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@IPCONTROLGAS2", Value = configuracion.IPCONTROLGAS2 ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@IPPLC", Value = configuracion.IPPLC ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@REPORTESPOR", Value = configuracion.REPORTESPOR ?? (object)DBNull.Value });



                DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local");

                if (DataAfectedLocal > 0)
                {
                    data.Message = "SUCCESSFUL INSERTION";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED UPGRADE OF CONFIGURACIÓN";
                    data.Status = "ERROR";
                }
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar datos en configuracion", "WsInsertarConfiguracion");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar datos en configuracion", "Ha ocurrido un error en la consulta al actualizar datos en configuracion", "", "", "", "");
                return Ok(mensaje);
            }



        }


        //gera
        // Este metodo es utilizado para obtener los estados
        [HttpGet] [Route("WsGetestados")]
        public IHttpActionResult  WsGetestados()
        {
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            List<estados> lstestados = new List<estados>();
            var ArrParametros1 = new List<SqlParameter>();
            try
            {
                string sSelectLocaL = "";

                sSelectLocaL = "Select * from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaEstados + "]";

                dT = BD.GetSQL(sSelectLocaL, "Local", ArrParametros1);

                foreach (DataRow dR1 in dT.Rows)
                {

                    estados estados = new estados();

                    estados.id = dR1["id"];
                    estados.nombre = dR1["nombre"];
                    estados.clave = dR1["clave"];
                    lstestados.Add(estados);
                }

                if (lstestados.Count > 0)
                {
                    data.lstestados = lstestados;
                    data.Message = "estados";
                    data.Status = "OK";
                }
                else
                {
                    data.lstestados = lstestados;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar datos en configuracion", "WsGetestados");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar datos en configuracion", "Ha ocurrido un error en la consulta al actualizar datos en configuracion", "", "", "", "");
                return Ok(mensaje);
            }



        }




        //gera

        // Este metodo es utilizado para obtener los datos de los  CP
        [HttpGet] 
        [Route("WsGetDatosCP")]
        public IHttpActionResult  WsGetDatosCP(string cp)
        {
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            List<cp> lstcp = new List<cp>();
            var ArrParametros1 = new List<SqlParameter>();
            try
            {
                string sSelectLocaL = "";

                sSelectLocaL = "SELECT * FROM [" + GlobalesLocal.BDLocal + "].dbo.[" + GlobalesLocal.TablaCodigos_postales + "] WHERE cp = @cp ";

                ArrParametros1.Add(new SqlParameter { ParameterName = "@cp", Value = cp });

                dT = BD.GetSQL(sSelectLocaL, "Local", ArrParametros1);

                foreach (DataRow dR1 in dT.Rows)
                {

                    cp codpos = new cp();

                    codpos.colonia = dR1["colonia"];
                    codpos.municipio = dR1["municipio"];
                    codpos.estado = dR1["estado"];
                    lstcp.Add(codpos);
                }

                if (lstcp.Count > 0)
                {
                    data.lstcp = lstcp;
                    data.Message = "CP";
                    data.Status = "OK";
                }
                else
                {
                    data.lstcp = lstcp;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al obtener los codigos postales", "WsGetDatosCP");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al al obtener los codigos postales", "Ha ocurrido un error en la consulta al obtener los codigos postales", "", "", "", "");
                return Ok(mensaje);
            }



        }




        //gera

        // Este metodo es utilizado para obtener los municipios
        [HttpGet] [Route("WsGetmunicipios")]
        public IHttpActionResult  WsGetmunicipios()
        {
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            List<municipios> lstmunicipios = new List<municipios>();
            try
            {
                string sSelectLocaL = "";

                sSelectLocaL = "Select * from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMunicipios + "]";
                var ArrParametros1 = new List<SqlParameter>();
                dT = BD.GetSQL(sSelectLocaL, "Local", ArrParametros1);

                foreach (DataRow dR1 in dT.Rows)
                {

                    municipios municipios = new municipios();

                    municipios.id = dR1["id"];
                    municipios.nombre = dR1["nombre"];
                    municipios.clave = dR1["clave"];
                    municipios.estado_id = dR1["estado_id"];

                    lstmunicipios.Add(municipios);
                }

                if (lstmunicipios.Count > 0)
                {
                    data.lstmunicipios = lstmunicipios;
                    data.Message = "municipios";
                    data.Status = "OK";
                }
                else
                {
                    data.lstmunicipios = lstmunicipios;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al obtener los municipios", "WsGetmunicipios");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al obtener los municipios", "Ha ocurrido un error en la consulta al obtener los municipios", "", "", "", "");
                return Ok(mensaje);
            }



        }

        //gera


        // Este metodo es utilizado para obtener las marcas
        [HttpGet] [Route("WsGetmarcas")]
        public IHttpActionResult  WsGetmarcas(string id = "", string all = "")
        {
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            List<marca> lstmarcas = new List<marca>();
            var ArrParametros1 = new List<SqlParameter>();
            try
            {


                string sSelectLocaL = "";
                if (id != "0" && id != "")
                {
                    sSelectLocaL = "Select * from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] where MarcID=@MarcID";
                }
                else
                {
                    if (all == "" || all == "0" || all == null)
                    {
                        sSelectLocaL = "Select * from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] where Status='1' order by Status";
                    }
                    else
                    {
                        sSelectLocaL = "Select * from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaMarca + "] order by Status";
                    }
                }

                ArrParametros1.Add(new SqlParameter { ParameterName = "@MarcID", Value = id });
                dT = BD.GetSQL(sSelectLocaL, "Local", ArrParametros1);

                foreach (DataRow dR1 in dT.Rows)
                {

                    marca marcas = new marca();

                    marcas.MarcID = dR1["MarcID"].ToString();
                    marcas.MarcDesc = dR1["MarcDesc"].ToString();
                    marcas.Status = dR1["Status"].ToString();
                    lstmarcas.Add(marcas);
                }

                if (lstmarcas.Count > 0)
                {
                    data.lstmarcas = lstmarcas;
                    data.Message = "marcas";
                    data.Status = "OK";
                }
                else
                {
                    data.lstmarcas = lstmarcas;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al obtener las marcas", "WsGetmarcas");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al obtener las marcas", "Ha ocurrido un error en la consulta al obtener las marcas", "", "", "", "");
                return Ok(mensaje);
            }



        }

        //gera
        // Este metodo es utilizado para actualizar una marca de vehiculo en la base de de datos local
        //Samy
        [HttpPost] [Route("WsUpdateMarca")]
        public IHttpActionResult  WsUpdateMarca(marca marca)
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {
                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    string sSelect = "";
                    var d = "";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.TablaMarca + "] SET [MarcDesc] =@MarcDesc WHERE MarcID = @MarcID";
                    var ArrParametros1 = new List<SqlParameter>();
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@MarcDesc", Value = marca.MarcDesc });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@MarcID", Value = marca.MarcID });
                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local", marca, "WsUpdateMarca");
                    if (DataAfected > 0)
                    {
                        data.Message = "SUCCESSFULLY UPDATED";
                        data.Status = "OK";
                    }
                    else
                    {
                        data.Message = "FAILED UPDATED OF BRAND";
                        data.Status = "ERROR";
                    }
                }
                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {
                    string sSelect = "";
                    var d = "";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.TablaMarca + "] SET [MarcDesc] = @MarcDesc WHERE MarcID = @MarcID";
                    var ArrParametros2 = new List<SqlParameter>();
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@MarcDesc", Value = marca.MarcDesc });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@MarcID", Value = marca.MarcID });
                    DataAfected = BD.SetSQL(ArrParametros2, sSelect, "Local");
                    if (DataAfected > 0)
                    {
                        data.Message = "SUCCESSFULLY UPDATED";
                        data.Status = "OK";
                    }
                    else
                    {
                        data.Message = "FAILED UPDATED OF BRAND";
                        data.Status = "ERROR";
                    }
                }
                return Ok(data);
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar una marca de vehiculo en la bd local", "WsUpdateMarca");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar  una marca de vehiculo en la bd local", "Ha ocurrido un error en la consulta al actualizar una marca de vehiculo en la bd local", "", "", "", "");
                return Ok(mensaje);
            }
        }
        // Este metodo es utilizado para insertar una marca de vehiculo en la base de de datos local
        [HttpPost] [Route("WsInsertMarca")]
        public IHttpActionResult  WsInsertMarca(marca marca)
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {
                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    string sSelect = "";
                    var d = "";

                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] INSERT INTO [dbo].[" + GlobalesLocal.TablaMarca + "] ([MarcID] ,[MarcDesc] ,[Status]) VALUES ((select MAX(MarcID)+1 from [" + GlobalesLocal.TablaMarca + "]), @MarcDesc , 1)";

                    var ArrParametros1 = new List<SqlParameter>();

                    ArrParametros1.Add(new SqlParameter { ParameterName = "@MarcDesc", Value = marca.MarcDesc });

                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local", marca, "WsInsertMarca");

                    if (DataAfected > 0)
                    {
                        data.Message = "SUCCESSFUL UPDATED";
                        data.Status = "OK";
                    }
                    else
                    {
                        data.Message = "FAILED INSERT OF BRAND";
                        data.Status = "ERROR";
                    }
                }

                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {
                    string sSelect = "";
                    var d = "";

                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] INSERT INTO [dbo].[" + GlobalesLocal.TablaMarca + "] ([MarcID] ,[MarcDesc] ,[Status]) VALUES ((select MAX(MarcID)+1 from [" + GlobalesLocal.TablaMarca + "]), @MarcDesc, 1)";
                    var ArrParametros2 = new List<SqlParameter>();

                    ArrParametros2.Add(new SqlParameter { ParameterName = "@MarcDesc", Value = marca.MarcDesc });
                    DataAfected = BD.SetSQL(ArrParametros2, sSelect, "Local");

                    if (DataAfected > 0)
                    {
                        data.Message = "SUCCESSFUL UPDATED";
                        data.Status = "OK";
                    }
                    else
                    {
                        data.Message = "FAILED INSERT OF BRAND";
                        data.Status = "ERROR";
                    }
                }
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al insertar una marca de vehiculo en la bd local", "WsInsertMarca");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al insertar  una marca de vehiculo en la bd local", "Ha ocurrido un error en la consulta al insertar una marca de vehiculo en la bd local", "", "", "", "");
                return Ok(mensaje);
            }
        }
        // Este metodo es utilizado para obtener los modelos
        [HttpGet] [Route("WsGetmodelos")]
        public IHttpActionResult  WsGetmodelos()
        {
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            List<modelo> lstmodelo = new List<modelo>();
            try
            {
                string sSelectLocaL = "";
                sSelectLocaL = "Select * from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.Tablamodelo + "] where status<>0";
                var ArrParametros1 = new List<SqlParameter>();
                dT = BD.GetSQL(sSelectLocaL, "Local", ArrParametros1);
                foreach (DataRow dR1 in dT.Rows)
                {
                    modelo modelo = new modelo();
                    modelo.ModelId = dR1["ModelId"].ToString();
                    modelo.ModelDesc = dR1["ModelDesc"].ToString();
                    modelo.MarcID = dR1["MarcID"].ToString();
                    lstmodelo.Add(modelo);
                }
                if (lstmodelo.Count > 0)
                {
                    data.lstmodelo = lstmodelo;
                    data.Message = "marcas";
                    data.Status = "OK";
                }
                else
                {
                    data.lstmodelo = lstmodelo;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                return Ok(data);
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al obtener los modelos", "WsGetmodelos");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al obtener los modelos", "Ha ocurrido un error en la consulta al obtener los modelos", "", "", "", "");
                return Ok(mensaje);
            }
        }
        // Este metodo es utilizado para actualizar un modelo de vehiculo en la base de de datos local
        [HttpPost] [Route("WsUpdateModelo")]
        public IHttpActionResult  WsUpdateModelo(modelo modelo)
        {
            int DataAfected = 0;
            DataResponse data = new DataResponse();
            try
            {
                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    string sSelect = "";
                    var d = "";

                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.Tablamodelo + "] SET [ModelDesc] = @ModelDesc WHERE ModelId = @ModelId";

                    var ArrParametros1 = new List<SqlParameter>();

                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ModelDesc", Value = modelo.ModelDesc });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@ModelId", Value = modelo.ModelId });

                    DataAfected = BD.SetSQL(ArrParametros1, sSelect, "Local", modelo, "WsUpdateModelo");

                    if (DataAfected > 0)
                    {

                        data.Message = "SUCCESSFULLY UPDATED";
                        data.Status = "OK";
                    }
                    else
                    {

                        data.Message = "FAILED UPDATED OF MODEL";
                        data.Status = "ERROR";
                    }

                }

                if (GlobalesCorporativo.StatusAPP == "HIJO")
                {
                    string sSelect = "";
                    var d = "";
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.Tablamodelo + "] SET [ModelDesc] = @ModelDesc WHERE ModelId = @ModelId";

                    var ArrParametros2 = new List<SqlParameter>();

                    ArrParametros2.Add(new SqlParameter { ParameterName = "@ModelDesc", Value = modelo.ModelDesc });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@ModelId", Value = modelo.ModelId });

                    DataAfected = BD.SetSQL(ArrParametros2, sSelect, "Local");

                    if (DataAfected > 0)
                    {
                        data.Message = "SUCCESSFULLY UPDATED";
                        data.Status = "OK";
                    }
                    else
                    {
                        data.Message = "FAILED UPDATED OF MODEL";
                        data.Status = "ERROR";
                    }
                }
                return Ok(data);
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar un modelo de vehiculo en la bd local", "WsUpdateModelo");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar  un modelo de vehiculo en la bd local", "Ha ocurrido un error en la consulta al actualizar un modelo de vehiculo en la bd local", "", "", "", "");
                return Ok(mensaje);
            }
        }
        // Este metodo es utilizado para insertar una configuracion en la base de datos local
        [HttpPost] [Route("WsUpdateConfiguracion")]
        public IHttpActionResult  WsUpdateConfiguracion(Configuracion configuracion)
        {
            int DataAfectedLocal = 0;
            DataResponse data = new DataResponse();
            try
            {
                string sSelectLocaL = "";
                sSelectLocaL = "USE [" + GlobalesLocal.BDLocal + "] UPDATE [dbo].[" + GlobalesLocal.TablaConfiguracion + "] SET [IPSERVIDOR]=@IPSERVIDOR,[IPCONTROLGAS1]=@IPCONTROLGAS1,[IPCONTROLGAS2]=@IPCONTROLGAS2,[REPORTESPOR]=@REPORTESPOR,[FECHA]=GETDATE(),  [DESTINATARIOS]=@DESTINATARIOS ," +
                    "[USER_ACOUNT]=@USER_ACOUNT, [PASSWORD]=@PASSWORD, [SERVER_SMTP]=@SERVER_SMTP, [PORT]=@PORT where ID=@ID";
                var ArrParametros1 = new List<SqlParameter>();
                ArrParametros1.Add(new SqlParameter { ParameterName = "@IPSERVIDOR", Value = configuracion.IPSERVIDOR ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@IPCONTROLGAS1", Value = configuracion.IPCONTROLGAS1 ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@IPCONTROLGAS2", Value = configuracion.IPCONTROLGAS2 ?? (object)DBNull.Value });
                //ArrParametros1.Add(new SqlParameter { ParameterName = "@IPPLC", Value = configuracion.IPPLC ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@REPORTESPOR", Value = configuracion.REPORTESPOR ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@DESTINATARIOS", Value = configuracion.DESTINATARIOS ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@USER_ACOUNT", Value = configuracion.USER_ACOUNT ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@PASSWORD", Value = configuracion.PASSWORD ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@SERVER_SMTP", Value = configuracion.SERVER_SMTP ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@PORT", Value = configuracion.PORT ?? (object)DBNull.Value });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@ID", Value = configuracion.ID ?? (object)DBNull.Value });
                DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local");
                if (DataAfectedLocal > 0)
                {
                    data.Message = "SUCCESSFUL UPDATE";
                    data.Status = "OK";
                }
                else
                {
                    data.Message = "FAILED UPDATED OF CONFIGURACIÓN";
                    data.Status = "ERROR";
                }
                return Ok(data);
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Insertar en configuracion", "WsUpdateConfiguracion");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Insertar en configuracion", "Ha ocurrido un error en la consulta al Insertar en configuracion", "", "", "", "");
                return Ok(mensaje);
            }
        }
        //Este ws es utilizado para actualizar los acumulados en un  vehiculo en la base de datos de control Gas
        [HttpPost] [Route("WsUpdateAcumuladosVehicles")]
        public IHttpActionResult  WsUpdateAcumuladosVehicles(int value = 0, string tag = "")
        {
            try
            {
                DataResponse data = new DataResponse();
                int acudia = 0;
                int acusem = 0;
                int acumes = 0;
                string json = JsonConvert.SerializeObject(GetVehiculos("0", tag));
                JObject resVehiculo = JObject.Parse(json);
                IList<JToken> results = resVehiculo["Data"]["lstVehicles"].Children().ToList();
                acudia = Convert.ToInt32(results[0]["acudia"]) + value;
                acusem = Convert.ToInt32(results[0]["acusem"]) + value;
                acumes = Convert.ToInt32(results[0]["acumes"]) + value;
                int DataAfected = 0;
                string sSelect = "";

                sSelect = "USE [" + GlobalesCorporativo.BDCorporativoModerno + "]  UPDATE [dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "]SET acudia=@acudia, acusem=@acusem, acumes=@acumes WHERE tag=@tag";

                var ArrParametros1 = new List<SqlParameter>();

                ArrParametros1.Add(new SqlParameter { ParameterName = "@acudia", Value = acudia });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@acusem", Value = acusem });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@acumes", Value = acumes });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@tag", Value = tag });

                DataAfected = BD.SetSQL(ArrParametros1, sSelect, "ControlGas");
                if (DataAfected > 0)
                {
                    data.Message = "SUCCESSFUL UPGRADE OF VEHICLES";
                    data.Status = "OK";
                }
                else
                {
                    data.Message = "FAILED UPGRADE OF VEHICLES";
                    data.Status = "ERROR";
                }
                return Ok(data);
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar los acumulados en vehiculos en control gas", "WsUpdateAcumuladosVehicles");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar los acumulados en vehiculos  en control gas", "Ha ocurrido un error en la consulta al actualizar los acumulados en vehiculos en control gas", "", "", "", "");
                return Ok(mensaje);
            }
        }
        // Este metodo es utilizado para obtener la configuracion guardada en la tabla configuracion
        [HttpGet] [Route("GetConfiguracion")]
        public IHttpActionResult  GetConfiguracion(string ID = "0")
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
                //return Ok(dT.Rows);
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
                return Ok(data);
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al Obtener los datos de la configuracion", "GetConfiguracion");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al Obtener los datos de la configuracion", "Ha ocurrido un error en la consulta al Obtener los datos de la configuracion", "", "", "", "");
                return Ok(data);
            }
        }


        [HttpPost] [Route("WsCheckReport")]
        public IHttpActionResult  WsCheckReport()
        {
            DataResponse data = new DataResponse();
            try
            {
                string sSelect = "";
                DataTable dT = new DataTable();
                Configuracion ObjConfiguracion = new Configuracion();
                sSelect = "  SELECT * FROM [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaConfiguracion + "]";
                var ArrParametros1 = new List<SqlParameter>();
                dT = BD.GetSQL(sSelect, "Local", ArrParametros1);
                foreach (DataRow dR1 in dT.Rows)
                {

                    ObjConfiguracion.ID = dR1["ID"].ToString();
                    ObjConfiguracion.IPSERVIDOR = dR1["IPSERVIDOR"].ToString();
                    ObjConfiguracion.IPCONTROLGAS1 = dR1["IPCONTROLGAS1"].ToString();
                    ObjConfiguracion.IPCONTROLGAS2 = dR1["IPCONTROLGAS2"].ToString();
                    ObjConfiguracion.FECHA = dR1["FECHA"].ToString();
                    ObjConfiguracion.IPPLC = dR1["IPPLC"].ToString();
                    ObjConfiguracion.REPORTESPOR = dR1["REPORTESPOR"].ToString();
                    //*******************Comente las siguientes lineas ya que no se encuetran en la tabla*******************
                    //ObjConfiguracion.DESTINATARIOS = dR1["DESTINATARIOS"].ToString();
                    //ObjConfiguracion.EMAIL = dR1["EMAIL"].ToString();
                    //ObjConfiguracion.PASSWORD = dR1["PASSWORD"].ToString();
                    //ObjConfiguracion.SMTP = dR1["SMTP"].ToString();
                    //ObjConfiguracion.PORT = dR1["PORT"].ToString();
                    //*******************----------------------------------------------------------------*******************
                    var numdias = 0;
                    if (ObjConfiguracion.REPORTESPOR != "")
                    {
                        if (Convert.ToInt32(ObjConfiguracion.REPORTESPOR) > 0)
                        {
                            numdias = Convert.ToInt32(ObjConfiguracion.REPORTESPOR);
                        }
                    }

                    DateTime date1 = DateTime.Now;
                    DateTime date2 = Convert.ToDateTime(dR1["FECHA"].ToString());//dR1["FECHA"].ToString()  para test "30/12/2020")
                    DateTime date3 = date2.AddDays(-numdias);

                    int result = DateTime.Compare(date1, date2);
                    string relationship;

                    //EnviaCorreo(ObjConfiguracion.DESTINATARIOS, "Reports Fuel", "test", "", "", "C:/Users/Juan Luis Quijano/Downloads/Ejemplo Excel Reporte v2.xlsx", "REPORT_01012021_01302021.xlsx");
                    if (result > 0)
                    {
                        //si es mayor se envia correos de reportes
                        //primero metodo que va hacer el excel y regrea la url del archivo
                        //despues se envia el correo con los archivos solicitados
                        List<ReporteConsumos> Datos = WSGetDatosReporteConsumos(date3.ToString("MM/dd/yyyy"), date2.ToString("MM/dd/yyyy"));
                        data = EnviarReporteConsumos(ObjConfiguracion, "TANK FARM", Datos);
                    }
                    //ObjConfiguracion.FECHA =;
                }
                var json = Ok(data);
                
                return json;
            }
            catch (Exception e)
            {
                //DataResponse data = new DataResponse();
                data.Message = e.Message.ToString();
                data.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar en inbox", "WsUpdateInbox");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar en inbox", "Ha ocurrido un error en la consulta al actualizar en inbox", "", "", "", "");
                var json = Ok(data);
                
                return json;
            }
        }

        [HttpGet] [Route("WSGetDatosReporteConsumos")]
        public List<ReporteConsumos> WSGetDatosReporteConsumos(string StartDate, string EndDate)
        {

            List<ReporteConsumos> Datos = new List<ReporteConsumos>();

            try
            {
                DataTable dT = new DataTable();
                string sSelect = "SELECT top 10 isnull(nrotrn,'UNDEFINED') nrotrn,xFecha,isnull(Departamento, 'UNDEFINED') Departamento,isnull(CentroCostos, 'UNDEFINED') CentroCostos,isnull(xProducto, 'UNDEFINED') xProducto,can,isnull(xNombreChofer, 'UNDEFINED') xNombreChofer,(isnull(xMarca + xModelo, 'UNDEFINED')) vehicle,isnull(xCliente, 'UNDEFINED') xCliente FROM  [" + GlobalesLocal.BDLocal + "].[dbo].[VDespachosAll] where xFecha >=CONVERT(datetime,@StartDate,121) and xFecha <=CONVERT(datetime,@EndDate,121)ORDER BY nrotrn asc";

                var ArrParametros1 = new List<SqlParameter>();

                ArrParametros1.Add(new SqlParameter { ParameterName = "@StartDate", Value = StartDate });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@EndDate", Value = EndDate });

                dT = BD.GetSQL(sSelect, "Local", ArrParametros1);
                foreach (DataRow dR1 in dT.Rows)
                {
                    ReporteConsumos DatosReporte = new ReporteConsumos();
                    DatosReporte.ID = dR1["nrotrn"].ToString();
                    DatosReporte.DATE = Convert.ToDateTime(dR1["xFecha"].ToString()).ToString("dd/MM/yyyy");
                    DatosReporte.DEPARTMENT = dR1["Departamento"].ToString().ToUpper();
                    DatosReporte.CC = dR1["CentroCostos"].ToString().ToUpper();
                    DatosReporte.COMPANY = dR1["xCliente"].ToString().ToUpper();
                    DatosReporte.FUEL = dR1["xProducto"].ToString().ToUpper();
                    DatosReporte.QUANTITY = dR1["can"].ToString();
                    DatosReporte.DRIVER = dR1["xNombreChofer"].ToString().ToUpper();
                    DatosReporte.VEHICLE = dR1["vehicle"].ToString().ToUpper();
                    Datos.Add(DatosReporte);

                }
                return Datos;

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar en inbox", "WsUpdateInbox");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar en inbox", "Ha ocurrido un error en la consulta al actualizar en inbox", "", "", "", "");
                return null;
            }

        }

        [HttpPost] [Route("EnviarReporteConsumos")]
        public DataResponse EnviarReporteConsumos(Configuracion objConfig, string Estacion, List<ReporteConsumos> Datos)
        {
            DataResponse data = new DataResponse();
            try
            {
                var numdias = 0;
                if (objConfig.REPORTESPOR != "")
                {
                    if (Convert.ToInt32(objConfig.REPORTESPOR) > 0)
                    {
                        numdias = Convert.ToInt32(objConfig.REPORTESPOR);
                    }
                }
                DateTime FechaFinal = Convert.ToDateTime(objConfig.FECHA);//dR1["FECHA"].ToString()
                DateTime FechaInicial = FechaFinal.AddDays(-numdias);

                var oAssembly = Assembly.GetExecutingAssembly();
                int iRow = 12;
                string sHTML = "WSTankFarm.bmw.html"; //template_reporte
                var sTream = oAssembly.GetManifestResourceStream(sHTML);
                var oRead = new StreamReader(sTream);
                string strFile = oRead.ReadToEnd();
                DateTime Hoy = DateTime.Now;
                string PathReporte = @"C:/REPORTES/REPORT_v2.xlsx";
                SLDocument sl = new SLDocument(PathReporte);
                //DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
                sl.SetCellValue("A6", "DATE: " + Hoy.ToString("dd/MM/yyyy"));
                sl.SetCellValue("A7", "HOUR: " + Hoy.ToString("hh:mm:ss tt"));

                //ESTACION
                sl.SetCellValue("C2", System.Configuration.ConfigurationManager.AppSettings["Estacion"].ToString());

                //FECHA INICIO
                sl.SetCellValue("A11", FechaInicial.ToString("dd/MM/yyyy"));
                //FECHA FIN
                sl.SetCellValue("C11", FechaFinal.ToString("dd/MM/yyyy"));

                foreach (ReporteConsumos dR1 in Datos)
                {
                    iRow++;
                    //ID
                    sl.SetCellValue("A" + iRow, dR1.ID);
                    //FECHA
                    sl.SetCellValue("B" + iRow, dR1.DATE);
                    //DEPARTAMENTO
                    sl.SetCellValue("C" + iRow, dR1.DEPARTMENT);
                    //CENTRO COSTOS
                    sl.SetCellValue("D" + iRow, dR1.CC);
                    //COMPPANY
                    sl.SetCellValue("E" + iRow, dR1.COMPANY);
                    //FUEL
                    sl.SetCellValue("F" + iRow, dR1.FUEL);
                    //CANTIDAD
                    sl.SetCellValue("G" + iRow, dR1.QUANTITY);
                    //CONDUCTOR
                    sl.SetCellValue("I" + iRow, dR1.DRIVER);
                    //VEHICULO MARCA/ MODELO
                    sl.SetCellValue("J" + iRow, dR1.VEHICLE);


                }




                //sl.Save();
                //sl.SaveAs(Server.MapPath((@"C:/REPORTES/"+ "REPORT_"+ Hoy.ToString("dd-MM-yyyy") + ".xlsx"))); // Si es un libro nuevo
                sl.SaveAs(@"C:/REPORTES/" + "REPORT_" + Hoy.ToString("ddMMyyyyhhmmss") + ".xlsx"); // Si es un libro nuevo

                string PathReporteCorreo = @"C:/REPORTES/" + "REPORT_" + Hoy.ToString("ddMMyyyyhhmmss") + ".xlsx";
                strFile = strFile.Replace("#Estacion", System.Configuration.ConfigurationManager.AppSettings["Estacion"].ToString());
                strFile = strFile.Replace("#FechaInicio", FechaInicial.ToString("dd/MM/yyyy"));
                strFile = strFile.Replace("#Fechafin", FechaFinal.ToString("dd/MM/yyyy"));
                strFile = strFile.Replace("#NombreReporte", "REPORT_" + Hoy.ToString("ddMMyyyyhhmmss"));
                bool statusCorreo = EnviaCorreoReporte(objConfig, objConfig.DESTINATARIOS, " TEST -- FUEL CONSUMPTIONS REPORT |  " + Estacion, strFile, PathReporteCorreo, "", "", "Reporte");

                if (statusCorreo == true)
                {
                    WsInsertarInbox("OK REPORTE GENERADO Y ENVIADO", objConfig.DESTINATARIOS, 2, "ENVIADO", 1);
                    data.Message = "OK REPORTE GENERADO Y ENVIADO";
                    data.Status = "OK";
                }
                else
                {
                    WsInsertarInbox("ERROR: NO SE A PODIDO ENVIAR EL REPORTE", objConfig.DESTINATARIOS, 2, "ERROR", 1);
                    data.Message = "ERROR: NO SE A PODIDO ENVIAR EL REPORTE";
                    data.Status = "ERROR";
                }




                return data;


            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                data.Message = e.Message.ToString();
                data.Status = "Error";
                AgregaLog("HA OCURRIDO UN ERROR AL HACER EL REPORTE E:" + data.Message, "WsUpdateInbox");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "ERROR EN WS TANK FARM E:", "HA OCURRIDO UN ERROR AL HACER EL REPORTE E:" + data.Message, "", "", "", "");

                return data;
            }
        }

        public bool EnviaCorreoReporte(Configuracion objConfig, string Destinatarios, string sAsunto, string sBody, string sFileXML, string sNameXML, string sFilePDF, string sNamePDF)
        {

            try
            {
                DatosCorreo oCorreo = new DatosCorreo();//GetDatosCorreo();
                oCorreo.Correo = objConfig.USER_ACOUNT;
                oCorreo.Contrasenia = objConfig.PASSWORD;
                oCorreo.ServidorSMTP = objConfig.SERVER_SMTP;
                oCorreo.Puerto = objConfig.PORT;
                oCorreo.CCO = "";
                oCorreo.EnableSSL = true;

                var builder = new BodyBuilder();
                MimeMessage mail;
                mail = new MimeMessage();
                mail.From.Add(new MailboxAddress("", oCorreo.Correo));
                var sDir = Destinatarios.Split(';');
                foreach (string sD in sDir)
                {
                    if (sD.Trim().Length > 0)
                    {
                        AgregaLog("Para: " + sD.Trim(), "");
                        mail.To.Add(new MailboxAddress("", sD.Trim()));
                    }
                }

                sDir = oCorreo.CCO.Split(';');
                foreach (string sD in sDir)
                {
                    if (sD.Trim().Length > 0)
                    {
                        AgregaLog("ENVIAR CORREO  CCO: " + sD.Trim(), "");
                        mail.Bcc.Add(new MailboxAddress("", sD.Trim()));
                    }
                }

                mail.Subject = sAsunto;
                AgregaLog("Body: " + sBody.ToString(), "");
                //sBody.
                builder.HtmlBody = sBody;

                if (System.IO.File.Exists(sFileXML))
                {
                    builder.Attachments.Add(sFileXML);
                }

                if (System.IO.File.Exists("file.txt"))
                {
                    Console.WriteLine("Specified file exists.");
                }

                AgregaLog("Archivo PDF x enviar: " + sFilePDF.ToString(), "");
                if (System.IO.File.Exists(sFilePDF))
                {
                    builder.Attachments.Add(sFilePDF);
                }

                mail.Body = builder.ToMessageBody();

                // AgregaLog("Va a crear el SMTPClient Servidor: " + oCorreo.ServidorSMTP.ToString + ", Puerto: " + oCorreo.Puerto.ToString + ", Correo: " + oCorreo.Correo.ToString + ", Contraseña: " + oCorreo.Contrasenia.ToString)
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    AgregaLog("Va a conectar", "");
                    client.ServerCertificateValidationCallback = (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
                    client.Connect(oCorreo.ServidorSMTP, int.Parse(oCorreo.Puerto), SecureSocketOptions.Auto);
                    AgregaLog("Asigna usuario y contraseña", "");
                    client.Authenticate(oCorreo.Correo, oCorreo.Contrasenia);
                    AgregaLog("Enviar", "");
                    client.Send(mail);
                    AgregaLog("Desconectar", "");
                    client.Disconnect(true);
                };

                return true;
            }
            catch (Exception ex)
            {
                AgregaLog("Error al enviar correo " + ex.Message.ToString(), "EnviaCorreo");
                //EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Error al enviar correo", "Error al enviar correo", "", "", "", "");
                return false;
            }
        }

        //VALIDACIONESSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
        // Este metodo es utilizado para obtener la configuracion guardada en la tabla configuracion
        [HttpPost] [Route("WsValidarVehiculo")]
        public IHttpActionResult  WsValidarVehiculo(string Tag = "0")
        {
            {
                List<Vehiculo> lstVehiculos = new List<Vehiculo>();
                DataTable dT = new DataTable();
                DataResponse data = new DataResponse();
                try
                {
                    string sSelect = "";
                    if (Tag != "0")
                    {
                        sSelect = "SELECT * FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[" + GlobalesCorporativo.TablaClientesVehiculos + "] WHERE tag =@Tag";
                    }

                    var ArrParametros1 = new List<SqlParameter>();

                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Tag", Value = Tag });

                    dT = BD.GetSQL(sSelect, "ControlGas", ArrParametros1);
                    var columnas = dT.Columns;
                    //return Ok(dT.Rows);
                    foreach (DataRow dR1 in dT.Rows)
                    {
                        Vehiculo ObjVehiculo = new Vehiculo();
                        ObjVehiculo.codcli = Convert.ToInt32(dR1["codcli"].ToString());
                        ObjVehiculo.nroveh = Convert.ToInt32(dR1["nroveh"].ToString());
                        ObjVehiculo.tar = Convert.ToInt32(dR1["tar"].ToString());
                        ObjVehiculo.plc = dR1["plc"].ToString();
                        ObjVehiculo.den = dR1["den"].ToString();

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

                        lstVehiculos.Add(ObjVehiculo);
                    }
                    if (lstVehiculos.Count > 0)
                    {
                        data.lstVehicles = lstVehiculos;
                        data.Message = "VEHICLE FOUND";
                        data.Status = "OK";
                    }
                    else
                    {
                        data.lstVehicles = lstVehiculos;
                        data.Message = "NO VEHICLE EXIST";
                        data.Status = "OK";
                    }
                    return Ok(data);
                }
                catch (Exception e)
                {
                    var mensaje = new Mensajes();
                    mensaje.Mensaje = e.Message.ToString();
                    mensaje.Status = "Error";
                    AgregaLog("Ha ocurrido un error en la consulta al verificar si existe un vehiculo", "WsValidarVehiculo");
                    EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al verificar si existe un vehiculo", "Ha ocurrido un error en la consulta al verificar si existe un vehiculo", "", "", "", "");
                    return Ok(mensaje);
                }
            }
        }

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


        [HttpPost] [Route("WsUpdateDespachoControlGas")]
        public IHttpActionResult  ValidarLogin(string UserName, string Password)
        {
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
                if (sSql.Length > 0)
                {

                    var ArrParametros1 = new List<SqlParameter>();

                    ArrParametros1.Add(new SqlParameter { ParameterName = "@UserName", Value = UserName });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Password", Value = Password });


                    dT = BD.GetSQL(sSql, "Local", ArrParametros1);

                    if (dT.Rows.Count > 0)
                    {
                        foreach (DataRow dR1 in dT.Rows)
                        {
                            ObjUsuarios.ID = Convert.ToInt32(dR1["ID"]);
                            ObjUsuarios.Nombre = dR1["Nombre"].ToString();
                            ObjUsuarios.LastName = dR1["LastName"].ToString();
                            ObjUsuarios.SecondLastName = dR1["SecondLastName"].ToString();
                            ObjUsuarios.UserName = dR1["UserName"].ToString();
                            ObjUsuarios.Password = dR1["Password"].ToString();
                            ObjUsuarios.IdRol = Convert.ToInt32(dR1["IdRol"]);
                            ObjUsuarios.IdIdioma = Convert.ToInt32(dR1["IdIdioma"]);
                            ObjUsuarios.Telefono = dR1["Telefono"].ToString();
                            ObjUsuarios.Correo = dR1["Correo"].ToString();
                            ObjUsuarios.Status = dR1["Status"].ToString();
                            ObjUsuarios.Compania = dR1["Compania"].ToString();
                            ObjUsuarios.Token = Guid.NewGuid().ToString();
                        }

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
                                lstUsuarios.Add(ObjUsuarios);
                            }
                        }
                        if (lstRoles.Count > 0)
                        {
                            data.lstRoles = lstRoles;
                            data.Message = "ROl";
                            data.Status = "OK";
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
                }
                return Ok(data);
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al validar el login" + e, "ValidarLogin");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al validar el login" + e, "Ha ocurrido un error en la consulta al validar el login", "", "", "", "");
                return Ok(mensaje);
            }

        }

        [HttpGet]
        [Route("ValidarHorarioChofer")]
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
        [Route("ValidarHorarioVehiculo")]
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
        [HttpPost] [Route("WsUpdateOdometro")]
        public IHttpActionResult  WsUpdateOdometro(string tag, string odometro)
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
                return Ok(data);

            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar el odometro en control gas", "WsUpdateOdometro");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar el odometro en control gas", "Ha ocurrido un error en la consulta al actualizar el odometro en control gas", "", "", "", "");
                return Ok(mensaje);
            }



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

        public void LeerInbox()
        {
            try
            {
                int intentos = 0;
                int dias = 0;
                int difDias;
                bool StatusReporte;
                DateTime FechaBd = new DateTime();
                TimeSpan difFechas;
                DateTime Hoy = DateTime.Now;
                List<Configuracion> lstConfiguracion = new List<Configuracion>();
                List<Inbox> lstInbox = new List<Inbox>();
                Inbox ObjInbox = new Inbox();
                //lstConfiguracion = GetConfiguracion("0");

                foreach (Configuracion item in lstConfiguracion)
                {
                    dias = Convert.ToInt32(item.REPORTESPOR);
                    FechaBd = Convert.ToDateTime(item.FECHA);
                }

                difFechas = Hoy - FechaBd;
                difDias = difFechas.Days;

                //Esto se deberia ejecutar una vez al dia para que no se este haciendo cada vez, si no es necesario poner alguna validacion aqui
                if (difDias >= dias)
                {
                    // lstInbox = GetDatosInbox();
                    //foreach (var item in lstInbox)
                    //{
                    //    intentos = Convert.ToInt32(item.INTENTOS);

                    //}
                    WsInsertarInbox("Correo de reportes", "gerardo.carrillo@tdcon40.com", 2, "Enviando", 0);




                    StatusReporte = ReporteExcelChoferes();
                    if (StatusReporte == true)
                    {
                        //intentos = intentos + 1;
                        WsUpdateInbox("Correo de reportes", "gerardo.carrillo@tdcon40.com", 2, "Enviado", 1);
                    }
                    else
                    {
                        //intentos = intentos - 1;
                        WsUpdateInbox("Correo de reportes", "gerardo.carrillo@tdcon40.com", 2, "Error", 0);
                        EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Error al enviar un correo", "Ocurrio un error al enviar el correo de los reportes", "", "", "", "");
                    }
                }
                else
                {
                    bool Inboxresult = GetInbox();
                    if (Inboxresult == true)
                    {


                        WsInsertarInbox("Correo de reportes", "gerardo.carrillo@tdcon40.com", 2, "Enviando", 0);
                        StatusReporte = ReporteExcelChoferes();
                        if (StatusReporte == true)
                        {
                            //intentos = intentos + 1;
                            WsUpdateInbox("Correo de reportes", "gerardo.carrillo@tdcon40.com", 2, "Enviado", 1);
                        }
                        else
                        {
                            //intentos = intentos - 1;
                            WsUpdateInbox("Correo de reportes", "gerardo.carrillo@tdcon40.com", 2, "Error", 0);
                            EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Error al enviar un correo", "Ocurrio un error al enviar el correo de los reportes", "", "", "", "");
                        }
                        intentos = 0;
                        //POdria llamar a que intetne enviar otra vez pero podria ciclarse
                    }
                }
            }
            catch (Exception)
            {


            }

        }




        public bool GetInbox()
        {
            bool result = false;
            System.Data.DataTable dT;
            string sSelect = "";
            sSelect = "select* from [" + GlobalesLocal.BDLocal + "].[dbo].[INBOX_CORREO] where STATUSMSJ='Error' or STATUSMSJ='Enviando' ";
            var ArrParametros1 = new List<SqlParameter>();

            dT = BD.GetSQL(sSelect, "Local", ArrParametros1);

            foreach (DataRow dR1 in dT.Rows)
            {
                if (dT.Rows.Count > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;

        }

        public List<Inbox> GetDatosInbox()
        {
            bool result = false;
            System.Data.DataTable dT;
            List<Inbox> lstInbox = new List<Inbox>();
            Inbox ObjInbox = new Inbox();
            string sSelect = "";
            sSelect = "select* from [" + GlobalesLocal.BDLocal + "].[dbo].[INBOX_CORREO] where STATUSMSJ='Error' or STATUSMSJ='Enviando' ";

            var ArrParametros1 = new List<SqlParameter>();

            dT = BD.GetSQL(sSelect, "Local", ArrParametros1);

            foreach (DataRow dR1 in dT.Rows)
            {
                ObjInbox.FECHA = dR1["FECHA"];
                ObjInbox.MENSAJE = dR1["MENSAJE"];
                ObjInbox.CORREO = dR1["CORREO"];
                ObjInbox.IDUSER = dR1["IDUSER"];
                ObjInbox.STATUSMSJ = dR1["STATUSMSJ"];
                ObjInbox.INTENTOS = dR1["INTENTOS"];

                lstInbox.Add(ObjInbox);
                break;
            }
            return lstInbox;

        }

        // Este metodo es utilizado para insertar una un mensaje por enviar en la tabla de inbox en la base de datos local
        [HttpPost] [Route("WsInsertarInbox")]
        public string WsInsertarInbox(string MENSAJE, string CORREO, int IDUSER, string STATUSMSJ, int INTENTOS)
        {
            int DataAfectedLocal = 0;
            DataResponse data = new DataResponse();
            try
            {
                string sSelectLocaL = "";

                sSelectLocaL = " INSERT INTO[dbo].[INBOX_CORREO] ([FECHA] ,[MENSAJE] ,[CORREO] ,[IDUSER] ,[STATUSMSJ],[INTENTOS]) VALUES (GETDATE() ,@MENSAJE,@CORREO,@IDUSER,@STATUSMSJ,@INTENTOS)  SELECT SCOPE_IDENTITY();";

                var ArrParametros1 = new List<SqlParameter>();

                ArrParametros1.Add(new SqlParameter { ParameterName = "@MENSAJE", Value = MENSAJE });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@CORREO", Value = CORREO });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@IDUSER", Value = IDUSER });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@STATUSMSJ", Value = STATUSMSJ });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@INTENTOS", Value = INTENTOS });

                DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local");

                if (DataAfectedLocal > 0)
                {
                    data.Message = "SUCCESSFUL INSERTION";
                    data.Status = "OK";
                }
                else
                {

                    data.Message = "FAILED INSERTION OF INBOX";
                    data.Status = "ERROR";
                }
                return data.Message;
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al insertar en inbox", "WsInsertarInbox");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al insertar en inbox", "Ha ocurrido un error en la consulta al insertar en inbox", "", "", "", "");
                return data.Message;
            }
        }

        // Este metodo es utilizado para actualizar una un mensaje por enviar en la tabla de inbox en la base de datos local
        [HttpPost] [Route("WsUpdateInbox")]
        public string WsUpdateInbox(string MENSAJE, string CORREO, int IDUSER, string STATUSMSJ, int INTENTOS)
        {
            int DataAfectedLocal = 0;
            DataResponse data = new DataResponse();
            try
            {
                string sSelectLocaL = "";

                sSelectLocaL = "UPDATE [dbo].[INBOX_CORREO] SET [FECHA]=GETDATE() ,[MENSAJE]=@MENSAJE,[CORREO]=@CORREO,[IDUSER]=@IDUSER,[STATUSMSJ]=@STATUSMSJ,INTENTOS=@INTENTOS WHERE STATUSMSJ='Enviando' or STATUSMSJ='Error'";

                var ArrParametros1 = new List<SqlParameter>();

                ArrParametros1.Add(new SqlParameter { ParameterName = "@MENSAJE", Value = MENSAJE });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@CORREO", Value = CORREO });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@IDUSER", Value = IDUSER });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@STATUSMSJ", Value = STATUSMSJ });
                ArrParametros1.Add(new SqlParameter { ParameterName = "@INTENTOS", Value = INTENTOS });

                DataAfectedLocal = BD.SetSQL(ArrParametros1, sSelectLocaL, "Local");

                if (DataAfectedLocal > 0)
                {
                    data.Message = "SUCCESSFUL UPDATE";
                    data.Status = "OK";
                }
                else
                {
                    data.Message = "FAILED UPGRADE OF INBOX";
                    data.Status = "ERROR";
                }
                return data.Message;
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar en inbox", "WsUpdateInbox");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar en inbox", "Ha ocurrido un error en la consulta al actualizar en inbox", "", "", "", "");
                return data.Message;
            }
        }

        // Este metodo es utilizado para actualizar una un mensaje por enviar en la tabla de inbox en la base de datos local
        [HttpPost] [Route("WsUpdatenNombres")]
        public string WsUpdatenNombres()
        {
            int DataAfectedLocal = 0;
            var name = "";
            var ID = "";
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            try
            {
                string sSelectLocaL = "";

                sSelectLocaL = "select replace(ltrim(rtrim(concat(nombre,LastName,SecondLastName))),' ','' )as Name,ID FROM [TANKFARM].[dbo].[CHOFERES]";

                var ArrParametros1 = new List<SqlParameter>();


                dT = BD.GetSQL(sSelectLocaL, "local", ArrParametros1);
                var columnas = dT.Columns;
                //return Ok(dT.Rows);
                foreach (DataRow dR1 in dT.Rows)
                {
                    name = dR1["Name"].ToString();
                    ID = dR1["ID"].ToString();

                    string updatename = "";
                    updatename = "UPDATE[dbo].[CHOFERES] SET[NameComplete] =@name WHERE ID=@ID";

                    var ArrParametros2 = new List<SqlParameter>();

                    ArrParametros2.Add(new SqlParameter { ParameterName = "@name", Value = name });
                    ArrParametros2.Add(new SqlParameter { ParameterName = "@ID", Value = ID });


                    DataAfectedLocal = BD.SetSQL(ArrParametros2, updatename, "Local");
                    if (DataAfectedLocal > 0)
                    {
                        data.Message = "SUCCESSFUL UPDATE";
                        data.Status = "OK";
                    }
                    else
                    {
                        data.Message = "FAILED UPGRADE OF INBOX";
                        data.Status = "ERROR";
                    }
                }
                return data.Message;
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Ha ocurrido un error en la consulta al actualizar en inbox", "WsUpdateInbox");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Ha ocurrido un error en la consulta al actualizar en inbox", "Ha ocurrido un error en la consulta al actualizar en inbox", "", "", "", "");
                return data.Message;
            }
        }

        [HttpPost] [Route("ReporteExcelChoferes")]
        public bool ReporteExcelChoferes()
        {
            var oAssembly = Assembly.GetExecutingAssembly();
            string sHTML = "WSTankFarm.bmw.html";
            var sTream = oAssembly.GetManifestResourceStream(sHTML);
            var oRead = new StreamReader(sTream);
            string strFile = oRead.ReadToEnd();
            DateTime Hoy = DateTime.Now;
            bool statusCorreo;
            bool statusReporte = false;
            try
            {
                SLDocument sl = new SLDocument();
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(@"C:/Users/Gerardo/Downloads/WSTankFarm sin https/WSTankFarm/img/logo.png");
                byte[] ba;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Close();
                    ba = ms.ToArray();
                }
                SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
                pic.SetPosition(0, 0);
                pic.ResizeInPixels(90, 120);
                sl.InsertPicture(pic);
                sl.SetCellValue("C3", "Reporte");
                SLStyle estiloT = sl.CreateStyle();
                estiloT.Font.FontName = "Arial";
                estiloT.Font.FontSize = 14;
                estiloT.Font.Bold = true;
                sl.SetCellStyle("C3", estiloT);
                sl.MergeWorksheetCells("C3", "C3");

                int celdaCabecera = 5;
                int celdaInicial = 5;
                sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Reporte");


                sl.SetCellValue("B" + celdaCabecera, "nrotrn");
                sl.SetCellValue("C" + celdaCabecera, "codgas");
                sl.SetCellValue("D" + celdaCabecera, "fchtrn");
                sl.SetCellValue("E" + celdaCabecera, "hratrn");
                sl.SetCellValue("F" + celdaCabecera, "fchcor");
                sl.SetCellValue("G" + celdaCabecera, "nrotur");
                sl.SetCellValue("H" + celdaCabecera, "codisl");
                sl.SetCellValue("I" + celdaCabecera, "nrobom");
                sl.SetCellValue("J" + celdaCabecera, "graprd");
                sl.SetCellValue("K" + celdaCabecera, "codprd");
                sl.SetCellValue("L" + celdaCabecera, "codcli");
                sl.SetCellValue("M" + celdaCabecera, "nroveh");
                sl.SetCellValue("F" + celdaCabecera, "tar");
                sl.SetCellValue("N" + celdaCabecera, "odm");
                sl.SetCellValue("O" + celdaCabecera, "rut");
                sl.SetCellValue("P" + celdaCabecera, "nrocho");
                sl.SetCellValue("Q" + celdaCabecera, "cho");
                sl.SetCellValue("R" + celdaCabecera, "codres");
                sl.SetCellValue("S" + celdaCabecera, "tiptrn");
                sl.SetCellValue("T" + celdaCabecera, "nrocte");
                sl.SetCellValue("U" + celdaCabecera, "fchcte");
                sl.SetCellValue("V" + celdaCabecera, "nrofac");
                sl.SetCellValue("W" + celdaCabecera, "gasfac");
                sl.SetCellValue("X" + celdaCabecera, "nroedc");
                sl.SetCellValue("Y" + celdaCabecera, "chkedc");
                sl.SetCellValue("Z" + celdaCabecera, "nroarc");
                sl.SetCellValue("AA" + celdaCabecera, "pto");
                sl.SetCellValue("AB" + celdaCabecera, "pre");
                sl.SetCellValue("AC" + celdaCabecera, "can");
                sl.SetCellValue("AD" + celdaCabecera, "mto");
                sl.SetCellValue("AE" + celdaCabecera, "mtogto");
                sl.SetCellValue("AF" + celdaCabecera, "niv");
                sl.SetCellValue("AG" + celdaCabecera, "xEstacion");
                sl.SetCellValue("AH" + celdaCabecera, "xFecha");
                sl.SetCellValue("AI" + celdaCabecera, "xFechaCorte");
                sl.SetCellValue("AJ" + celdaCabecera, "xTurno");
                sl.SetCellValue("AK" + celdaCabecera, "xCorte");
                sl.SetCellValue("AL" + celdaCabecera, "xDespacho");
                sl.SetCellValue("AM" + celdaCabecera, "xProducto");
                sl.SetCellValue("AN" + celdaCabecera, "xProductoUni");
                sl.SetCellValue("AO" + celdaCabecera, "xChofer");
                sl.SetCellValue("AP" + celdaCabecera, "xDespachador");
                sl.SetCellValue("AQ" + celdaCabecera, "xPlacas");
                sl.SetCellValue("AR" + celdaCabecera, "datref");
                sl.SetCellValue("AS" + celdaCabecera, "datref");
                sl.SetCellValue("AT" + celdaCabecera, "satrfc");
                sl.SetCellValue("AU" + celdaCabecera, "satuid");

                SLStyle estiloCA = sl.CreateStyle();
                estiloCA.Font.FontName = "Arial";
                estiloCA.Font.FontSize = 12;
                estiloCA.Font.Bold = true;
                estiloCA.Font.FontColor = System.Drawing.Color.White;
                estiloCA.Fill.SetPattern(PatternValues.Solid, System.Drawing.ColorTranslator.FromHtml("#40a8d9"), System.Drawing.ColorTranslator.FromHtml("#40a8d9"));
                sl.SetCellStyle("B" + celdaCabecera, "AU" + celdaCabecera, estiloCA);

                SqlConnection cnn = new SqlConnection();
                string SelectString = "SELECT TOP 1000 [nrotrn] ,[codgas] ,[fchtrn] ,[hratrn] ,[fchcor] ,[nrotur] ,[codisl] ,[nrobom] ,[graprd] ,[codprd] ,[codcli] ,[nroveh] ,[tar] ,[odm] ,[rut] ,[nrocho] ,[cho] ,[codres] ,[tiptrn] ,[nrocte] ,[fchcte] ,[nrofac] ,[gasfac] ,[nroedc] ,[chkedc] ,[nroarc] ,[pto] ,[pre] ,[can] ,[mto] ,[mtogto] ,[niv] ,[xEstacion] ,[xFecha] ,[xFechaCorte] ,[xTurno] ,[xCorte] ,[xDespacho] ,[xProducto] ,[xProductoUni] ,[xCliente] ,[xChofer] ,[xDespachador] ,[xPlacas] ,[datref] ,[satrfc] ,[satuid] FROM [" + GlobalesCorporativo.BDCorporativoModerno + "].[dbo].[VDespachosAll]";
                cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionSQLControlGas"].ToString());
                cnn.Open();
                SqlCommand comando = new SqlCommand(SelectString, cnn);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    celdaCabecera++;
                    sl.SetCellValue("B" + celdaCabecera, reader["nrotrn"].ToString());
                    sl.SetCellValue("C" + celdaCabecera, reader["codgas"].ToString());
                    sl.SetCellValue("D" + celdaCabecera, reader["fchtrn"].ToString());
                    sl.SetCellValue("E" + celdaCabecera, reader["hratrn"].ToString());
                    sl.SetCellValue("F" + celdaCabecera, reader["fchcor"].ToString());
                    sl.SetCellValue("G" + celdaCabecera, reader["nrotur"].ToString());
                    sl.SetCellValue("H" + celdaCabecera, reader["codisl"].ToString());
                    sl.SetCellValue("I" + celdaCabecera, reader["nrobom"].ToString());
                    sl.SetCellValue("J" + celdaCabecera, reader["graprd"].ToString());
                    sl.SetCellValue("K" + celdaCabecera, reader["codprd"].ToString());
                    sl.SetCellValue("L" + celdaCabecera, reader["codcli"].ToString());
                    sl.SetCellValue("M" + celdaCabecera, reader["nroveh"].ToString());
                    sl.SetCellValue("F" + celdaCabecera, reader["tar"].ToString());
                    sl.SetCellValue("N" + celdaCabecera, reader["odm"].ToString());
                    sl.SetCellValue("O" + celdaCabecera, reader["rut"].ToString());
                    sl.SetCellValue("P" + celdaCabecera, reader["nrocho"].ToString());
                    sl.SetCellValue("Q" + celdaCabecera, reader["cho"].ToString());
                    sl.SetCellValue("R" + celdaCabecera, reader["codres"].ToString());
                    sl.SetCellValue("S" + celdaCabecera, reader["tiptrn"].ToString());
                    sl.SetCellValue("T" + celdaCabecera, reader["nrocte"].ToString());
                    sl.SetCellValue("U" + celdaCabecera, reader["fchcte"].ToString());
                    sl.SetCellValue("V" + celdaCabecera, reader["nrofac"].ToString());
                    sl.SetCellValue("W" + celdaCabecera, reader["gasfac"].ToString());
                    sl.SetCellValue("X" + celdaCabecera, reader["nroedc"].ToString());
                    sl.SetCellValue("Y" + celdaCabecera, reader["chkedc"].ToString());
                    sl.SetCellValue("Z" + celdaCabecera, reader["nroarc"].ToString());
                    sl.SetCellValue("AA" + celdaCabecera, reader["pto"].ToString());
                    sl.SetCellValue("AB" + celdaCabecera, reader["pre"].ToString());
                    sl.SetCellValue("AC" + celdaCabecera, reader["can"].ToString());
                    sl.SetCellValue("AD" + celdaCabecera, reader["mto"].ToString());
                    sl.SetCellValue("AE" + celdaCabecera, reader["mtogto"].ToString());
                    sl.SetCellValue("AF" + celdaCabecera, reader["niv"].ToString());
                    sl.SetCellValue("AG" + celdaCabecera, reader["xEstacion"].ToString());
                    sl.SetCellValue("AH" + celdaCabecera, reader["xFecha"].ToString());
                    sl.SetCellValue("AI" + celdaCabecera, reader["xFechaCorte"].ToString());
                    sl.SetCellValue("AJ" + celdaCabecera, reader["xTurno"].ToString());
                    sl.SetCellValue("AK" + celdaCabecera, reader["xCorte"].ToString());
                    sl.SetCellValue("AL" + celdaCabecera, reader["xDespacho"].ToString());
                    sl.SetCellValue("AM" + celdaCabecera, reader["xProducto"].ToString());
                    sl.SetCellValue("AN" + celdaCabecera, reader["xProductoUni"].ToString());
                    sl.SetCellValue("AO" + celdaCabecera, reader["xChofer"].ToString());
                    sl.SetCellValue("AP" + celdaCabecera, reader["xDespachador"].ToString());
                    sl.SetCellValue("AQ" + celdaCabecera, reader["xPlacas"].ToString());
                    sl.SetCellValue("AR" + celdaCabecera, reader["datref"].ToString());
                    sl.SetCellValue("AS" + celdaCabecera, reader["datref"].ToString());
                    sl.SetCellValue("AT" + celdaCabecera, reader["satrfc"].ToString());
                    sl.SetCellValue("AU" + celdaCabecera, reader["satuid"].ToString());
                }

                SLStyle EstiloBorde = sl.CreateStyle();
                EstiloBorde.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                EstiloBorde.Border.LeftBorder.Color = System.Drawing.Color.Black;

                EstiloBorde.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                EstiloBorde.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                EstiloBorde.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;



                sl.SetCellStyle("B" + celdaInicial, "AU" + celdaCabecera, EstiloBorde);
                SLStyle estiloNumero = sl.CreateStyle();
                //estiloNumero.FormatCode = "#,##0.00";
                //sl.SetCellStyle("B" + celdaInicial, "AT" + celdaCabecera, estiloNumero);

                sl.AutoFitColumn("B", "AU");

               // //sl.SaveAs(Server.MapPath((@"\Reporte.xlsx"))); // Si es un libro nuevo
               //// string Path = Server.MapPath((@"\Reporte.xlsx")).ToString();
               // strFile = strFile.Replace("#FECHA", Hoy.ToString());
               // strFile = strFile.Replace("#NOMBRE", "Reporte por Chofer");
               // //statusCorreo = EnviaCorreo("gerardo.carrillo@tdcon40.com", "Reporte generado", strFile, Path, "", "", "Reporte");

               // if (statusCorreo == true)
               // {
               //     statusReporte = true;
               // }
               // else
               // {
               //     statusReporte = false;
               // }
            }
            catch (Exception e)
            {
                statusReporte = false;

            }
            return statusReporte;
        }

        [HttpGet] [Route("GetDatosProc")]
        public IHttpActionResult  GetDatosProc(string FechaIni = "", string FechaFin = "", string columnaBuscar = "", string TipoCombustible = "")
        {
            List<Despacho> lstDespachos = new List<Despacho>();
            List<DatosResultProc> lstProc = new List<DatosResultProc>();
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            List<String> lsColumns = new List<string>();
            List<String> lsRows = new List<string>();
            DatosResultProc ObjProc = new DatosResultProc();
            var f = 0;
            try
            {

                string sSelect = "USE [" + GlobalesLocal.BDLocal + "] EXEC [dbo].[ProcxReporte] @FechaInicio=@FechaIni,@FechaFin=@FechaFin,@ColumnaBuscar=@columnaBuscar,@Combustible=@TipoCombustible";

                if (sSelect != "")
                {
                    var ArrParametros1 = new List<SqlParameter>();

                    ArrParametros1.Add(new SqlParameter { ParameterName = "@FechaIni", Value = FechaIni });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@FechaFin", Value = FechaFin });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@columnaBuscar", Value = columnaBuscar });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@TipoCombustible", Value = TipoCombustible });

                    dT = BD.GetSQL(sSelect, "ControlGas", ArrParametros1);
                    var columnas = dT.Columns;
                    ObjProc.ArrayFechas = lsColumns;
                    foreach (DataRow dR1 in dT.Rows)
                    {
                        ObjProc = new DatosResultProc();
                        for (int x = 0; x < dR1.ItemArray.Length; x++)
                        {

                            lsRows.Add(dR1.ItemArray[x].ToString());


                            if (f == 0)
                            {
                                f = f + 1;
                                for (int i = 0; i < dT.Columns.Count; i++)
                                {

                                    lsColumns.Add(dT.Columns[i].ColumnName.ToString());

                                }
                            }


                        }

                        ObjProc.ArrayProc = lsRows;

                        lstProc.Add(ObjProc);
                        lsRows = new List<string>();
                    }
                    ObjProc.ArrayFechas = lsColumns;
                }

                if (lstProc.Count > 0)
                {
                    data.lstProc = lstProc;
                    data.Message = "DATA PROC";
                    data.Status = "OK";
                }
                else
                {
                    data.lstProc = lstProc;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                var json = Ok(data);
                
                return json;
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Error en la consulta no se pueden obtener los Despachos", "GetDatosProc");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Error en la consulta no se pueden obtener los despachos de la vista", "Error en la consulta no se pueden obtener los despachos de la vista", "", "", "", "");
                return Ok(mensaje);
            }
        }


        [HttpGet]
        [Route("GetReporteGlobal")]
        public IHttpActionResult  GetReporteGlobal(string FechaIni = "", string FechaFin = "", string Combustible = "")
        {

            List<ReporteGlobal> lstReporteGlobal = new List<ReporteGlobal>();
            DataTable dT = new DataTable();
            DataResponse data = new DataResponse();
            List<string> ArrayCentroCostos = new List<string>();
            List<double> ArrayCantidad = new List<double>();
            ReporteGlobal ObjReporteGlobal = new ReporteGlobal();
            double Total = 0;


            try
            {
                string sSelect = "";
                if (GlobalesCorporativo.StatusAPP == "PADRE")
                {
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] Select B.nameCentro, isnull(Cantidad,0)Cantidad from (Select CentroCostos, SUM(Cantidad)Cantidad from( SELECT isnull(CentroCostos,'UNDEFINED') CentroCostos, SUM(can) Cantidad FROM [" + GlobalesLocal.BDLocal + "].[dbo].[VDespachosAll] as A where ltrim(xProducto) =  @Combustible  and xFecha >= CONVERT(datetime,  @FechaIni , 121) and xFecha <= CONVERT(datetime, @FechaFin, 121) GROUP BY CentroCostos UNION ALL SELECT  isnull(CentroCostos, 'UNDEFINED') CentroCostos, SUM(can) Cantidad FROM [10.70.100.137\\MSSQLSERVER2014].[TANKFARM].[dbo].[VDespachosAll] as A where ltrim(xProducto) = @Combustible and xFecha >= CONVERT(datetime, @FechaIni, 121) and xFecha <= CONVERT(datetime, @FechaFin, 121) GROUP BY CentroCostos )T0 group by CentroCostos)T0 right JOIN(Select NameCentro from [" + GlobalesLocal.BDLocal + "].[dbo].[" + GlobalesLocal.TablaCentroCostos + "] UNION Select 'UNDEFINED' as NameCentro) as B ON T0.CentroCostos = B.nameCentro order by CentroCostos";

                }
                else
                {
                    sSelect = "USE [" + GlobalesLocal.BDLocal + "] Select B.nameCentro, isnull(Cantidad,0)Cantidad from (SELECT isnull(CentroCostos,'UNDEFINED') CentroCostos, SUM(can) Cantidad FROM   [" + GlobalesLocal.BDLocal + "].[dbo].[VDespachosAll] as A where  ltrim(xProducto) = @Combustible and xFecha >= CONVERT(datetime,  @FechaIni, 121) and xFecha <= CONVERT(datetime, @FechaFin, 121)  GROUP BY CentroCostos)T0  right JOIN(Select NameCentro from CentroCostos UNION Select 'UNDEFINED' as NameCentro) as B  ON T0.CentroCostos = B.nameCentro  order by CentroCostos";

                }



                if (sSelect != "")
                {
                    var ArrParametros1 = new List<SqlParameter>();

                    ArrParametros1.Add(new SqlParameter { ParameterName = "@FechaIni", Value = FechaIni });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@FechaFin", Value = FechaFin });
                    ArrParametros1.Add(new SqlParameter { ParameterName = "@Combustible", Value = Combustible });


                    dT = BD.GetSQL(sSelect, "ControlGas", ArrParametros1);
                    var columnas = dT.Columns;

                    foreach (DataRow dR1 in dT.Rows)
                    {
                        ObjReporteGlobal = new ReporteGlobal();
                        ArrayCentroCostos.Add(dR1["NameCentro"].ToString());
                        ArrayCantidad.Add(Convert.ToDouble(dR1["Cantidad"]));
                        Total += Convert.ToDouble(dR1["Cantidad"]);
                    }
                    ArrayCantidad.Add(Total);
                    ArrayCentroCostos.Add("TOTAL");
                    ObjReporteGlobal.ArrayCantidad = ArrayCantidad;
                    ObjReporteGlobal.ArrayCentroCostos = ArrayCentroCostos;
                    lstReporteGlobal.Add(ObjReporteGlobal);

                    Total = 0;
                }

                if (lstReporteGlobal.Count > 0)
                {
                    data.lstReporteGlobal = lstReporteGlobal;
                    data.Message = "DATA REPORTE GLOBAL";
                    data.Status = "OK";
                }
                else
                {
                    data.lstReporteGlobal = lstReporteGlobal;
                    data.Message = "NO DATA FOUND";
                    data.Status = "OK";
                }
                var json = Ok(data);
                
                return json;
            }
            catch (Exception e)
            {
                var mensaje = new Mensajes();
                mensaje.Mensaje = e.Message.ToString();
                mensaje.Status = "Error";
                AgregaLog("Error en la consulta no se pueden obtener los datos en el reporte global", "GetReporteGlobal");
                EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Error en la consulta no se pueden obtener los datos en el reporte global", "Error en la consulta no se pueden obtener los datos en el reporte global", "", "", "", "");
                return Ok(mensaje);
            }
        }
        //Samy


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

        //[Route("GetReporteGlobal")]
        private DatosCorreo GetDatosCorreo()
        {
            var oCorr = new DatosCorreo();
            string adminName = ConfigurationManager.AppSettings["AdminName"];

            oCorr.Contrasenia = System.Configuration.ConfigurationManager.AppSettings["contrasenia"].ToString();
            oCorr.Correo = System.Configuration.ConfigurationManager.AppSettings["correo"].ToString();
            oCorr.Puerto = System.Configuration.ConfigurationManager.AppSettings["puerto"].ToString();
            oCorr.ServidorSMTP = System.Configuration.ConfigurationManager.AppSettings["smtp"].ToString();
            if (System.Configuration.ConfigurationManager.AppSettings["SSL"].ToString() == "1")
            {
                oCorr.EnableSSL = true;
            }
            else
            {
                oCorr.EnableSSL = false;
            }

            oCorr.CCO = System.Configuration.ConfigurationManager.AppSettings["CCO"].ToString();
            return oCorr;
        }


        public bool EnviaCorreo(string Destinatarios, string sAsunto, string sBody, string sFileXML, string sNameXML, string sFilePDF, string sNamePDF)
        {

            try
            {
                DatosCorreo oCorreo = GetDatosCorreo();
                var builder = new BodyBuilder();
                MimeMessage mail;
                mail = new MimeMessage();
                mail.From.Add(new MailboxAddress("", oCorreo.Correo));
                var sDir = Destinatarios.Split(';');
                foreach (string sD in sDir)
                {
                    if (sD.Trim().Length > 0)
                    {
                        AgregaLog("Para: " + sD.Trim(), "");
                        mail.To.Add(new MailboxAddress("", sD.Trim()));
                    }
                }

                sDir = oCorreo.CCO.Split(';');
                foreach (string sD in sDir)
                {
                    if (sD.Trim().Length > 0)
                    {
                        AgregaLog("ENVIAR CORREO  CCO: " + sD.Trim(), "");
                        mail.Bcc.Add(new MailboxAddress("", sD.Trim()));
                    }
                }

                mail.Subject = sAsunto;
                AgregaLog("Body: " + sBody.ToString(), "");
                builder.HtmlBody = sBody;

                if (System.IO.File.Exists(sFileXML))
                {
                    builder.Attachments.Add(sFileXML);
                }

                if (System.IO.File.Exists("file.txt"))
                {
                    Console.WriteLine("Specified file exists.");
                }

                AgregaLog("Archivo PDF x enviar: " + sFilePDF.ToString(), "");
                if (System.IO.File.Exists(sFilePDF))
                {
                    builder.Attachments.Add(sFilePDF);
                }

                mail.Body = builder.ToMessageBody();

                // AgregaLog("Va a crear el SMTPClient Servidor: " + oCorreo.ServidorSMTP.ToString + ", Puerto: " + oCorreo.Puerto.ToString + ", Correo: " + oCorreo.Correo.ToString + ", Contraseña: " + oCorreo.Contrasenia.ToString)
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    AgregaLog("Va a conectar", "");
                    client.ServerCertificateValidationCallback = (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
                    client.Connect(oCorreo.ServidorSMTP, int.Parse(oCorreo.Puerto), SecureSocketOptions.Auto);
                    AgregaLog("Asigna usuario y contraseña", "");
                    client.Authenticate(oCorreo.Correo, oCorreo.Contrasenia);
                    AgregaLog("Enviar", "");
                    client.Send(mail);
                    AgregaLog("Desconectar", "");
                    client.Disconnect(true);
                };

                return true;
            }
            catch (Exception ex)
            {
                AgregaLog("Error al enviar correo " + ex.Message.ToString(), "EnviaCorreo");
                //EnviaCorreo("gerardo.carrillo@tdcon40.com;juan.quijano@tdcon40.com", "Error al enviar correo", "Error al enviar correo", "", "", "", "");
                return false;
            }
        }

    }



    public class Cliente
    {
        public string cod { get; set; }
        public string den { get; set; }
        public string dom { get; set; }
        public string col { get; set; }
        public string del { get; set; }
        public string ciu { get; set; }
        public string est { get; set; }
        public string tel { get; set; }
        public string fax { get; set; }
        public string rfc { get; set; }
        public double tipval { get; set; } = 0;
        public double mtoasg { get; set; } = 0;
        public double mtodis { get; set; } = 0;
        public double mtorep { get; set; } = 0;
        public double cndpag { get; set; } = 0;
        public double diarev { get; set; } = 0;
        public string horrev { get; set; }
        public double diapag { get; set; } = 0;
        public string horpag { get; set; }
        public string cto { get; set; }
        public string obs { get; set; }
        public string codext { get; set; }
        public double datcon { get; set; } = 0;
        public string codpos { get; set; }
        public double pto { get; set; } = 0;
        public double ptosdo { get; set; } = 0;
        public double debsdo { get; set; } = 0;
        public double cresdo { get; set; } = 0;
        public double fmtexp { get; set; } = 0;
        public string arcexp { get; set; }
        public double polcor { get; set; } = 0;
        public double ultcor { get; set; } = 0;
        public double debnro { get; set; } = 0;
        public double crenro { get; set; } = 0;
        public double debglo { get; set; } = 0;
        public double codtip { get; set; } = 0;
        public double codzon { get; set; } = 0;
        public double codgrp { get; set; } = 0;
        public double codest { get; set; } = 0;
        public double logusu { get; set; } = 0;
        public string logfch { get; set; }
        public string lognew { get; set; }
        public string pai { get; set; }
        public string correo { get; set; }
        public double dattik { get; set; } = 0;
        public double ptodebacu { get; set; } = 0;
        public double ptodebfch { get; set; } = 0;
        public double ptocreacu { get; set; } = 0;
        public double ptocrefch { get; set; } = 0;
        public double ptovenacu { get; set; } = 0;
        public double ptovenfch { get; set; } = 0;
        public string domnroext { get; set; }
        public string domnroint { get; set; }
        public double datvar { get; set; } = 0;
        public string nroctapag { get; set; }
        public double tipopepag { get; set; } = 0;
        public string cveest { get; set; }
        public string cvetra { get; set; }
        public string geodat { get; set; }
        public double geolat { get; set; } = 0;
        public double geolng { get; set; } = 0;
        public double taxext { get; set; } = 0;
        public string taxextid { get; set; }
        public double bcomn1cod { get; set; } = 0;
        public string bcomn1den { get; set; }
        public string bcomn1cta { get; set; }
        public double bcomn2cod { get; set; } = 0;
        public string bcomn2den { get; set; }
        public double bcomn2cta { get; set; } = 0;
        public double bcome1cod { get; set; } = 0;
        public string bcome1den { get; set; }
        public string bcome1cta { get; set; }
        public double bcome2cod { get; set; } = 0;
        public string bcome2den { get; set; }
        public string bcome2cta { get; set; }
        public double perfis { get; set; } = 0;
        public string perfisnom { get; set; }
        public string perfisapp { get; set; }
        public string perfisapm { get; set; }
        public string curp { get; set; }
        public double codrefban { get; set; } = 0;
        public string paisat { get; set; }
        public string satuso { get; set; }



        public string mensaje { get; set; }
    }


    public class Vehiculo
    {

        public int codcli { get; set; }
        public int nroveh { get; set; }
        public int tar { get; set; }
        public string plc { get; set; }
        public string den { get; set; }
        public string rsp { get; set; }
        public string grp { get; set; }
        public int diacar { get; set; }
        public int hraini { get; set; }
        public int hrafin { get; set; }
        public int carmax { get; set; }
        public int candia { get; set; }
        public int cansem { get; set; }
        public int canmes { get; set; }
        public int acudia { get; set; } = 0;
        public int acusem { get; set; } = 0;
        public int acumes { get; set; } = 0;
        public int ultcar { get; set; } = 0;
        public int ultodm { get; set; } = 0;
        public int codgas { get; set; }
        public int codprd { get; set; }
        public double debsdo { get; set; } = 0;
        public int debfch { get; set; } = 0;
        public int debnro { get; set; } = 0;
        public double debcan { get; set; } = 0;
        public int nip { get; set; } = 0;
        public double ptosdo { get; set; } = 0;
        public int ptofch { get; set; } = 0;
        public double ptocan { get; set; } = 0;
        public double premto { get; set; } = 0;
        public double prepgo { get; set; } = 0;
        public double prefid { get; set; } = 0;
        public string cnvemp { get; set; }
        public string cnvobs { get; set; }
        public int cnvfch { get; set; } = 0;
        public string manobs { get; set; }
        public int manper { get; set; } = 0;
        public int manult { get; set; } = 0;
        public string rut { get; set; }
        public string tag { get; set; }
        public int vto { get; set; } = 0;
        public int limtur { get; set; } = 0;
        public int ulttur { get; set; } = 0;
        public int acutur { get; set; } = 0;
        public int limprd { get; set; } = 0;
        public int acuprd { get; set; } = 0;
        public int crefch { get; set; } = 0;
        public int crenro { get; set; } = 0;
        public double crecan { get; set; } = 0;
        public int crefch2 { get; set; } = 0;
        public int crenro2 { get; set; } = 0;
        public double crecan2 { get; set; } = 0;
        public int debfch2 { get; set; } = 0;
        public int debnro2 { get; set; } = 0;
        public double debcan2 { get; set; } = 0;
        public int est { get; set; }
        public string niplog { get; set; }
        public int logusu { get; set; }
        public string logfch { get; set; }
        public string lognew { get; set; }
        public string tagadi { get; set; }
        public string ctapre { get; set; }
        public string nropat { get; set; }
        public string nroeco { get; set; }
        public int hraini2 { get; set; } = 0;
        public int hrafin2 { get; set; } = 0;
        public int hraini3 { get; set; } = 0;
        public int hrafin3 { get; set; } = 0;
        public int aju { get; set; } = 0;
        public double ptodebacu { get; set; } = 0;
        public int ptodebfch { get; set; } = 0;
        public double ptocreacu { get; set; } = 0;
        public int ptocrefch { get; set; } = 0;
        public double ptovenacu { get; set; } = 0;
        public int ptovenfch { get; set; } = 0;
        public string tagex1 { get; set; }
        public string tagex2 { get; set; }
        public string tagex3 { get; set; }
        public double ultcan { get; set; } = 0;
        public int datvar { get; set; }
        public string catprd { get; set; }
        public string catuni { get; set; }
        public string dialim { get; set; }
        public object company { get; set; }
        public object profileName { get; set; }
        public string Departamentos { get; set; } = "";
        public string ValidacionOdometro { get; set; } = "";
        public string NombreDepartamento { get; set; } = "";
    }

    public class ChangueStatus
    {
        public string ID { get; set; }
        public string Status { get; set; }
        public string table { get; set; }
        public string nameid { get; set; }
        public string nameStatus { get; set; }
        public string BDD { get; set; }

    }
    public class Choferes
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public int IdIdioma { get; set; }
        public string Gerente { get; set; }
        public string Correo { get; set; }
        public string Perfil { get; set; }
        public int codcli { get; set; }
        public string Company { get; set; }
        public int nrocho { get; set; }
        public string den { get; set; }
        public int diacar { get; set; }
        public int hraini { get; set; }
        public int hrafin { get; set; }
        public string tag { get; set; }
        public int codest { get; set; }

        public string Status { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Departamento { get; set; }
        public string CentroCostos { get; set; }
        public string ManagerEmail { get; set; }
        public string DepartamentoName { get; set; }
        public string CentroCostosName { get; set; }
        public string TagOld { get; set; }
        public string IDP { get; set; }

    }

    public class UsuariosSystema
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
        public int IdIdioma { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Status { get; set; }
        public string Compania { get; set; }
        public string codcli { get; set; }
        public string Mensaje { get; set; }
        public string Rol { get; set; }
        public string Idioma { get; set; }
        public string Departamento { get; set; }
        public string IdDep { get; set; }
        public string Manager { get; set; }
        public Roles Roles { get; set; }
        public string Token { get; set; }




    }
    public class Languague
    {
        public object ID { get; set; }
        public object Idioma { get; set; } = "";
        public object Status { get; set; } = "0";


    }
    public class Departamentos
    {
        public string ID { get; set; }
        public string Departamento { get; set; } = "";
        public string Status { get; set; } = "0";


    }


    public class Marcas
    {
        public object ID { get; set; }
        public object Marca { get; set; } = "";
        public object Status { get; set; } = "0";


    }

    public class Modelos
    {
        public object ID { get; set; }
        public object Modelo { get; set; } = "";
        public object Status { get; set; } = "0";


    }

    public class Roles
    {
        public object ID { get; set; }
        public object Rol { get; set; } = "Sin rol";
        public object Status { get; set; } = "";
        public object Users { get; set; } = "";
        public object Customers { get; set; } = "";
        public object Vehicles { get; set; } = "";
        public object Drivers { get; set; } = "";
        public object Department { get; set; } = "";
        public object Brand { get; set; } = "";
        public object Model { get; set; } = "";
        public object CostCenter { get; set; } = "";
        public object Perfiles { get; set; } = "";
        public object Configuration { get; set; } = "";


    }

    public class Configuracion
    {
        public String ID { get; set; }
        public String IPSERVIDOR { get; set; }
        public String IPCONTROLGAS1 { get; set; }
        public String IPCONTROLGAS2 { get; set; }
        public String IPPLC { get; set; }
        public String REPORTESPOR { get; set; }
        public String FECHA { get; set; }
        public String MANAGER { get; set; }
        public String TELEFONO { get; set; }
        public String CORREO { get; set; }
        public String bandCambios { get; set; }
        public String DESTINATARIOS { get; set; }
        public String USER_ACOUNT { get; set; }
        public String PASSWORD { get; set; }
        public String SERVER_SMTP { get; set; }
        public String PORT { get; set; }
    }
    public class Inbox
    {
        public object FECHA { get; set; }
        public object MENSAJE { get; set; }
        public object CORREO { get; set; }
        public object IDUSER { get; set; }
        public object STATUSMSJ { get; set; }
        public object INTENTOS { get; set; }

    }
    public class estados
    {
        public object id { get; set; }
        public object clave { get; set; }
        public object nombre { get; set; }

    }

    public class municipios
    {
        public object id { get; set; }
        public object estado_id { get; set; }
        public object clave { get; set; }
        public object nombre { get; set; }

    }

    public class marca
    {
        public string MarcID { get; set; }
        public string MarcDesc { get; set; }
        public string Status { get; set; }
    }
    public class modelo
    {
        public string ModelId { get; set; }
        public string ModelDesc { get; set; }
        public string MarcID { get; set; }
        public string Status { get; set; }

    }

    public class cp
    {
        public object colonia { get; set; }
        public object municipio { get; set; }
        public object estado { get; set; }


    }
    public class Perfiles
    {
        public int ID { get; set; } = 0;
        public string namePerfil { get; set; } = "";
        public string codcli { get; set; } = "";
        public string Unlimited { get; set; } = "";
        //public string nroveh = "";
        public string diacar { get; set; } = "";
        public string hraini { get; set; } = "";
        public string hrafin { get; set; } = "";
        public string carmax { get; set; } = "";
        public string candia { get; set; } = "";
        public string cansem { get; set; } = "";
        public string canmes { get; set; } = "";
        public string codgas { get; set; } = "";
        public string codprd { get; set; } = "";
        public string fecha { get; set; } = "";
        public string status { get; set; } = "";
        public string tipoPerfil { get; set; } = "";
        public string xNombreCliente { get; set; } = "";
        public string xProducto { get; set; } = "";
        public string xEstacion { get; set; } = "";
        public string Departamentos { get; set; } = "";
        public string Odometro { get; set; } = "";
        public string NombreDepartamento { get; set; } = "";
    }
    public class CentroCostos
    {
        public int id { get; set; } = 0;
        public string idDepartamento { get; set; } = "";
        public string nameCentro { get; set; } = "";
        public string status { get; set; } = "";

    }
    public class ReporteConsumos
    {
        public string ID { get; set; } = "";
        public string DATE { get; set; } = "";
        public string DEPARTMENT { get; set; } = "";
        public string CC { get; set; } = "";
        public string COMPANY { get; set; } = "";
        public string FUEL { get; set; } = "";
        public string QUANTITY { get; set; } = "";
        public string DRIVER { get; set; } = "";
        public string VEHICLE { get; set; } = "";

    }

    static class GlobalesCorporativo
    {
        //PADRE
        //public static string BDCorporativoModerno = "WS1";//ConfigurationManager.ConnectionStrings["BDCorporativoModerno"].ToString();
        //HIJO
        //public static string BDCorporativoModerno = "WS2";//ConfigurationManager.ConnectionStrings["BDCorporativoModerno"].ToString();

        //CORPORATIVO
        public static string BDCorporativoModerno = "WS3-CORPO";//ConfigurationManager.ConnectionStrings["BDCorporativoModerno"].ToString();


        public static string TablaClientesGasolineras = "ClientesGasolineras";
        public static string TablaClientesChoferes = "ClientesChoferes";
        public static string TablaClientesVehiculos = "ClientesVehiculos";
        public static string TablaClientes = "Clientes";
        //public static string StatusAPP = "PADRE";
        public static string StatusAPP = "HIJO";  //-- CORPORATIVO

    }


    static class GlobalesLocal
    {
        //PADRE
        //public static string BDLocal = "TANKFARM2";//ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
        //HIJO  -- CORPORATIVO
        public static string BDLocal = "TANKFARM";//ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

        public static string TablaChoferes = "CHOFERES";
        public static string TablaCodigos_postales = "codigos_postales";
        public static string TablaConfiguracion = "CONFIGURACION";
        public static string TablaEstados = "estados";
        public static string TablaIdioma = "IDIOMA";
        public static string TablaMarca = "marca";
        public static string Tablamodelo = "modelo";
        public static string TablaMunicipios = "municipios";
        public static string TablaPerfiles = "Perfiles";
        public static string TablaRol = "ROL";
        public static string TablaUsuarios = "USUARIOS";
        public static string TablaDepartamento = "Departamento";
        public static string TablaCentroCostos = "CentroCostos";

    }


}
