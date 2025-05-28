using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionDelay = 2f;
    public GameObject explosionEffect;
    private Animator anim;
    private Rigidbody2D rb;

    public Vector3 rotationSpeed = new Vector3(0f, 0f, 1000f);

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            anim.SetTrigger("burst");
            rotationSpeed = new Vector3(0f, 0f, 0f);
            Destroy(gameObject, 0.2f);
            rb.simulated = false;
        }
    }

}
