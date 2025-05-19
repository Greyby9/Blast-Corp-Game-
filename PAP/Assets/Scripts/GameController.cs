using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject spawnerEnemy;
    public GameObject spawnerDrone;

    //<<<<<<Menus
    public GameObject menuGameOver;
    public GameObject pauseMenu;
    public TextMeshProUGUI textBulletSMG;
    public TextMeshProUGUI textBulletPistol;
    public TextMeshProUGUI textBulletShotGun;

    public  GameObject activePlayer;





 private void Awake()
{
       if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
}
void Start()
{
}


void Update()
{

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
