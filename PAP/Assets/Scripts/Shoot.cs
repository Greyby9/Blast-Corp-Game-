using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public GameObject bulletStandard, bullet, bulletShotgun;

    public bool shootingVertically, shootingDiognally, shootingDiognallyNeg;
    public float bulletSpeed;

    public static Shoot instance;

    private float shoot_time;
    private bool fistShoot;

    public bool shooting;
    private float interval;

    private Animator anim;

    // Sound
    public AudioClip soundShot;

    public float intervalPistol;
    public float intervalSMG;
    public float intervalShotgun;
    public Image circle;



    void Awake()
    {
        instance = this;

    }
    void Start()
    {
        anim = GetComponent<Animator>();
        updateText();
    }
    void Update()
    {
        intervalDeterminer();


        /*if (GameData.instance.weaponIndex == 3)
        {
            if (shooting)
            {
                shoot_time += Time.deltaTime;
                circle.fillAmount = shoot_time / intervalShotgun;

                if (shoot_time >= intervalShotgun)
                {
                    circle.fillAmount = 1f;
                }
            }
            else
            {
                circle.fillAmount = 1f; 
            }
        }
        else
        {

            circle.fillAmount = 0f;
        }*/ // La rueda que quiero poner estilo minecaft para la escopeta
    
        if (Input.GetKey(KeyCode.S))
        {
            shooting = true;
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

        }
        else
        {
            shooting = false;
        }

        if (shooting)
        {
            shoot_time += Time.deltaTime;

            if (shoot_time >= interval)
            {

                anim.SetLayerWeight(0, 0);
                anim.SetLayerWeight(1, 1);
                StartCoroutine(ESPERARTIMEPO());
                anim.SetLayerWeight(0, 1);
                anim.SetLayerWeight(1, 0);                
                shoot();
                shoot_time = 0f;
            }
            else
            {

                anim.SetLayerWeight(0, 1);
                anim.SetLayerWeight(1, 0);
            }
        }
        else
        {

            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);

        }


    }
    void shoot()
    {

        if (Input.GetKey(KeyCode.S) && amountBullets() > 0 && Player.instance.anim.GetBool("isJump")==false)
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
    void verifyWhoShoot() //Calcula Quantas BALAS tem
    {
        if (whoIs() == 1)
        {
            GameData.instance.bulletAmountPistol--;
        }
        if (whoIs() == 2)
        {
            GameData.instance.bulletAmountSMG--;
        }
        if (whoIs() == 3)
        {
            GameData.instance.bulletAmountShotgun--;
        }
        updateText();
    }
    int amountBullets() //Retorna Quantidade de Balas que tem
    {
        if (whoIs() == 1)
        {
            return GameData.instance.bulletAmountPistol;
        }
        if (whoIs() == 2)
        {
            return GameData.instance.bulletAmountSMG;
        }
        if (whoIs() == 3)
        {
            return GameData.instance.bulletAmountShotgun;
        }
        return 0;
    }
    void intervalDeterminer() // Tempo entre balas por armas
    {
        if (whoIs() == 1)
        {
            interval = intervalPistol;
        }
        if (whoIs() == 2)
        {
            interval = intervalSMG;
        }
        if (whoIs() == 3)
        {
            interval = intervalShotgun;
        }
    }

    int whoIs()
    {
        if (GameData.instance.weaponIndex == 1)
        {
            return 1;
        }
        if (GameData.instance.weaponIndex == 2)
        {
            return 2;
        }
        if (GameData.instance.weaponIndex == 3)
        {
            return 3;
        }
        return 1;

    }



    private void ShootBullet(float angle)
    {

        if (whoIs() == 1 | whoIs() == 2)
        {
            bullet = Instantiate(bulletStandard, Player.instance.shootPoint.position, Quaternion.identity);
            bullet.transform.SetParent(null);

            float radianes = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radianes), Mathf.Sin(radianes));


            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;

            soundController.Instace.exeSound(soundShot);
        }
        if (whoIs() == 3)
        {
            bullet = Instantiate(bulletShotgun, Player.instance.shootPoint.position, Quaternion.identity);
            bullet.transform.SetParent(Player.instance.transform);
        }


    }
    public void updateText()
    {
        if (GameData.instance.hasPistol)
        {
            CanvasController.instance.textBulletPistol.text = "x" + GameData.instance.bulletAmountPistol.ToString();
        }
        if (GameData.instance.hasSMG)
        {
            CanvasController.instance.textBulletSMG.text = "x" + GameData.instance.bulletAmountSMG.ToString();
        }
        if (GameData.instance.hasShotgun)
        {
            CanvasController.instance.textBulletShotGun.text = "x" + GameData.instance.bulletAmountShotgun.ToString();
        }



    }
    IEnumerator ESPERARTIMEPO()
    {
        yield return new WaitForSeconds(0.15f);
    }


}
