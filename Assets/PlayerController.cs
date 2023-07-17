using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveVector;
    private NewControls input;
    
    // Start is called before the first frame update
    void Start()
    {
        input = new NewControls();
        input.PlayerInput.Enable();

        //input.PlayerInput.positioning.started +=
        input.PlayerInput.positioning.performed += OnMoveCallback;
        input.PlayerInput.positioning.canceled += context =>
        {
            //람다식 방식
            moveVector = Vector2.zero;
            Debug.Log("Canceled");
        };
        
    }

    // Update is called once per frame
    void Update()
    {
        var moveVectorTemp = moveVector * Time.deltaTime * 1.5f; //1.5는 speed
        transform.Translate(moveVectorTemp.x, 0f, moveVectorTemp.y);
    }

    private void OnMove(InputValue value)
    {
        moveVector = value.Get<Vector2>();
    }

    public void OnMoveCallback(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
}
