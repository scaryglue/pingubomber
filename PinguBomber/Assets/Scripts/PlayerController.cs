using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float timeToMove = 0.2f;    //time to move
    public float bombSize = 2;
    public int fire = 2;


    private Vector3 originPos;
    private Vector3 targetPos;

    private bool isMoving;

    public Tilemap tilemap;

    private Vector2 movementInput = Vector2.zero;

    public int playerNumber;

    void Update()
    {
        if(isMoving)
        {
            return;
        }

        if(movementInput.x > 0 && !isMoving)
        {
            if(CanMove(Vector3.right))
                StartCoroutine(MovePlayer(Vector3.right));
        }
        else if(movementInput.x < 0 && !isMoving)
        {
            if(CanMove(Vector3.left))
                StartCoroutine(MovePlayer(Vector3.left));
        }
        else if(movementInput.y > 0 && !isMoving)
        {
            if(CanMove(Vector3.up))
                StartCoroutine(MovePlayer(Vector3.up));
        }
        else if(movementInput.y < 0 && !isMoving)
        {
            if(CanMove(Vector3.down))
                StartCoroutine(MovePlayer(Vector3.down));
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private IEnumerator MovePlayer(Vector3 movement)
    {
        isMoving = true;

        float elapsedTime = 0;

        originPos = transform.position;
        targetPos = originPos + movement;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }

    private bool CanMove(Vector3 movement)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(transform.position + movement);

        if (tilemap.HasTile(gridPosition))
        {
            return false;
        }

        return true;
    }

}