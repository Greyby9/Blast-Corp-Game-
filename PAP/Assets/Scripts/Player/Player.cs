using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections;



public class Player : MonoBehaviour
{
    public int playerHP;
    public float speedMoviment;
    public float initialSpeedMoviment;

    public float movementX;

    
    public bool lookingRight, isCollidingWithBarrier;
    
    public float jumpForce = 15f;

    public static Player instance;

    public float lengthRayCast = 0.1f;

    public LayerMask capaSuelo;
    public bool enSuelo;

    private Rigidbody2D miCuerpoRigido;
    public Animator anim;
    public float timeDie;

    public CanvasController canvasController;
    public int lives;
    public SpriteRenderer characterSpriteRenderer;
    public Sprite[] weaponSprites;

    public bool playerStatic;
    private BoxCollider2D boxCollider;

    private Vector2 originalSizeCollider;
    private Vector2 originalOffsetCollider;

    //>>>>>>>>>>>>>>>>>>Sound
    public AudioClip soundJump;
    public AudioClip soundWalk;
    public bool statusDuck;

    void Awake()
    {   
        instance = this;
        initialSpeedMoviment=speedMoviment;
    }
    void Start(){
        miCuerpoRigido = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalSizeCollider = boxCollider.size;
        originalOffsetCollider = boxCollider.offset; 
        lookingRight= true;
        anim.SetBool("onground", true);


        if (!PlayerPrefs.HasKey("lives"))
        {
        PlayerPrefs.SetInt("lives", playerHP);
        }
        
    }

    // Update is called once per frame
    void Update()
    { 
        staticPlayer();      
        canvasController.UpdateUp(PlayerPrefs.GetInt("lives"));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, lengthRayCast, capaSuelo);
        enSuelo = hit.collider != null;
        if (enSuelo)
        {
        anim.SetBool("onground", true);
        } 
        else 
        {
            anim.SetBool("onground", false);
        }

        duck();
        jump();
        move();  

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
            Debug.Log(lookingRight);
            }
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                
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
            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.E)){
                movementX= 1f;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.E)){
                movementX= -1f;
            }

        Vector2 posicionJug = transform.position;

        if (movementX > 0 && !anim.GetBool("isDuck")) 
        {
            transform.eulerAngles = new Vector3(0f,0f,0f);
            lookingRight = true;
            anim.SetBool("move", true);
        } else if (movementX < 0 && !anim.GetBool("isDuck")) 
        {
            transform.eulerAngles = new Vector3(0f,180f,0f);
            lookingRight = false;
            anim.SetBool("move", true);
        }
        else 
        { 
            anim.SetBool("move", false);
        }

        if (anim.GetBool("move"))
        {
        posicionJug = posicionJug + new Vector2 (movementX, 0f) * speedMoviment * Time.deltaTime;            
        } 
        else 
        {
            anim.SetBool("move", false);
        }


        transform.position = posicionJug;
     
}


    void duck(){
        if(Input.GetKey(KeyCode.DownArrow) && enSuelo && !Input.GetKey(KeyCode.UpArrow))
        {
            speedMoviment=0f;
            anim.SetBool("move", false);
            anim.SetBool("isDuck", true);
            statusDuck=true;
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
            } else {
                speedMoviment=initialSpeedMoviment;
                anim.SetBool("isDuck", false);
                statusDuck=false;
            boxCollider.size = originalSizeCollider; // Restaura el tamaño original
            boxCollider.offset = originalOffsetCollider; // Restaura el offset original
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
        if (anim.GetBool("onground") && Input.GetKeyDown(KeyCode.Space) && anim.GetBool("isDuck")==false)
        {
            miCuerpoRigido.linearVelocity = new Vector2(miCuerpoRigido.linearVelocity.x, jumpForce);
            soundController.Instace.exeSound(soundJump);

        }
       
    }
 /*   IEnumerator waitAndExe(){
        yield return new WaitForSeconds(2f);
        anim.SetBool("onground", true);
        anim.SetBool("isJump", false);
    }
    IEnumerator endJump(){
        yield return new WaitForSeconds(2f);    
        anim.SetBool("isJump", false);
    }*/
 /*       public void loseHP(){
        anim.SetTrigger("isHit");
        lives = PlayerPrefs.GetInt("lives");
        lives--;
        if (lives <= 0){
            PlayerPrefs.DeleteKey("lives");
            UIController.Instance.gameOver();
        }else if (lives > 0){
            PlayerPrefs.SetInt("lives", lives);
            SceneManager.LoadScene(1);
            
        }
    }*/

IEnumerator LoseHPCoroutine()
{
    anim.SetTrigger("isHit");
    
    lives = PlayerPrefs.GetInt("lives");
    lives--;

    if (lives <= 0)
    {
        PlayerPrefs.DeleteKey("lives");

        yield return new WaitForSeconds(timeDie); // espera a que termine la animación
        GameController.instance.gameOver();
    }
    else if (lives > 0)
    {
        PlayerPrefs.SetInt("lives", lives);

        yield return new WaitForSeconds(timeDie); // también puedes esperar aquí si quieres que se vea la animación antes de recargar

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
    public void loseHP()
{
    StartCoroutine(LoseHPCoroutine());
}
    void OnDrawGizmos(){
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * lengthRayCast);
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
       if (collider.gameObject.tag == "Bullet_enemy1") {
            Destroy(collider.gameObject);
            loseHP();
            }
         if (collider.gameObject.tag == "BulletRobot") {
            Destroy(collider.gameObject);
            loseHP();
            }           
        if (collider.gameObject.CompareTag("BoxBulletSMG")) 
        {
        GameData.instance.bulletAmountSMG=GameData.instance.bulletAmountSMG+10;
        Shoot.instance.updateText();
        Destroy(collider.gameObject); 
        }
        if (collider.gameObject.CompareTag("SMG")){
        Destroy(collider.gameObject);
        GameData.instance.hasSMG=true;
        GameController.instance.spawnerEnemy.SetActive(false);
        GameController.instance.spawnerDrone.SetActive(false);  
        CanvasController.instace.changePistolBetweenSMGAds.SetActive(true);

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



