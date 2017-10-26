using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace Cptec
{
    /// <summary>
    /// Summary description for listaCidades
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class listaCidades : System.Web.Services.WebService
    {

        //Estrutura XML
        public struct cidade
        {
            public String nome;
            public String uf;
            public String id;            
        }

        //WebService para listar as cidades vindas do site da CPTEC
        [WebMethod]
        public cidade[] GetCidades(string city)
        {
            
            DataSet ds = new DataSet();
            string url = "http://servicos.cptec.inpe.br/XML/listaCidades?city=" + city;
            ds.ReadXml(url);

            cidade[] cidades = null;
            cidades = new cidade[1];

            //try
            //{
                if (ds != null)
                {
                    int count = ds.Tables[0].Rows.Count;

                    if (count > 0)
                    {

                        DataTable dt = ds.Tables[0];

                        cidades = new cidade[count];

                        for (int i = 0; i < count; i++)
                        {
                            cidades[i].nome = dt.Rows[i]["nome"].ToString().Trim();
                            cidades[i].uf = dt.Rows[i]["uf"].ToString().Trim();
                            cidades[i].id = dt.Rows[i]["id"].ToString().Trim();
                        }
                    }
                }
            //}
            //catch (Exception)
            //{

            //}
            return cidades;            
        }
    }
}
