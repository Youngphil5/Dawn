using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{


 //technique for making sure there isn't a null reference during runtime if you are going to use get component
    private Rigidbody2D myRigidbody2D;

    public float speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {

        myRigidbody2D = GetComponent<Rigidbody2D>();
        //Fire();
    }

    // Update is called once per frame
    private void Fire()
    {
        myRigidbody2D.velocity = -Vector2.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);

    }

}
