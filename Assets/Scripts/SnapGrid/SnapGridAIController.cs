using UnityEngine;
using UnityEngine.Assertions;

public class SnapGridAIController : MonoBehaviour
{
    private SnapGridMovement movement;
    private SnapGridMovementDirection currentDirection;

    // Keep cap u

    // Change direction if: bit reach / cannot do current move

    private void Start()
    {
        this.movement = this.GetComponent<SnapGridMovement>();
        Assert.IsNotNull(this.movement, "Missing asset in component");
    }

    private void FixedUpdate()
    {
        // Move each 1 sec
        if(Time.time % 1 == 0)
        {
            SnapGridMovementDirection randomDirection = this.movement.GetRandomSnapGridDirection();
            this.movement.Move(randomDirection);
        }
    }
}

