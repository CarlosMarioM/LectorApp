using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;




namespace QRLectorApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Services.Services _services = new Services.Services();

        public MainPage()
        {
            InitializeComponent();
        }

       

           void ZXingScannerView_OnScanResult(ZXing.Result result)
        { 
            Device.BeginInvokeOnMainThread (async () =>
            {
                 _services.SelectAsync(result.Text);
                string Color = Xamarin.Forms.Application.Current.Properties["color"].ToString();
                string Status = Xamarin.Forms.Application.Current.Properties["status"].ToString();
                if (Status == "2")                {
                    
                    var color = Xamarin.Forms.Application.Current.Properties["color"];
                    scanResult.Text = result.Text + " " + color.ToString();
                    
                    
                    await DisplayAlert("Notificación", "El número de registro " + result.Text + " ya se encuentra registrado", "Aceptar");
                  
                }
                else 
                {
                    
                    var color = Xamarin.Forms.Application.Current.Properties["color"];
                    scanResult.Text = result.Text + " " + color.ToString();

                    await DisplayAlert("Notificación", "El número de registro es " + result.Text + " " + color.ToString() + "", "Entendido");
                   
                }


                scanResult.Text = "";
               
            });
            

        }
    }
}
