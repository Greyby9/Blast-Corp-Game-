using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletDroneScript : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 5f;
    private Animator anim;
    private Rigidbody2D rbBullet;

    void Start()
    {
        anim = GetComponent<Animator>();
        rbBullet = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
          
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("isContact");
            Destroy(collision.gameObject);
            Player.instance.loseHP();
            Destroy(gameObject);
        } 

        if (collision.CompareTag("Ground"))
        {
            anim.SetTrigger("isContact");
            Destroy(gameObject, 0.2f);
            rbBullet.simulated = false;

        }
}
}
