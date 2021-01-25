using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : Interactible
{
    public static List<CharMovement> characters = new List<CharMovement>();

    [SerializeField] public float speed = 1,
        animSpeed = 0.5f,
        maxHealth,
        agro = 10,
        attackCooldown = 1,
        attackRange = 5;
    [SerializeField] GameObject selectionRing;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected string attack;

    public FillBar healthBar;

    protected CharacterController controller;
    protected Animator anim;
    public Vector3 target = Vector3.zero;
    public CharMovement targetChar;
    protected bool selected = false;
    public bool isDead = false;
    public bool isEnemy = false;
    protected string selector;

    float lastAttack;

    // Start is called before the first frame update
    void Start()
    {
        characters.Add(this);

        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        //set up instance of selection ring
        selectionRing = GameObject.Instantiate(selectionRing, transform.position, Quaternion.identity, transform);
        selectionRing.transform.localScale = Vector3.one * 80;
        selectionRing.transform.localEulerAngles = new Vector3(-90, 0, 0);
        selectionRing.SetActive(false);

        //set up instance of health bar
        healthBar = GameObject.Instantiate(healthBar, transform.position, Quaternion.identity, transform);
        healthBar.transform.localScale = Vector3.one;
        healthBar.transform.localPosition = Vector3.up * 2.2f;
        healthBar.fullValue = maxHealth;
        healthBar.currentValue = 0;
        healthBar.UpdateValue(maxHealth);

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            controller.Move(Vector3.zero);
            return;
        }
        if (!string.IsNullOrEmpty(selector) && Input.GetKeyDown(selector) && !Controls.Add && !Controls.Assign)
        {
            Interact();
        }
        if ((targetChar == null || targetChar.isDead || Vector3.Distance(targetChar.transform.position, transform.position) > attackRange) &&
            Random.value < Time.deltaTime)
        {
            CheckTargets();
            if (targetChar != null)
            {
                target = targetChar.transform.position + (attackRange * 0.8f) * (transform.position - targetChar.transform.position).normalized;
                target = new Vector3(target.x, 0, target.z);
            }
        }
        if (targetChar != null &&
            Vector3.Distance(targetChar.transform.position, transform.position) <= attackRange &&
            attackCooldown + lastAttack < Time.time)
        {
            target = new Vector3(transform.position.x, 0, transform.position.z);
            lastAttack = Time.time;
            Attack();
        }
        anim.speed = 1 / transform.localScale.x * animSpeed;
        float dist = Vector3.Distance(target, new Vector3(transform.position.x, 0, transform.position.z));
        Vector3 dir = (target - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
        if (targetChar != null && Vector3.Distance(targetChar.transform.position, transform.position) < agro)
        {
            transform.LookAt(new Vector3(targetChar.transform.position.x, transform.position.y, targetChar.transform.position.z));
        }
        else if (dist > 0.5f)
        {
            transform.LookAt(transform.position + dir);
        }
        float currentSpeed = Mathf.Min(speed, dist * 3) * transform.localScale.x;
        anim.SetFloat("Speed", currentSpeed / 5 * anim.speed / animSpeed / animSpeed);
        controller.Move(currentSpeed * Time.deltaTime * dir * anim.speed / animSpeed);
        if (selected)
        {
            //if selected do my special actions
            WhileSelected();
        }

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    protected virtual void CheckTargets()
    {
        float dist = agro;
        targetChar = null;
        foreach (var checkChar in characters)
        {
            if (checkChar.isEnemy != isEnemy && !checkChar.isDead)
            {
                float thisDist = Vector3.Distance(checkChar.transform.position, transform.position);
                if (thisDist < dist)
                {
                    dist = thisDist;
                    targetChar = checkChar;
                }
            }
        }
    }

    public void FootL()
    {
        //Debug.Log("FootL");
    }
    public void FootR()
    {
        //Debug.Log("FootR");
    }

    public override void Interact()
    {
        Debug.Log("Interact");
        if (isDead)
        {
            return;
        }
        if (isEnemy)
        {
            ManagementControls.Instance.TargetEnemy(this);
        }
        else
        {
            ManagementControls.Instance.SelectChar(this);
        }
    }

    public void Select()
    {
        if (isDead)
        {
            return;
        }
        selected = true;
        selectionRing.SetActive(true);
    }
    public void Deselect()
    {
        selected = false;
        selectionRing.SetActive(false);
    }

    public void UpdateHealth(float change)
    {
        if (isDead)
        {
            return;
        }
        healthBar.UpdateValue(change);
        if (healthBar.currentValue <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Deselect();
        isDead = true;
    }

    protected virtual void Init() { }

    protected virtual void WhileSelected() { }

    protected virtual void Attack() { }

    public virtual void Hit()
    {
        GameObject go = GameObject.Instantiate(projectile, transform.position + transform.forward + Vector3.up, transform.rotation);
        DamageArea da = go.GetComponent<DamageArea>();
        if (da != null)
        {
            da.Init(this);
        }
        else
        {
            ParticleEvent pe = go.GetComponent<ParticleEvent>();
            if (pe != null)
            {
                pe.Init(this);
            }
        }
    }

    public virtual void Revive() { }

    private void OnDestroy()
    {
        characters.Remove(this);
    }
}