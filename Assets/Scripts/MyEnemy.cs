using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MyEnemy : MonoBehaviour
{
    private MyMothership mothership;
    public float enemySpeed = 2;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool stopMoving;
    public GameObject bullet;
    public ParticleSystem particleSystem;
    private float ShootTimer = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        mothership = FindObjectOfType<MyMothership>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool("Idle",false);
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        move();
    }

    void move()
    {
        if (!stopMoving)
        {
            if (transform.position.x <= -9.6)
            {
                turnArround();

            }
            else if (transform.position.x >= 9.8)
            {
                turnArround();
            }
            transform.Translate(Vector3.right * Time.deltaTime * enemySpeed);
        }
    }

    void turnArround()
    {
        enemySpeed = -enemySpeed;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            particleSystem.Play();
            animator.SetTrigger("Die");
            Destroy(gameObject, 0.60f);
            Destroy(other.gameObject);
            mothership.deathCount++;
            
        }
        if (other.gameObject.tag == "Enemy")
        {
           turnArround(); 
        }
    }

    public void shoot()
    {
            Vector2 newPos = new Vector2(transform.position.x + enemySpeed , transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(newPos, Vector2.right * enemySpeed,3.7f);
            if (hit)
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    stopMoving = true;
                    animator.SetTrigger("Shoot");
                    shootingDirection();
                }
            }
            else
            {
                stopMoving = false;
            }
        
    }

    void shootingDirection()
    {
        if (enemySpeed < 0 )
        {
            
            Vector3 newPostion = new Vector3(transform.position.x - 1.3f, transform.position.y, transform.position.z);
            GameObject shot = Instantiate(bullet, newPostion, Quaternion.identity); 
            shot.GetComponent<Rigidbody2D>().velocity = -Vector2.right * 3.2f;
            Destroy(shot, 3f);
            
        }
        else
        {
            Vector3 newPostion = new Vector3(transform.position.x + 1.3f, transform.position.y, transform.position.z);
            GameObject shot = Instantiate(bullet, newPostion, Quaternion.identity); 
            shot.GetComponent<Rigidbody2D>().velocity = Vector2.right * 3.2f;
            Destroy(shot, 3f);
        }
    }

    private void FixedUpdate()
    {
        ShootTimer -= Time.deltaTime;
        if (ShootTimer <= 0)
        {
            shoot();
            ShootTimer = 1.2f;
        }

    }
}
