using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletDroneScript : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 5f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, lifeTime); // Destruye la bala después de un tiempo
    }

    void Update()
    {
        // La bala se mueve hacia abajo
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("isContact");
            Destroy(collision.gameObject);
            Player.instance.loseHP();
            Destroy(gameObject,0.2f);
            if (Player.instance.lives <= 0){
                UIController.Instance.gameOver();
            }else {
                SceneManager.LoadScene(1);
            }
            

        } 

        if (collision.CompareTag("Ground"))
        {
            anim.SetTrigger("isContact");
            Destroy(gameObject, 0.2f);

        }
}
}
