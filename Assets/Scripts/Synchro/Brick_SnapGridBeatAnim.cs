using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick_SnapGridBeatAnim : Brick
{
    private Animation animBeat;
    private Animation animBar;

    public Brick_SnapGridBeatAnim(Animation animBeat, Animation animBar)
    {
        this.animBeat = animBeat;
        this.animBar = animBar;
    }

    public void ApplyBeat()
    {
        this.animBeat.Play();
    }

    public void ApplyBar()
    {
        this.animBar.Play();
    }
}
