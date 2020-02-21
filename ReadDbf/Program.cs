using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDbf
{
    class Program
    {
        private static string FileName = @"C:\Users\Joche\Desktop\LD\ld.dbf";
        static void Main(string[] args)
        {
            if (System.IO.File.Exists(FileName))
            {
                var Builder = new OleDbConnectionStringBuilder()
                {
                    DataSource = Path.GetDirectoryName(FileName),
                    Provider = "Microsoft.Jet.OLEDB.4.0"
                };
                Builder.Add("Extended Properties", "dBase III");
                using (var cn = new System.Data.OleDb.OleDbConnection() { ConnectionString = Builder.ConnectionString })
                {
                    using (var cmd = new OleDbCommand() { Connection = cn })
                    {
                        cmd.CommandText = "SELECT * FROM " + Path.GetFileName(FileName);
                        cn.Open();
                        var dt = new DataTable("Data");
                        dt.Load(cmd.ExecuteReader());
                        dt.WriteXml("Data.xml");
                    }
                }
            }


        }
    }
}
