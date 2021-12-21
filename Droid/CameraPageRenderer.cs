using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CustomRenderer;
using CustomRenderer.Droid;
using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Views;
using Android.Graphics;
using Android.Widget;
using Xamarin.Essentials;
using Android.Media;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using System.Drawing.Imaging;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Plugin.Permissions;
using Plugin.CurrentActivity;

[assembly: ExportRenderer(typeof(CameraPage), typeof(CameraPageRenderer))]
namespace CustomRenderer.Droid
{
    public class CameraPageRenderer : PageRenderer, TextureView.ISurfaceTextureListener
    {
        global::Android.Hardware.Camera camera;
        global::Android.Widget.Button takePhotoButton;
        global::Android.Widget.Button toggleFlashButton;
        global::Android.Widget.Button switchCameraButton;
        global::Android.Widget.Button finishSV;
        global::Android.Widget.TextView PJD;

        global::Android.Views.View view;


        public string Cur_Prj;

        string[] ProjectsDetails;
        string value = "";


        Activity activity;
        CameraFacing cameraType;
        TextureView textureView;
        SurfaceTexture surfaceTexture;
        Page aa;
        bool flashOn;


        protected const string TAG = "location-settings";
        protected const int REQUEST_CHECK_SETTINGS = 0x1;
        public CameraPageRenderer(Context context) : base(context)
        {
            //CustomRenderer.MainPage.ContentProperty.
            //CameraPage.
            //this.view.
            //aa = context;

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {

                aa = e.NewElement;
                //e.NewElement.
                //e.NewElement.Navigation.PopAsync();
               // Cur_Prj = Context.Class.Name;// CustomRenderer.CameraPage();//CustomRenderer.CameraPage{ PorterDuff};
                //Cur_Prj = e.NewElement.Navigation.PopAsync();// ;// CustomRenderer.CameraPage();//CustomRenderer.CameraPage{ PorterDuff};
                //e.NewElement.
                //MainPage
                //Cur_Prj = 
                //e.NewElement(Cur_Prj);
                //Cur_Prj = CustomRenderer.MainPage
                //.Col_SV_InComp; //: CameraPage.CameraPage();

                //Cur_Prj = 
                //e.NewElement;
                SetupUserInterface();
                SetupEventHandlers();
                AddView(view);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
            }
        }

        void SetupUserInterface()
        {
            activity = this.Context as Activity;
            view = activity.LayoutInflater.Inflate(Resource.Layout.CameraLayout, this, false);
            cameraType = CameraFacing.Back;

            textureView = view.FindViewById<TextureView>(Resource.Id.textureView);
            textureView.SurfaceTextureListener = this;
        }

        void SetupEventHandlers()
        {
            takePhotoButton = view.FindViewById<global::Android.Widget.Button>(Resource.Id.takePhotoButton);
            takePhotoButton.Click += TakePhotoButtonTapped;

            switchCameraButton = view.FindViewById<global::Android.Widget.Button>(Resource.Id.switchCameraButton);
            switchCameraButton.Click += SwitchCameraButtonTapped;

            toggleFlashButton = view.FindViewById<global::Android.Widget.Button>(Resource.Id.toggleFlashButton);
            toggleFlashButton.Click += ToggleFlashButtonTapped;

            finishSV = view.FindViewById<global::Android.Widget.Button>(Resource.Id.finishSV);
            finishSV.Click += finishSVTapped;

            //Read Data from pre Stored txt file

            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "Current_SV_Project.txt");

            if (backingFile == null || !File.Exists(backingFile))
            {

            }
            else
            {
                var count = 0;
                var reader = new StreamReader(backingFile, true);

                value = reader.ReadToEnd();//.ReadLine();//.ReadLineAsync();

            }
            string sv_inp = "";
            //string[] a = new string[StringSplitOptions];
           
            ProjectsDetails = value.Split('|');//.Split("|");
            int Lp = 0;
            while (ProjectsDetails.GetEnumerator().MoveNext())
            {
               // if (Lp == ProjectsDetails.Length)
                    //break;

                sv_inp = ProjectsDetails[Lp ];
                Cur_Prj = ProjectsDetails[Lp+ 1];

                //Lp = Lp + 1;
                break;
            }

            PJD = view.FindViewById<global::Android.Widget.TextView>(Resource.Id.textView1);
            PJD.Text = "Site_Visit_InProgress_for_Project " + sv_inp;
        }

        async void finishSVTapped(object sender, EventArgs e)
        {

            var backingFile1 = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "Current_SV_Project.txt");
            //var backingFile = Path.Combine(System.Environment.GetFolderPath(Android.Content.Context.FilesDir), "count.txt");
            if (File.Exists(backingFile1))
            {
                File.Delete(backingFile1);
            }


            Cur_Prj = Cur_Prj.Replace("\n", "");
            var backingFile = System.IO.Path.Combine(Cur_Prj, "Project_Details.txt");

            //if (!File.Exists(backingFile))
            //{
            //    File.Create(backingFile);
            //}
            using (var writer = File.CreateText(backingFile))
            {
                writer.Flush();
                var totnoof_photos = Directory.GetFiles(Cur_Prj).Length - 1;
                string txt_val = Cur_Prj + "|" + totnoof_photos;
                await writer.WriteLineAsync(txt_val);
            }



            // await aa.Navigation.PopAsync();
            //camera.StopPreview();
            //camera.Release();
            //await Navigation.PopAsync();
            //OnSurfaceTextureDestroyed(surfaceTexture);
            //CustomRenderer.App.Current.Quit;
            // Xamarin.Forms.Xaml.
            //navigtion
            //string val = NavigationContext.QueryString.TryGetValue("key1", out val);
            // MainPage.
            //camera.StopPreview();
            //camera.Release();
            //Page page = new NavigationPage(new CustomRenderer.MainPage());
            //await page.Navigation.PushModalAsync(MainPage = new NavigationPage(new CustomRenderer.MainPage());
            //await page.Navigation.PopAsync();
            //activity.NavigateUpTo(1);
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopModalAsync();

            //activity.Finish();
            //Xamarin.Android.Net.
            //System.exit(0);
            
            //activity.CloseContextMenu();
            //this.nav
            // activity.NavigateUpTo();
            //activity.star
            //await Xamarin.Forms.INavigation.push
            //await aa.Navigation.PopAsync();
            //await INavigation.PushAsync(new CustomRenderer.MainPage());
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

            view.Measure(msw, msh);
            view.Layout(0, 0, r - l, b - t);
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {

        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            camera = global::Android.Hardware.Camera.Open((int)cameraType);
            textureView.LayoutParameters = new FrameLayout.LayoutParams(width, height);
            surfaceTexture = surface;

            camera.SetPreviewTexture(surface);
            PrepareAndStartCamera();
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            camera.StopPreview();
            camera.Release();
            return true;
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            PrepareAndStartCamera();
        }

        void PrepareAndStartCamera()
        {
            camera.StopPreview();

            var display = activity.WindowManager.DefaultDisplay;
            if (display.Rotation == SurfaceOrientation.Rotation0)
            {
                camera.SetDisplayOrientation(90);
            }

            if (display.Rotation == SurfaceOrientation.Rotation270)
            {
                camera.SetDisplayOrientation(180);
            }

            camera.StartPreview();
        }

        void ToggleFlashButtonTapped(object sender, EventArgs e)
        {
            flashOn = !flashOn;
            if (flashOn)
            {
                if (cameraType == CameraFacing.Back)
                {
                    toggleFlashButton.SetBackgroundResource(Resource.Drawable.FlashButton);
                    cameraType = CameraFacing.Back;

                    camera.StopPreview();
                    camera.Release();
                    camera = global::Android.Hardware.Camera.Open((int)cameraType);
                    var parameters = camera.GetParameters();
                    parameters.FlashMode = global::Android.Hardware.Camera.Parameters.FlashModeTorch;
                    camera.SetParameters(parameters);
                    camera.SetPreviewTexture(surfaceTexture);
                    PrepareAndStartCamera();
                }
            }
            else
            {
                toggleFlashButton.SetBackgroundResource(Resource.Drawable.NoFlashButton);
                camera.StopPreview();
                camera.Release();

                camera = global::Android.Hardware.Camera.Open((int)cameraType);
                var parameters = camera.GetParameters();
                parameters.FlashMode = global::Android.Hardware.Camera.Parameters.FlashModeOff;
                camera.SetParameters(parameters);
                camera.SetPreviewTexture(surfaceTexture);
                PrepareAndStartCamera();
            }
        }

        void SwitchCameraButtonTapped(object sender, EventArgs e)
        {
            if (cameraType == CameraFacing.Front)
            {
                cameraType = CameraFacing.Back;

                camera.StopPreview();
                camera.Release();
                camera = global::Android.Hardware.Camera.Open((int)cameraType);
                camera.SetPreviewTexture(surfaceTexture);
                PrepareAndStartCamera();
            }
            else
            {
                cameraType = CameraFacing.Front;

                camera.StopPreview();
                camera.Release();
                camera = global::Android.Hardware.Camera.Open((int)cameraType);
                camera.SetPreviewTexture(surfaceTexture);
                PrepareAndStartCamera();
            }
        }

        async void TakePhotoButtonTapped(object sender, EventArgs e)
        {
            //Another method of getting location
            //var results = await Xamarin.Essentials.Permissions.CrossGeolocator.Current.GetPositionAsync(10000);

            //var result = await LocationServices.SettingsApi.CheckLocationSettingsAsync(mGoogleApiClient, mLocationSettingsRequest);
            //await HandleResult(result);
            //var status = locationSettingsResult.Status;
            //try
            //{
            //    status.StartResolutionForResult(this, REQUEST_CHECK_SETTINGS);
            //}
            //catch (IntentSender.SendIntentException)
            //{
            //    Log.Info(TAG, "PendingIntent unable to execute request.");
            //}

            //Get the Geo location

            double lat = 0.0;
            double lon = 0.0;
            //var location;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
               var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    lat = location.Latitude;
                    lon = location.Latitude;
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }


            camera.StopPreview();

            var image = textureView.Bitmap;
            var filePath = "";
            try
            {
                var absolutePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).AbsolutePath;
                var folderPath = absolutePath + "/Camera";
                //var filePath = System.IO.Path.Combine(folderPath, string.Format("photo_{0}.jpg", Guid.NewGuid()));
                Cur_Prj = Cur_Prj.Replace("\n", "");
                //var filePath = System.IO.Path.Combine(Cur_Prj, string.Format("photo_{0}.jpg", Guid.NewGuid()));
                filePath = System.IO.Path.Combine(Cur_Prj, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".jpg");
                //var filePath = new System.IO.FileStream(Cur_Prj + DateTime.Now.ToString("YYYY_MM_dd_HH_mm_ss") + ".jpg", System.IO.FileMode.CreateNew);

                //CameraPage

                //CustomRenderer.MainPage.

                var fileStream = new FileStream(filePath, FileMode.Create);
                await image.CompressAsync(Bitmap.CompressFormat.Jpeg, 50, fileStream);
                //fileStream.BeginWrite()
                fileStream.Flush();
                fileStream.Close();
                

                image.Recycle();

                var intent = new Android.Content.Intent(Android.Content.Intent.ActionMediaScannerScanFile);
                var file = new Java.IO.File(filePath);
                var uri = Android.Net.Uri.FromFile(file);
                intent.SetData(uri);
                MainActivity.Instance.SendBroadcast(intent);

                //if (location.Longtitude != null)
                //{


                //ExifInterface newExif = new ExifInterface(filePath);
                //newExif.SetAttribute(ExifInterface.TagGpsLongitudeRef, lat.ToString());
                //newExif.SetAttribute(ExifInterface.TagGpsLongitudeRef, lon.ToString());
                //newExif.SaveAttributes();
                //var files = Directory.GetFiles(filePath);
                var dir = new DirectoryInfo(Cur_Prj);
                dir.Refresh();

                int milliseconds = 2000;
                Task.Delay(milliseconds);
                dir.Refresh();
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var loc = await Geolocation.GetLocationAsync(request);
                    ExifInterface ef = new ExifInterface(filePath);
                    ef.SetAttribute(ExifInterface.TagGpsLatitude, dec2DMS(loc.Latitude));
                    ef.SetAttribute(ExifInterface.TagGpsLongitude, dec2DMS(loc.Longitude));
                    if (loc.Latitude > 0)
                        ef.SetAttribute(ExifInterface.TagGpsLatitudeRef, "N");
                    else
                        ef.SetAttribute(ExifInterface.TagGpsLatitudeRef, "S");
                    if (loc.Latitude > 0)
                        ef.SetAttribute(ExifInterface.TagGpsLongitudeRef, "E");
                    else
                        ef.SetAttribute(ExifInterface.TagGpsLongitudeRef, "W");
                    ef.SaveAttributes();
                }
                catch (Exception)
                {
                    throw;
                }
                //Image img = Image.FromFile("C:\\temp\\pics\\mypic.JPG");
                //System.Drawing.Imaging.PropertyItem prop = img.PropertyItems[0];
                //SetProperty(ref prop, 33432, "Copyright Information...");
                //img.SetPropertyItem(prop);
                //prop = img.PropertyItems[0];
                //SetProperty(ref prop, 315, "Artist...");
                //img.SetPropertyItem(prop);
                //prop = img.PropertyItems[0];
                //SetProperty(ref prop, 270, "Title...");
                //img.SetPropertyItem(prop);
                //prop = img.PropertyItems[0];
                //SetProperty(ref prop, 272, "Software...");
                //img.SetPropertyItem(prop);
                //img.Save("C:\\temp\\pics\\mypic_modified.JPG");

            //// Get the thumbnail image
            //Image thumb = file.ThumbnailImage;

            //// Set the date time to now
            //file.Properties[ExifTag.DateTime].Value = DateTime.Now;
            //// Modify GPS location
            //GPSLatitudeLongitude location =  file.Properties[ExifTag.GPSLatitude]
            //    as GPSLatitudeLongitude;
            //location.Degrees.Set(22, 0);

            //// Save exif data with the image
            //file.Save("path_to_my_image");

            //}
            //Image img = Image.FromStream(ms);
            //AddProperty(img, ExifTagGPSVersionID, ExifTypeByte, new byte[] { 2, 3, 0, 0 });
            //AddProperty(img, ExifTagGPSLatitudeRef, ExifTypeAscii, new byte[] { (byte)latHemisphere, 0 });
            //AddProperty(img, ExifTagGPSLatitude, ExifTypeRational, ConvertToRationalTriplet(lat));
            //AddProperty(img, ExifTagGPSLongitudeRef, ExifTypeAscii, new byte[] { (byte)lngHemisphere, 0 });
            //AddProperty(img, ExifTagGPSLongitude, ExifTypeRational, ConvertToRationalTriplet(lng));



            //BitmapMetadata metadata = original.Frames[0].Metadata.Clone() as BitmapMetadata;

            ////pad the metadata so that it can be expanded with new tags
            //metadata.SetQuery("/app1/ifd/PaddingSchema:Padding", paddingAmount);
            //metadata.SetQuery("/app1/ifd/exif/PaddingSchema:Padding", paddingAmount);
            //metadata.SetQuery("/xmp/PaddingSchema:Padding", paddingAmount);

            ////form the new metadata that is to be added
            //double latitude = 30.0 + 15.0 / 60.0 + 22.0 / 3600.0;
            //double longitude = -(86.0 + 16.0 / 60.0 + 23.0 / 3600.0);
            //double altitude = 44;

            //GPSRational latitudeRational = new GPSRational(latitude);
            //GPSRational longitudeRational = new GPSRational(longitude);
            //metadata.SetQuery(GPSLatitudeQuery, latitudeRational.bytes);
            //metadata.SetQuery(GPSLongitudeQuery, longitudeRational.bytes);
            //if (latitude > 0) metadata.SetQuery(GPSLatitudeRefQuery, "N");
            //else metadata.SetQuery(GPSLatitudeRefQuery, "S");
            //if (longitude > 0) metadata.SetQuery(GPSLongitudeRefQuery, "E");
            //else metadata.SetQuery(GPSLongitudeRefQuery, "W");

            //Rational altitudeRational = new Rational((int)altitude, 1);  //denoninator = 1 for Rational
            //metadata.SetQuery(GPSAltitudeQuery, altitudeRational.bytes);


            ////create the output image using the image data, thumbnail, and metadata from the original image as modified above
            //output.Frames.Add(
            //    BitmapFrame.Create(original.Frames[0], original.Frames[0].Thumbnail, metadata, original.Frames[0].ColorContexts));


            ////save the output image
            //using (Stream outputFile = File.Open(outputPath, FileMode.Create, FileAccess.ReadWrite))
            //{
            //    output.Save(outputFile);
            //}





        }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"				", ex.Message);
            }

            camera.StartPreview();

            //ExifInterface exif = new ExifInterface(filePath);

            //// call this next setAttributes a few times to write all the GPS data to it.
            //exif.setAttribute(... );

            //// don't forget to save
            //exif.saveAttributes();
        }
        String dec2DMS(double coord)
        {
            coord = coord > 0 ? coord : -coord;  // -105.9876543 -> 105.9876543
            //String sOut = integer.toString((int)coord) + "/1,";   // 105/1,
            String sOut = coord.ToString() + "/1,";   // 105/1,
            coord = (coord % 1) * 60;         // .987654321 * 60 = 59.259258
            //sOut = sOut + Integer.toString((int)coord) + "/1,";   // 105/1,59/1,
            sOut = sOut + coord.ToString() + "/1,";   // 105/1,59/1,
            coord = (coord % 1) * 60000;             // .259258 * 60000 = 15555
            //sOut = sOut + Integer.toString((int)coord) + "/1000";   // 105/1,59/1,15555/1000
            sOut = sOut + coord.ToString() + "/1000";   // 105/1,59/1,15555/1000
            return sOut;
        }
        //private void SetProperty(ref System.Drawing.Imaging.PropertyItem prop, int iId, string sTxt)
        //{
        //    int iLen = sTxt.Length + 1;
        //    byte[] bTxt = new Byte[iLen];
        //    for (int i = 0; i < iLen - 1; i++)
        //        bTxt[i] = (byte)sTxt[i];
        //    bTxt[iLen - 1] = 0x00;
        //    prop.Id = iId;
        //    prop.Type = 2;
        //    prop.Value = bTxt;
        //    prop.Len = iLen;
        //}
    }
}

