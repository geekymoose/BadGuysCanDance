using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<SnapGridAIController> AIPlayers;
    public SnapGridPlayerController player;

    public void MoveAllAIs()
    {
        foreach(SnapGridAIController currentAI in this.AIPlayers)
        {
            currentAI.Move();
        }
    }

    public void ChangeAllAIsDirections()
    {
        foreach (SnapGridAIController currentAI in this.AIPlayers)
        {
            currentAI.ChangeRandomMovementNewDirection();
        }
    }
}
