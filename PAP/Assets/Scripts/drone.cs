using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour
{
    public float speedMovement = 5f;
    public float exitSpeed = 10f;
    public GameObject bulletDrone;
    public Transform player;

    public float hoverHeight = 3f;
    public float shootInterval = 1f;
    public float hoverDuration = 4f;
    public float exitOffset = 10f;

    private float nextShotTime = 0f;
    private float hoverStartTime;
    private bool hovering = false;
    private bool exiting = false;
    private Vector2 targetPosition;

    private string exitSide = "right"; // lado por el que va a salir

    public System.Action<string> onDroneExit;
    private Animator anim;
    public float explosionDuration = 0.1f;
    public AudioClip soundBulletDrone;
    public AudioClip explosionDroneSound;

    

    void Start()
    {
        anim = GetComponent<Animator>();
        if (player == null) return;
        targetPosition = new Vector2(player.position.x, player.position.y + hoverHeight);
    }

    void Update()
    {
        if (player == null) return;

        if (!exiting)
        {
            targetPosition = new Vector2(player.position.x, player.position.y + hoverHeight);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speedMovement * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                if (!hovering)
                {
                    hovering = true;
                    hoverStartTime = Time.time;
                }

                if (Time.time >= nextShotTime)
                {
                    Shoot();
                    nextShotTime = Time.time + shootInterval;
                }

                if (Time.time - hoverStartTime >= hoverDuration)
                {
                    exiting = true;
                }
            }
        }
        else
        {
            float exitX = (exitSide == "right") ? player.position.x + exitOffset : player.position.x - exitOffset;
            Vector2 exitPosition = new Vector2(exitX, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, exitPosition, exitSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - exitX) < 0.1f)
            {
                onDroneExit?.Invoke(exitSide); // Notificamos al spawner
                Destroy(gameObject);
            }
        }
    }
     public void Explode()
    {
        anim.SetTrigger("Explode");
        if (soundController.Instace.audioSource.isPlaying)
        {
        soundController.Instace.audioSource.Stop();
        }
        soundController.Instace.exeSound(explosionDroneSound);
        StartCoroutine(DestroyAfterExplosion());
    }

    private IEnumerator DestroyAfterExplosion()
    {
        yield return new WaitForSeconds(explosionDuration);
        Destroy(gameObject);
    }

    public void SetExitSide(string side)
    {
        exitSide = side;
    }

    void Shoot()
    {
        if (bulletDrone != null)
        {
            Instantiate(bulletDrone, transform.position, Quaternion.identity);
            soundController.Instace.exeSound(soundBulletDrone);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.CompareTag("Bullet"))
    {
        
        // Destruye la bala
        Destroy(collision.gameObject);

        // Notifica al spawner que el dron fue destruido por impacto
        onDroneExit?.Invoke(exitSide);
        Explode();
    } 
    }
}
