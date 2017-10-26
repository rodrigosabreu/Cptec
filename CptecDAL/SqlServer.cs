using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptecDAL
{
    public class SqlServer
    {
        private static SqlServer instance = null;

        private SqlServer() { }

        public static SqlServer getInstance()
        {
            if (instance == null)
                instance = new SqlServer();
            return instance;
        }

        public SqlConnection getConnection()
        {
            //desenvolvimento local
            //string conn = @"Data Source =172.27.72.236; Initial Catalog=GPS; User Id=TESTE; Password=TESTE";

            //desenvolvimento publico
            //string conn = @"Data Source =189.1.84.236; Initial Catalog=GPS; User Id=TESTE; Password=TESTE";

            //producao
            string conn = @"Data Source =172.27.72.8; Initial Catalog=Previsao; User Id=TESTE; Password=TESTE"; //Produção

            return new SqlConnection(conn);
        }
    }
}
