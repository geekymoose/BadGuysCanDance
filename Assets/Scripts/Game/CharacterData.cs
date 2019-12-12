using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public Sprite spriteIdle1;
    public Sprite spriteIdel2;
    public Sprite spriteKilled;

    public string characterName;
    public string characterDescription;
}
