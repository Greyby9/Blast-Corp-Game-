using UnityEngine;

public class punchingBag : MonoBehaviour
{
    public int hpPunchingBag;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            anim.SetTrigger("damage");
            hpPunchingBag -= 1;
            Destroy(collider.gameObject);
            if (hpPunchingBag == 0)
            {
            Destroy(gameObject);                
            }

        }
    }
}
