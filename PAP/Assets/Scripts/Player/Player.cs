using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections;



public class Player : MonoBehaviour
{
    public float speedMoviment;
    public float initialSpeedMoviment;
    public Transform respawnPoint;

    public float movementX;
    public bool isDuck;

    public bool lookingRight, isCollidingWithBarrier;
    
    public float jumpForce = 15f;

    public static Player instance;

    public float lengthRayCast = 0.1f;

    public LayerMask capaSuelo;
    public bool enSuelo;

    private Rigidbody2D miCuerpoRigido;
    public Animator anim;
    public float timeDie;

  //  public CanvasController canvasController;

    public bool playerStatic;
    private BoxCollider2D boxCollider;

    private Vector2 originalSizeCollider;
    private Vector2 originalOffsetCollider;

    //>>>>>>>>>>>>>>>>>>Sound
    public AudioClip soundJump;
    public AudioClip soundWalk;


    // Sho0tingPoing  SMG
    public Transform shootPointLookingUpSMG;
    public Transform shootPointDuckSMG;
    public Transform shootPointDiagonallySMG;
    public Transform shootPointIdleSMG;


// Sho0tingPoing  PISTOL
    public Transform shootPointLookingUpPistol;
    public Transform shootPointDuckPistol;
    public Transform shootPointDiagonallyPistol;
    public Transform shootPointIdlePistol;

//ShootingPoint Shotgun
    public Transform shootPointLookingUpShotgun;
    public Transform shootPointDuckShotgun;
    public Transform shootPointDiagonallyShotgun;
    public Transform shootPointIdleShotgun;
    public Transform shootPoint;

    void viewPoint()
    {
        if (GameData.instance.weaponIndex==1)
        {
        shootPoint=shootPointIdlePistol;
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow)) //Bug
        {
        shootPoint=shootPointIdlePistol;
        }
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && enSuelo &&  !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) // Shooting Vertically
        {
        anim.SetBool("isLookingUp", true);
        shootPoint=shootPointLookingUpPistol;
        }
        else 
        {
        anim.SetBool("isLookingUp", false);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && enSuelo && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow)) //Shooting Left
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallyPistol;            
        anim.SetBool("isLookingUp", false);
        }
        else 
        {
        if (movementX > 0)
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        shootPoint=shootPointIdlePistol;
        } 
        else if (movementX < 0)
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        shootPoint=shootPointIdlePistol;
        }
        anim.SetBool("Diagonally", false);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) && enSuelo && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow)) // Shooting Right
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallyPistol;
        anim.SetBool("isLookingUp", false);
        }else {
        if (movementX > 0)
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        shootPoint=shootPointIdlePistol;
        } else if (movementX < 0)
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        shootPoint=shootPointIdlePistol;
        }
        }
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)) //isDuck
        {
        shootPoint=shootPointDuckPistol;
        anim.SetBool("isDuck", true);
        }
        else 
        {
        anim.SetBool("isDuck", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        shootPoint=shootPointIdlePistol;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        shootPoint=shootPointIdlePistol;
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) &&  Input.GetKey(KeyCode.LeftArrow)) // bug
        {
        anim.SetBool("isLookingUp", true);
        shootPoint=shootPointLookingUpPistol;
        anim.SetBool("Diagonally", false);
        }
        if (movementX > 0 && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) )
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallyPistol;  
        }
        if (movementX < 0 && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) )
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallyPistol;  
        } 
        }
        if (GameData.instance.weaponIndex==2)
        {
        shootPoint=shootPointIdleSMG;
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow)) //Bug
        {
        shootPoint=shootPointIdleSMG;
        }
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && enSuelo &&  !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) // Shooting Vertically
        {
        anim.SetBool("isLookingUp", true);
        shootPoint=shootPointLookingUpSMG;
        }
        else 
        {
        anim.SetBool("isLookingUp", false);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && enSuelo && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow)) //Shooting Left
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallySMG;            
        anim.SetBool("isLookingUp", false);
        }
        else 
        {
        if (movementX > 0)
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        shootPoint=shootPointIdleSMG;
        } 
        else if (movementX < 0)
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        shootPoint=shootPointIdleSMG;
        }
        anim.SetBool("Diagonally", false);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) && enSuelo && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallySMG;
        anim.SetBool("isLookingUp", false);
        }else {
        if (movementX > 0)
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        shootPoint=shootPointIdleSMG;
        } else if (movementX < 0)
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        shootPoint=shootPointIdleSMG;
        }
        }
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)) //isDuck
        {
        shootPoint=shootPointDuckSMG;
        anim.SetBool("isDuck", true);
        }
        else 
        {
        anim.SetBool("isDuck", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        shootPoint=shootPointIdleSMG;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        shootPoint=shootPointIdleSMG;
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) &&  Input.GetKey(KeyCode.LeftArrow)) // bug
        {
        anim.SetBool("isLookingUp", true);
        shootPoint=shootPointLookingUpSMG;
        anim.SetBool("Diagonally", false);
        }
        if (movementX > 0 && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) )
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallySMG;  
        }
        if (movementX < 0 && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow)  )
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallySMG;  
        } 
        }
        if (GameData.instance.weaponIndex==3)
        {
        shootPoint=shootPointIdleShotgun;
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow)) //Bug
        {
        shootPoint=shootPointIdleShotgun;
        }
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && enSuelo &&  !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) // Shooting Vertically
        {
        anim.SetBool("isLookingUp", true);
        shootPoint=shootPointLookingUpShotgun;
        }
        else 
        {
        anim.SetBool("isLookingUp", false);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && enSuelo && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow)) //Shooting Left
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallyShotgun;            
        anim.SetBool("isLookingUp", false);
        }
        else 
        {
        if (movementX > 0)
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        shootPoint=shootPointIdleShotgun;
        } 
        else if (movementX < 0)
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        shootPoint=shootPointIdleShotgun;
        }
        anim.SetBool("Diagonally", false);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) && enSuelo && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow)) // Shooting Right
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallyShotgun;
        anim.SetBool("isLookingUp", false);
        }else {
        if (movementX > 0)
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        shootPoint=shootPointIdleShotgun;
        } else if (movementX < 0)
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        shootPoint=shootPointIdleShotgun;
        }
        }
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)) //isDuck
        {
        shootPoint=shootPointDuckShotgun;
        anim.SetBool("isDuck", true);
        }
        else 
        {
        anim.SetBool("isDuck", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        shootPoint=shootPointIdleShotgun;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        shootPoint=shootPointIdleShotgun;
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) &&  Input.GetKey(KeyCode.LeftArrow)) // bug
        {
        anim.SetBool("isLookingUp", true);
        shootPoint=shootPointLookingUpShotgun;
        anim.SetBool("Diagonally", false);
        }
        if (movementX > 0 && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) )
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallyShotgun;  
        }
        if (movementX < 0 && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) )
        {
        anim.SetBool("Diagonally", true);
        shootPoint=shootPointDiagonallyShotgun;  
        } 
        }

}
 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // persiste GameController
        }
        else
        {
            Destroy(gameObject); // evita duplicados
        }

        initialSpeedMoviment=speedMoviment;
    }
    void Start()
    {
        miCuerpoRigido = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalSizeCollider = boxCollider.size;
        originalOffsetCollider = boxCollider.offset; 
        anim.SetBool("onGround", true);
        GameData.instance.hasPistol=true;
        GameData.instance.weaponIndex=1;

        if (!PlayerPrefs.HasKey("lives"))
        {
        PlayerPrefs.SetInt("lives", GameData.instance.playerHP);
        }
        
    }


    // Update is called once per frame
    void Update()
    {

      //  canvasController.UpdateUp(PlayerPrefs.GetInt("lives")); // Vidas

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, lengthRayCast, capaSuelo);
        enSuelo = hit.collider != null;
        if (enSuelo)
        {
            anim.SetBool("onGround", true);
        }
        else
        {
            anim.SetBool("onGround", false);
        }
        verifySprite();
        duck();
        jump();
        move();
        staticPlayer();
        viewPoint();
        changeWeapon();

    }
        void staticPlayer()
        {
        if (Input.GetKey(KeyCode.E))
        {
        playerStatic=true;
        speedMoviment=0;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
            lookingRight=false; 
            transform.eulerAngles = new Vector3(0f,180f,0f);
            } 
            if (Input.GetKey(KeyCode.RightArrow))
            {
            lookingRight=true;
            transform.eulerAngles = new Vector3(0f,0f,0f);
            }
        } 
        else 
        {
        speedMoviment=initialSpeedMoviment;
        playerStatic=false;
        }





    }   
    void soundmove()
    {
        soundController.Instace.exeSound(soundWalk);
    }
    void move()
    {
        movementX = 0f;
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.E))
        {
            movementX = 1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.E))
        {
            movementX = -1f;
        }

        Vector2 posicionJug = transform.position;

        if (movementX > 0 && !isDuck)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            lookingRight = true;
            anim.SetBool("move", true);
        }
        else if (movementX < 0 && !isDuck)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            lookingRight = false;
            anim.SetBool("move", true);
        }
        else
        {
            anim.SetBool("move", false);
        }

        if (anim.GetBool("move"))
        {
            posicionJug = posicionJug + new Vector2(movementX, 0f) * speedMoviment * Time.deltaTime;
        }
        else
        {
            anim.SetBool("move", false);
        }


        transform.position = posicionJug;

    }
public void changeWeapon()
{
        if (Input.GetKeyDown(KeyCode.Alpha1) && GameData.instance.hasPistol==true)
        {
            GameData.instance.weaponIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && GameData.instance.hasSMG == true)
        {
            GameData.instance.weaponIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && GameData.instance.hasShotgun == true)
        {
            GameData.instance.weaponIndex = 3;
        }
}


    void duck(){
        if(Input.GetKey(KeyCode.DownArrow) && enSuelo && !Input.GetKey(KeyCode.UpArrow))
        { 
            speedMoviment=0f;
            isDuck=true;
            boxCollider.size = new Vector2(originalSizeCollider.x, originalSizeCollider.y * 0.5f); // Reduce la altura a la mitad
            boxCollider.offset = new Vector2(originalOffsetCollider.x, originalOffsetCollider.y - (originalSizeCollider.y * 0.25f)); // Baja el Collider
            
            if (Input.GetKey(KeyCode.LeftArrow)){
                lookingRight=false;
                transform.eulerAngles = new Vector3(0f,180f,0f);
            } 
            if (Input.GetKey(KeyCode.RightArrow)){
                lookingRight=true;
                transform.eulerAngles = new Vector3(0f,0f,0f);
            }
             else {
                speedMoviment=initialSpeedMoviment;
            boxCollider.size = originalSizeCollider; // Restaura el tamaño original
            boxCollider.offset = originalOffsetCollider; // Restaura el offset original

        }
    
    }else
    {
            isDuck=false;
            anim.SetBool("isDuck", false);

    }
    }
    void jump()
    {
            if(enSuelo==false)
            {
                anim.SetBool("isJump",true);
            }
            if (enSuelo==true){
                anim.SetBool("isJump", false);
            }         
            if (anim.GetBool("onGround") && Input.GetKeyDown(KeyCode.Space) && !isDuck)
            {
                miCuerpoRigido.linearVelocity = new Vector2(miCuerpoRigido.linearVelocity.x, jumpForce);
                soundController.Instace.exeSound(soundJump);

            }
       
    }
    IEnumerator LoseHPCoroutine()
    {
    anim.SetTrigger("isHit"); //anim 
    
    GameData.instance.playerHP--;

        if (GameData.instance.playerHP <= 0)
        {
            PlayerPrefs.DeleteKey("lives");

            yield return new WaitForSeconds(timeDie); // espera a que termine la animación
            GameController.instance.gameOver(); // Menu Morte
            Time.timeScale= 0f;
            
    }
        else if (GameData.instance.playerHP > 0)
        {
            PlayerPrefs.SetInt("lives", GameData.instance.playerHP);

            //yield return new WaitForSeconds(timeDie); // anim morte

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga La nueva escena
        }
    }
    public void loseHP()
    {
        StartCoroutine(LoseHPCoroutine());
    }
    IEnumerator Respawn()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(timeDie);
        transform.position = respawnPoint.position;
        GetComponent<SpriteRenderer>().enabled = true;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * lengthRayCast);
    }

    void verifySprite()
    {
        if (GameData.instance.weaponIndex==1)
        {
            anim.SetBool("inPistol", true);
        } else {
            anim.SetBool("inPistol", false);
        }
        if (GameData.instance.weaponIndex==2)
        {
            anim.SetBool("inSMG", true);
        } else {
            anim.SetBool("inSMG", false);
        }
        if (GameData.instance.weaponIndex==3)
        {
            anim.SetBool("inShotgun", true);
        } else {
            anim.SetBool("inShotgun", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 6) {
            movementX = 0;
        }

        if (collision.gameObject.tag == "barrier") {
            isCollidingWithBarrier = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "barrier") {
            isCollidingWithBarrier = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 6) {
            movementX = 0;
        }
    }
        void OnTriggerEnter2D(Collider2D collider)
    {
       if (collider.gameObject.tag == "Bullet_enemy1")
        {
            Destroy(collider.gameObject);
            loseHP();
        }
         if (collider.gameObject.tag == "BulletRobot")
        {
            Destroy(collider.gameObject);
            loseHP();
        }
        if (collider.gameObject.tag == "BulletDrone")
        {
            Destroy(collider.gameObject);
            loseHP();
        }
                  
        if (collider.gameObject.CompareTag("BoxBulletSMG"))
        {
            GameData.instance.bulletAmountSMG = GameData.instance.bulletAmountSMG + 10;
            Shoot.instance.updateText();
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.CompareTag("SMG")){
        Destroy(collider.gameObject);
        GameData.instance.hasSMG=true;
        GameController.instance.spawnerEnemy.SetActive(false);
        GameController.instance.spawnerDrone.SetActive(false);  
        CanvasController.instance.changePistolBetweenSMGAds.SetActive(true);

        }
        if (collider.gameObject.CompareTag("BoxBulletShotgun")) {
        GameData.instance.bulletAmountShotgun=GameData.instance.bulletAmountShotgun+10;
        Destroy(collider.gameObject); 
            }
        if (collider.gameObject.CompareTag("BoxBulletPistols")) {
        GameData.instance.bulletAmountPistol=GameData.instance.bulletAmountPistol+10;
        Shoot.instance.updateText();
        Destroy(collider.gameObject); 
            }
    
    }   
    }



