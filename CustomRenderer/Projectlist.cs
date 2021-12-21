using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
namespace CustomRenderer
{
    public class Projectlist
    {

        //public static MobileServiceClient client_1 = new MobileServiceClient("https://sitephotowebapp.azurewebsites.net");

        public string SMA_ID { get; set; }
        public string ROLC_ID { get; set; }
        public string PROJECT_NAME_AT_ROLC { get; set; }
        public Nullable<int> PROJECT_STATUS { get; set; }
        public Nullable<bool> PHOTO_UPLOADED { get; set; }
        public string PROJECT_FOLDER_NAME { get; set; }



        //public static async Task<List<Projectlist>> CustomRenderer()
        //{
        //    return await client_1.GetTable<Projectlist>().ToListAsync();
        //}


        [Microsoft.WindowsAzure.MobileServices.Version ]//James
        public string AzureVersion { get; set; }//James


    }
}
