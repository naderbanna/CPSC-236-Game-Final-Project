using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentBulletController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public GameObject impactEffect;
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
        Destroy(gameObject, 8f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();
        if (player != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            player.TakeDamage(damage);
            Destroy(gameObject);
        }

        PlayerBulletController bullet = hitInfo.GetComponent<PlayerBulletController>();
        if (bullet != null)
            Instantiate(impactEffect, transform.position, Quaternion.identity);
    }
    
}
