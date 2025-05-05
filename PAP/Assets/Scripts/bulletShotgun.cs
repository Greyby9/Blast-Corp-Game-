using UnityEngine;

public class bulletShotgun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
void OnTriggerEnter2D(Collider2D collider)
{
    if (collider.gameObject.CompareTag("Player")) {
        Debug.Log("10 balas de escopeta");
        Destroy(collider.gameObject); 


    }}

    
}
