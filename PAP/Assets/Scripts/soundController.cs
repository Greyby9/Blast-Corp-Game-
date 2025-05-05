using UnityEngine;

public class soundController : MonoBehaviour
{
    public static soundController Instace;

    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instace == null){
            Instace = this;
            DontDestroyOnLoad(gameObject);

        } else {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void exeSound(AudioClip sound){
        audioSource.PlayOneShot(sound);

    }
}
