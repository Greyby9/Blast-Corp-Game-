using UnityEngine;

public class changeScene : MonoBehaviour
{
    public SceneloadManager sceneloadManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sceneloadManager.LoadNextScene();
        }
    }
}
