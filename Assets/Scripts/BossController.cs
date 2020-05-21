using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;
    public int health;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    private Text healthText;
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        healthText = GameObject.Find("BossHealth").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }else if((Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)) { 
            transform.position = this.transform.position;
        }else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position,-speed * Time.deltaTime);

        }
        

        if(timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthText.GetComponent<BossHealthController>().health -= damage;
        healthText.GetComponent<BossHealthController>().UpdateScore();
        if (health <= 0)
            Die();
    }

    void Die()
    {
        scoreText.GetComponent<ScoreController>().score += 1000;
        scoreText.GetComponent<ScoreController>().UpdateScore();
        Destroy(gameObject);
    }
}
