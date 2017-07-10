using Android.App;
using Android.Graphics;
using Android.Widget;
using Android.OS;
using Java.Net;
using System.IO;


namespace DownloadingImages
{
    [Activity(Label = "DownloadingImages", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button _buttonDownloadImage;
        private ImageView _imageView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            initFields();
        }

        private void initFields()
        {
            _buttonDownloadImage = FindViewById<Button>(Resource.Id.buttonDownloadImage);
            _buttonDownloadImage.Click += _buttonDownloadImage_Click;

            _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
        }

        private void _buttonDownloadImage_Click(object sender, System.EventArgs e)
        {
            string imageUrl = "https://assets.fxnetworks.com/cms/prod/2016/12/simpsonsworld_social_og_bart_1200x1200.jpg";
            ImageDowloader imageDowloader=new ImageDowloader();
            Bitmap downloadedBitmap= imageDowloader.Execute(imageUrl).GetResult();
            _imageView.SetImageBitmap(downloadedBitmap);
        }
    }

    public class ImageDowloader : AsyncTask<string, int, Bitmap>
    {
        protected override Bitmap RunInBackground(params string[] @params)
        {
            URL url=new URL(@params[0]);
            HttpURLConnection connection = (HttpURLConnection)url.OpenConnection();
            connection.Connect();
            Stream inputStream =  connection.InputStream;
            Bitmap imageBitmap = BitmapFactory.DecodeStream(inputStream);
            return imageBitmap;

        }
    }
}

