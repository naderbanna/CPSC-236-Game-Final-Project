using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBulletController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public int damage = 10;
    private Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 8f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        TurrentController enemy = hitInfo.GetComponent<TurrentController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);

        if (hitInfo.gameObject.tag.Equals("TurrentBullet"))
            Destroy(hitInfo.gameObject);
        Destroy(gameObject);

        Patrol skeleton = hitInfo.GetComponent<Patrol>();
        if (skeleton != null)
        {
            skeleton.TakeDamage(damage);
        }

        BossController boss = hitInfo.GetComponent<BossController>();
        if(boss != null)
        {
            boss.TakeDamage(damage);
        }
        Destroy(gameObject);

        if (hitInfo.gameObject.tag == "Enemy")
        {
            scoreText.GetComponent<ScoreController>().score += 20;
            scoreText.GetComponent<ScoreController>().UpdateScore();
        }
    }
}
 