using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptecDAL
{
    public class LuaDAL
    {
        public DataTable getLua(string data)
        {
           
                DbManager db = new DbManager();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@data", data);
                return db.ExecuteDataSet("stp_SelecionaLua", p).Tables[0];
            
        }
    }
}
