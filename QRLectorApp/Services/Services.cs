using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace QRLectorApp.Services
{
    class Services
    {
        private string color;
        public string Color
        {
            get
            {
                return this.color;
            }
        }
        public async Task <string> SelectAsync(string result)
        {
            string url = "https://masterqr.azurewebsites.net/API/qr/Select?result=";
            string color = String.Empty;
            await Task.Run(() =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url + result);
                SqlConnection con = new SqlConnection(@"Data Source=tcp:systrackuat.database.windows.net,1433;" +
               "                                   Initial Catalog=SysDesarrolloH;" +
               "                                   Persist Security Info=False;" +
               "                                   User ID=AdministradorCEC;" +
               "                                   Password=Pankis@99;" +
               "                                   MultipleActiveResultSets=False;" +
               "                                   Connect Timeout=45;" +
               "                                   Encrypt=True;" +
               "                                   TrustServerCertificate=False ");

                SqlDataAdapter hsh = new SqlDataAdapter("SELECT a.Lng_IdRegistro,b.Txt_Color as Color FROM Tb_Registro a inner join ct_color b on a.Int_Color = b.Int_Color where int_idstatus= 1 and  Num_Registro = " + result + " ", con);
                DataSet a = new DataSet();
                hsh.Fill(a);
                if (a.Tables[0].Rows.Count > 0)
                {
                    con.Open();                
                    var Color = a.Tables[0].Rows[0]["Color"].ToString();
                    Xamarin.Forms.Application.Current.Properties["color"] = Color;
                    
                    con.Close();
                }
                else
                {
                    var status = 2;
                    Xamarin.Forms.Application.Current.Properties["color"] = status;
                }
                var client = new HttpClient();
                
                var response = client.SendAsync(request).Result;               
            });

         
            
            return result;
        } 

    }
}
