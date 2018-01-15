using System;
using Android;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using CoreLogic;

namespace BossMandadero.Services
{
    public class ServiceGPS : Service, ILocationListener
    {
        private Context mContext;
        private bool isGPSEnabled = false;
        private bool isNetworkEnabled = false;
        private bool canGetLocation = false;
        public Location location;
        private double latitude;
        private double longitude;

        private static Int64 MIN_DISTANCE_CHANGE_FOR_UPDATES = 10;
        private static  Int64 MIN_TIME_BW_UPDATES = 1000 * 60 * 1;
        protected LocationManager locationManager;

        public ServiceGPS(Context context)
        {
            this.mContext = context;
            GetLocation();
        }

        public Location GetLocation()
        {
            try
            {
                locationManager = (LocationManager)mContext.GetSystemService(LocationService);
                isGPSEnabled = locationManager.IsProviderEnabled(LocationManager.GpsProvider);
                isNetworkEnabled = locationManager.IsProviderEnabled(LocationManager.NetworkProvider);

                if (!isGPSEnabled && !isNetworkEnabled)
                {

                }
                else
                {
                    this.canGetLocation = true;

                    if (isNetworkEnabled)
                    {
                        locationManager.RequestLocationUpdates(
                            LocationManager.NetworkProvider,
                                MIN_TIME_BW_UPDATES,
                                MIN_DISTANCE_CHANGE_FOR_UPDATES, this);
                        if (locationManager != null)
                        {
                            location = locationManager
                                .GetLastKnownLocation(LocationManager.NetworkProvider);
                            if (location != null)
                            {
                                latitude = location.Latitude;
                                longitude = location.Longitude;
                            }
                        }
                    }

                    if (isGPSEnabled)
                    {
                        if (location == null)
                        {
                            locationManager.RequestLocationUpdates(
                                LocationManager.GpsProvider,
                                    MIN_TIME_BW_UPDATES,
                                    MIN_DISTANCE_CHANGE_FOR_UPDATES, this);
                            if (locationManager != null)
                            {
                                location = locationManager
                                    .GetLastKnownLocation(LocationManager.GpsProvider);
                                if (location != null)
                                {
                                    latitude = location.Latitude;
                                    longitude = location.Longitude;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                
            }

            return location;
        }


        public void stopUsingGPS()
        {
            if (locationManager != null)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted && CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted)
                {
                    // TODO: Consider calling
                    //    public void requestPermissions(@NonNull String[] permissions, int requestCode)
                    // here to request the missing permissions, and then overriding
                    //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
                    //                                          int[] grantResults)
                    // to handle the case where the user grants the permission. See the documentation
                    // for Activity#requestPermissions for more details.
                    return;
                }
                locationManager.RemoveUpdates(this);
            }
        }


        public double GetLatitude()
        {
            if (location != null)
            {
                latitude = location.Latitude;
            }
            return latitude;
        }


        public double GetLongitude()
        {
            if (location != null)
            {
                longitude = location.Longitude;
            }
            return longitude;
        }


        public bool CanGetLocation()
        {
            return canGetLocation;
        }

        public void ShowSettingsAlert()
        {
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(mContext);
            alertDialog.SetTitle("Warning");
            alertDialog.SetMessage("Please enable GPS from settings");
            /*
            alertDialog.SetPositiveButton("Settings", new DialogInterface.IOnClickListener()
            {
                public void onClick(DialogInterface dialog, int which)
            {
                Intent intent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);
                mContext.startActivity(intent);
            }
        });



            alertDialog.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                public void onClick(DialogInterface dialog, int which)
        {
            dialog.cancel();
        }
    });


*/
            alertDialog.Show();
        }

        public void OnLocationChanged(Location location)
        {
            double lat = location.Latitude;
            double longi = location.Longitude;
            GetLocation();
            if (User.CanSetUbicacion)
            {
                User.UpdateLocation(location, this);
            }
        }
        public void OnProviderDisabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}
