using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<SnapGridAIController> AIPlayers;
    public SnapGridPlayerController player;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if(Time.time % 1 == 0)
        {
            //this.MoveAllAIs();
        }
    }

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
