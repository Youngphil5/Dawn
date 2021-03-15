using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    public GameObject bullet;
    public float speed = 2;
    private Vector3 newPostion;
    public static int ShootDirection =0;
    public bool canJump = false;
    public float jumpForce;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int health;

    public ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        shoot();
        jump();
    }

    void move()
    {
        if (Input.GetButton("Horizontal"))
        {
            /*float y = (Input.GetAxis("Horizontal") < 0) ? 180 : 0;
            Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
            transform.rotation = newRotation;*/
            transform.localPosition += 
                Input.GetAxis("Horizontal") * transform.right * Time.deltaTime * speed;
            if (Input.GetAxis("Horizontal") < 0)
            {
                spriteRenderer.flipX = true;
                ShootDirection = -1;
            }
            else
            {
                spriteRenderer.flipX = false;
                ShootDirection = 1;
            }
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse );
            animator.SetTrigger("Jump");
        }
    }
    

    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            newPostion = new Vector3(transform.position.x + ShootDirection, transform.position.y, transform.position.z);
            GameObject shot = Instantiate(bullet, newPostion, Quaternion.identity);
            
            Destroy(shot, 3f);
            animator.SetTrigger("Shoot");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            canJump = true;
            animator.SetBool("Touching ground", true);
        }
        if (other.gameObject.tag == "Bullet")
        {
            health -= 10;
            Destroy(other.gameObject);
            animator.SetTrigger("Die");
            
            if (health == 0)
            {
                particleSystem.Play();
                Destroy(gameObject,0.5f);
                wizard.health = 0;
                GameManager.MainScene = false;
            }
            
        }
        
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            canJump = false;
            animator.SetBool("Touching ground", false);
        }
    }
}
