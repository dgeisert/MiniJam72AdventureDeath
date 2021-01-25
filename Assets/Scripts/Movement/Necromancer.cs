using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : CharMovement
{
    [SerializeField] GameObject raiseEffect;
    float raiseCooldown;

    protected override void Init()
    {
        selector = "1";
        Interact();
        target = new Vector3(transform.position.x, 0, transform.position.z);
    }
    protected override void Attack()
    {
        anim.SetTrigger(attack);
    }
    protected override void Die()
    {
        Game.Instance.Victory();
    }
    protected override void WhileSelected()
    {
        if (Controls.Raise && raiseCooldown + 2 < Time.time)
        {
            raiseCooldown = Time.time;
            raiseEffect.SetActive(false);
            raiseEffect.SetActive(true);
            foreach (var c in CharMovement.characters)
            {
                if (c.isDead && c.isEnemy)
                {
                    float dist = Vector3.Distance(
                        new Vector3(transform.position.x, 0, transform.position.z),
                        new Vector3(c.transform.position.x, 0, c.transform.position.z));
                    if (dist < 3)
                    {
                        c.Revive();
                    }
                }
            }
        }
    }

    public override void Hit()
    {
        base.Hit();
    }
}