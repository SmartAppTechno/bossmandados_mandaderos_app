using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Com.Bumptech.Glide;
using Felipecsl.GifImageViewLibrary;
using Java.IO;
using static Android.App.ActionBar;

namespace Common.Utils
{
    public class Dialogs
    {
        private static ProgressDialog progressDialog;
        private static Dialog _progressDialog;
        private static Activity mAct;
        public static void CreateAndShowDialog(Exception exception, String title, Context context, int style)
        {
            CreateAndShowDialog(exception.Message, title, context,style);
        }

        public static void CreateAndShowDialog(string message, string title, Context context, int style)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(new ContextThemeWrapper(context,style));

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();

        }

        public static void CreateProgressDialog(Context context, int style){
            progressDialog = new ProgressDialog(context); // this = YourActivity
            //progressDialog.SetProgressStyle(style);
            progressDialog.SetMessage("Cargando...");
            progressDialog.Indeterminate = true;
            progressDialog.SetCanceledOnTouchOutside(false);
            progressDialog.Show();
        }
        public static void DismissProgressDialog(){
            progressDialog.Dismiss();
            progressDialog = null;
        }

        public static void TestCreateProgressDialog(Activity activity, Context context, int layout, int style, int gif)
        {
            _progressDialog = new Dialog(activity, style);
            //AlertDialog.Builder builder = new AlertDialog.Builder(new ContextThemeWrapper(activity, style));
            LayoutInflater inflater = activity.LayoutInflater;
            View view = inflater.Inflate(layout, null);
            WebView iv = view.FindViewById<WebView>(gif);
            //iv.SetBackgroundColor(Color.Azure);
            iv.LoadUrl(string.Format("file:///android_asset/Loading.gif"));
            iv.SetBackgroundColor(Color.Transparent);
            iv.SetLayerType(LayerType.Software, null);

            _progressDialog.SetContentView(view);

            //builder.SetView(inflater.Inflate(layout, null));

            //_progressDialog = builder.Create();

            _progressDialog.SetCanceledOnTouchOutside(false);
            _progressDialog.Show();
        }
        public static void TestDismissProgressDialog()
        {
            _progressDialog.Dismiss();
            _progressDialog = null;
        } 


        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static AlertDialog.Builder YesNoDialog(string title, string content, Context context, int style)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(new ContextThemeWrapper(context, style));

            builder.SetMessage(content);
            builder.SetTitle(title);


            return builder;
        }

        public static void ExitDialog(Activity activity, int style)
        {

            mAct = activity;
            AlertDialog.Builder builder = new AlertDialog.Builder(new ContextThemeWrapper(activity, style));

            builder.SetMessage("¿Desea salir de la aplicación?");
            builder.SetTitle("Advertencia");
            builder.SetPositiveButton("OK", ExitApp);
            builder.SetNegativeButton("Cancelar",Cancel);
            builder.Create().Show();
        }

        public static void BasicDialog(string message, string title, Context context)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(context);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

        private static void ExitApp(object sender, EventArgs e)
        {
            mAct.Finish();
        }

        private static void Cancel(object sender, EventArgs e)
        {
            
        }
    }
}
