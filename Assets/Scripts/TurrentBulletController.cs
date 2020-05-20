using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentBulletController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
        Destroy(gameObject, 8f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
