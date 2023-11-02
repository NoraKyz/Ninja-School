using System;
using UnityEngine;

public class Player : Character
{
    [Header("=========================================")]
    [SerializeField] private PlayerData playerData;
    
    [Header("=========================================")]
    [SerializeField] private Rigidbody2D rb;

    [Header("=========================================")]
    [SerializeField] private Kunai kunaiPrefabs;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;

    private bool _isGrounded;
    private bool _isJumping;
    private bool _isAttack;
    private float _horizontal;
    private string _buttonPressed;

    private float moveSpeed;
    private float jumpForce;
    private int jumpCount;
    
    private int coin;
    
    private Vector3 _savePoint;

    #region Unity Functions

    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
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
    
    private void OnDrawGizmos()
    {
        var cacheTransform = transform;
        Gizmos.DrawWireCube(cacheTransform.position - cacheTransform.up * playerData.castDistance, playerData.groundBoxSize);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            UIManager.Instance.SetCoin(coin);
        }

        if (col.CompareTag("DeathZone"))
        {
            OnHit(Mathf.Infinity);
        }
    }

    #endregion

    #region Base Functions

    protected override void OnInit()
    {
        base.OnInit();

        ChangeAnim("idle");
        DeActiveAttack();
        UIManager.Instance.SetCoin(coin);
    }
    protected override void InitProperties()
    {
        base.InitProperties();
        
        transform.position = _savePoint;
        
        Hp = playerData.health;
        moveSpeed = playerData.speed;
        jumpForce = playerData.jumpForce;
        jumpCount = 0;
        
        _isAttack = false;
        _isJumping = false;
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }

    #endregion

    #region Check Functions

    private bool CheckGrounded()
    {
        var cacheTransform = transform;
        return Physics2D.BoxCast(cacheTransform.position, playerData.groundBoxSize, 0, -cacheTransform.up, playerData.castDistance, playerData.groundLayer);
    }

    #endregion

    #region Action Functions

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
            
            ResetJumpCount();

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
            rb.velocity = new Vector2(_horizontal * moveSpeed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, _horizontal > 0 ? 0 : 180, 0));
        }
        // idle
        else if (_isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }
    
    public void Jump()
    {
        if (!_isGrounded && jumpCount >= 2)
        {
            return;
        }
        
        _isJumping = true;
        jumpCount++;
        
        ChangeAnim("jump");
        rb.velocity = Vector2.zero;
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
    
    #endregion
    
    #region Other Functions
    public void SetMove(float horizontal)
    {
        _horizontal = horizontal;
    }

    private void ResetJumpCount()
    {
        jumpCount = 0;
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

    internal void SavePoint()
    {
        _savePoint = transform.position;
    }
    #endregion
}