using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public GameObject panelGameSettings;

    [SerializeField]
    private SessionSettings session = null;

    [SerializeField]
    private SessionSettings defaultSettings = null;

    private void Awake(){
        if(defaultSettings!=null){
            session.Player1Character=defaultSettings.Player1Character;
            session.Player2Character=defaultSettings.Player2Character;
        }    
    }

   public void buttonPlayClick(){
       panelGameSettings.SetActive(true);
   }
   public void buttonQuitClick(){

   }
   public void SetP1Character(int character){
       session.Player1Character=character;
   }
   public void SetP2Character(int character){
       session.Player2Character=character;
   }
   public void closePanel(){
       panelGameSettings.SetActive(false);
   }
   public void startGame(){
    
   }
}
