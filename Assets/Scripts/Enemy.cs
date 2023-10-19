using System;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private GameObject attackArea;
    
    private IState _currentState;
    private bool _isRight = true;
    
    private Character _target;
    public Character Target => _target;

    private void Update()
    {
        if (_currentState != null && !IsDead)
        {
            _currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        
        ChangeState(new IdleState());
        DeActiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(gameObject);
    }

    protected override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }
    
    internal void SetTarget(Character target)
    {
        _target = target;
        
        if (_target != null)
        {
            if (IsTargetInRange())
            {
                ChangeState(new AttackState());
            }
            else
            {
                ChangeState(new PatrolState());
            }
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    public void ChangeState(IState newState)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(this);
        }
        
        _currentState = newState;
        
        if (_currentState != null)
        {
            _currentState.OnEnter(this);
        }
    }

    public void Moving()
    {
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        ChangeAnim("attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.35f);
    }

    public bool IsTargetInRange()
    {
        return Vector2.Distance(_target.transform.position, transform.position) <= attackRange;
    }
    
    public void ChangeDirection(bool isRight)
    {
        _isRight = isRight;
        transform.rotation = _isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
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
        if (col.CompareTag("EnemyWall"))
        {
            ChangeDirection(!_isRight);
        }
    }

}
