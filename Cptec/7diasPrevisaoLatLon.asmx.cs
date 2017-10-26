using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Cptec
{
    /// <summary>
    /// Summary description for _7diasPrevisaoLatLon
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class _7diasPrevisaoLatLon : System.Web.Services.WebService
    {

        //Estrutura XML

        public struct cidade
        {
            public String nome;
            public String uf;
            public String atualizacao;
            public List<previsao> previsoes;


            public struct previsao
            {
                public String dia;
                public String tempo;
                public String maxima;
                public String minima;
                public String iuv;
                public String lua;

            }

        }

        [WebMethod]
        public cidade getSeteDiasPrevisaoLatLon(string lat, string lon)
        {

            DataSet ds = new DataSet();
            string url = "http://servicos.cptec.inpe.br/XML/cidade/7dias/"+lat+"/"+lon+"/previsaoLatLon.xml";
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

                        CptecDAL.LuaDAL objLua = new CptecDAL.LuaDAL();

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
                            p.tempo = dt.Rows[i]["tempo"].ToString().Trim();
                            p.maxima = dt.Rows[i]["maxima"].ToString().Trim();
                            p.minima = dt.Rows[i]["minima"].ToString().Trim();
                            p.iuv = dt.Rows[i]["iuv"].ToString().Trim();
                            p.lua = objLua.getLua(p.dia).Rows[0]["descricao"].ToString();
                            pr.Add(p);
                        }

                        c.previsoes = pr;

                    }

                }

            //}
            //catch (Exception)
            //{

            //}

            return c;
        }

      
    }
}
