using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public AudioClip soundMainInitial;
    public GameObject menuPrincipal;
    public GameObject menuOpciones;
  //  public void Start()
  //  {
  //      soundController.Instace.exeSound(soundMainInitial);
  //  }
    public void play(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;

    }
    public void options(){
        menuPrincipal.SetActive(false);
        menuOpciones.SetActive(true);

    }

    public void exit(){
        Application.Quit();
    }

}
