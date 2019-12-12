using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    private List<Brick> listBrick;
    private bool isPaused;

    public void Start()
    {
        this.isPaused = false;
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

    public void TogglePause()
    {
        this.isPaused = !this.isPaused;
    }

    public void Pause()
    {
        this.isPaused = true;
    }

    public void RemovePause()
    {
        this.isPaused = false;
    }

    public bool IsPaused()
    {
        return this.isPaused == true;
    }

    // -------------------------------------------------------------------------

    public void ApplyBeat()
    {
        if (this.isPaused)
        {
            return;
        }

        foreach(Brick brick in this.listBrick)
        {
            brick.ApplyBeat();
        }
    }

    public void ApplyBar()
    {
        Debug.Log("BAR");
        if (this.isPaused)
        {
            return;
        }

        foreach (Brick brick in this.listBrick)
        {
            brick.ApplyBar();
        }
    }
}
