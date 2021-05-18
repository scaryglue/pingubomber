using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float bombSize = 2;
    public int fire = 2;

    public bool started = false;

    private Vector2 movementInput = Vector2.zero;

    public int playerNumber;

    void Update()
    {
            transform.position += new Vector3(movementInput.x * moveSpeed * Time.deltaTime, movementInput.y * moveSpeed * Time.deltaTime, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

}