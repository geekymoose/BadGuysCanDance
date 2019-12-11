using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick_SnapGridMoveAI : Brick
{
    private SnapGridAIController controller;

    public Brick_SnapGridMoveAI(SnapGridAIController controller)
    {
        this.controller = controller;
    }

    public void ApplyBeat()
    {
        this.controller.Move();
    }

    public void ApplyBar()
    {
        this.controller.ChangeRandomMovementNewDirection();
    }
}
