using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;

    // nếu nhấn chuột trái thì attack 
    public bool attackInput;
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (!attackInput && Time.timeScale != 0)
        {
            attackInput = Input.GetMouseButtonDown(0); // nhấn chuột trái, số 0 là trái, 1 là phải  
        }
    }

    private void OnDisable()
    {
        horizontalInput = 0;
        verticalInput = 0;
        attackInput = false;
    }

}
