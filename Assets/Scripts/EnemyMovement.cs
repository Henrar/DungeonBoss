using System;
using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public Collider2D EnemyCollider;
    public Rigidbody2D EnemyRigidBody;
    public float JumpHeight;
    public float WalkSpeed;

    public Collider2D wall;

    public bool DebugMovement = false;


    public enum MoveDirection
    {
        None = 0,
        Right = 1,
        Left = 2
    }

    public Action<MoveDirection> OnSideWallHitAction;

    private MoveDirection _currentDirection = MoveDirection.None;
    private bool _jumpLocked = false;

    public bool IsGrounded { get; set; }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        bool sideWallHit = false;
        foreach (var contactPoint2D in collision.contacts)
        {
            if (contactPoint2D.normal.y > 0)
            {
                IsGrounded = true;
            }
            else
            {
                sideWallHit = true;
            }
        }
        if (sideWallHit)
        {
            OnSideWallHitAction(_currentDirection);
        }
    }

    public void Jump()
    {
        if (IsGrounded && !_jumpLocked)
        {
            EnemyRigidBody.velocity += Vector2.up*JumpHeight;
            _jumpLocked = true;
            StartCoroutine(UnlockJump());
            IsGrounded = false;
        }
    }

    IEnumerator UnlockJump()
    {
        yield return new WaitForSeconds(0.3f);
        _jumpLocked = false;
    }

    public void Move(bool left)
    {
        if (left)
        {
            Move(MoveDirection.Left);
        }
        else
        {
            Move(MoveDirection.Right);
        }
    }

    public void Move(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.None:
            {
                EnemyRigidBody.velocity = new Vector2(0, EnemyRigidBody.velocity.y);
                break;
            }
            case MoveDirection.Right:
            {
                EnemyRigidBody.velocity = new Vector2(WalkSpeed, EnemyRigidBody.velocity.y);
                break;
            }
            case MoveDirection.Left:
            {
                EnemyRigidBody.velocity = new Vector2(-WalkSpeed, EnemyRigidBody.velocity.y);
                break;
            }
        }
        _currentDirection = direction;
    }

    public void FixedUpdate()
    {
        if (!DebugMovement)
        {
            Move(_currentDirection);
        }
        else
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            if (horizontal > 0)
            {
                Move(MoveDirection.Right);
            }
            else if (horizontal < 0)
            {
                Move(MoveDirection.Left);
            }
            else
            { 
                Move(MoveDirection.None);
            }
            if (vertical > 0)
            {
                Jump();
            }
        }

        if (EnemyRigidBody.velocity.y <= 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("GoesThroughFloor");
        }
        if (EnemyRigidBody.velocity.x <= 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); 
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
