using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{
    public Animator anim;
    bool isRotating = false;
    public Transform activator;
    public float activationDistance = 5;
    void Start()
    {
        
    }

      void Update()
    {
    //     if(Input.GetKeyDown(KeyCode.Space)){
    //         isRotating = !isRotating;
    //     }
    //     anim.SetBool("turretRotate", isRotating);

        float distance = Vector3.Distance(transform.position, activator.position);

        if(distance < activationDistance){
            Debug.DrawLine(transform.position, activator.position, Color.green);
            anim.SetBool("turretRotate", true);
        }else{
             Debug.DrawLine(transform.position, activator.position, Color.red);
             anim.SetBool("turretRotate", false);
        }

    }
}
