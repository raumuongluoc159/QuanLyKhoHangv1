using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DATALAYER
{
    [Serializable]
     public partial class entities
    {
        private entities(DbConnection connectionString, bool contextOwnsConnection = true){ }
        public static entities CreateEntities(bool contextOwnsConnection = true)
        {
            //Doc File Connect
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open("connectdb.dba",FileMode.Open, FileAccess.Read);
            connect cp = (connect) bf.Deserialize(fs);
            //decrypt noi dung
            string servername = Encryptor.Decrypt(cp.servername, "qwertyuiop", true);
            string username = Encryptor.Decrypt(cp.username, "qwertyuiop", true);
            string pass = Encryptor.Decrypt(cp.passwd, "qwertyuiop", true);
            string database = Encryptor.Decrypt(cp.database, "qwertyuiop", true);

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            SqlConnectionStringBuilder sqlConnectionBuilder = new SqlConnectionStringBuilder();

            sqlConnectionBuilder.DataSource = servername;
            sqlConnectionBuilder.InitialCatalog = database;
            sqlConnectionBuilder.UserID = username;
            sqlConnectionBuilder.Password = pass;
            string sqlConnectionString = sqlConnectionBuilder.ConnectionString;

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = sqlConnectionString;
            entityBuilder.Metadata = @"res://*/KHOHANG.csdl|res://*/KHOHANG.ssdl|res://*/KHOHANG.msl";

            EntityConnection connection = new EntityConnection(entityBuilder.ConnectionString);
            fs.Close();
            return new entities(connection);
        }
    }
}
