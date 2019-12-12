using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class CharacterCardUI : MonoBehaviour
{
    public Text textName;
    public Text textDescription;
    public Image imageCharacter;
    public Image imageCross;

    private CharacterData characterData;


    private void Start()
    {
        Assert.IsNotNull(this.textName, "Missing asset");
        Assert.IsNotNull(this.textDescription, "Missing asset");
        Assert.IsNotNull(this.imageCharacter, "Missing asset");

        this.imageCross.enabled = false;
    }

    public void SetCharacterData(CharacterData characterData)
    {
        this.characterData = characterData;
        this.textName.text = this.characterData.name;
        this.textDescription.text = this.characterData.characterDescription;
        this.imageCharacter.sprite = this.characterData.spriteIdle1;
    }

    public void DisplayCross()
    {
        this.imageCross.enabled = true;
    }
}
