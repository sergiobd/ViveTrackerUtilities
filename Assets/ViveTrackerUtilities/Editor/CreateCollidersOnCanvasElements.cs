using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR_64

public class CreateCollidersOnCanvasElements : Editor {

	
    private static List <GameObject> mInteractiveElements;

    public static string layerName = "VR_UI";

    private static float mThickness = 1f;

    // Use this for initialization
    [MenuItem("Tools/ViveUtilities/Add collider to buttons")]

	public static void AddColliderToButtons () {

        mInteractiveElements = new List <GameObject>(); 

        mInteractiveElements = GetAllObjectsInLayer(layerName);


        foreach (GameObject element in mInteractiveElements)
        {

            if( element.GetComponent<Collider>() != null){ // If already has collider, do not add

                Debug.Log("Element " + element.name + " already has a collider" );

            }
            else{

                RectTransform rectTransform = element.GetComponent<RectTransform>();

                if( rectTransform != null ){

                    if( element.GetComponent<Button>()){

                        AddColliderToRectTransform(rectTransform);

                        // Horrible trick to sort them from panels
                        if(rectTransform.anchoredPosition3D.z == 0){
                            rectTransform.anchoredPosition3D += -0.001f * Vector3.forward;
                        }

                    }

                }
                else if( element.GetComponent<EventTrigger>()){ // Doesn't have a collider nor a RectTransform, but its a GameObject with VR_UI layer

                    // Probably dont do anything

                }

            }

        }
		
	}
    [MenuItem("Tools/ViveUtilities/Add collider to element")]
    public static void  AddColliderToElement(){

        GameObject [] selection = Selection.gameObjects;

        foreach (GameObject element in selection)
        {
            RectTransform rectTransform = element.GetComponent<RectTransform>();

            if(rectTransform != null){

                AddColliderToRectTransform(rectTransform);
            }
        }

    }
    public static  List <GameObject> GetAllObjectsInLayer (string _layerName){

        List <GameObject> objectsInLayerList = new List <GameObject>();

        GameObject [] allObjects = (GameObject [] )FindObjectsOfType(typeof(GameObject));

        foreach (GameObject g in allObjects)
        {
            if( g.layer == LayerMask.NameToLayer(_layerName) ){

                objectsInLayerList.Add( g );

            }
        }

        return objectsInLayerList;

    }
    public static void AddColliderToRectTransform(RectTransform _rectTransform){

        AddColliderToRectTransform(_rectTransform, _rectTransform.rect.size, new Vector2(0, 0));

    }
    public static void AddColliderToRectTransform(RectTransform _rectTransform, Vector2 _size, Vector2 _center){

        //Debug.Log(" Added collider to " + _rectTransform.gameObject.name);

        BoxCollider collider = _rectTransform.gameObject.AddComponent<BoxCollider>();
        collider.size = _size;
        collider.center = _center;

    }
}

#endif
