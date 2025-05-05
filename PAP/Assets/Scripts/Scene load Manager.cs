using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneloadManager : MonoBehaviour
{
    private Animator animScene;
    public float timeTransition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animScene = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadNextScene(){
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(sceneLoad(nextSceneIndex));
    }
    public IEnumerator sceneLoad(int sceneIndex) {
        animScene.SetTrigger("startTransition");
        yield return new WaitForSeconds(timeTransition);
        SceneManager.LoadScene(sceneIndex);

    }
}
