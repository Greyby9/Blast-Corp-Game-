using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{

    public float bulletCounter, bulletTime;
    bool noMove = false;


    void Start()
    {
        bulletCounter = bulletTime;

    }

    void Update()
    {
        if (bulletTime > 0)
        {
            bulletTime -= Time.deltaTime;
        }

        if (bulletTime <= 0f)
        {
            Destroy(gameObject);
        }
        if (GameData.instance.weaponIndex == 3)
        {
            verifyAngle();
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

