using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] float range, damage, dropoff;
    public void Init(CharMovement source)
    {
        foreach (var c in CharMovement.characters)
        {
            if (damage < 0 ? (c.isEnemy == source.isEnemy) : (c.isEnemy != source.isEnemy))
            {
                float dist = Vector3.Distance(
                    new Vector3(transform.position.x, 0, transform.position.z),
                    new Vector3(c.transform.position.x, 0, c.transform.position.z));
                if (dist < range)
                {
                    c.UpdateHealth(-damage * (1 - dist / range * dropoff));
                }
            }
        }
        Destroy(gameObject);
    }
}