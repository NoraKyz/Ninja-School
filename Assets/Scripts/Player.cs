using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    
    [SerializeField] private float speed = 200f;
    [SerializeField] private float jumpForce = 300f;
    
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isAttack;
    private float _horizontal;

    private string _currentAnimName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         _isGrounded = CheckGrounded();
    }

    void FixedUpdate()
    {
        Move();
    }

    private bool CheckGrounded()
    {
        var position  = transform.position;
        
        Debug.DrawLine(position, position + Vector3.down * 1.3f, Color.red);
        
        RaycastHit2D hit = Physics2D.Raycast(position,Vector2.down, 1.3f, groundLayer);
        
        return hit.collider != null;
    }

    private void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if (_isGrounded)
        {
            if (_isJumping)
            {
                return;
            }
 
            // jump
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                Jump();
            }

            // change anim run
            if (Mathf.Abs(_horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }

            if (_isJumping)
            {
                return;
            }
        }
        
        if (!_isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            _isJumping = false;
        }

        if (Mathf.Abs(_horizontal) > 0.1f)
        {
            ChangeAnim("run");
            rb.velocity = new Vector2(_horizontal * speed * Time.fixedDeltaTime, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, _horizontal > 0 ? 0 : 180, 0));
        }
        else if (_isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }

    private void Jump()
    {
        Debug.Log("Jump");
        _isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }

    private void ChangeAnim(string animName)
    {
        if (_currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            _currentAnimName = animName;
            anim.SetTrigger(animName);
        }
    }
}
