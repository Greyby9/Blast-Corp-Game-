using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{

    public float bulletCounter, bulletTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletCounter = bulletTime;

        if (Shoot.instance.shootingVertically) {
            transform.eulerAngles = new Vector3(0f,0f,90f);
        }
        else if (Shoot.instance.shootingDiognally) {
            transform.eulerAngles = new Vector3(0f,0f,30f);
        }
    }

    

}
