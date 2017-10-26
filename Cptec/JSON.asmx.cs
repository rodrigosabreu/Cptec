using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Data;
using System.ComponentModel;


namespace Cptec
{
    /// <summary>
    /// Summary description for JSON
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class JSON : System.Web.Services.WebService
    {

        //Estrutura XML

        public struct _JSON
        {
            public string sucess;
            public string error;                      

            public _DATA data;                   
        }

        public struct _ERROR
        {
            public string sucess;
            public string error;           
        }


        public struct _DATA
        {            
            public string previous;
            public string next;
            public string total_pages;
            public string page;

            public Cidade dados;
        }



        public class Cidade
        {
            
            public String nome;
            public String uf;            
            public String atualizacao;
            public List<Previsao> previsoes;

            public class Previsao
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
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void getSeteDiasPrevisao(string cod)
        {
            try
            {

            DataSet ds = new DataSet();
            string url = "http://servicos.cptec.inpe.br/XML/cidade/7dias/" + cod + "/previsao.xml";
            ds.ReadXml(url);
                
            int count = ds.Tables[0].Rows.Count;
            DataTable dt = ds.Tables[0];

            Cidade c = new Cidade();

            
            if (ds != null)
            {

                if (count > 0)
                {
                    CptecDAL.LuaDAL objLua = new CptecDAL.LuaDAL();
                    
                    for (int i = 0; i < count; i++)
                    {                        
                        c.nome = dt.Rows[i]["nome"].ToString().Trim();
                        if (c.nome == "null")
                        {
                            throw new Exception("Registros com valores nulo.");
                        }
                        c.uf = dt.Rows[i]["uf"].ToString().Trim();
                        c.atualizacao = dt.Rows[i]["atualizacao"].ToString().Trim();
                    }

                    count = ds.Tables[1].Rows.Count;
                    dt = ds.Tables[1];

                    List<Cidade.Previsao> pr = new List<Cidade.Previsao>();

                    for (int i = 0; i < count; i++)
                    {
                        Cidade.Previsao p = new Cidade.Previsao();
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

            _DATA d = new _DATA();
            d.dados = c;

            _JSON j = new _JSON();
            j.data = d;
            j.error = "";
            j.sucess = "true";

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";           
            this.Context.Response.Write(jsSerializer.Serialize(j));

            }
            catch (Exception ex)
            {
                _ERROR e = new _ERROR();                
                e.error = ex.Message;
                e.sucess = "false";

                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(jsSerializer.Serialize(e));

            }

            


            

            
        }






    }
}
