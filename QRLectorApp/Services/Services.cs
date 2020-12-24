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
using QRLectorApp.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QRLectorApp.Services
{
    class Services
    {
       
        public  void  SelectAsync(string result)
        {
           string url = "https://masterqr.azurewebsites.net/API/qr/Select?result=";        
            
                using (var client = new HttpClient())
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
                    SqlDataAdapter hsi = new SqlDataAdapter("SELECT a.Lng_IdRegistro,b.Txt_Color as Color FROM Tb_Registro a inner join ct_color b on a.Int_Color = b.Int_Color where int_idstatus= 2 and  Num_Registro = " + result + " ", con);
                    DataSet b = new DataSet();
                    hsi.Fill(b);

                    if (b.Tables[0].Rows.Count > 0)
                    {
                        con.Open();
                        var status = 2;
                        var Color = b.Tables[0].Rows[0]["Color"].ToString();
                        Xamarin.Forms.Application.Current.Properties["color"] = Color;
                        Xamarin.Forms.Application.Current.Properties["status"] = status;
                        con.Close();
                    }

                   
                }

                var response = client.SendAsync(request).Result;
            };         
        }

     

    }
}
