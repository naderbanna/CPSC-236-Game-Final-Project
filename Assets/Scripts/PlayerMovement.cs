using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public Transform firePoint;
    public GameObject playerBullet;

    public float runSpeed = 25f;
    public int health = 100;
    public bool hasJumpPotion = false;
    public bool hasSpeedPotion = false;
    public bool hasGun = false;
    public int potionModAmonut = 0;

    public AudioClip jumpClip;

    private Text scoreText;
    private Text healthText;

    private float potionTimeMax = 10f;
    private float potionTimeCur = 0f;

    float horizontalMove = 0f;
    bool jumpFlag = false;
    bool jump = false;

    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        healthText = GameObject.Find("Player Health").GetComponent<Text>();

    }
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (jumpFlag)
        {
            animator.SetBool("IsJumping", true);
            jumpFlag = false;
        }

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            if (animator.GetBool("IsJumping") == false)
            {
                AudioSource.PlayClipAtPoint(jumpClip, transform.position);
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            
        }
        if (Input.GetButtonDown("Shoot") || Input.GetButtonDown("Fire2"))
        {
            Shoot();
        
        }
    }

    void Shoot()
    {
       
        StartCoroutine(MyCoroutine());
       
        IEnumerator MyCoroutine()
        {
            animator.SetBool("IsShooting", true);
            Instantiate(playerBullet, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(.4f);
            animator.SetBool("IsShooting", false);

        }
    }

    
    public void TakeDamage (int damage)
    {
        health -= damage;
        healthText.GetComponent<HealthController>().health -= damage;
        healthText.GetComponent<HealthController>().UpdateScore();
        if (health <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        jump = false;
    }

    public void OnShooting(bool isShooting)
    {
        animator.SetBool("IsShooting", isShooting);
    }

    void FixedUpdate()
    {
        if (hasJumpPotion && potionTimeCur < potionTimeMax)
        {
            controller.m_JumpForceMod = potionModAmonut;
            potionTimeCur += Time.fixedDeltaTime;
        }
        else
        {
            potionTimeCur = 0f;
            controller.m_JumpForceMod = 0;
            hasJumpPotion = false;
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        if (jump)
        {
            jumpFlag = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GemstoneP")
        {
            scoreText.GetComponent<ScoreController>().score += 5;
            scoreText.GetComponent<ScoreController>().UpdateScore();
        }
        else if (collision.gameObject.tag == "GemstoneY")
        {
            scoreText.GetComponent<ScoreController>().score += 10;
            scoreText.GetComponent<ScoreController>().UpdateScore();
        }
       
    }
}
