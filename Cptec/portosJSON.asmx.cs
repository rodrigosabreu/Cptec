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
    /// Summary description for portosJSON
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class portosJSON : System.Web.Services.WebService
    {


        public struct portos
        {
            public List<porto> listPortos;


            //Estrutura XML
            public struct porto
            {
                public String IdPorto;
                public String NomePorto;
                public String NomeResumido;
            }



        }




        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPortos()
        {

            PortosDAL objDAL = new PortosDAL();
            DataTable dtPortos = objDAL.SelectPortos();

            List<portos.porto> objListPortos = new List<portos.porto>();

            for (int i = 0; i < dtPortos.Rows.Count; i++)
            {

                portos.porto obj = new portos.porto();
                obj.IdPorto = dtPortos.Rows[i]["id"].ToString().Trim();
                obj.NomePorto = dtPortos.Rows[i]["nome_porto"].ToString().Trim();
                obj.NomeResumido = dtPortos.Rows[i]["nome_resumido"].ToString().Trim();
                objListPortos.Add(obj);

            }

            portos o = new portos();
            o.listPortos = objListPortos;



            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(jsSerializer.Serialize(o));


        }








    }
}
