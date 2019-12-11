using UnityEngine;
using UnityEngine.Assertions;

public class SnapGridAIController : MonoBehaviour
{
    private SnapGridMovement movement;
    private SnapGridMovementDirection currentMoveDirection;


    private void Start()
    {
        this.movement = this.GetComponent<SnapGridMovement>();
        Assert.IsNotNull(this.movement, "Missing asset in component");

        this.currentMoveDirection = this.movement.GetRandomSnapGridDirection();
    }

    public void ChangeRandomMovementNewDirection()
    {
        SnapGridMovementDirection newMovementDirection = this.movement.GetRandomSnapGridDirection();
        while (newMovementDirection == this.currentMoveDirection)
        {
            newMovementDirection = this.movement.GetRandomSnapGridDirection();
        }
        this.currentMoveDirection = newMovementDirection;
    }

    public void Move()
    {
        bool moveDone = this.movement.Move(this.currentMoveDirection);
        if (!moveDone)
        {
            // Tries to change direction if cannot move with the old direction (try only once)
            this.ChangeRandomMovementNewDirection();
            this.movement.Move(this.currentMoveDirection);
        }
    }
}

