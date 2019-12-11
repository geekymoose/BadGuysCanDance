using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Beat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBeat()
    {
        AkSoundEngine.PostEvent("Play_Beep", gameObject);
    }
}
