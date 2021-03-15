using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ide : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.SetBool("Idle",true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
