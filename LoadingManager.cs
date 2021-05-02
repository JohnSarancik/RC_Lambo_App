using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingManager : MonoBehaviour
{

    public TextMeshProUGUI start_button;
    public TextMeshProUGUI instruction_button;
    public GameObject loadingScreenButton;
    public GameObject loadingScreenBackground;
    public GameObject instruction_screen_background;
    public GameObject instruction_screen_button;
    public GameObject controller;

    public void ChangeStartButtonText(){

        start_button.GetComponent<TextMeshProUGUI>().text = "Loading.....";
        StartCoroutine("GarageDoorDown");

    }
    public void TurnOffBackground(){

        loadingScreenButton.SetActive(false);
        loadingScreenBackground.SetActive(false);

    }   
    public void LoadInstructions(){
        
        instruction_screen_background.SetActive(true);

    }
     public void ChangeInstructionButtonText(){

        instruction_button.GetComponent<TextMeshProUGUI>().text = "Loading.....";
        StartCoroutine("GarageDoorDown2");
    }
    public void TurnOffInstructions(){

        instruction_screen_button.SetActive(false);
        instruction_screen_background.SetActive(false);
    }

    public void LoadGame(){

        controller.SetActive(true);

    }

    IEnumerator GarageDoorDown(){

        yield return new WaitForSeconds(2f);

        TurnOffBackground();
        LoadInstructions();
    }

    IEnumerator GarageDoorDown2(){

        yield return new WaitForSeconds(2f);

        TurnOffInstructions();
        LoadGame();
    }

}
