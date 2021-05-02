using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(ARRaycastManager))] // when u place the script onto an object, it will automatically add the ARRaycastMAnager class as well
public class SpawnObjectOnPlane : MonoBehaviour
{
    MoveScript moveScript;
    CheckForRoad road;
    private ARRaycastManager raycast_manager;
    public GameObject spawned_object; // object that is spawned
    private List<GameObject> placed_prefab_list = new List<GameObject>();
    [SerializeField]
    private int max_prefab_spawn_count; // the limit to how many objects to spawn
    public int placed_prefab_count = 0; // tracks how many objects have been placed
    [SerializeField]
    private GameObject placeable_prefab;
    static List<ARRaycastHit> s_hits = new List<ARRaycastHit>(); // initialize list
    [SerializeField]
    private GameObject[] boundaries;
    public GameObject[] cars;
    public Pose hit_position;
    public bool car_is_ready_to_respawn;
    public GameObject car_button;
    public GameObject car_prefab;
    public TextMeshProUGUI in_game_text;

    private void Awake(){

        moveScript = FindObjectOfType<MoveScript>();
        road = FindObjectOfType<CheckForRoad>();
        raycast_manager = GetComponent<ARRaycastManager>(); // config raycastmanager
        max_prefab_spawn_count = 1;
        in_game_text.GetComponent<TextMeshProUGUI>().text = "Tap on Spawn Car button to select car";
    }

    bool GetTouchPosition(out Vector2 touch_position){ // records where the user pressses on the screen

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){

            touch_position = Input.GetTouch(0).position;
            return true;
        }

        touch_position = default;
        return false;
    }

    bool IsPointOverUIObject(Vector2 pos) // ignores the UI elements
    {
        if (EventSystem.current.IsPointerOverGameObject()){
            return false;
        }

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(pos.x, pos.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;

    }

    private void Update(){

        //cars = GameObject.FindGameObjectsWithTag("Lambo");
        
        if(!GetTouchPosition(out Vector2 touch_position)){ // check for valid touch position

            return; // return out of update if no touch
        }

        if(!IsPointOverUIObject(touch_position) && raycast_manager.Raycast(touch_position, s_hits, TrackableType.PlaneWithinPolygon)){ // person is touching screen, check to see if touching plane

            hit_position = s_hits[0].pose;

            if(placed_prefab_count < max_prefab_spawn_count){

                SpawnPrefabs(hit_position);
            }
        }
    }

    public void SetPrefabTypeCar(){

            placeable_prefab = car_prefab;
            car_button.SetActive(false);
            car_is_ready_to_respawn = true;
            in_game_text.GetComponent<TextMeshProUGUI>().text = "Tap on plane to spawn car";
    }

    public void SetPrefabTypePowerUp(GameObject powerup_prefab){

        placeable_prefab = powerup_prefab;
        max_prefab_spawn_count = 3;
        in_game_text.GetComponent<TextMeshProUGUI>().text = "Tap on plane to spawn powerup";
    }

    private void SpawnPrefabs(Pose hit_position){

        spawned_object = Instantiate(placeable_prefab, hit_position.position, hit_position.rotation);
        placed_prefab_list.Add(spawned_object);
        placed_prefab_count++;
    }

    public bool CheckForRoad(){

        foreach(GameObject boundary in GameObject.FindGameObjectsWithTag("Boundary")){

            var ray = new Ray(boundary.transform.position, Vector3.down);

            var inside_plane = raycast_manager.Raycast(ray, s_hits, TrackableType.Planes);

            if(inside_plane){

                return true;
            }else{

               return false;
            }
        }
        return true;
    }

    IEnumerator RespawnCar(){

        yield return new WaitForSeconds(3f);
        placed_prefab_count--;
        foreach(GameObject car in GameObject.FindGameObjectsWithTag("Lambo")){
            Destroy(car);
        }
        car_button.SetActive(true);
        in_game_text.GetComponent<TextMeshProUGUI>().text = "Tap on Spawn Car button to select car";

    }
}
