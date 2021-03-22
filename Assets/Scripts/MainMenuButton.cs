using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField]
    private GameObject panelGameSettings;

    [SerializeField]
    private GameObject panelHowToPlay;

    [SerializeField]
    private SessionSettings session = null;

    [SerializeField]
    private SessionSettings defaultSettings = null;

    [SerializeField]
    private SceneLoader sceneLoader = null;

    private void Awake(){
        if(defaultSettings!=null){
            session.GameMode=defaultSettings.GameMode;
        }    
    }

   public void buttonPlayClick(){
       panelGameSettings.SetActive(true);
   }
   public void buttonQuitClick(){

   }
   public void SetGameMode(int mode){
       session.GameMode=mode;
   }
   public void closePanel(){
       panelGameSettings.SetActive(false);
   }
   public void startGame(){
       Debug.Log("mode "+session.GameMode);
        if(session.GameMode==0){
            sceneLoader.LoadScene("SingleplayerScene");
        }else{
            sceneLoader.LoadScene("MultiplayerScene");
        }
   }
   public void showHowToPlay(){
        panelHowToPlay.SetActive(true);
   }
   
   public void closeHowToPlay(){
       panelHowToPlay.SetActive(false);
   }

   public void Quit()
   {
       Application.Quit();
   }
}
