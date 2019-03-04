using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System.IO;
using System;

public class DataLogger : MonoBehaviour {

    public bool mLogging = false;

    private SteamVR_TrackedObject tracker;

    private List<TrackerDataPoint> trackerDataPoints;

	void Start () {

        tracker = GetComponent<SteamVR_TrackedObject>();

        if (tracker == null)
        {

            Debug.LogError( "No SteamVR_TrackedObject found on this GameObject");

        }

        trackerDataPoints = new List<TrackerDataPoint>();
		
	}
	
	void Update () {

        if (mLogging)
        {
            trackerDataPoints.Add( new TrackerDataPoint( transform.position, Time.time));
        }
		
	}

    public void SetRecording( bool _value){
    
        mLogging = _value;

    }

    public void StartLogging(){

        mLogging = true;

    }

    public void StopLogging(){
    
        mLogging = false;

    }

    void OnDisable(){

        print("Log size " + trackerDataPoints + " size " + trackerDataPoints.Count );

        string [] dates = System.DateTime.Now.GetDateTimeFormats();

        string timeNow = System.DateTime.Now.TimeOfDay.Hours.ToString() + "_" + System.DateTime.Now.TimeOfDay.Minutes.ToString();

        string filename = gameObject.name + " " + dates[7] + "_" + timeNow + ".json";

        int index = 0;

        if (trackerDataPoints.Count > 0) 
        {   

            TrackerDataLog log  = new TrackerDataLog( trackerDataPoints.ToArray(), gameObject.name, dates[5], "" );

            string planeDataString = JsonUtility.ToJson(log, true);

            string path = System.IO.Path.Combine(Application.dataPath, "ViveTrackerUtilities/LoggingData/" + filename);

            File.WriteAllText(path, planeDataString);


        } else
        {

            Debug.Log("No logging data recorded");

        }
    
    }
}
[Serializable]
public struct TrackerDataPoint{

    public Vector3 position;
    public float time;

    public TrackerDataPoint( Vector3 _position, float _time){
    
        position = _position;

        time = _time;
    }

}
[Serializable]
public struct TrackerDataLog{

    public string trackerGameObjectName;

    public string date;

    public string meta;

    public TrackerDataPoint [] datalog;

    public TrackerDataLog (TrackerDataPoint [] _datalog, string _trackerGameObjectName, string _date, string _meta){

        datalog = _datalog;
        trackerGameObjectName = _trackerGameObjectName;
        date = _date;
        meta = _meta;

    }



}
