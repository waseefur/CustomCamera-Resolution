using System;
using Xamarin.Forms;

namespace CustomRenderer
{
    public partial class CameraPage : ContentPage
    {
        //public Command seleted_Proj { get; }
        //string pf1;
        //MainPage mp;

        //public System.Windows.Input.ICommand NavigateCommand { get; private set; }

        public CameraPage(string pf)
        {



            // A custom renderer is used to display the camera UI
            //InitializeComponent(pf);
            InitializeComponent();

            //BindingContext = personViewModel;
            // = "";
            //pf1 = pf;
            //seleted_Proj = new Command(() =>
            //{
            //    var det - new IMasterDetailPageController()
            //});

            //CustomRenderer.CameraPage.


            //NavigateCommand = new Command<Type>(
            //    async (Type pageType) =>
            //    {
            //        Page page = (Page)Activator.CreateInstance(pageType);
            //        await Navigation.PushAsync(page);
            //    });

            //BindingContext = this;

        }

        protected override bool OnBackButtonPressed()
        {
            //InitializeComponent();
            //if (_browser.CanGoBack)
            //{
            //    _browser.GoBack();
            //    return true;
            //}
            //else
            //{
            //    //await Navigation.PopAsync(true);
                //base.OnBackButtonPressed();
                return true;
            //}
        }
        //public CameraPage(MainPage m)
        //{
        //    mp = m;
        //}

        //public string Name
        //{
        //    get { return pf1; }
        //}
        //public popFunc()
        //{
        //    // now you can call any function on the main page just before you pop
        //    mp.myFunc();
        //    Navigation.PopModalAsync();
        //    return pf1;
        //}
        //public class ItemPage(Action<Someclass> callback);
    }
}

