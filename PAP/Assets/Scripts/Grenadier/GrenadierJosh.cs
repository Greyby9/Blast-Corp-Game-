using System.Collections;
using UnityEngine;

public class GrenadierJosh : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform throwPoint;
    public Transform target;
    public float throwForce = 10f;
    public float throwInterval = 3f;
    public float minDistance = 5f;
    public bool isAlive = true;
    private float lastThrowTime;
    public Animator anim;
    public float timeAnimation;
    public float heightGranade = 8f;
    public float addedDistance;

    void Update()
    {
        target = Player.instance.transform;
        if (!isAlive || target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= minDistance && Time.time >= lastThrowTime + throwInterval)
        {
            anim.SetBool("attack", true);
            StartCoroutine(WaitAndShoot());
            lastThrowTime = Time.time;
        }
    }

    IEnumerator WaitAndShoot()
    {
        yield return new WaitForSeconds(timeAnimation);
        anim.SetBool("attack", false);
    }

    public void Shoot() // Llame desde Animation Event también
    {
        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, Quaternion.identity);

        if (grenade.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            Vector2 fullDirection = (target.position - throwPoint.position).normalized;

            // Aplica el ajuste restando 1 unidad en esa dirección
            Vector2 adjustedTarget = (Vector2)target.position - fullDirection * addedDistance;
            Vector2 direction = (adjustedTarget - (Vector2)throwPoint.position);
            float distance = direction.magnitude;
            direction.Normalize();
            float g = Mathf.Abs(Physics2D.gravity.y);
            float time = (2 * heightGranade) / g;
            float throwForce = distance / time;

            Vector2 horizontalDirection = new Vector2(direction.x, 0f).normalized;
            Vector2 finalForce = (horizontalDirection * throwForce) + (Vector2.up * heightGranade);

            rb.AddForce(finalForce, ForceMode2D.Impulse);
        }
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

