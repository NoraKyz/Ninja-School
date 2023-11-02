using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Parent variables")]
    [SerializeField] private Animator anim;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private TextCombat textCombatPrefabs;

    private float hp;
    private string currentAnimName;
    protected bool IsDead => hp <= 0;
    public float Hp
    {
        get => hp;
        set => hp = value;
    }

    #region Unity Functions

    private void Start()
    {
        OnInit();
    }

    #endregion

    #region Base Functions

    protected virtual void OnInit()
    {
        InitProperties();
        healthBar.OnInit(hp);
    }
    protected virtual void InitProperties() { }
    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 1f);
    }
    public virtual void OnDespawn() { }

    #endregion

    #region Other Functions

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName == animName)
        {
            return;
        }
        
        anim.ResetTrigger(animName);
        currentAnimName = animName;
        anim.SetTrigger(animName);
    }
    public void OnHit(float damage)
    {
        if (IsDead)
        {
            return;
        }

        hp -= damage;

        if (IsDead)
        {
            hp = 0;
            OnDeath();
        }
            
        healthBar.SetNewHp(hp);
        Instantiate(textCombatPrefabs, transform.position, Quaternion.identity).OnInit(damage);
    }

    #endregion
    
}