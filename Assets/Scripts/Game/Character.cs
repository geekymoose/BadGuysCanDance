using UnityEngine;
using UnityEngine.Assertions;

public class Character : MonoBehaviour
{
    public GameEvent eventAIKilled;
    public GameEvent eventPlayerKilled;

    public SpriteRenderer spriteRenderer; // Hack: GetComponentInChildren<SpriteRenderer> does not work :/
    private Collider2D collider;

    private CharacterData characterData;
    private SnapGridCharacter gridCharacterControl;

    private bool isKilled;


    // Start is called before the first frame update
    void Start()
    {
        this.gridCharacterControl = this.GetComponent<SnapGridCharacter>();
        this.collider = this.GetComponentInChildren<Collider2D>();

        Assert.IsNotNull(this.gridCharacterControl, "Missing asset");
        Assert.IsNotNull(this.collider, "Missing asset");
        Assert.IsNotNull(this.spriteRenderer, "Missing asset");

        Assert.IsNotNull(this.eventPlayerKilled, "Missing asset");
        Assert.IsNotNull(this.eventAIKilled, "Missing asset");
    }

    public void Kill()
    {
        if(this.isKilled)
        {
            // What? You wanna kill a killed guy? You're definitely ugly!
            return;
        }

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

        AkSoundEngine.PostEvent(this.characterData.soundKilledEventName, gameObject);

        this.gridCharacterControl.DisableControl();
        this.enabled = false;
        this.collider.enabled = false;
        this.spriteRenderer.sprite = this.characterData.spriteKilled;
        this.spriteRenderer.sortingOrder = 1; // Alive character displayed in fron of killed on (0 is ground)
    }

    public bool IsKilled()
    {
        return this.isKilled;
    }

    public void GiveIdentityToThisPoorCharacter(CharacterData characterData)
    {
        this.characterData = characterData;
        this.spriteRenderer.sprite = characterData.spriteIdle1;
    }

    public CharacterData GetCharacterData()
    {
        return this.characterData;
    }
}
