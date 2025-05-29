using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{

    public float bulletCounter, bulletTime;
    bool noMove = false;
    float margin = 0.05f;


    void Start()
    {
 
    }

    void Update()
    {
        bulletCounter += Time.deltaTime;        
        if (GameData.instance.weaponIndex == 1 | GameData.instance.weaponIndex == 2)// Destruye las balas de Pistol Y SMG
        {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); 
        if (viewPos.x < -margin || viewPos.x > 1 + margin || viewPos.y < -margin || viewPos.y > 1 + margin) 
        {
            Destroy(gameObject);
        }            
        }
        if (GameData.instance.weaponIndex == 3)
        {
        verifyAngle();
        if (bulletCounter >= bulletTime) //Destruye bala Shotgun
        {
        Destroy(gameObject);
        }
        }

    }

    void verifyAngle()//Arregla El shootPoint del Shotgun
    {
        if (Player.instance.lookingRight && Player.instance.anim.GetBool("Diagonally") == false) // idle
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Player.instance.lookingRight == false && Player.instance.anim.GetBool("Diagonally") == false && Player.instance.anim.GetBool("isLookingUp") == false && Player.instance.anim.GetBool("isDuck") == false) // idle neg
        {
            transform.eulerAngles = new Vector3(0f, 0f, 180f);
            transform.position = new Vector3(transform.position.x, -2.53f, transform.position.z);
        }
        if (Player.instance.anim.GetBool("isLookingUp") && Player.instance.lookingRight == false) // LookingUp Neg
        {
            if (noMove == false)
            {
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
                noMove = true;
            }
            transform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        else
       if (Player.instance.anim.GetBool("isLookingUp")) // LookingUp
        {
            transform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        if (Player.instance.anim.GetBool("Diagonally") && Player.instance.lookingRight) // Diagonally 
        {
            transform.eulerAngles = new Vector3(0f, 0f, 50f);
        }
        if (Player.instance.anim.GetBool("Diagonally") && Player.instance.lookingRight == false) // Diagonally Neg
        {
            transform.eulerAngles = new Vector3(0f, 0f, 130f);
            if (noMove == false)
            {
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y - 0.05f, transform.position.z);
                noMove = true;
            }

        }
        if (Player.instance.anim.GetBool("isDuck") && Player.instance.lookingRight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Player.instance.anim.GetBool("isDuck") && Player.instance.lookingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 180f);
            if (noMove == false)
            {
                transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y - 0.05f, transform.position.z);
                noMove = true;
            }
        }

    }
        



} 

