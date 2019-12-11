using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDebugger : MonoBehaviour
{
    private void Awake()
    {
        if(!Debug.isDebugBuild)
        {
            // Script enabled ONLY in debug build
            this.gameObject.GetComponent<GameDebugger>().enabled = false;
            return;
        }
        Debug.LogWarning("GamePlayer Debugger activated");
    }
}
