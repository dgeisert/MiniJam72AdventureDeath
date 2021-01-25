using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAdventurer : CharMovement
{
    [SerializeField] GameObject charSkin, minionSkin;
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject undeadProjectile;
    Coroutine slowDeath;
    protected override void Init()
    {
        weapons.Random1().SetActive(true);
    }
    protected override void Attack()
    {
        anim.SetTrigger(attack);
    }
    protected override void Die()
    {
        controller.enabled = false;
        base.Die();
        anim.SetTrigger("Die");
        if (!isEnemy)
        {
            StartCoroutine(FinalDeath());
        }
        else
        {
            Game.Score += 50;
            slowDeath = StartCoroutine(SlowDeath());
        }
    }
    protected override void WhileSelected()
    {
        if (!isEnemy && !isDead)
        {
            if (Controls.Add2)
            {
                selector = "2";
            }
            if (Controls.Add3)
            {
                selector = "3";
            }
            if (Controls.Add4)
            {
                selector = "4";
            }
            if (Controls.Add5)
            {
                selector = "5";
            }
            if (Controls.Add6)
            {
                selector = "6";
            }
            if (Controls.Add7)
            {
                selector = "7";
            }
            if (Controls.Add8)
            {
                selector = "8";
            }
            if (Controls.Add9)
            {
                selector = "9";
            }
            if (Controls.Add0)
            {
                selector = "0";
            }
        }
    }

    public override void Hit()
    {
        base.Hit();
    }

    IEnumerator FinalDeath()
    {

        yield return new WaitForSeconds(1.5f);
        float t = 2;
        while (t > 0)
        {
            t -= Time.deltaTime;
            transform.position -= Vector3.up * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    IEnumerator SlowDeath()
    {

        yield return new WaitForSeconds(25f);
        float t = 2;
        while (t > 0)
        {
            t -= Time.deltaTime;
            transform.position -= Vector3.up * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    public override void Revive()
    {
        Game.Score += 100;
        controller.enabled = true;
        if (slowDeath != null)
        {
            StopCoroutine(slowDeath);
        }
        anim.SetTrigger("Revive");
        charSkin.SetActive(false);
        minionSkin.SetActive(true);
        isEnemy = false;
        isDead = false;
        maxHealth *= 0.5f;
        speed = Mathf.Max(2, speed - 1);
        animSpeed += 1;
        healthBar.currentValue = 0;
        UpdateHealth(maxHealth);
        if (undeadProjectile != null)
        {
            projectile = undeadProjectile;
            attackRange = 1.5f;
            attack = "Sword";
        }
    }
    protected override void CheckTargets()
    {
        if (attack == "Heal")
        {
            float health = 1;
            targetChar = null;
            foreach (var checkChar in characters)
            {
                if (checkChar.isEnemy == isEnemy && !checkChar.isDead)
                {
                    float thisHealth = checkChar.healthBar.Percent;
                    if (thisHealth < health)
                    {
                        health = thisHealth;
                        targetChar = checkChar;
                    }
                }
            }
        }
        else
        {
            base.CheckTargets();
        }
    }

    public void WeaponSwitch()
    {
        GameObject go = GameObject.Instantiate(projectile, transform.position + transform.forward + Vector3.up, transform.rotation);
        DamageArea da = go.GetComponentInChildren<DamageArea>();
        if (da != null)
        {
            da.Init(this);
        }
    }
}