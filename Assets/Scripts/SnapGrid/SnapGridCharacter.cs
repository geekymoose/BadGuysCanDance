using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class SnapGridCharacter : MonoBehaviour
{
    private SnapGridAIController AIController;
    private SnapGridPlayerController playerController;
    private PlayerInput playerInput;


    private void Start()
    {
        this.AIController = this.GetComponent<SnapGridAIController>();
        this.playerController = this.GetComponent<SnapGridPlayerController>();
        this.playerInput = this.GetComponent<PlayerInput>();

        Assert.IsNotNull(this.AIController, "Missing asset");
        Assert.IsNotNull(this.playerController, "Missing asset");
        Assert.IsNotNull(this.playerInput, "Missing asset");

        // By default, use AI
        this.UseAIControls();
    }

    private void UseAIControls()
    {
        this.AIController.enabled = true;
        this.playerController.enabled = false;
        this.playerInput.enabled = false;
    }

    private void usePlayerControls()
    {
        this.AIController.enabled = false;
        this.playerController.enabled = true;
        this.playerInput.enabled = true;
    }
}
