using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlSys : MonoBehaviour
{
    public PlayerInput m_PlayerInput;

    public Vector3 rawInputMovement;
    public float speed = 2;
    public static Vector3 MousePosition;

    public Rigidbody m_Rigibody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Rigibody.velocity = rawInputMovement * speed;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 moveMent = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(moveMent.x, 0, moveMent.y);
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Debug.Log("attack");
        }
    }

    public void OnMousePosition(InputAction.CallbackContext value)
    {
        Vector2 pos = value.ReadValue<Vector2>();
        MousePosition = new Vector3(pos.x, pos.y);
    }
}
