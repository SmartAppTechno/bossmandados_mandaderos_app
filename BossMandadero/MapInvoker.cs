
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BossMandadero
{
    public class MapInvoker
    {
        public GoogleMap Map { get; set; }
        public MapFragment MapFrag { get; set; }
        private Activity mAct;
        private MapType mType;

        private Dialog mDialog;
        public bool Displayed { get; set; }

        public MapInvoker(Activity activity, MapType type)
        {
            mAct = activity;
            mType = type;
            Displayed = false;
        }
        public bool StartMap()
        {
            if(mType == MapType.Dialog)
            {
                DisplayMap();
            }
            return true;
        }
        private void DisplayMap()
        {
            if (mDialog != null)
            {
                mDialog.Show();
            }
            else
            {
                mDialog = new Dialog(mAct, Resource.Style.AlertDialogDefault);
                LayoutInflater inflater = mAct.LayoutInflater;
                View view = inflater.Inflate(Resource.Layout.Map, null);
                mDialog.SetContentView(view);
                mDialog.Show();
                Displayed = true;
            }
        }
        public void DismissMap()
        {
            mDialog.Hide();
            Displayed = false;
        } 



        public void MapReady()
        {
            CameraPosition.Builder builder = new CameraPosition.Builder();
            builder.Target(new LatLng(21.88234, -102.28259));
            builder.Zoom(17);
            builder.Bearing(90);
            builder.Tilt(30);
            CameraPosition cameraPosition = builder.Build();

            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            Map.MoveCamera(cameraUpdate);
        }
    }

    public enum MapType
    {
        Dialog
    }
}
