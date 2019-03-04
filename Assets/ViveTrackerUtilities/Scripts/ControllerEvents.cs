using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ControllerEvents : MonoBehaviour
{

    private SteamVR_TrackedController mController;
    private bool mButtonClicked = false;
    private EventSystem mEventSystem;

    // Maximum distance for RayCast
    public float maxDistance = 30f;

    // History of previous hits
    private GameObject mPreviousHitGO;

    // Laser 
    public Color noEventColor;
    public Color onHoverColor;

    // A cube
    public GameObject laserPrefab;
    public bool drawLaser;
    [Range(0f, 10f)]
    public float thickness = 1f ;
    public float defaultDistance = 20f;

    private GameObject mLaserPrefabContainer;

    public LayerMask layerMask;

    // Use this for initialization
    void Start()
    {
        //layerMask = LayerMask.NameToLayer("UI");
        mController = GetComponent<SteamVR_TrackedController>();

        if (!mController)
        {
            Debug.LogError("No SteamVR_TrackedController object found. Add this script to a controller object.");
        } else
        {
            mController.TriggerClicked += OnTriggerClicked;
            mController.TriggerUnclicked += OnTriggerUnclicked; 
        }

        mEventSystem = EventSystem.current;

        if (!mEventSystem)
        {
            Debug.LogError("No event system present. Add a canvas with an event system");
        }

        // Dummy initialization.  Will use this gameObject and transform as initial data. Will get overridden after first event.
        mPreviousHitGO = new GameObject();
        mPreviousHitGO = gameObject;

        mLaserPrefabContainer = new GameObject("LaserContainer");
        mLaserPrefabContainer.transform.SetParent( this.transform);
        mLaserPrefabContainer.transform.localPosition = new Vector3(0, 0, 0);
        laserPrefab.transform.SetParent(mLaserPrefabContainer.transform);

          
    }


    void Update()
    {   
        
        RaycastHit closestHit;

        // If there was a collision (I mean, a ray intersected an object)
        if (GetClosestHit(out closestHit))
        {   
            // If we are entering colliding with a new Gameobject
            if (mPreviousHitGO != closestHit.transform.gameObject)
            {
                Debug.Log("hovered + " + closestHit.transform.name);
            
                // Make Pointer event data
                PointerEventData pointer = new PointerEventData(mEventSystem);
                pointer.position = closestHit.point;
                pointer.pointerEnter = closestHit.transform.gameObject;
            
                ExecuteEvents.Execute(closestHit.transform.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

                // If there was a previous gameObject that we collided with, send the pointerOut
                if (mPreviousHitGO != gameObject)
                { 

                    ExecuteEvents.Execute(mPreviousHitGO, pointer, ExecuteEvents.pointerExitHandler);

                }

                if (drawLaser)
                {
                    //DrawLaser(new Vector3(0, 0, 0), new Vector3(10, 10, 10));
                    DrawLaser(closestHit.distance, onHoverColor);
                }
            }

            mPreviousHitGO = closestHit.transform.gameObject; 
        } else if (mPreviousHitGO != gameObject) // If there was no collision, but there was one befere, send the pointerOut, reset
        {   
            // (We dont really care about the pointer position now 
            ExecuteEvents.Execute(mPreviousHitGO, new PointerEventData(mEventSystem), ExecuteEvents.pointerExitHandler);

            mPreviousHitGO = gameObject;

        } else if(drawLaser)
        {
            DrawLaser(defaultDistance, noEventColor);
        }



    }

    private bool GetClosestHit(out RaycastHit closestHit)
    {
    
        Ray pointerRay = new Ray(mController.transform.position, mController.transform.forward);

        RaycastHit[] hits;

//        if (layerMask == LayerMask. > 0)
//        {
            hits = Physics.RaycastAll(pointerRay, maxDistance, layerMask ); 
//
//        } else
//        {
//            hits = Physics.RaycastAll(pointerRay, maxDistance); 
//        }


        // SORT
        int closestHitIndex = 0;
        
        for (int i = 1; i < hits.Length; i++)
        {
            
            if (hits [i].distance < hits [closestHitIndex].distance)
            {

                closestHitIndex = i;
                
            }
            
        }
        if (hits.Length > 0)
        {
            closestHit = hits [closestHitIndex];
            return true;
        } else
        {
            closestHit = new RaycastHit();
            return false;
        }
    }
    
    public void OnTriggerClicked(object sender, ClickedEventArgs e)
    {
        RaycastHit closestHit;

        if (GetClosestHit(out closestHit))
        {
            ExecuteEvents.Execute(closestHit.transform.gameObject, new BaseEventData(mEventSystem), ExecuteEvents.submitHandler);
        }


    }

    public void OnTriggerUnclicked(object sender, ClickedEventArgs e)
    {
        
        Debug.Log("Unclicked");
        mButtonClicked = false;
        
    }

    private void DrawLaser(float magnitude, Color _color){

        Vector3 localScale = new Vector3( 0.01f * thickness, 0.01f * thickness, magnitude );
        Vector3 localPosition = 0.5f * magnitude * Vector3.forward;
        laserPrefab.transform.localScale = localScale;
        laserPrefab.transform.localPosition = localPosition;
        laserPrefab.GetComponent<MeshRenderer>().material.color = _color;

    }

}
