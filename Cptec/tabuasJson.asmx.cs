using CptecDAL;
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
    /// Summary description for tabuasJson
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class tabuasJson : System.Web.Services.WebService
    {


        public struct tabuasMare
        {
            public List<tabuaMare> listTabuasMare;

            public object Context { get; private set; }


            //Estrutura XML
            public struct tabuaMare
            {
                public String IdPorto;
                public String Nome_PortoMare;
                public String DiaSemana;
                public String data2;
                public String hora;
                public String altura;
                public String NomePorto_Resumido;
            }



          



            }




        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetTabuasMare(string id_porto, string mes)
        {


            PortosDAL objDAL = new PortosDAL();
            DataTable dtMares = objDAL.SelectTabuaMares(id_porto, mes);
            int i = 0;



            List<tabuasMare.tabuaMare> objListMares = new List<tabuasMare.tabuaMare>();

            for (i = 0; i < dtMares.Rows.Count; i++)
            {

                tabuasMare.tabuaMare obj = new tabuasMare.tabuaMare();
                obj.Nome_PortoMare = dtMares.Rows[i]["nome_porto"].ToString();
                obj.DiaSemana = dtMares.Rows[i]["dia_semana"].ToString();
                obj.data2 = dtMares.Rows[i]["data2"].ToString();
                obj.hora = dtMares.Rows[i]["hora"].ToString();
                obj.altura = dtMares.Rows[i]["altura"].ToString();
                obj.NomePorto_Resumido = dtMares.Rows[i]["nome_resumido"].ToString();
                objListMares.Add(obj);


            }

            tabuasMare o = new tabuasMare();
            o.listTabuasMare = objListMares;


            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(jsSerializer.Serialize(o));
            
        }







    }
}
