using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CheckForRoad : MonoBehaviour
{
    MoveScript moveScript;
    ButtonScript button;
    SpawnObjectOnPlane spawn;
    public bool is_on_road;
    [SerializeField]
    private Rigidbody car_body; 
    public GameObject car;
    public float force = 3f;

    //Debug Stuff
    GameObject indicator;
    Renderer ren;


    public void Start(){

        FindCar();
        FindButton();
        spawn = FindObjectOfType<SpawnObjectOnPlane>();

        is_on_road = true;
        car = GameObject.FindGameObjectWithTag("Lambo");

        indicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        indicator.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        indicator.transform.SetParent(transform, false);
        ren = indicator.GetComponent<Renderer>();

    }

    public void Update(){

        CheckRoad();
    }

    public void CheckRoad(){

        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, 10f)){
            
            ren.material.color = Color.green;
            is_on_road = true;
        }
        else{

            ren.material.color = Color.red;
            is_on_road = false;
            if(is_on_road == false && spawn.car_is_ready_to_respawn == true){
                spawn.StartCoroutine("RespawnCar");
                spawn.car_is_ready_to_respawn = false;
            }
        }
    }

    public void FindCar(){

        moveScript = FindObjectOfType<MoveScript>();
    }

    public void FindButton(){

        button = FindObjectOfType<ButtonScript>();
    }

}
