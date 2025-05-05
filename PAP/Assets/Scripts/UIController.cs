using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public GameObject menuGameOver;
    public GameObject pauseMenu;

    public void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
    }

    public void gameOver(){
        Time.timeScale= 0f;
        menuGameOver.SetActive(true); 
    }
    public void pause(){
        Time.timeScale= 0f;
        pauseMenu.SetActive(true); 
    } 
    public void continuePause(){
        Time.timeScale= 1f;
        pauseMenu.SetActive(false); 
    }
    public void restart(){
        SceneManager.LoadScene(1);
    }
    public void options(){
        Debug.Log("todavia no esta");
    }
    public void exit(){
        Application.Quit();
    }



}
