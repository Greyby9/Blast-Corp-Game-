using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Data;
public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject spawnerEnemy;
    public GameObject spawnerDrone;

    public GameObject playerSMG;
    public GameObject playerPistol;
    public GameObject playerShotgun;
    private  GameObject activePlayer;
    public int weaponIndex=1;
    public int bulletAmountSMG= 10;
    public int bulletAmountPistol= 10;
    public int bulletAmountShotgun=10;
    public TextMeshProUGUI textBulletSMG;
    public TextMeshProUGUI textBulletPistol;
    public TextMeshProUGUI textBulletShotGun;
    public bool hasSMG;
    public bool hasShotgun;

    // Sho0tingPoing  SMG
    public Transform shootPointVerticallySMG;
    public Transform shootPointDuckSMG;
    public Transform shootPointDiagonallySMG;
    public Transform shootPointDiagonallyLeftSMG;
    public Transform shootPointIdleSMG;


    // Sho0tingPoing  PISTOL
    public Transform shootPointVerticallyPistol;
    public Transform shootPointDuckPistol;
    public Transform shootPointDiagonallYPistol;
    public Transform shootPointDiagonallyLeftPistol;
    public Transform shootPointIdlePistol;

    //ShootingPoint Shotgun
    public Transform shootPointVerticallyShotgun;
    public Transform shootPointDuckShotgun;
    public Transform shootPointDiagonallYShotgun;
    public Transform shootPointDiagonallyLeftShotgun;
    public Transform shootPointIdleShotgun;


     private void Awake()
    {
            instance = this;
    }
    void Start()
    {
    activePlayer = playerPistol;
    playerSMG.SetActive(false);
    playerPistol.SetActive(true);
    playerShotgun.SetActive(false);
    }


    void Update()
    {
        changeWeapon();
        whoIsActive();
        viewPoint();
    }
    void changeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
        SwitchToPlayer(playerPistol);
        weaponIndex=1;
        }
        else 
        if (Input.GetKeyDown(KeyCode.Alpha2) && hasSMG==true)
        {
        SwitchToPlayer(playerSMG);
        weaponIndex=2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && hasShotgun==true){
        SwitchToPlayer(playerShotgun);
        weaponIndex=3;
        }
    }

    void SwitchToPlayer(GameObject newPlayer)
    {
    if (activePlayer != newPlayer)
    {
      
        newPlayer.transform.position = activePlayer.transform.position;
        newPlayer.transform.rotation = activePlayer.transform.rotation;

        // Activar/desactivar jugadores
        activePlayer.SetActive(false);
        newPlayer.SetActive(true);
        activePlayer = newPlayer;
    }  
    }
    void whoIsActive(){
        if (playerPistol.activeSelf){
            transform.position=playerPistol.transform.position;
        }
        if (playerSMG.activeSelf){
            transform.position=playerSMG.transform.position;
        }
        if(playerShotgun.activeSelf){
            transform.position=playerShotgun.transform.position;
        }
    }
        void viewPoint()
    {
            if (playerPistol.activeSelf)
        {
            Shoot.instance.shootPoint=shootPointIdlePistol;
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
        Shoot.instance.shootPoint=shootPointIdlePistol;
        }
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && Player.instance.enSuelo &&  !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) // Shooting Vertically
        {
        Player.instance.anim.SetBool("shootingVertically", true);
        Shoot.instance.shootPoint=shootPointVerticallyPistol;
        }
        else 
        {
        Player.instance.anim.SetBool("shootingVertically", false);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && Player.instance.enSuelo && !Input.GetKey(KeyCode.DownArrow)) //Shooting Left
        {
            if (Mathf.Approximately(transform.eulerAngles.y, 180f)) 
            {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            Player.instance.anim.SetBool("DiagonallyLeft", true);
            Shoot.instance.shootPoint=shootPointDiagonallyLeftPistol;            
            Player.instance.anim.SetBool("shootingVertically", false);
        }
        else 
        {
            if (Player.instance.movementX > 0)
            {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Shoot.instance.shootPoint=shootPointIdlePistol;
            } 
            else if (Player.instance.movementX < 0)
            {
            transform.eulerAngles = new Vector3(0f, 180f, 0f); 
            Shoot.instance.shootPoint=shootPointIdlePistol;
            }
            Player.instance.anim.SetBool("DiagonallyLeft", false);
            Player.instance.anim.SetBool("isDuck", true);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) && Player.instance.enSuelo && !Input.GetKey(KeyCode.DownArrow) )
        {
        Player.instance.anim.SetBool("DiagonallyRight", true);
        Shoot.instance.shootPoint=shootPointDiagonallYPistol;
        Player.instance.anim.SetBool("shootingVertically", false);
        Debug.Log("diagonaright");

        }else {
            if (Player.instance.movementX > 0)
            {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Shoot.instance.shootPoint=shootPointIdlePistol;
            } else if (Player.instance.movementX < 0)
            {
            transform.eulerAngles = new Vector3(0f, 180f, 0f); 
            Shoot.instance.shootPoint=shootPointIdlePistol;
            }
            Player.instance.anim.SetBool("DiagonallyRight", false);
        }

        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
        Player.instance.anim.SetBool("isDuck", true);
        Player.instance.anim.SetBool("isJump", false);
        Shoot.instance.shootPoint=shootPointDuckPistol;
        }
        else 
        {
        Player.instance.anim.SetBool("isDuck", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        Shoot.instance.shootPoint=shootPointIdlePistol;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        Shoot.instance.shootPoint=shootPointIdlePistol;
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) &&  Input.GetKey(KeyCode.LeftArrow))
        {
        Player.instance.anim.SetBool("shootingVertically", true);
        Shoot.instance.shootPoint=shootPointVerticallyPistol;
        Player.instance.anim.SetBool("DiagonallyRight", false);
        Player.instance.anim.SetBool("DiagonallyLeft", false);
        }
        if (Player.instance.movementX > 0 && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) )
        {
        Shoot.instance.shootPoint=shootPointDiagonallYPistol;  
        }
        if (Player.instance.movementX < 0 && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) )
        {
        Shoot.instance.shootPoint=shootPointDiagonallyLeftPistol;  
        } 
        }
            if (playerSMG.activeSelf)
        {
            Shoot.instance.shootPoint=shootPointIdleSMG;
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
        Shoot.instance.shootPoint=shootPointIdleSMG;
        }
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && Player.instance.enSuelo &&  !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) // Shooting Vertically
        {
        Player.instance.anim.SetBool("shootingVertically", true);
        Shoot.instance.shootPoint=shootPointVerticallySMG;
        }
        else 
        {
        Player.instance.anim.SetBool("shootingVertically", false);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && Player.instance.enSuelo && !Input.GetKey(KeyCode.DownArrow)) //Shooting Left
        {
            if (Mathf.Approximately(transform.eulerAngles.y, 180f)) 
            {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            Player.instance.anim.SetBool("DiagonallyLeft", true);
            Shoot.instance.shootPoint=shootPointDiagonallyLeftSMG;            
            Player.instance.anim.SetBool("shootingVertically", false);
        }
        else 
        {
            if (Player.instance.movementX > 0)
            {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Shoot.instance.shootPoint=shootPointIdleSMG;
            } 
            else if (Player.instance.movementX < 0)
            {
            transform.eulerAngles = new Vector3(0f, 180f, 0f); 
            Shoot.instance.shootPoint=shootPointIdleSMG;
            }
            Player.instance.anim.SetBool("DiagonallyLeft", false);
            Player.instance.anim.SetBool("isDuck", true);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) && Player.instance.enSuelo && !Input.GetKey(KeyCode.DownArrow) )
        {
        Player.instance.anim.SetBool("DiagonallyRight", true);
        Shoot.instance.shootPoint=shootPointDiagonallySMG;
        Player.instance.anim.SetBool("shootingVertically", false);
        Debug.Log("diagonaright");

        }else {
            if (Player.instance.movementX > 0)
            {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Shoot.instance.shootPoint=shootPointIdleSMG;
            } else if (Player.instance.movementX < 0)
            {
            transform.eulerAngles = new Vector3(0f, 180f, 0f); 
            Shoot.instance.shootPoint=shootPointIdleSMG;
            }
            Player.instance.anim.SetBool("DiagonallyRight", false);
        }

        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
        Player.instance.anim.SetBool("isDuck", true);
        Player.instance.anim.SetBool("isJump", false);
        Shoot.instance.shootPoint=shootPointDuckSMG;
        }
        else 
        {
        Player.instance.anim.SetBool("isDuck", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        Shoot.instance.shootPoint=shootPointIdleSMG;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        Shoot.instance.shootPoint=shootPointIdleSMG;
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) &&  Input.GetKey(KeyCode.LeftArrow))
        {
        Player.instance.anim.SetBool("shootingVertically", true);
        Shoot.instance.shootPoint=shootPointVerticallySMG;
        Player.instance.anim.SetBool("DiagonallyRight", false);
        Player.instance.anim.SetBool("DiagonallyLeft", false);
        }
        if (Player.instance.movementX > 0 && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) )
        {
        Shoot.instance.shootPoint=shootPointDiagonallySMG;  
        }
        if (Player.instance.movementX < 0 && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) )
        {
        Shoot.instance.shootPoint=shootPointDiagonallyLeftSMG;  
        } 
        }
            if (playerShotgun.activeSelf)
        {
            Shoot.instance.shootPoint=shootPointIdleShotgun;
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
        Shoot.instance.shootPoint=shootPointIdleShotgun;
        }
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && Player.instance.enSuelo &&  !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) // Shooting Vertically
        {
        Player.instance.anim.SetBool("shootingVertically", true);
        Shoot.instance.shootPoint=shootPointVerticallyShotgun;
        }
        else 
        {
        Player.instance.anim.SetBool("shootingVertically", false);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && Player.instance.enSuelo && !Input.GetKey(KeyCode.DownArrow)) //Shooting Left
        {
            if (Mathf.Approximately(transform.eulerAngles.y, 180f)) 
            {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            Player.instance.anim.SetBool("DiagonallyLeft", true);
            Shoot.instance.shootPoint=shootPointDiagonallyLeftShotgun;            
            Player.instance.anim.SetBool("shootingVertically", false);
        }
        else 
        {
            if (Player.instance.movementX > 0)
            {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Shoot.instance.shootPoint=shootPointIdleShotgun;
            } 
            else if (Player.instance.movementX < 0)
            {
            transform.eulerAngles = new Vector3(0f, 180f, 0f); 
            Shoot.instance.shootPoint=shootPointIdleShotgun;
            }
            Player.instance.anim.SetBool("DiagonallyLeft", false);
            Player.instance.anim.SetBool("isDuck", true);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) && Player.instance.enSuelo && !Input.GetKey(KeyCode.DownArrow) )
        {
        Player.instance.anim.SetBool("DiagonallyRight", true);
        Shoot.instance.shootPoint=shootPointDiagonallYShotgun;
        Player.instance.anim.SetBool("shootingVertically", false);
        Debug.Log("diagonaright");

        }else {
            if (Player.instance.movementX > 0)
            {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Shoot.instance.shootPoint=shootPointIdleShotgun;
            } else if (Player.instance.movementX < 0)
            {
            transform.eulerAngles = new Vector3(0f, 180f, 0f); 
            Shoot.instance.shootPoint=shootPointIdleShotgun;
            }
            Player.instance.anim.SetBool("DiagonallyRight", false);
        }

        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
        Player.instance.anim.SetBool("isDuck", true);
        Player.instance.anim.SetBool("isJump", false);
        Shoot.instance.shootPoint=shootPointDuckShotgun;
        }
        else 
        {
        Player.instance.anim.SetBool("isDuck", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        Shoot.instance.shootPoint=shootPointIdleShotgun;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        Shoot.instance.shootPoint=shootPointIdleShotgun;
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) &&  Input.GetKey(KeyCode.LeftArrow))
        {
        Player.instance.anim.SetBool("shootingVertically", true);
        Shoot.instance.shootPoint=shootPointVerticallyShotgun;
        Player.instance.anim.SetBool("DiagonallyRight", false);
        Player.instance.anim.SetBool("DiagonallyLeft", false);
        }
        if (Player.instance.movementX > 0 && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) )
        {
        Shoot.instance.shootPoint=shootPointDiagonallYShotgun;  
        }
        if (Player.instance.movementX < 0 && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) )
        {
        Shoot.instance.shootPoint=shootPointDiagonallyLeftShotgun;  
        }
        }
               

    }
      
  }