using System;
using System.Collections.Generic;
using System.IO;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Util;
using Common.DBItems;
using Java.IO;
using Java.Lang;
using Java.Net;
using Java.Util;
using Org.Json;

namespace CoreLogic
{
    public class Route
    {
        /*
        public void GetRoute(GoogleMap map, LatLng origin, LatLng dest)
        {
            string url = DirectionsURL(origin, dest);
            DownloadTask downloadTask = new DownloadTask();
            downloadTask.Execute(url);
        }

        private string DirectionsURL(LatLng origin, LatLng dest)
        {

            // Origin of route
            string str_origin = "origin=" + origin.Latitude + "," + origin.Longitude;

            // Destination of route
            string str_dest = "destination=" + dest.Latitude + "," + dest.Longitude;

            // Sensor enabled
            string sensor = "sensor=false";
            string mode = "mode=driving";

            // Building the parameters to the web service
            string parameters = str_origin + "&" + str_dest + "&" + sensor + "&" + mode;

            // Output format
            string output = "json";

            // Building the url to the web service
            string url = "https://maps.googleapis.com/maps/api/directions/" + output + "?" + parameters;

            return url;
        }
        */

    }
}

/*
public class DownloadTask : AsyncTask
{
    
    protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
    {
        string data = string.Empty;
        try
        {
            data = DownloadUrl(@params[0]);
        }
        catch (System.Exception e)
        {
            //Log.d("Background Task", e.ToString());
        }
        return data;
    }

    private string DownloadUrl(string strUrl)
    {
        string data = "";
        Stream iStream = null;
        HttpURLConnection urlConnection = null;
        try 
        {
            URL url = new URL(strUrl);
            urlConnection = (HttpURLConnection)url.OpenConnection();
            urlConnection.Connect();

            iStream = urlConnection.InputStream;
            BufferedReader br = new BufferedReader(new InputStreamReader(iStream));
            StringBuffer sb = new StringBuffer();

            string line = string.Empty;
            while ((line = br.ReadLine()) != null)
            {
                sb.Append(line);
            }

            data = sb.ToString();

            br.Close();

        } catch (System.Exception e)
        {
        //Log.D("Exception", e.toString());
        } finally 
        {
            iStream.Close();
            urlConnection.Disconnect();
        }
        return data;
    }
    protected override void OnPostExecute(Java.Lang.Object result)
    {
        base.OnPostExecute(result);
        ParserTask parserTask = new ParserTask();
        parserTask.execute(result);

    }

}

public class ParserTask : AsyncTask<string, Integer, List<List<HashMap>>>
{
    protected override List<List<HashMap>> RunInBackground(params string[] @params)
    {
        JSONObject jObject;
        List<List<HashMap>> routes = null;

        try
        {
            jObject = new JSONObject(jsonData[0]);
            DirectionsJSONParser parser = new DirectionsJSONParser();

            routes = parser.parse(jObject);
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
        return routes;
    }
}

/*

    }

    private class ParserTask extends AsyncTask<String, Integer, List<List<HashMap>>> {

    // Parsing the data in non-ui thread
    @Override
        protected List<List<HashMap>> doInBackground(String...jsonData)
    {

        
    }

    @Override
        protected void onPostExecute(List<List<HashMap>> result)
    {
        ArrayList points = null;
        PolylineOptions lineOptions = null;
        MarkerOptions markerOptions = new MarkerOptions();

        for (int i = 0; i < result.size(); i++)
        {
            points = new ArrayList();
            lineOptions = new PolylineOptions();

            List<HashMap> path = result.get(i);

            for (int j = 0; j < path.size(); j++)
            {
                HashMap point = path.get(j);

                double lat = Double.parseDouble(point.get("lat"));
                double lng = Double.parseDouble(point.get("lng"));
                LatLng position = new LatLng(lat, lng);

                points.add(position);
            }

            lineOptions.addAll(points);
            lineOptions.width(12);
            lineOptions.color(Color.RED);
            lineOptions.geodesic(true);

        }

        // Drawing polyline in the Google Map for the i-th route
        mMap.addPolyline(lineOptions);
    }
}




}
*/