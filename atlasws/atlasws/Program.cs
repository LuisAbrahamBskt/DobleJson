using Microsoft.Reporting.WebForms;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml.Serialization;
using atlasws.atlaservice;
using atlasws.hdiprod;
using atlasws.ServiceReference1;
using Newtonsoft.Json;

namespace atlasws
{
   public class Program
   {    
       
       private static void GuardaCatalogos(List<Categorias> categorias)
        {
            List<FutureVisitsModel> lstcatalogos = new List<FutureVisitsModel>();
            FutureVisitsModel cat = new FutureVisitsModel();

            foreach (var categoria in categorias)
            {
                
                cat.id = categoria.Id.ToString();
                cat.nombre = categoria.Nombre;
                
                foreach (var marca in categoria.Submarcas)
                {
                    //cat.descripcion = marca.Descripcion;
                    //cat.marca = marca.Marca;
                    //cat.subMarca = marca.Submarca;
                    //cat.claveBanorte = marca.ClaveBanorte;
                    //cat.anio = marca.Anio.ToString();
                    cat.idSM = marca.Id.ToString();
                    cat.nombreSM = marca.Nombre;

                    //cat.idLoc = local.Id.ToString();
                    //cat.municipio = local.Municipio;
                    //cat.estado = local.Estado;
                    lstcatalogos.Add(cat);
                    cat = new FutureVisitsModel();
                }
                //lstcatalogos.Add(cat);
            }

            //Catalogos catal = new Catalogos();
            //foreach (var catalogo in catalogos.Result)
            //{
            //    catal.Codigo = catalogo.Codigo;
            //    catal.Descripcion = catalogo.Descripcion;
            //}

            Microsoft.Reporting.WebForms.ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            rv.ProcessingMode = ProcessingMode.Remote;
            rv.LocalReport.ReportPath = @"C:/Users/SaoSistemas/Desktop/atlasws/atlasws/Report9.rdlc";
            rv.LocalReport.DataSources.Clear();

            rv.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", lstcatalogos));
            byte[] bytes = rv.LocalReport.Render("EXCEL", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            string absoluteFileName = Path.Combine(@"C:/Users/SaoSistemas/Desktop/atlasws", "SubMarca_VehRegularizados.xls");
            FileStream file = new FileStream(absoluteFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            file.Write(bytes, 0, bytes.Length);
            file.Close();
            file.Dispose();


        }

        private static string getmarca(int idMarca)
        {
            hdiservice.PublicServicesAutosContractClient srvhdi = new hdiservice.PublicServicesAutosContractClient();
            var marcashdi = new hdiservice.ObtenerMarcasRequest
            {
                IdModelo = 2019,
                IdNegocio = "",
                usuario = "0922080001",
                IdTipoVehiculo = 3829

            };
            var marcas = srvhdi.ObtenerMarcas(marcashdi);

            var a= marcas.Where(c => c.Clave==idMarca) ;
            return a.First().Descripcion;
        }

        public static void Main(string[] args)
        {
            
            string url = "https://api-pre.segurosbanorte.com/cotizadores/api/v1/producto/SEGURO%20DE%20AUTOM%C3%93VILES%20RESIDENTES/marca/VEH.%20REGULARIZADO/submarca";
            HttpMessageHandler handler = new HttpClientHandler()
            {
            };

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(url),
                Timeout = new TimeSpan(0, 1, 0)
            };

            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json; Charset=UTF-8");
           
            //This is the key section you were missing    
            var user = "Br_0bb0000";
            var password = "iCpqFsgj9k";
            var parameters = new Dictionary<string, string> { { "Usuario", "BROKER" }, { "numOficina", "0BB" }, { "nombreRamo", "Autos" } };
            var encodedContent = new FormUrlEncodedContent (parameters);
            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);
            httpClient.DefaultRequestHeaders.Add("usuario","BROKER" );
            httpClient.DefaultRequestHeaders.Add("nombreRamo", "Autos");
            httpClient.DefaultRequestHeaders.Add("numOficina", "0BB");
            var method = new HttpMethod("GET");
            
            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            string content = string.Empty;
            using (StreamReader stream = new StreamReader(response.Content.ReadAsStreamAsync().Result, System.Text.Encoding.UTF8))
            {
                content = stream.ReadToEnd();
            }
            var obj = JsonConvert.DeserializeObject<Data>(content);
            List<Categorias> categorias=  new List<Categorias>();
            List<Submarca> localizacion = new List<Submarca>();
            foreach (var categoria in obj.data.categorias)
            {
                categorias.Add(categoria);
                foreach (var local in categoria.Submarcas)
                {
                    localizacion.Add(local);
                }
                
            }
            GuardaCatalogos(categorias);
            //obtenerMarcas();
        }

        public static void obtenerMarcas()
        {
            string url = "https://api-pre.segurosbanorte.com/cotizadores/api/v1/producto/SEGURO%20DE%20AUTOM%C3%93VILES%20RESIDENTES/marca/VOLKSWAGEN/submarca";
            HttpMessageHandler handler = new HttpClientHandler()
            {
            };

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(url),
                Timeout = new TimeSpan(0, 1, 0)
            };

            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

            //This is the key section you were missing    
            var user = "Br_0bb0000";
            var password = "iCpqFsgj9k";
            var parameters = new Dictionary<string, string> { { "Usuario", "BROKER" }, { "numOficina", "0BB" }, { "nombreRamo", "Autos" } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);
            httpClient.DefaultRequestHeaders.Add("usuario", "BROKER");
            httpClient.DefaultRequestHeaders.Add("nombreRamo", "Autos");
            httpClient.DefaultRequestHeaders.Add("numOficina", "0BB");
            var method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            string content = string.Empty;
            using (StreamReader stream = new StreamReader(response.Content.ReadAsStreamAsync().Result, System.Text.Encoding.GetEncoding(1255)))
            {
                content = stream.ReadToEnd();
            }
            var obj = JsonConvert.DeserializeObject<Data>(content);
            List<Submarca> marcas = new List<Submarca>();
            foreach (var marca in obj.data.categorias.FirstOrDefault().Submarcas)
            {
                marcas.Add(marca);
            }

            //GuardaCatalogos(marcas);
        } 
        public static void CPANA()
        {
            ServiceReference1.ServiceSoapClient servana = new ServiceSoapClient();
            string user = "13896";
            string pass = "g6jpsULA";
            string codigotxt = "";
            string coltxt = "";
            string idcol = "";
            List<codigopost> listacodigos = new List<codigopost>();
            for (var i = 9; i <= 32; i++)
            {

                var ciudades = servana.DelMun(1199, i, user, pass);
                var xmlLista = new XmlDocument();
                xmlLista.LoadXml(ciudades);
                var trans = xmlLista.SelectSingleNode("transacciones");
                //var transn = trans.SelectSingleNode("delmun");
                //var ciudadtxt = transn.InnerText;
                //var idciudad = Convert.ToInt32( transn.Attributes["id"].Value);
                for (int j = 3; j < trans.ChildNodes.Count; j++)
                {

                    var yy = trans.ChildNodes.Item(j);
                    var ciudadtxt = yy.InnerText;
                    if (ciudadtxt != "")
                    {


                        var idciudad = Convert.ToInt32(yy.Attributes["id"].Value);
                        var codigos = servana.CodigoPostal(i, idciudad, 1199, user, pass);
                        var codigosadd = new XmlDocument();
                        codigosadd.LoadXml(codigos);
                        var transcod = codigosadd.SelectSingleNode("transacciones");
                        //var transncod = transcod.SelectSingleNode("cp");
                        //var codp = transncod.Attributes["id"].Value;
                        for (int k = 0; k < transcod.ChildNodes.Count; k++)
                        {
                            var uu = transcod.ChildNodes.Item(k);
                            codigotxt = uu.InnerText;
                            if (codigotxt != "")
                            {
                                var colonia = servana.Colonia(i, idciudad, codigotxt, 1199, user, pass);
                                var coloniadoc = new XmlDocument();
                                coloniadoc.LoadXml(colonia);
                                var transcol = coloniadoc.SelectSingleNode("transacciones");
                                var col = transcol.SelectSingleNode("colonia");
                                idcol = col.Attributes["id"].Value;
                                coltxt = col.InnerText;

                                var codigoinfo = new codigopost
                                {
                                    Codigo = codigotxt,
                                    ciudad = ciudadtxt,
                                    colonia = coltxt,
                                    idasentamiento = Convert.ToInt32(idcol),
                                    idestado = i

                                };
                                listacodigos.Add(codigoinfo);
                            }

                        }
                    }

                }
            }

            //GuardaCatalogos(listacodigos);
            var finish = "";

        }
        private static  void Obtenercathdi()
        {
            var user = "XXX";
            var pass = "XXX";
            hdiservice.PublicServicesAutosContractClient srvhdi = new hdiservice.PublicServicesAutosContractClient();
            atlaservice.WS_CatXModImplClient srv = new atlaservice.WS_CatXModImplClient();
            List<MarcaModPet> lstmarcatlas = new List<MarcaModPet>();
           
            atlaservice.MarcaModPet mods = new atlaservice.MarcaModPet
            {
                Categoria=1,
                Cve_negocio=3,
                Modelo=19,
                Liga= 1001054,
                MPassword ="XXX",
                MUsuario="XXX"
            };
            lstmarcatlas.Add(mods);
            var EDOS = srvhdi.ObtenerEstados("7");
            var cod = srvhdi.ObtenerEstados("00007");
            var infoubi = srvhdi.ObtenerInfoUbicacion("57000");
            var mdsarr = lstmarcatlas.ToArray();
            var stWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(atlaservice.MarcaModPet));
            serializer.Serialize(stWriter, mods);
            var modsrq=srv.WS_ModMarcas(mdsarr);
            
            var marcashdi = new hdiservice.ObtenerMarcasRequest
            {
                IdModelo = 2019,
                IdNegocio = "",
                usuario = "0922080001",
                IdTipoVehiculo = 3829

            };

            var hditv = srvhdi.ObtenerTiposVehiculo("0922080001"); //4579 o 3829
            var usosrq = new hdiservice.ObtenerUsoRequest
            {
                TipoVehiculoID = 3829,
                Usuario = "0922080001"
            };
            var hdiusos = srvhdi.ObtenerUsos(usosrq);//4581
            var hdiserv = srvhdi.ObtenerServicios();//4601
            List<hdiservice.Tipos> tiposvgrd = new List<hdiservice.Tipos>();
            var marcas = srvhdi.ObtenerMarcas(marcashdi);
            List<hdiservice.CaracteristicasVehiculo> veh = new List<hdiservice.CaracteristicasVehiculo>();
            for (var i = 2016; i <= 2019; i++)
            {
                foreach (var marca in marcas)
                {
                    var tiposvrq = new hdiservice.ObtenerTiposRequest
                    {
                        IdMarca = marca.Clave,
                        IdTipoVehiculo = 3829,
                        IdModelo = i

                    };
                    var tiposv = srvhdi.ObtenerTipos(tiposvrq);

                    foreach (var tipov in tiposv)
                    {
                        var versi = new hdiservice.ObtenerVersionesRequest
                        {
                            IdMarca = marca.Clave,
                            IdModelo = i,
                            IdNegocio = "",
                            IdTipo = tipov.Clave,
                            IdTipoVehiculo = 3829,
                            usuario = "0922080001"

                        };
                        var versobt = srvhdi.ObtenerVersiones(versi);
                        foreach (var version in versobt)
                        {
                            var rqtransmi = new hdiservice.ObtenerTransmisionesRequest
                            {
                                IdMarca = marca.Clave,
                                IdModelo = i,
                                IdTipo = tipov.Clave,
                                IdVersion = version.Clave,
                                IdTipoVehiculo = 3829
                            };
                            var transmision = srvhdi.ObtenerTransmisiones(rqtransmi);

                            foreach (var transmi in transmision)
                            {
                                var obtenveh = new hdiservice.ObtenerInformacionVehiculoRequest
                                {
                                    IdMarca = marca.Clave,
                                    idModelo = i,
                                    IdTipoVehiculo = 3829,
                                    IdTipo = tipov.Clave,
                                    idTransmision = transmi.Clave,
                                    IdVersion = version.Clave
                                };
                                var vehinf = srvhdi.ObtenerInformacionVehiculo(obtenveh);
                                if (vehinf != null)
                                {
                                    veh.Add(vehinf);
                                }
                                
                            }
                        }


                    }

                }
            }
            //GuardaCatalogos(veh);
            //var cobertrq= new hdiservice.CaracteristicasVehiculo{

            //};

            //foreach(var marca in marcas)
            //{
            //    marcasgrd.Add(marca);
            //}
            //XmlDocument xdoc = new XmlDocument();
            //xdoc.LoadXml(marcasgrd.ToString());
            //xdoc.Save("C:/Users/Serch Diaz/Documents/HDI/Catalogos/Marcas" + ".xml");
            //var hdis = new hdiservice.ObtenerMarcasRequest
            //    {


            //};

        }
    }
}
