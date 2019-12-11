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

    private void FixedUpdate()
    {
        // Apply move each 1 sec
        if(Time.time % 1 == 0)
        {
            bool moveDone = this.movement.Move(this.currentMoveDirection);
            if(!moveDone)
            {
                // Tries to change direction if cannot move with the old dir
                this.ChangeRandomMovementDirection();
                this.movement.Move(this.currentMoveDirection);
            }
        }

        // Change dir each 3 secs
        if(Time.time % 3 == 0)
        {
            this.ChangeRandomMovementDirection();
        }
    }

    private void ChangeRandomMovementDirection()
    {
        SnapGridMovementDirection newMovementDirection = this.movement.GetRandomSnapGridDirection();
        while (newMovementDirection == this.currentMoveDirection)
        {
            newMovementDirection = this.movement.GetRandomSnapGridDirection();
        }
        this.currentMoveDirection = newMovementDirection;
    }
}

