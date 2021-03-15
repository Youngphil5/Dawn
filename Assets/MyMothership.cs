using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MyMothership : MonoBehaviour
{

    public GameObject EnemyType1;
    public GameObject EnemyType2;
    public GameObject EnemyType3;
    public wizard EnemyType4;

    private int randomInt;
    public int deathCount;
    private bool respawnNow= false;
    public float disapperTime = 5f;
    public float apperTime;
    public bool onScene = true;

    private Enemy[] b;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    
    void Start()
    {
        deathCount = 1;
        respawn();
        StartCoroutine(nextRespawnTime());
        
        MyInstantate("EnemyType4", transform,5.38064f, -6.5f);
        EnemyType4 = FindObjectOfType<wizard>();
        EnemyType4.onScene = true;

    }

    void MyInstantate(String gameTag, Transform position, float x, float y)
    {
        Vector3 pos = new Vector3(transform.position.x + x, transform.position.y + y,
            transform.position.z);

        switch (gameTag)
        {
            case "EnemyType1" :
                Instantiate(EnemyType1, pos, transform.rotation);
                break;
            case "EnemyType2" :
                Instantiate(EnemyType2, pos, transform.rotation);
                break;
            case "EnemyType3" :
                Instantiate(EnemyType3, pos, transform.rotation);
                break;
            case "EnemyType4" :
                Instantiate(EnemyType4, pos, transform.rotation);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        respawn();
        disapperTime -= Time.deltaTime;
        apperTime -= Time.deltaTime;
        if (disapperTime <= 0 && onScene)
        {
            disAppear();
        }
        if (apperTime <= 0 && !onScene)
        {
            appear();
        }
    }

    private void FixedUpdate()
    {
        
    }

    void respawn()
    {
        if (deathCount >= 10 || respawnNow)
        {
            deathCount = 0;
            randomInt = Random.Range(1, 4);
            switch (randomInt)
            {
                case 1:
                    MyInstantate("EnemyType1", transform,8.52f, -6.5f);
                    MyInstantate("EnemyType3", transform, -8.52f, -6.5f);
                    break;
                case 2:
                    MyInstantate("EnemyType2", transform,-2, 3.27f);
                    MyInstantate("EnemyType1", transform,8.52f, -6.5f);
                    break;
                case 3:
                    MyInstantate("EnemyType3", transform,-8.52f, -6.5f);
                    MyInstantate("EnemyType1", transform,8.52f, -6.5f);
                    break;
                default:
                    break;
            }
            
        }
        respawnNow = false;
    }

    IEnumerator nextRespawnTime()
    {
        while (true)
        {
            respawnNow = true; 
            yield return new WaitForSecondsRealtime(6f);
        }
    }
    void disAppear()
    {
        if (EnemyType4)
        {
            EnemyType4.gameObject.SetActive(false);
            onScene = false;
            apperTime = 10f;
            EnemyType4.onScene = false;
        }
    }
    void appear()
    {
        if (EnemyType4)
        {
            EnemyType4.gameObject.SetActive(true);
            //EnemyType4.animator.SetTrigger("Die");
            onScene = true;
            disapperTime = 5f;
            EnemyType4.onScene = true;
        }
    }
}
