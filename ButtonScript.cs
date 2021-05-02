using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public bool isPressed;
    public GameObject flaming_wings;
    public bool wings_ready = false;
    public Image item_image;
    public Sprite default_button_img;
    MoveScript moveScript;
    ButtonScript button;
    SpawnObjectOnPlane spawn;
    public void OnPointerDown(PointerEventData eventData){

        isPressed = true;

        if(isPressed == true && wings_ready == true){

            FindSpawn();
            spawn.in_game_text.GetComponent<TextMeshProUGUI>().text = "Speed Wings Active";
            flaming_wings.SetActive(true);
            Debug.Log("wings on");
            FindCar();
            moveScript.move_speed = 5f;
            StartCoroutine("WingDuration");
        }
    }

     public void OnPointerUp(PointerEventData eventData){

        isPressed = false;
    }

    public void FindCar(){

        moveScript = FindObjectOfType<MoveScript>();
    }

    public void FindButton(){

        button = FindObjectOfType<ButtonScript>();
    }

    public void FindSpawn(){

        spawn = FindObjectOfType<SpawnObjectOnPlane>();
    }
    IEnumerator WingDuration(){

        yield return new WaitForSeconds(5f);
        wings_ready = false;
        flaming_wings.SetActive(false);
        FindCar();
        moveScript.move_speed = 1f;
        FindButton();
        item_image = button.GetComponent<Image>();
        item_image.sprite = default_button_img;
    }

}
