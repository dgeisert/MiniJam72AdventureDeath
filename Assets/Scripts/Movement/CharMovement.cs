using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : Interactible
{
    [SerializeField] float speed = 1, animSpeed = 0.5f, maxHealth;
    [SerializeField] GameObject selectionRing;
    [SerializeField] FillBar healthBar;
    [SerializeField] string selector;

    CharacterController controller;
    Animator anim;
    public Vector3 target = Vector3.zero;
    bool selected = false;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            controller.Move(Vector3.zero);
            return;
        }
        if (Input.GetKeyDown(selector))
        {
            Interact();
        }
        anim.speed = 1 / transform.localScale.x * animSpeed;
        float dist = Vector3.Distance(target, new Vector3(transform.position.x, 0, transform.position.z));
        Vector3 dir = (target - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
        if (dist > 0.5f)
        {
            transform.LookAt(transform.position + dir);
        }
        float currentSpeed = Mathf.Min(speed, dist * 3) * transform.localScale.x;
        anim.SetFloat("Speed", currentSpeed / 5 * anim.speed / animSpeed / animSpeed);
        controller.Move(currentSpeed * Time.deltaTime * dir * anim.speed / animSpeed);
        if (selected)
        {
            //if selected do my special actions
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
        ManagementControls.Instance.SelectChar(this);
    }

    public void Select()
    {
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
        healthBar.UpdateValue(change);
        if (healthBar.currentValue <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Deselect();
        Debug.Log(gameObject.name + " is Dead");
        isDead = true;
    }

    public virtual void WhileSelected()
    {
        
    }
}