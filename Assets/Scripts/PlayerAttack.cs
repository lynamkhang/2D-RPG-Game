using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Collider2D attackCollider;
    Vector2 attackOffset;
    Transform playerTransform;

     public void Initialize(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        attackCollider = GetComponent<Collider2D>();
        attackOffset = transform.localPosition;
    }

    private void Start() 
    {
        attackCollider = GetComponent<Collider2D>();
        attackOffset = transform.localPosition;
        playerTransform = transform.parent;
    }

   public void AttackLeft() 
   {    
        attackCollider.enabled = true;
        Vector3 newPosition = playerTransform.position + new Vector3(-attackOffset.x, attackOffset.y, 0);
        transform.position = newPosition;
   }

   public void AttackRight() 
   {
        attackCollider.enabled = true;
        Vector3 newPosition = playerTransform.position + new Vector3(attackOffset.x, attackOffset.y, 0);
        transform.position = newPosition;
   }


   public void StopAttack() 
   {
        attackCollider.enabled = false;
   }
}
