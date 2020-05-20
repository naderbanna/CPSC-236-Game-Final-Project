using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 25f;
    public bool hasJumpPotion = false;
    public bool hasSpeedPotion = false;
    public bool hasGun = false;
    public int potionModAmonut = 0;

    public AudioClip jumpClip;

    private Text scoreText;

    private float potionTimeMax = 10f;
    private float potionTimeCur = 0f;

    float horizontalMove = 0f;
    bool jumpFlag = false;
    bool jump = false;

    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
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

        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        jump = false;
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
