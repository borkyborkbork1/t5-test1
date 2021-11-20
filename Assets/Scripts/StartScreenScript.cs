using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenScript : MonoBehaviour
{
    public static bool isStartScreenPaused = false;
    // Update is called once per frame
    public GameObject StartScreenCanvas;
    

    void Start(){
        StartScreenCanvas.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("startscreen triggered");

            if (isStartScreenPaused == true){
                Resume();
            } else {
                Pause();
            }
        }
    }

    void Resume(){ 
        StartScreenCanvas.SetActive(false);
        Time.timeScale=1;
        isStartScreenPaused=false;
    }


    void Pause(){
        StartScreenCanvas.SetActive(true);
        Time.timeScale=0;
        isStartScreenPaused=true;
    }


}
