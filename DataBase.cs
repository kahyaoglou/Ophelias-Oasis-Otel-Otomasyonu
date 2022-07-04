using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace VeriTabaniProje
{
    public class DataBase
    {
        public SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FH90DII;Initial Catalog=OtelOtomasyonu;Integrated Security=True");
    }
}