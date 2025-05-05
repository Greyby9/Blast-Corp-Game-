using UnityEngine;

public class boxDanger : MonoBehaviour
{
    private Animator anim;

    private BoxCollider2D boxCollider;
    private int ramdom;
    public GameObject bulletSMG;
    public GameObject bulletPistols;
    public GameObject bulletShotgun;
    private Vector2 boxBulletPosition;
    public AudioClip boxSound;
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        boxBulletPosition = new Vector2 (transform.position.x, transform.position.y - 0.5f);
        
        
    }
void OnTriggerEnter2D(Collider2D collider)
{
    if (collider.gameObject.CompareTag("Bullet")) {
        soundController.Instace.exeSound(boxSound);
            if (boxCollider != null) {
            boxCollider.enabled = false;
            transform.position= new Vector2 (transform.position.x, transform.position.y - 0.2f);
        }
        ramdom = UnityEngine.Random.Range(1, 3);
        if (ramdom == 1){
            Instantiate(bulletSMG, boxBulletPosition, Quaternion.identity);
        }
        if (ramdom == 2){
            Instantiate(bulletPistols, boxBulletPosition, Quaternion.identity);
        }
       // if (ramdom == 3){
       //     Instantiate(bulletShotgun, boxBulletPosition, Quaternion.identity);
       // }

        
        if (anim != null) {
            anim.SetBool("destroyed", true);

            
            Destroy(collider.gameObject); 
        } else {
            
            Destroy(collider.gameObject);
        }
    }
}
}
