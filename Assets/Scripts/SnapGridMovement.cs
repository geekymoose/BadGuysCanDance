using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public enum SnapGridMovementState
{
    IDLE,
    MOVING
}

public enum SnapGridMovementDirection
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}

public class SnapGridMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Grid data to use for the movement (mandatory)")]
    private SnapGridData snapGridData;

    [SerializeField]
    [Tooltip("Time it takes to go from current position to the next position (jump time)")]
    private float moveDurationInSec = 0.5f;
    
    private Vector3 targetPosition;
    private float moveReloadAccumulatorInSec = 0.0f; // Remaining time before next available move
    private SnapGridMovementState movementState = SnapGridMovementState.IDLE;
    private Collider2D localCollider; // Collider of this object


    // -------------------------------------------------------------------------

    private void Start()
    {
        this.localCollider = this.GetComponentInChildren<Collider2D>();
        this.movementState = SnapGridMovementState.IDLE;

        Assert.IsNotNull(this.localCollider, "Missing asset in component");
        Assert.IsNotNull(this.snapGridData, "Missing asset in component");
    }

    private void Update()
    {
        this.moveReloadAccumulatorInSec -= Time.fixedDeltaTime;
        if (this.moveReloadAccumulatorInSec <= 0.0f)
        {
            this.movementState = SnapGridMovementState.IDLE;
        }
        else
        {
            this.movementState = SnapGridMovementState.MOVING;
        }
    }

    public bool IsMoving()
    {
        return this.movementState == SnapGridMovementState.MOVING;
    }

    private Vector2 GetNormVectorFromDirection(SnapGridMovementDirection moveDirection)
    {
        Vector2 result = new Vector2(0.0f, 0.0f);
        switch(moveDirection)
        {
            case SnapGridMovementDirection.LEFT:
                result.x = -1.0f;
                break;
            case SnapGridMovementDirection.RIGHT:
                result.x = 1.0f;
                break;
            case SnapGridMovementDirection.UP:
                result.y = 1.0f;
                break;
            case SnapGridMovementDirection.DOWN:
                result.y = -1.0f;
                break;
        }
        return result;
    }

    public bool Move(Vector2 moveVectDirection)
    {
        moveVectDirection.Normalize();
        if(moveVectDirection == Vector2.zero)
        {
            return false;
        }
        
        float dotproduct = Vector2.Dot(Vector2.right, moveVectDirection);
        SnapGridMovementDirection resultMoveDirection;
        // Dev note: using the property a.b = |a| * |b| * cos(ab) may have been cleaner probably (here, |a| * |b| = 1 since normalized)
        
        // RIGHT or LEFT
        if(dotproduct > 0.0f && moveVectDirection.x > Mathf.Abs(moveVectDirection.y))
        {
            resultMoveDirection = SnapGridMovementDirection.RIGHT;
        }
        else if(dotproduct < 0.0f && -moveVectDirection.x > Mathf.Abs(moveVectDirection.y))
        {
            resultMoveDirection = SnapGridMovementDirection.LEFT;
        }
        // Here, we know it is not LEFT or RIGHT (so it is UP or DOWN)
        else if (moveVectDirection.y > 0)
        {
            resultMoveDirection = SnapGridMovementDirection.UP;
        }
        else
        {
            resultMoveDirection = SnapGridMovementDirection.DOWN;
        }

        return this.Move(resultMoveDirection);
    }

    public bool Move(SnapGridMovementDirection moveDirection)
    {
        if(this.IsMoving())
        {
            return false;
        }

        Vector2 moveVector = GetNormVectorFromDirection(moveDirection) * this.snapGridData.snapDistanceInUnityUnits;
        Debug.Log("Move vector debug (for direction " + moveDirection + "): " + moveVector);
        
        // Check that the target position is not already in use (this is pretty ugly but it's a gamejam)
        Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(moveVector.x, moveVector.y, 0.0f), Color.yellow, 0.5f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, moveVector, this.snapGridData.snapDistanceInUnityUnits);

        bool canMove = true;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == this.localCollider)
            {
                continue;
            }
            else
            {
                canMove = false;
                break;
            }
        }
        if (canMove)
        {
            this.moveReloadAccumulatorInSec = this.moveDurationInSec;
            this.movementState = SnapGridMovementState.MOVING;
            this.transform.Translate(moveVector);
        }

        return true;
    }
}
