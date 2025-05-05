using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public int currentWeaponIndex = 0;
    public GameObject menuOptions;
    public GameObject mainPause;
    public AudioMixer audioMixer;
    public int lifeAmount;
    public TextMeshProUGUI textLife;
    public GameObject canvasPistol;
    public GameObject canvasSMG;
    public GameObject canvasShotgun;

    void Start()
    {
        canvasSMG.SetActive(false);
        canvasPistol.SetActive(true);
        canvasShotgun.SetActive(false);

        
    }

    void Update()
    {
    showWeapon();
    }
    void showWeapon(){
        canvasPistol.SetActive(true);
        if (GameController.instance.hasSMG==true)
        {
        canvasSMG.SetActive(true);
        }
        if(GameController.instance.hasShotgun==true){
        canvasShotgun.SetActive(true);
        }
    }


    
    public void UpdateUp(int lives)
    {
        if(lives == 3){
        lifeAmount=3;
        textLife.text = "x" + lifeAmount.ToString();
        }
        if(lives == 2){
        lifeAmount=2;
        textLife.text = "x" + lifeAmount.ToString();
        }
        if(lives == 1){
        lifeAmount=1;
        textLife.text = "x" + lifeAmount.ToString();  
        }

    }
    public void options(){
        menuOptions.SetActive(true);
        mainPause.SetActive(false);
    }
    public void exit(){
        Application.Quit();
    }
    public void replay(){
        Time.timeScale = 1f;
        mainPause.SetActive(false); 
    }
    public void back(){
        menuOptions.SetActive(false);
        mainPause.SetActive(true);
    }
    public void soloparamanana(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    
    public void fullScreen(bool fullScreen){
        Screen.fullScreen = fullScreen; 
    }
    public void changeVolume(float volume){
        audioMixer.SetFloat("volume",volume);
    }
    public void changeQuality(int index){
        QualitySettings.SetQualityLevel(index);
    }
      void pause()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            if (Time.timeScale == 0){
            Time.timeScale = 1f;
            mainPause.SetActive(false);   
            } else {
            Time.timeScale = 0f;
            mainPause.SetActive(true); 
            }
 
        }
    }
}
