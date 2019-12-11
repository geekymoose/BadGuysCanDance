using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class SnapGridCharacter : MonoBehaviour
{
    public GameEvent eventAIKilled;
    public GameEvent eventPlayerKilled;

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
        
        Assert.IsNotNull(this.eventPlayerKilled, "Missing asset");
        Assert.IsNotNull(this.eventAIKilled, "Missing asset");

        // By default, use AI
        this.UseAIControls();
    }

    public void UseAIControls()
    {
        this.AIController.enabled = true;
        this.playerController.enabled = false;
        this.playerInput.enabled = false;
    }

    public void UsePlayerControls()
    {
        this.AIController.enabled = false;
        this.playerController.enabled = true;
        this.playerInput.enabled = true;
    }

    public bool IsAIControls()
    {
        return this.AIController.enabled == true;
    }

    public bool IsPlayerControlled()
    {
        return this.playerController.enabled == true;
    }

    public void Kill()
    {
        if(this.IsAIControls())
        {
            this.eventAIKilled.Raise();
        }
        else if(this.IsPlayerControlled())
        {
            this.eventPlayerKilled.Raise();
        }

        // TODO play sounds
        this.AIController.enabled = false;
        this.playerController.enabled = false;
        this.playerInput.enabled = false;

        this.transform.gameObject.SetActive(false);
    }
}
