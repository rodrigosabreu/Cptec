using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
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
    public class listaCidadesJson : System.Web.Services.WebService
    {

        public struct city
        {
            public List<cidade> cidades;
        }


        //Estrutura XML
        public struct cidade
        {
            public String nome;
            public String uf;
            public String id;
        }

        //WebService para listar as cidades vindas do site da CPTEC
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCidades(string city)
        {

            DataSet ds = new DataSet();
            string url = "http://servicos.cptec.inpe.br/XML/listaCidades?city=" + city;
            ds.ReadXml(url);

            //cidade[] cidades = null;
            //cidades = new cidade[1];

            city ci = new city();

            //try
            //{
            if (ds != null)
            {
                int count = ds.Tables[0].Rows.Count;

                if (count > 0)
                {

                    DataTable dt = ds.Tables[0];

                    //cidades = new cidade[count];


                    cidade c = new cidade();
                    List<cidade> listCidade = new List<cidade>();

                    for (int i = 0; i < count; i++)
                    {
                        //cidades[i].nome = dt.Rows[i]["nome"].ToString().Trim();
                        //cidades[i].uf = dt.Rows[i]["uf"].ToString().Trim();
                        //cidades[i].id = dt.Rows[i]["id"].ToString().Trim();

                        c.nome = dt.Rows[i]["nome"].ToString().Trim();
                        c.uf = dt.Rows[i]["uf"].ToString().Trim();
                        c.id = dt.Rows[i]["id"].ToString().Trim();
                        listCidade.Add(c);
                    }

                    ci.cidades = listCidade;
                }
            }
            //}
            //catch (Exception)
            //{

            //}
            //return cidades;           

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(jsSerializer.Serialize(ci));
        }
    }
}
