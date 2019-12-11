using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public List<SnapGridAIController> listAIPlayers;
    public GameEvent gameOverEvent;

    private Conductor conductor;
    private SnapGridCharacter player;
    private int countCharacters = 8; // Hard coded to 8 characters


    private void Start()
    {
        this.conductor = this.GetComponent<Conductor>();

        Assert.IsTrue(this.listAIPlayers.Count == countCharacters, "Missing asset (wrong value)");
        Assert.IsNotNull(this.gameOverEvent, "Missing asset");
        Assert.IsNotNull(this.conductor, "Missing asset");

        // Select a random player
        float randValue = Random.Range(0, 10000);
        int indice = (int)(randValue % countCharacters);
        this.player = this.listAIPlayers[indice].GetComponent<SnapGridCharacter>();
        this.player.UsePlayerControls();
        this.listAIPlayers.RemoveAt(indice);

        // Place All AI in brick
        foreach (SnapGridAIController currentAI in this.listAIPlayers)
        {
            this.conductor.AddBrick(new Brick_SnapGridMoveAI(currentAI));
        }
    }

    public void OnPlayerKilledEvent()
    {
        // There is only one player, so on kill, game is over
        Debug.Log("OnPlayerKilledEvent received");
        countCharacters--;
        this.gameOverEvent.Raise();
    }

    public void OnAIKilledEvent()
    {
        countCharacters--;
        Debug.Log("OnPlayerKilledEvent received");
    }
}
