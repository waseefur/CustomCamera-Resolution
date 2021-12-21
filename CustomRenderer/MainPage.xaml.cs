using System;
using Xamarin.Forms;

using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Input;
using Newtonsoft.Json;
//using Android.Widget;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
//using Android.Content;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;

using MvvmHelpers;
using System.Diagnostics;
using System.Linq;
using Microsoft.WindowsAzure.MobileServices;
using HtmlAgilityPack;


namespace CustomRenderer
{
    //public partial class MainPage : ContentPage
    public partial class MainPage : ContentPage
    {

        public class Col_SV_InComp : IEnumerable<string>
        {
            public List<string> List_Incmp = new List<string>();
            public IEnumerator<string> GetEnumerator() => List_Incmp.GetEnumerator();

            //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => List_Incmp.GetEnumerator();


            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => List_Incmp.GetEnumerator();

            public void Add(string ProjectID, string Projectname) =>
                List_Incmp.Add($@"{ProjectID} {Projectname}  ");

            //List<string> sortedprojects = List_Incmp.GetEnumerator.OrderByDescending(city => city).ToList();
        }

        public class Col_SV_Comp : IEnumerable<string>
        //public static IEnumerable<string> Col_SV_Comp()
        {
            public List<string> List_cmp = new List<string>();
            public IEnumerator<string> GetEnumerator() => List_cmp.GetEnumerator();

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => List_cmp.GetEnumerator();

            public void Add(string Projectname, string ProjectID) =>
                List_cmp.Add($@"{Projectname} {ProjectID} ");

            public IEnumerator<string> P_List2() { return List_cmp.GetEnumerator(); }
        }

        public string _textBox1
        {
            get { return sel_Pj; }
        }

        public Xamarin.Forms.Element Parent { get; set; }

        internal static MainPage Instance1 { get; private set; }
    
        public static List<Projectlist> ActiveProjectList = new List<Projectlist>();

        public string sel_Pj;
        public string sel_Pj_fld;

        ArrayList myAL = new ArrayList();
        ArrayList myAL_Actual = new ArrayList();

        int pms_cam = 0;
        int pms_net = 0;
        int pms_str = 0;
        int pms_loc = 0;
        string doc = "";


        public MainPage ()
		{
            //InitializeComponent (sel_Pj);

            
            Microsoft.WindowsAzure.MobileServices.IMobileServiceClient azureservice;

            azureservice = DependencyService.Get<IMobileServiceClient>();

            InitializeComponent();
            Instance1 = this;

            string[] ProjectsDetails;
            string value = "";
            int Lp = 0;
            int SV_Cp = 0; //SiteVisit Completed Projects
            int SV_Ip = 0; //SiteVisit InCompleted Projects
            Col_SV_Comp List_cmp = new Col_SV_Comp();
            Col_SV_InComp List_Incmp = new Col_SV_InComp();

            do
            {
                Get_Perm();//Get all required permission to work this app 
            }
            while (pms_cam == 0 || pms_str == 0 || pms_net == 0 || pms_loc == 0);
            {
                Get_Perm();//Get all required permission to work this app 
            }


            string P_Details_Coll = "";
            ProjectsDetails = new string[2];

            int loop = 0;
            int f_itm1 = 0;

            string Blt_str = "P25175ROL 61 Cavendish Road" + "\n" + "\n";// + "^";

            P_Details_Coll = "P26520ROL 26 Shortlands Grove" + "|" + Blt_str;


            ProjectsDetails[0] = "P26520ROL 26 Shortlands Grove" + "\n" + "\n";

            ProjectsDetails[1] = "P25175ROL 61 Cavendish Road" + "\n" + "\n";
            SV_Prj_Lst.ItemsSource = ProjectsDetails;


            var backingFile1 = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "Current_SV_Project.txt");
            //var backingFile = Path.Combine(System.Environment.GetFolderPath(Android.Content.Context.FilesDir), "count.txt");
            if (File.Exists(backingFile1))
            {
                Dir_SecPage();
            }

        }

        public async Task Get_Perm()
        {

            //Check wherther we got all required permission
            var status_location = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            var status_network = await Permissions.CheckStatusAsync<Permissions.NetworkState>();

            var status_camera = await Permissions.CheckStatusAsync<Permissions.Camera>();

            var status_storage_read = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            var status_storage_write = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

           // var status_internet = await Permissions.CheckStatusAsync<Permissions.>();
          //  var status_internet = await Permissions.CheckStatusAsync<Xamarin.Forms.p.>();

            if (status_location != PermissionStatus.Granted)
            {
                var req_loc = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                pms_loc = 1;
            }
            else
            {
                pms_loc = 1;
            }

            if (status_network != PermissionStatus.Granted)
            {
                var req_net = await Permissions.RequestAsync<Permissions.NetworkState>();
                pms_net = 1;
            }
            else
            {
                pms_net = 1;
            }

            if (status_camera != PermissionStatus.Granted)
            {
                var req_cam = await Permissions.RequestAsync<Permissions.Camera>();
                pms_cam = 1;
            }
            else
            {
                pms_cam = 1;
            }

            if (status_storage_read != PermissionStatus.Granted)
            {
                var req_storage_read = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            if (status_storage_write != PermissionStatus.Granted)
            {
                var req_storage_write = await Permissions.RequestAsync<Permissions.StorageWrite>();
                pms_str = 1;
            }
            else
            {
                pms_str = 1;
            }
        }

        public async Task SaveDataAsync(string P_Details_Coll)
        {

            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "P_Details.txt");
            //var backingFile = Path.Combine(System.Environment.GetFolderPath(Android.Content.Context.FilesDir), "count.txt");
            using (var writer = File.CreateText(backingFile))
            {
                writer.Flush();
                await writer.WriteLineAsync(P_Details_Coll);
            }
        }


        async void OnTakePhotoButtonClicked (object sender, EventArgs e)
		{

            //if (String.IsNullOrEmpty(sel_Pj))
            //{
            //    DisplayAlert("Alert", "Please select the Project first and then try again...!!!", "OK");
            //    goto end;
            //}
            


            //string Fldna = sel_Pj.Substring(1, sel_Pj.IndexOf(" ") - 1);
            //try
            //{
            //    Fldna = Fldna.Replace("\\r", "");
            //}
            //catch (Exception)
            //{
            //    string pp = Fldna;
            //}
            //try
            //{
            //    Fldna = Fldna.Replace("\\n", "");
            //}
            //catch (Exception)
            //{
            //    string pp = Fldna;
            //}
                            

            string sel_Pj_fld0 = System.IO.Path.Combine("storage", "emulated", "0", "SitePhotos");
            sel_Pj_fld = System.IO.Path.Combine("storage", "emulated", "0", "SitePhotos", "P26520ROL", "");

            if (!Directory.Exists(sel_Pj_fld0))
            {
                var writer = Directory.CreateDirectory(sel_Pj_fld0);
            }
            if (!Directory.Exists(sel_Pj_fld))
            {
                var writer = Directory.CreateDirectory(sel_Pj_fld);
            }


           string Curr_P_Details = "P26520ROL" + "|" + sel_Pj_fld;

            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "Current_SV_Project.txt");
            //var backingFile = Path.Combine(System.Environment.GetFolderPath(Android.Content.Context.FilesDir), "count.txt");
            using (var writer = File.CreateText(backingFile))
            {
                writer.Flush();
                await writer.WriteLineAsync(Curr_P_Details);
            }

            await Navigation.PushModalAsync(new CameraPage (sel_Pj_fld),true);
 
            end:
            int loop = 0;
        }

        public async Task Dir_SecPage()
        {
            await Navigation.PushModalAsync(new CameraPage(sel_Pj_fld), true);
        }


        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
           // myAL_Actual.Clear();
           // SV_Prj_Lst.ItemsSource = myAL_Actual;
           // string[] ProjectsDetails = new string[myAL.Count];
           // int loop = 0;
           // foreach (string P_Details in myAL)
           // {
           //     string P_Val = P_Details.ToLower();
           //     if (P_Val.Contains(e.NewTextValue.ToLower().ToString()))
           //     {
           //         myAL_Actual.Add(P_Details);


           //         myAL_Actual.IndexOf(0);


           //         ProjectsDetails[loop] = P_Details;
           //         loop = loop + 1;
           //     }
           //     //players.Add(P_Details);

           // }

           // //SV_Prj_Lst.
           // SV_Prj_Lst.ItemsSource = ProjectsDetails;

           //// lv = FindViewById<Android.Widget.ListView>(Resource.Id.lv);
           //// adapter = new CustomAdapter(this, Resource.Layout.model, ProjectsDetails);

           //// lv.Adapter = adapter;

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var item = myAL_Actual[e.SelectedItemIndex];
            //sel_Pj = JsonConvert.SerializeObject(item);
            ////sel_Pj1 = sel_Pj;

            ////GetPj.sel_Pj1 => sel_Pj;
        }
    }
}

