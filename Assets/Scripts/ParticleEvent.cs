using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleEvent : MonoBehaviour
{
    ParticleSystem part;
    List<ParticleCollisionEvent> collisionEvents;
    public GameObject damageSphere;
    public CharMovement source;

    public void Init(CharMovement source)
    {
        this.source = source;
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
    
        damageSphere.SetActive(true);
        damageSphere.transform.position = collisionEvents[0].intersection;
        damageSphere.GetComponent<DamageArea>().Init(source);
        Destroy(gameObject, 2);
    }
}
