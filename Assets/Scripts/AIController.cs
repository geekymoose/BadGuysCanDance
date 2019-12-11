using UnityEngine;
using UnityEngine.Assertions;

public class AIController : MonoBehaviour
{
    private SnapGridMovement movement;

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
            this.movement.MoveRandom();
        }
    }
}

