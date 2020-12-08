using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorWebPagesApp.Pages.DatabaseConnection
{
    public class DatabaseConnect
    {
        public string DBStringConnection()
        {
            string DataSource = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = RazorPageWebAppDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            return DataSource;
        }
    }
}
