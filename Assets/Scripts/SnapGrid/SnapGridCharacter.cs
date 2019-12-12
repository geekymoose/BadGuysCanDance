using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class SnapGridCharacter : MonoBehaviour
{
    private SnapGridAIController AIController;
    private SnapGridPlayerController playerController;
    private SnapGridMovement movement;
    private PlayerInput playerInput;


    private void Start()
    {
        this.AIController = this.GetComponent<SnapGridAIController>();
        this.playerController = this.GetComponent<SnapGridPlayerController>();
        this.movement = this.GetComponent<SnapGridMovement>();
        this.playerInput = this.GetComponent<PlayerInput>();

        Assert.IsNotNull(this.AIController, "Missing asset");
        Assert.IsNotNull(this.playerController, "Missing asset");
        Assert.IsNotNull(this.movement, "Missing asset");
        Assert.IsNotNull(this.playerInput, "Missing asset");

        // By default, use AI
        this.UseAIControls();
    }

    public void UseAIControls()
    {
        this.movement.enabled = true;
        this.AIController.enabled = true;
        this.playerController.enabled = false;
        this.playerInput.enabled = false;
    }

    public void UsePlayerControls()
    {
        this.movement.enabled = true;
        this.AIController.enabled = false;
        this.playerController.enabled = true;
        this.playerInput.enabled = true;
    }

    public void DisableControl()
    {
        this.movement.enabled = false;
        this.AIController.enabled = false;
        this.playerController.enabled = false;
        this.playerInput.enabled = false;
    }

    public bool IsAIControls()
    {
        return this.AIController.enabled == true;
    }

    public bool IsPlayerControlled()
    {
        return this.playerController.enabled == true;
    }
}
