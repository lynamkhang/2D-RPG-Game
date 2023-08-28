using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public Animator animator;

    public float Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                Killed();
            } 
        }

        get
        {
            return health;  
        }
    }

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }


    public void Killed()
    {
        animator.SetBool("isKilled", true);
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }

    public void RemoveEnemy() 
    {
        Destroy(gameObject);

    }
}


