using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Cptec
{
    /// <summary>
    /// Summary description for ondasJson
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ondasJson : System.Web.Services.WebService
    {


        public struct cidade
        {
            public String nome;
            public String uf;
            public String atualizacao;
            public List<previsao> previsoes;

            public struct previsao
            {
                public String dia;
                public String agitacao;
                public String altura;
                public String direcao;
                public String vento;
                public String vento_dir;

            }

        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void getOndasPrevisao(string cod)
        {

            DataSet ds = new DataSet();
            string url = "http://servicos.cptec.inpe.br/XML/cidade/"+cod+"/todos/tempos/ondas.xml";
            ds.ReadXml(url);

            int count = ds.Tables[0].Rows.Count;
            DataTable dt = ds.Tables[0];

            cidade c = new cidade();

            //try
            //{
            if (ds != null)
            {

                if (count > 0)
                {


                  


                    for (int i = 0; i < count; i++)
                    {
                        c.nome = dt.Rows[i]["nome"].ToString().Trim();
                        c.uf = dt.Rows[i]["uf"].ToString().Trim();
                        c.atualizacao = dt.Rows[i]["atualizacao"].ToString().Trim();
                    }

                    count = ds.Tables[1].Rows.Count;
                    dt = ds.Tables[1];

                    List<cidade.previsao> pr = new List<cidade.previsao>();

                    for (int i = 0; i < count; i++)
                    {
                        cidade.previsao p = new cidade.previsao();
                        p.dia = dt.Rows[i]["dia"].ToString().Trim();
                        p.agitacao = dt.Rows[i]["agitacao"].ToString().Trim();
                        p.altura = dt.Rows[i]["altura"].ToString().Trim();
                        p.direcao = dt.Rows[i]["direcao"].ToString().Trim();
                        p.vento = dt.Rows[i]["vento"].ToString().Trim();
                        p.vento_dir = dt.Rows[0]["vento_dir"].ToString();
                        pr.Add(p);
                    }

                    c.previsoes = pr;

                }

            }

            //}
            //catch (Exception)
            //{

            //}

            //return c;
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(jsSerializer.Serialize(c));

        }



    }
}
