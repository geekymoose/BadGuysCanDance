using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    private SnapGridMovement movement;

    private void Start()
    {
        this.movement = this.GetComponent<SnapGridMovement>();
        Assert.IsNotNull(this.movement, "Missing asset in component");
    }

    public void OnInputMove(InputAction.CallbackContext context)
    {
        Vector2 directionVector = context.ReadValue<Vector2>();
        this.movement.Move(directionVector);
    }
}

