using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 1f; //tốc độ di chuyển
    public ContactFilter2D movementFilter; 
    public float collisionOffset = 0.05f;
    public PlayerAttack attack;

    Vector2 movementInput;
    Rigidbody2D rb;

    Animator animator;

    SpriteRenderer spriteRenderer;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;


    // Start is called before the first frame update
    private void Start()
    {
        attack.Initialize(transform);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove) 
        {
            //Nếu đầu vào chuyển động khác 0, thử di chuyển
            if(movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);
                
                if (!success && movementInput.x > 0)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));

                }

                if (!success && movementInput.y > 0)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isMoving", success); //kiểm tra nếu có chuyển động animation Player_walk
            } 
            else
            {
                animator.SetBool("isMoving", false); //animation Player_ilde
            }

            //Flip player sprite khi chuyển động sang trái hoặc phải
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                 direction,
                 movementFilter,
                 castCollisions,
                 moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire() 
    {
        if (Time.time >= attack.nextAttackTime && canMove) 
        {
            animator.SetTrigger("Attack");
            attack.nextAttackTime = Time.time + attack.attackCooldown;
        }
    }

    public void Attack() 
    {
        LockMovement();
        if (spriteRenderer.flipX == true) 
        {
            attack.AttackLeft();
        } 
        else 
        {
            attack.AttackRight();
        }
    }

    public void StopAttack()
    {
        attack.StopAttack();
        UnlockMovement();
    }

    public void LockMovement() 
    {
        canMove = false;
    }

    public void UnlockMovement() 
    {
        canMove = true;
    }
}