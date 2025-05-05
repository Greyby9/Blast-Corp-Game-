using UnityEngine;
using UnityEngine.Audio;
public class mainOptions : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject menuPrincipal;
    public GameObject menuOpciones;
    public void fullScreen(bool fullScreen){
        Screen.fullScreen = fullScreen; 
    }
    public void changeVolume(float volume){
        audioMixer.SetFloat("volume",volume);
    }
    public void changeQuality(int index){
        QualitySettings.SetQualityLevel(index);
    }
    public void back(){
        menuOpciones.SetActive(false);
        menuPrincipal.SetActive(true);
    }

}
