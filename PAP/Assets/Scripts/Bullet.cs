using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletCounter, bulletTime;


    
    void Start()
    {
        bulletCounter = bulletTime;

        if (Shoot.instance.shootingVertically) {
            transform.eulerAngles = new Vector3(0f,0f,90f);
        }
        else if (Shoot.instance.shootingDiognally) {
            transform.eulerAngles = new Vector3(0f,0f,30f);
        }else if (Shoot.instance.shootingDiognallyNeg){
            transform.eulerAngles = new Vector3(0f,0f,120f);
        }
    }

    void Update()
    {
        if (bulletTime > 0) {
            bulletTime -= Time.deltaTime;
        }

        if (bulletTime <= 0f) {
            Destroy(gameObject);
        }
    }

}
