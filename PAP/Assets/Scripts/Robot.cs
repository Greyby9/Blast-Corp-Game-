using Unity.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class TowerPatrolShooter : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public Transform leftLimit, rightLimit;
    private bool movingRight = true;

    public Transform firePointLeftDown;
    public Transform firePointDown;
    public Transform firePointRightDown;

    public GameObject bulletPrefab;
    private  GameObject bulletRobot;
    public float fireRate = 1.5f;
    private float nextFireTime;

    public float detectionRange = 10f;
    public float patrolStartRange = 7f;

    private Transform player;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public GameObject destroyprefab;
    public AudioClip moveRobotSound;
    public AudioClip shootRobotSound;
    public AudioClip robotDestroyed; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= patrolStartRange)
        {
            Patrol();
            soundController.Instace.exeSound(moveRobotSound);
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        if (distanceToPlayer <= detectionRange)
        {
            DetectAndShoot();
        }
    }

    void Patrol()
    {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        transform.Translate(direction * patrolSpeed * Time.deltaTime);

        // Flip visual del sprite
        spriteRenderer.flipX = !movingRight;

        if (transform.position.x >= rightLimit.position.x)
            movingRight = false;
        else if (transform.position.x <= leftLimit.position.x)
            movingRight = true;
    }

    void DetectAndShoot()
    {
        if (player == null || Time.time < nextFireTime)
            return;

        Vector2 playerDirection = player.position - transform.position;
        float angle = Vector2.SignedAngle(Vector2.down, playerDirection.normalized);

        if (angle > -135 && angle < -45)
        {
            Shoot(firePointLeftDown);
            anim.SetBool("shootLeftDown", true);
            Debug.Log("se activa");
        } else {
            anim.SetBool("shootLeftDown", false);
        }
        if (angle >= -45 && angle <= 45)
        {
            Shoot(firePointDown);
            anim.SetBool("shootDown", true);
        } else {
            anim.SetBool("shootDown", false);
        
        }
        if (angle > 45 && angle < 135)
        {
            Shoot(firePointRightDown);
            anim.SetBool("shootRightDown", true);
        } else{
            anim.SetBool("shootRightDown", false);
 //me falta porle que cuando se acaba la animacion la ponga en false;

        }
    }

    void Shoot(Transform firePoint)
    {
    bulletRobot = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    soundController.Instace.exeSound(shootRobotSound);
    bulletRobot.transform.SetParent(null);

    Rigidbody2D rb = bulletRobot.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        float bulletSpeed = 8f;

        // Dirección personalizada por firePoint
        Vector2 direction = Vector2.down; // por defecto

        if (firePoint == firePointLeftDown)
            direction = (Vector2.down + Vector2.left).normalized;
        else if (firePoint == firePointRightDown)
            direction = (Vector2.down + Vector2.right).normalized;

        rb.linearVelocity = direction * bulletSpeed;
    }

    nextFireTime = Time.time + fireRate;
}
public void OnAnimationEnd()
    {
        anim.SetBool("shootLeftDown", false);
        anim.SetBool("shootRightDown", false);
        anim.SetBool("shootDown", false);

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
     
            Destroy(collider.gameObject);
            Instantiate(destroyprefab, transform.position, transform.rotation );
            soundController.Instace.exeSound(robotDestroyed);
            Destroy(gameObject);
            
        }
    }
}
