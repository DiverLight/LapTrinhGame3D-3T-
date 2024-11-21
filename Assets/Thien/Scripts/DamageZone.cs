
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public Collider damageCollider;
    public int damageAmount = 20;
    public string targetTag;
    public List<Collider> colliderTarget = new List<Collider>();

    //public bool flag = false;
    void Start()
    {
        damageCollider.enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag) && !colliderTarget.Contains(other) /*&& !flag*/)
        {
            colliderTarget.Add(other);
            var go = other.GetComponent<Health_>();
            if (go != null)
            {
                go.TakeDamage(damageAmount);
            }
            //flag = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag) && !colliderTarget.Contains(other))
        {
            colliderTarget.Add(other);
            var go = other.GetComponent<Health_>();
            if (go != null)
            {
                go.TakeDamage(damageAmount);
            }

        }
    }
    public void BeginAttack()
    {
        //flag = true;
        colliderTarget.Clear();
        damageCollider.enabled = true;
    }
    public void EndAttack()
    {
        //flag = false;
        colliderTarget.Clear();
        damageCollider.enabled = false;
    }
}
