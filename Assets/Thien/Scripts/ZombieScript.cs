using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target; // m?c tiêu

    public float radius = 10f; // bán kính tìm ki?m m?c tiêu
    public Vector3 originalePosition; // v? trí ban ??u
    public float maxDistance = 50f; // kho?ng cách t?i ?a
    public Health health;

    public Animator animator; // khai báo component

    public DamageZone damageZone;
    // state machine
    public enum CharacterState
    {
        Normal,
        Attack,
        Die
    }
    public CharacterState currentState; // tr?ng thái hi?n t?i


    void Start()
    {
        originalePosition = transform.position;

    }

    void Update()
    {
        if (health.currentHP <= 0)
        {
            ChangeState(CharacterState.Die);
        }
        if (target != null)
        {
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
        }
        if (currentState == CharacterState.Die)
        {
            return;
        }
        // kho?ng cách t? v? trí hi?n t?i ??n v? trí ban ??u
        var distanceToOriginal = Vector3.Distance(originalePosition, transform.position);
        // kho?ng cách t? v? trí hi?n t?i ??n m?c tiêu
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= radius && distanceToOriginal <= maxDistance)
        {
            // di chuy?n ??n m?c tiêu
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            distance = Vector3.Distance(target.position, transform.position);
            if (distance < 2f)
            {
                // t?n công
                ChangeState(CharacterState.Attack);
            }
        }

        if (distance > radius || distanceToOriginal > maxDistance)
        {
            // quay v? v? trí ban ??u
            navMeshAgent.SetDestination(originalePosition);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            // chuy?n sang tr?ng thái ??ng yên
            distance = Vector3.Distance(originalePosition, transform.position);
            if (distance < 1f)
            {
                animator.SetFloat("Speed", 0);
            }

            // bình th??ng
            ChangeState(CharacterState.Normal);
        }
    }

    // chuy?n ??i tr?ng thái
    private void ChangeState(CharacterState newState)
    {
        // exit current state
        switch (currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                break;
            case CharacterState.Die:
                break;


        }

        // enter new state
        switch (newState)
        {
            case CharacterState.Normal:
                damageZone.EndAttack();
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                damageZone.BeginAttack();
                break;
            case CharacterState.Die:
                animator.SetTrigger("Die");
                Destroy(gameObject, 5f);
                break;



                // update current state

        }
        currentState = newState;

    }
    //public override void TakeDamage(float damage)
    //{
    //    base.TakeDamage(damage);
    //    if(currentHP <= 0)
    //    {
    //        ChangeState(CharacterState.Die);
    //    }
    //}
    //public void Wander()
    //{

    //    var randomDirection = Random.insideUnitSphere * radius;
    //    randomDirection += originalePosition;
    //    NavMeshHit hit;
    //    NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
    //    var finalPosition = hit.position;
    //    navMeshAgent.SetDestination(finalPosition);
    //    animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    //}

}