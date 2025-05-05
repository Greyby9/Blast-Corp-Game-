using UnityEngine;

public class CameraBroundFollow : MonoBehaviour
{

    public float idleTime, idleCounter;

    public static CameraBroundFollow instance;
    public Transform rightLimit;
    private bool cameraStopped = false; 

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (idleCounter > 0) {
            idleCounter -= Time.deltaTime;
        }

        Move();    
    }

    void Move()
    {
    if (cameraStopped) return; // si ya se detuvo, no sigue moviéndose

    if (Player.instance.movementX > 0 && !Player.instance.isCollidingWithBarrier) {
        if (idleCounter <= 0) {
            Vector2 posicionJug = transform.position;
            posicionJug = posicionJug + new Vector2(Player.instance.movementX, 0f) * Player.instance.speedMoviment * Time.deltaTime;

            // 👉 Verifica si la nueva posición pasaría el límite
            if (posicionJug.x >= rightLimit.position.x) {
                posicionJug.x = rightLimit.position.x;
                cameraStopped = true;
                Debug.Log("pasa");

            }

            idleTime = 0;
            transform.position = posicionJug;
        }
    }
    else if (Player.instance.movementX < 0 && !Player.instance.isCollidingWithBarrier) {
        idleTime += Time.deltaTime;
        idleCounter = idleTime;
    }
    else if (Player.instance.movementX < 0 && Player.instance.isCollidingWithBarrier) {
        idleCounter = idleTime;
    }
    else {
        idleCounter = idleTime;
    }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
            if (collision.gameObject.tag == "end") {
                Debug.Log("llego");
            }
    }



}
