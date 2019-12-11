using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    private List<Brick> listBrick;

    public void Start()
    {
        this.listBrick = new List<Brick>();
    }

    public void AddBrick(Brick brick)
    {
        if(!this.listBrick.Contains(brick))
        {
            this.listBrick.Add(brick);
        }
    }


    // -------------------------------------------------------------------------

    public void ApplyBeat()
    {
        foreach(Brick brick in this.listBrick)
        {
            brick.ApplyBeat();
        }
    }

    public void ApplyBar()
    {
        foreach (Brick brick in this.listBrick)
        {
            brick.ApplyBar();
        }
    }
}
