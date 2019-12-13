using UnityEngine;
using UnityEngine.Assertions;

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
    DOWN,

    DIRECTIONS_COUNT // Keep last (internal use)
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

    private bool canMove = true;


    // -------------------------------------------------------------------------

    private void Start()
    {
        this.canMove = true;
        this.movementState = SnapGridMovementState.IDLE;
        
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

    private void OnEnable()
    {
        this.canMove = true;
    }

    private void OnDisable()
    {
        this.canMove = false;
    }


    // -------------------------------------------------------------------------

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
            default:
                Assert.IsTrue(false, "Unsupported direction");
                break;
        }
        return result;
    }

    public bool IsMoving()
    {
        return this.movementState == SnapGridMovementState.MOVING;
    }

    public bool CanMove()
    {
        return this.canMove;
    }

    public SnapGridMovementDirection GetRandomSnapGridDirection()
    {
        float rand = Random.Range(0, 10000); // Arbitrary high number
        SnapGridMovementDirection dir = (SnapGridMovementDirection)(rand % (float)SnapGridMovementDirection.DIRECTIONS_COUNT);
        return dir;
    }

    public bool CanMoveInDirection(SnapGridMovementDirection direction)
    {
        Vector2 moveVector = GetNormVectorFromDirection(direction) * this.snapGridData.snapDistanceInUnityUnits;

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, moveVector, this.snapGridData.snapDistanceInUnityUnits);

        if(hit.collider == null)
        {
            return true;
        }
        return false;
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
        if(this.IsMoving() || !this.canMove)
        {
            return false;
        }

        if(this.CanMoveInDirection(moveDirection))
        {
            Vector2 moveVector = GetNormVectorFromDirection(moveDirection) * this.snapGridData.snapDistanceInUnityUnits;
            Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(moveVector.x, moveVector.y, 0.0f), Color.yellow, 0.5f);

            this.moveReloadAccumulatorInSec = this.moveDurationInSec;
            this.movementState = SnapGridMovementState.MOVING;
            this.transform.Translate(moveVector);
            return true;
        }
        
        return false;
    }
}
