using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using Unity.VisualScripting;


public class GameData : MonoBehaviour
{
    public static GameData instance;

    //Player
    public int playerHP;
    public Animator anim;

    //Pistols
    public int weaponIndex = 1;
    public bool hasPistol;
    public bool hasSMG=false;
    public bool hasShotgun=false;
    // Bullets Pistols
    public int bulletAmountSMG= 10;
    public int bulletAmountPistol= 10;
    public int bulletAmountShotgun=10;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // evita duplicados
            return;
        }

        anim = GetComponent<Animator>();  
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // Cada Vez que Una escena es cargada
    {
        // Reasigna Cinemachine
        CinemachineCamera vcam = Object.FindFirstObjectByType<CinemachineCamera>();
        if (vcam != null)
        {
            vcam.Follow = transform;
            vcam.LookAt = transform;
        }

        // Mueve al nuevo punto de aparición
        GameObject spawn = GameObject.FindWithTag("SpawnPoint");
        if (spawn != null)
        {
            transform.position = spawn.transform.position;
        }

        setPlayerHp();
        Shoot.instance.updateText(); // Atualiza las balas en el canvas;

    }
    void setPlayerHp()
    {
        if (playerHP == 0)
        {
            playerHP = 3;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void assignWeapon()
    {
        
    }
}
