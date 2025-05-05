using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class Shoot : MonoBehaviour
{
    public GameObject bullet1, bullet;

    public bool shootingVertically, shootingDiognally, shootingDiognallyNeg;
    public float bulletSpeed;

    public static Shoot instance;

    public Transform shootPoint;    
    private float shoot_time;
    private bool fistShoot;

    public bool shooting;
    public CanvasController canvasController;
    public float intervalo;  

    private Animator anim;

    // Sound
    public AudioClip soundShot;



    void Awake()
    {
        instance=this;

    }
    void Start()
    {
    shootPoint = GameController.instance.shootPointIdlePistol;        
     anim = GetComponent<Animator>();
     updateText(); 
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.S)){

            if (!fistShoot) 
            {
                shoot(); 
                fistShoot = true;
            } else if(!shooting){
                fistShoot = false;
            }
            shooting=true;
        } else {
            shooting=false;
        }
     
        if(shooting){  
        shoot_time +=Time.deltaTime;
        
            if (shoot_time >= intervalo){
                shoot();
                shoot_time=0f;
            }
            anim.SetLayerWeight(0,0);
            anim.SetLayerWeight(1,1);
            
        } else {
            anim.SetLayerWeight(0,1);
            anim.SetLayerWeight(1,0);


            
            
        }


    }
    void shoot()
    {
            if (GameController.instance.weaponIndex == 1)
            {
              
            if(Input.GetKey(KeyCode.S) && GameController.instance.bulletAmountPistol > 0)
            {
                if(Player.instance.enSuelo && anim.GetBool("DiagonallyRight"))
                {
                ShootBullet(45);
                }else   if (Player.instance.enSuelo && anim.GetBool("DiagonallyLeft"))
                {
                //shootPointDiagonally.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                ShootBullet(135);
                } else if (anim.GetBool("shootingVertically"))
                {
                ShootBullet(90);
                } else if (Player.instance.movementX > 0 && anim.GetBool("move") && Player.instance.lookingRight)
                {
                ShootBullet(0);
                }else if (Player.instance.movementX < 0 && anim.GetBool("move") && !Player.instance.lookingRight) {
                ShootBullet(180);
                } else 
                {
                if (Player.instance.lookingRight) 
                {
                    ShootBullet(0);
                
                }else if(!Player.instance.lookingRight)
                {
                    ShootBullet(180);
                
                
                }  
                } 
            GameController.instance.bulletAmountPistol--;
            updateText();
            }
        }
            if (GameController.instance.weaponIndex == 2)
            {
            Debug.Log("Aqui entra");
            if(Input.GetKey(KeyCode.S) && GameController.instance.bulletAmountSMG > 0)
            {
            if(Player.instance.enSuelo && anim.GetBool("DiagonallyRight"))
            {
                ShootBullet(45);
            }else if (Player.instance.enSuelo && anim.GetBool("DiagonallyLeft"))
            {
                //shootPointDiagonally.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                ShootBullet(135);

            } else if (anim.GetBool("shootingVertically"))
            {
                ShootBullet(90);
            } else if (Player.instance.movementX > 0 && anim.GetBool("move") && Player.instance.lookingRight)
            {
                ShootBullet(0);
            } else if (Player.instance.movementX < 0 && anim.GetBool("move") && !Player.instance.lookingRight) 
            {
                ShootBullet(180);
            } else 
            {
                if (Player.instance.lookingRight) 
                {
                    ShootBullet(0);
                } else if (!Player.instance.lookingRight)
                {
                    ShootBullet(180);
                }
            }
            GameController.instance.bulletAmountSMG--;
            updateText();

            }
              
            }
}
private void ShootBullet(float angle){   
      
    bullet = Instantiate(bullet1, shootPoint.position, Quaternion.identity);

    bullet.transform.SetParent(null);

    // Convierte el ángulo en una dirección (usando seno y coseno)
    float radianes = angle * Mathf.Deg2Rad;
    Vector2 direction = new Vector2(Mathf.Cos(radianes), Mathf.Sin(radianes));


    bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;

    soundController.Instace.exeSound(soundShot);
    Debug.Log($"Disparando en ángulo: {angle} grados");
}
   public void updateText()
    {
            GameController.instance.textBulletSMG.text = "x" + GameController.instance.bulletAmountSMG.ToString();
            GameController.instance.textBulletPistol.text = "x" + GameController.instance.bulletAmountPistol.ToString();

        
    }







   /* void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("BoxDanger")) {
            Debug.Log("CAJA DE BALAS");
            Destroy(collider.gameObject);
        }
    }*/
}
