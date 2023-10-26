using System;
using UnityEngine;

public class Player1 : Character
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

    private void Awake()
    {
        _coin = PlayerPrefs.GetInt("coin", 0);
    }

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

        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Throw();
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        _isAttack = false;
        _isJumping = false;

        ChangeAnim("idle");
        DeActiveAttack();
        transform.position = _savePoint;
        UIManager.Instance.SetCoin(_coin);
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
        var position = transform.position;

        Debug.DrawLine(position, position + Vector3.down * 0.37f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 0.37f, groundLayer);

        return hit.collider != null;
    }

    private void Move()
    {
        if (_isAttack)
        {
            return;
        }
        
        Debug.LogWarning("Move() by button disabled");
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
            rb.velocity = new Vector2(_horizontal * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, _horizontal > 0 ? 0 : 180, 0));
        }
        // idle
        else if (_isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }
    
    public void SetMove(float horizontal)
    {
        _horizontal = horizontal;
    }

    public void Jump()
    {
        if (!_isGrounded)
        {
            return;
        }
        
        _isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }

    public void Attack()
    {
        if (_isAttack || !_isGrounded)
        {
            return;
        }

        _isAttack = true;
        rb.velocity = Vector2.zero;
        ChangeAnim("attack");
        Invoke(nameof(ResetAttack), 0.35f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.35f);
    }

    public void Throw()
    {
        if (_isAttack || !_isGrounded)
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
            PlayerPrefs.SetInt("coin", _coin);
            UIManager.Instance.SetCoin(_coin);
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