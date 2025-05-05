using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public Transform player;

    public float speed;
    public float minDistance;
    public float shootTime;

    private float shootCounter;
    private Animator anim;
    private bool isShooting = false;
    private Vector3 initialScale;
    private Vector2 diretion;
    void Start()
    {
        anim = GetComponent<Animator>();
        shootCounter = 0f; // Para que dispare de inmediato al acercarse
        initialScale = transform.localScale;
        FlipTowardsPlayer();
    }

    void Update()
    {
        if (isShooting) return; // No hacer nada si está disparando
        if (player != null){
          float distanceToPlayer = Vector2.Distance(transform.position, player.position);  

        if (distanceToPlayer > minDistance)
        {
            anim.SetBool("move", true);
            anim.SetBool("isShooting", false);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            TimeShoot(); // Detona disparo si ya está a distancia mínima
        }
        }
        


    }

    public void FlipTowardsPlayer()
    {
        if (player.position.x >= transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Mira a la derecha
            diretion = Vector2.right;
        }
        else if (player.position.x <= transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Mira a la izquierda
            diretion=Vector2.left;
        }
    }

    private void TimeShoot()
    {
        anim.SetBool("move", false);

        if (shootCounter > 0)
        {
            shootCounter -= Time.deltaTime;
            return;
        }

        isShooting = true;
        anim.SetTrigger("shoot");
        ShootBullet();

        shootCounter = shootTime;

        Invoke("StopShooting", 0.5f); // Después de medio segundo termina de disparar
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.transform.SetParent(null);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = diretion * 5f;
    }

    private void StopShooting()
    {
        anim.SetBool("isShooting", false);
        isShooting = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}
