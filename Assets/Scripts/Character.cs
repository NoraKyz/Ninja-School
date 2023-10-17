using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private float _hp;
    private string _currentAnimName;
    public bool IsDead => _hp <= 0;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        _hp = 100;
    }
    public virtual void OnDespawn()
    {
    }
    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        
        Invoke(nameof(OnDespawn), 1f);
    }
    
    public void OnHit(float damage)
    {
        if (!IsDead)
        {
            _hp -= damage;

            if (IsDead)
            {
                OnDeath();
            }
        }
    }
    protected void ChangeAnim(string animName)
    {
        if (_currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            _currentAnimName = animName;
            anim.SetTrigger(animName);
        }
    }
}