using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    public float timeToMove = 0.3f;    //time to move
    public float bombSize = 2;
    public int fire = 2;


    private Vector3 originPos;
    private Vector3 targetPos;

    private bool isMoving;

    public Tilemap tilemap;

    private Vector2 movementInput = Vector2.zero;

    public int playerNumber;

    public AudioSource moveSound;
    public AudioSource pickupSound;
    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(movementInput.x + movementInput.y));

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

        moveSound.Play();

        float elapsedTime = 0;

        originPos = transform.position;
        targetPos = originPos + movement;


        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        moveSound.Stop();

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

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Powerup")) {
            pickupSound.Play();
        }
    }

}