using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick_SnapGridBeatAnim : Brick
{
    private Animation animBeat;

    public Brick_SnapGridBeatAnim(Animation anim)
    {
        this.animBeat = anim;
    }

    public void ApplyBeat()
    {
        this.animBeat.Play();
    }

    public void ApplyBar()
    {
        // Nothing
    }
}
