using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveDistanceInUnityUnits = 100.0f;
    public float moveDurationInSec = 0.5f;

    private float moveReloadAccumulatorInSec = 0.0f;
    private Vector2 moveDirection;

    public void OnInputMove(InputAction.CallbackContext context)
    {
        this.moveDirection = context.ReadValue<Vector2>();
        this.moveDirection.Normalize();

        if(this.moveDirection != Vector2.zero && this.moveReloadAccumulatorInSec <= 0.0f)
        {
            this.moveReloadAccumulatorInSec = this.moveDurationInSec;
            this.transform.Translate(this.moveDirection * this.moveDistanceInUnityUnits * Time.fixedDeltaTime);
        }
    }

    private void FixedUpdate()
    {
        this.moveReloadAccumulatorInSec -= Time.fixedDeltaTime;
    }
}

