using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_ : MonoBehaviour
{

    public float MaxHP;
    public float currentHP;


    private void Start()
    {
        currentHP = MaxHP;
    }
    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);

    }
}

