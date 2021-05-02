using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARRayCaster : MonoBehaviour
{
    [Header("Config Options")]
    [SerializeField]
    private GameObject objectToMake;
    public bool is_ready_to_spawn = true;
    List<ARRaycastHit> hits;
    [SerializeField]
    ARRaycastManager arm;
    void Start(){
        
        hits = new List<ARRaycastHit>();

    }

    void Update(){
        
        if(Input.GetMouseButtonDown(0)){

#if UNITY_ANDROID
            //will only for if I'm on Android
#elif UNITY_IOS
            //will only work if I'm on IOS
#endif

#if UNITY_EDITOR
            //will run in the editor
            //do my raycast in the editor
#else

            Debug.Log("Trying Raycast");
            hits.Clear();

           //arm.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes | UnityEngine.XR.ARSubsystems.TrackableType.PlaneEstimated);
           arm.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

           if(hits.Count > 0 && is_ready_to_spawn == true){

                Debug.Log("Did hit");

                GameObject newObject = Instantiate(objectToMake);

                newObject.transform.position = hits[0].pose.position;
                newObject.transform.rotation = hits[0].pose.rotation;
                
                is_ready_to_spawn = false;
           }
#endif

        }


    }
}
