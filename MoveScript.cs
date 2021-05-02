using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class MoveScript : MonoBehaviour
{
    Joystick joystick;
    ButtonScript button;
    SpawnObjectOnPlane spawn;
    public float move_speed;
    public float rotation_speed;
    public AudioClip acceleration;
    public AudioClip deceleration;
    public AudioSource car_noise_forward;
    public AudioSource car_noise_backward;
    public GameObject audio_f;
    public GameObject audio_b;
    
    void Start()
    {
        move_speed = 1f;
        rotation_speed = 40f;
    }

    void Update()
    {
        
        joystick = FindObjectOfType<Joystick>();
        button = FindObjectOfType<ButtonScript>();

        float move_car = CrossPlatformInputManager.GetAxis("Vertical") * move_speed;
        float rotate_car = CrossPlatformInputManager.GetAxis("Horizontal") * rotation_speed;

        move_car *= Time.deltaTime;
        rotate_car *= Time.deltaTime;

        Debug.Log(move_car);

        if(move_car > 0){

            car_noise_forward.Play();
        }else{

            car_noise_backward.Play();
        }

        transform.Translate(0, 0, move_car);
        transform.Rotate(0, rotate_car, 0);
        
    }

}
