using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListener : MonoBehaviour {

    SteamVR_TrackedController controller;

	void Start () {

        controller = GetComponent<SteamVR_TrackedController>();	

        controller.TriggerClicked += OnTriggerClicked; // Also check the rest of the events.


	}
	
	void Update () {
		
	}
   
    void OnTriggerClicked(object sender, ClickedEventArgs e){

        /*
        GameObject smallCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        smallCube.transform.position = transform.position;
        smallCube.transform.localScale = 0.1f*Vector3.one;
        */
        print("Controller trigger clicked");

    }
    void OnDisable(){

        controller.TriggerClicked -= OnTriggerClicked;

    }

}
