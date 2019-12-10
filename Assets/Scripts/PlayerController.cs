using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    public float moveDistanceInUnityUnits = 100.0f;
    public float moveDurationInSec = 0.5f;

    private float moveReloadAccumulatorInSec = 0.0f;
    private Vector2 moveDirection;

    private Collider2D playerCollider;

    private void Start()
    {
        this.playerCollider = this.GetComponentInChildren<Collider2D>();
        Assert.IsNotNull(this.playerCollider, "Missing Collider");
    }

    public void OnInputMove(InputAction.CallbackContext context)
    {
        this.moveDirection = context.ReadValue<Vector2>();
        this.moveDirection.Normalize();

        // TODO allow only Vertical or Horizontal movement (no diagonal)
        // TODO move in fixed update

        if(this.moveDirection != Vector2.zero && this.moveReloadAccumulatorInSec <= 0.0f)
        {
            Vector2 movementVector = this.moveDirection * this.moveDistanceInUnityUnits;

            Debug.DrawLine(this.transform.position, new Vector3(movementVector.x, movementVector.y, 0.0f) +  this.transform.position, Color.yellow, 0.5f);
            RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, this.moveDirection, this.moveDistanceInUnityUnits);

            // This is ugly but it's a gamejam
            bool canMove = true;
            foreach(RaycastHit2D hit in hits)
            {
                if(hit.collider == this.playerCollider)
                {
                    continue;
                }
                else
                {
                    canMove = false;
                    break;
                }
            }
            if(canMove)
            {
                this.moveReloadAccumulatorInSec = this.moveDurationInSec;
                this.transform.Translate(this.moveDirection * this.moveDistanceInUnityUnits);
            }
        }
    }

    private void FixedUpdate()
    {
        this.moveReloadAccumulatorInSec -= Time.fixedDeltaTime;
    }
}

