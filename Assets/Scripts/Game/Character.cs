using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Character : MonoBehaviour
{
    public GameEvent eventAIKilled;
    public GameEvent eventPlayerKilled;

    private SnapGridCharacter gridCharacterControl;
    private CharacterData characterData;

    private bool isKilled;


    // Start is called before the first frame update
    void Start()
    {
        this.gridCharacterControl = this.GetComponent<SnapGridCharacter>();

        Assert.IsNotNull(this.gridCharacterControl, "Missing asset");

        Assert.IsNotNull(this.eventPlayerKilled, "Missing asset");
        Assert.IsNotNull(this.eventAIKilled, "Missing asset");
    }

    public void Kill()
    {
        this.isKilled = true;
        if (this.gridCharacterControl.IsAIControls())
        {
            Debug.Log("Killing AI character");
            this.eventAIKilled.Raise();
        }
        else if (this.gridCharacterControl.IsPlayerControlled())
        {
            Debug.Log("Killing player character");
            this.eventPlayerKilled.Raise();
        }

        this.gameObject.SetActive(false);
    }

    public bool IsKilled()
    {
        return this.isKilled;
    }

    public void GiveIdentityToThisPoorCharacter(CharacterData characterData)
    {
        this.characterData = characterData;
    }
}
