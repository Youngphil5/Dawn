using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizard : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Animator animator;
    public float ShootTimer = 1.2f;
    private int distance = -2;
    public GameObject bullet;
    private float turnTime = 3.5f;
    public bool onScene;
    public static int health = 50;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = 50;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    void turnArround()
    {
        distance = -distance;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    void spotPlayer()
    {
        
        Vector2 newPos = new Vector2(transform.position.x + distance , transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(newPos, Vector2.right * distance,4.2f);
        if (hit)
        {
            
            if (hit.transform.gameObject.tag == "Player")
            {
                
                Fire();
            }
        }
        else
        {
        }

    }
    void Fire()
    {
        animator.SetTrigger("Attack");
        if (spriteRenderer.flipX)
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
        turnTime -= Time.deltaTime;
        if (ShootTimer <= 0 && onScene)
        {
            spotPlayer();
            ShootTimer = 1.2f;
        }
        
        if (turnTime <= 0 && onScene)
        {
            turnArround();
            turnTime = 3.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            health -= 10;
            if (health <= 0)
            {
                animator.SetTrigger("Die");
                onScene = false;
                GameManager.MainScene = false;
                Destroy(gameObject,0.2f);
            }
        }
    }
}
