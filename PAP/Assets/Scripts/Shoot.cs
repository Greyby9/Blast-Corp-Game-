using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class Shoot : MonoBehaviour
{
    public GameObject bulletStandard, bullet,bulletShotgun;

    public bool shootingVertically, shootingDiognally, shootingDiognallyNeg;
    public float bulletSpeed;

    public static Shoot instance;
   
    private float shoot_time;
    private bool fistShoot;

    public bool shooting;
    public CanvasController canvasController;
    public float interval;  

    private Animator anim;

    // Sound
    public AudioClip soundShot;



    void Awake()
    {
        instance=this;

    }
    void Start()
    {       
     anim = GetComponent<Animator>();
     updateText(); 
    }
    void Update()
    {
        intervalDeterminer();
        if (Input.GetKey(KeyCode.S))
        {

            if (!fistShoot)
            {
                shoot();
                fistShoot = true;
            }
            else
            if (!shooting)
            {
                fistShoot = false;
            }
            shooting = true;
        }
        else
        {
            shooting = false;
        }
     
        if(shooting)
        {  
        shoot_time +=Time.deltaTime;
        
            if (shoot_time >= interval)
            {
                shoot();
                shoot_time=0f;
            }
            anim.SetLayerWeight(0,0);
            anim.SetLayerWeight(1,1);
            
        } else
        {
            anim.SetLayerWeight(0,1);
            anim.SetLayerWeight(1,0);

        }


    }
    void shoot()
    {

        if (Input.GetKey(KeyCode.S) && Player.instance.bulletAmountPistol > 0)
        {
            if (Player.instance.enSuelo && anim.GetBool("Diagonally") && Player.instance.lookingRight == true)
            {
                ShootBullet(45);
            }
            else
            if (Player.instance.enSuelo && anim.GetBool("Diagonally") && Player.instance.lookingRight == false)
            {
                ShootBullet(135);
            }
            else
            if (anim.GetBool("isLookingUp"))
            {
                ShootBullet(90);
            }
            else
            if (Player.instance.movementX > 0 && anim.GetBool("move") && Player.instance.lookingRight)
            {
                ShootBullet(0);
            }
            else if (Player.instance.movementX < 0 && anim.GetBool("move") && !Player.instance.lookingRight)
            {
                ShootBullet(180);
            }
            else
            {
                if (Player.instance.lookingRight)
                {
                    ShootBullet(0);

                }
                else if (!Player.instance.lookingRight)
                {
                    ShootBullet(180);


                }
            }
            verifyWhoShoot();
        }
    }
    void verifyWhoShoot()
    {
            if (whoIs() == 1)
            {
                Player.instance.bulletAmountPistol--;
            }
            if (whoIs() == 2)
            {
                Player.instance.bulletAmountSMG--;
            }
            if (whoIs() == 3)
            {
                Player.instance.bulletAmountShotgun--;
            }
            updateText();
    }
    void intervalDeterminer()
    {
            if (whoIs() == 1)
            {
                interval=5;
            }
            if (whoIs() == 2)
            {
                interval = 2;
            }
            if (whoIs() == 3)
            {
                interval=7;
            }  
    }
        
    int whoIs()
    {
            if (Player.instance.weaponIndex == 1)
            {
                return 1;
            }
            if (Player.instance.weaponIndex == 2)
            {
                return 2;
            }
            if (Player.instance.weaponIndex == 3)
            {
                return 3;
            }
            return 1;     

    }
       

    
private void ShootBullet(float angle){

        if (whoIs() == 1 | whoIs() == 2)
        {
            bullet = Instantiate(bulletStandard, Player.instance.shootPoint.position, Quaternion.identity);
            bullet.transform.SetParent(null, true);
    
            float radianes = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radianes), Mathf.Sin(radianes));


            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;

            soundController.Instace.exeSound(soundShot);
        }
        if (whoIs() == 3)
        {
            bullet = Instantiate(bulletShotgun, Player.instance.shootPoint.position, Quaternion.identity);            
        }


}
    public void updateText()
    {
        if (Player.instance.hasPistol)
        {
            GameController.instance.textBulletPistol.text = "x" + Player.instance.bulletAmountPistol.ToString();
        }
        if (Player.instance.hasSMG)
        {
             GameController.instance.textBulletSMG.text = "x" + Player.instance.bulletAmountSMG.ToString();           
        }
        if (Player.instance.hasShotgun)
        {
             GameController.instance.textBulletShotGun.text = "x" + Player.instance.bulletAmountShotgun.ToString();           
        }
            

        
    }

}
