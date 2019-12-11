using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public List<SnapGridAIController> listAIPlayers;

    private SnapGridCharacter player;


    private void Start()
    {
        Assert.IsTrue(this.listAIPlayers.Count == 8, "Missing asset (8 characters expected)");

        // Select a random player
        float randValue = Random.Range(0, 10000);
        int indice = (int)(randValue % 8);
        this.player = this.listAIPlayers[indice].GetComponent<SnapGridCharacter>();
        this.player.UsePlayerControls();
        this.listAIPlayers.RemoveAt(indice);
    }

    public void MoveAllAIs()
    {
        foreach(SnapGridAIController currentAI in this.listAIPlayers)
        {
            currentAI.Move();
        }
    }

    public void ChangeAllAIsDirections()
    {
        foreach (SnapGridAIController currentAI in this.listAIPlayers)
        {
            currentAI.ChangeRandomMovementNewDirection();
        }
    }
}
