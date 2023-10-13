using System;
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
    private bool _isAttack = false;
    private bool _isDeath = false;
    private float _horizontal;
    private string _buttonPressed;

    private int _coin = 0;
    private Vector3 _savePoint;

    private string _currentAnimName;
    
    // Start is called before the first frame update
    void Start()
    {
        SavePoint();
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDeath)
        {
            return;
        }
        
        _isGrounded = CheckGrounded();

         if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
         {
             Jump();
         }
         
         if(Input.GetKeyDown(KeyCode.C) && _isGrounded)
         {
             Attack();
         }
         
         if(Input.GetKeyDown(KeyCode.V) && _isGrounded)
         {
             Throw();
         }
    }

    void FixedUpdate()
    {
        if (_isDeath)
        {
            return;
        }
        
        Move();
    }

    public void OnInit()
    {
        _isAttack = false;
        _isDeath = false;
        _isJumping = false;
        
        ChangeAnim("idle");
        transform.position = _savePoint;
    }

    private bool CheckGrounded()
    {
        var position  = transform.position;

        Debug.DrawLine(position, position + Vector3.down * 0.75f, Color.red);
        
        RaycastHit2D hit = Physics2D.Raycast(position,Vector2.down, 0.75f, groundLayer);
        
        return hit.collider != null;
    }
    private void Move()
    {
        if (_isAttack)
        {
            return;
        }
        
        _horizontal = Input.GetAxisRaw("Horizontal");

        if (_isGrounded)
        {
            if (_isJumping)
            {
                return;
            }

            // change anim run
            if (Mathf.Abs(_horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }
        }
        // check fall
        if (!_isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            _isJumping = false;
        }

        // move
        if (Mathf.Abs(_horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(_horizontal * speed * Time.fixedDeltaTime, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, _horizontal > 0 ? 0 : 180, 0));
        }
        // idle
        else if (_isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }
    private void Jump()
    {
        _isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }
    private void Attack()
    {
        if (_isAttack)
        {
            return;
        }
        
        _isAttack = true;
        rb.velocity = Vector2.zero;
        ChangeAnim("attack");
        Invoke(nameof(ResetAttack), 0.35f);
    }
    private void Throw()
    {
        if (_isAttack)
        {
            return;
        }

        rb.velocity = Vector2.zero;
        ChangeAnim("throw");
        _isAttack = true;
        Invoke(nameof(ResetAttack), 0.35f);
    }
    private void ResetAttack()
    {
        _isAttack = false;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
            _coin++;
        }

        if (col.CompareTag("DeathZone"))
        {
            _isDeath = true;
            ChangeAnim("die");
            
            Invoke(nameof(OnInit), 1f);
        }
    }

    internal void SavePoint()
    {
        _savePoint = transform.position;
    }
}
