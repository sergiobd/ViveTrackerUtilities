using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class SteamVR_TrackingWithoutHMD : MonoBehaviour
{
    private CVRSystem _vrSystem;
    private TrackedDevicePose_t[] _poses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];

    // initialize
    void Awake()
    {
        var err = EVRInitError.None;
        _vrSystem = OpenVR.Init(ref err, EVRApplicationType.VRApplication_Other);
        if (err != EVRInitError.None)
        {
            // handle init error
        }
    }
    
    // get tracked device poses
    void Update()
    {
        // get the poses of all tracked devices
        _vrSystem.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseStanding, 0.0f, _poses);
        //_vrSystem.

        //print(_poses.Length);

        // send the poses to SteamVR_TrackedObject components
        SteamVR_Events.NewPoses.Send(_poses);
        //SteamVR_Events.

        // Check what happens in SteamVRModule.cs to see if its possible to forward events.

    }

    
    // shutdown
    void OnDestroy()
    {
        OpenVR.Shutdown();
    }
}