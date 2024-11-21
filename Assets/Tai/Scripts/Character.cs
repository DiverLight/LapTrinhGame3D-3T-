using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public CharacterController characterController; // Khai báo compnent

    public float speed = 2f; // vận tốc chuyển động
    public Vector3 movementVelocity; // vector vận tốc chuyển động
    public PlayerInput playerInput; // khai báo component

    public Animator anim;

    public DamageZone damageZone;

    public Health_ health_;


    //State Machine
    public enum CharacterState
    {
        Normal,
        Attack,
        Die
    }
    public CharacterState currentState; // trạng thái hiện tại 




    void FixedUpdate()
    {
        if (health_.currentHP <= 0)
        {
            ChangeState(CharacterState.Die);
            return;
        }

        switch (currentState)
        {
            case CharacterState.Normal:
                CalculateMovement();
                break;
            case CharacterState.Attack:
                break;
        }

        characterController.Move(movementVelocity);

    }

    void CalculateMovement()
    {
        if (playerInput.attackInput)
        {
            ChangeState(CharacterState.Attack);
            anim.SetFloat("Speed", 0);
            return;
        }

        movementVelocity.Set(playerInput.horizontalInput, 0, playerInput.verticalInput);
        movementVelocity.Normalize(); // chuẩn hóa vector 
        movementVelocity = Quaternion.Euler(0, -45, 0) * movementVelocity;
        movementVelocity *= speed * Time.deltaTime;

        anim.SetFloat("Speed", movementVelocity.magnitude);



        // Xoay hướng mặt nhân vật 
        if (movementVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity);
        }
    }

    // chuyển đổi trạng thái 
    private void ChangeState(CharacterState newState)
    {
        // xóa các animation trước đó (cache)
        playerInput.attackInput = false;

        // exit current state
        switch (currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                break;
        }
        // enter new state
        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                anim.SetTrigger("Attack");
                break;
            case CharacterState.Die:

                ////roi kiem va khien
                //sword.transform.SetParent(null);
                //shield.transform.SetParent(null);

                ////roi xuong dat

                //sword.AddComponent<Rigidbody>().isKinematic = false;

                //shield.AddComponent<Rigidbody>().isKinematic = false;

                Debug.Log("Playing death animation...");
                anim.SetTrigger("Die");
                break;
        }

        // update currentState
        currentState = newState;

    }

    public void OnAttack01End()
    {
        Debug.Log("OnAttack01End....");
        ChangeState(CharacterState.Normal);
    }

    public void BeginAttack()
    {
        damageZone.BeginAttack();
    }
    public void EndAttack()
    {
        damageZone.EndAttack();
    }


}
