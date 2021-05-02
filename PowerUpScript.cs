using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpScript : MonoBehaviour
{
    ButtonScript button;
    public Image item_image;
    public Sprite flaming_wings_img;
    private void OnTriggerEnter(Collider other){

        if(other.gameObject.tag == "Lambo"){

            button = FindObjectOfType<ButtonScript>();
            button.wings_ready = true;
            this.gameObject.SetActive(false);
            item_image = button.GetComponent<Image>();
            item_image.sprite = flaming_wings_img;
            Debug.Log("acquired wings");
        }
    }
}
