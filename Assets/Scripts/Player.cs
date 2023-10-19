using System;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float speed = 200f;
    [SerializeField] private float jumpForce = 300f;

    [SerializeField] private Kunai kunaiPrefabs;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;

    private bool _isGrounded;
    private bool _isJumping;
    private bool _isAttack = false;
    private float _horizontal;
    private string _buttonPressed;

    private int _coin = 0;
    private Vector3 _savePoint;

    private void Start()
    {
        SavePoint();
        OnInit();
    }
    void Update()
    {
        if (IsDead)
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
        if (IsDead)
        {
            return;
        }
        
        Move();
    }

    public override void OnInit()
    {
        base.OnInit();

        _isAttack = false;
        _isJumping = false;
        
        ChangeAnim("idle");
        DeActiveAttack();
        transform.position = _savePoint;
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
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
        Invoke(nameof(ResetAttack), 0.5f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }
    private void Throw()
    {
        if (_isAttack)
        {
            return;
        }
        
        _isAttack = true;
        rb.velocity = Vector2.zero;
        ChangeAnim("throw");
        Invoke(nameof(ResetAttack), 0.35f);
        Instantiate(kunaiPrefabs, throwPoint.position, throwPoint.rotation);
    }
    private void ResetAttack()
    {
        _isAttack = false;
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
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
            OnHit(Mathf.Infinity);
        }
    }
    internal void SavePoint()
    {
        _savePoint = transform.position;
    }
}
