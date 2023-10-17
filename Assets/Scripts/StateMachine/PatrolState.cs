using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private float timer;
    private float randomTime;
    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(4f, 6f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (enemy.Target != null)
        {
            // quay mat ve phia player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);

            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }
        }
        else
        {
            if (timer < randomTime)
            {
                enemy.Moving();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }
        
        
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
