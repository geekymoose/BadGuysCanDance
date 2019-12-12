using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public GameEvent gameOverEvent;

    public Transform[] listSpawnPoints;
    public GameObject characterPrefab;
    public CharacterData[] listCharacterData;

    private Conductor conductor;
    private int countCharacters = 8; // Hard coded to 8 characters
    private List<Character> listCharacters;
    private int playerIndexInList;
    private SnapGridCharacter player;

    private bool areAIspaned = false;
    private float spawningAIsAccumulator = 0.0f;
    private float spawningAIsAfterSeconds = 6.0f; // Hard coded. AIs spawns in X seconds


    private void Start()
    {
        Assert.IsTrue(this.countCharacters > 1);

        this.conductor = this.GetComponent<Conductor>();
        this.listCharacters = new List<Character>(this.countCharacters);

        Assert.IsNotNull(this.listCharacterData, "Missing asset");
        Assert.IsTrue(this.listCharacterData.Length == this.countCharacters, "Invalid asset value");
        Assert.IsNotNull(this.gameOverEvent, "Missing asset");
        Assert.IsTrue(this.listSpawnPoints.Length != 0, "Missing asset");
        Assert.IsNotNull(this.characterPrefab, "Missing asset");
        Assert.IsNotNull(this.conductor, "Missing asset");

        // Generate list characters (kind of character pool)
        for(int k = 0; k < this.countCharacters; ++k)
        {
            GameObject characterObj = Instantiate(characterPrefab);
            Assert.IsNotNull(characterObj.GetComponent<Character>(), "Invalid prefab");
            this.listCharacters.Add(characterObj.GetComponent<Character>());
            characterObj.transform.position = new Vector3(50.0f, 0.0f, 0.0f); // Hack: place outside (I had issue with SetActive to false)
            characterObj.GetComponent<Character>().GiveIdentityToThisPoorCharacter(this.listCharacterData[k]);
        }

        this.SpawnRandomPlayer();

        AkSoundEngine.PostEvent("Set_State_Phase0", gameObject);
    }

    private void Update()
    {
        // Hack: See SpawnRandomPlayer
        if(this.player != null && !this.player.IsPlayerControlled())
        {
            this.player.UsePlayerControls();
        }

        // Ugly but ok for now
        if (!this.areAIspaned)
        {
            this.spawningAIsAccumulator += Time.deltaTime;
            if(this.spawningAIsAccumulator >= this.spawningAIsAfterSeconds)
            {
                AkSoundEngine.PostEvent("Set_State_Phase1", gameObject);
                this.areAIspaned = true;
                this.SpawnRandomAIs();
            }
        }
    }

    public void SpawnRandomPlayer()
    {
        int randomIndex = (int)(Random.Range(0, this.listSpawnPoints.Length - 1));
        Transform randomTransform = this.listSpawnPoints[randomIndex].transform;

        this.playerIndexInList = (int)(Random.Range(0, this.listCharacters.Count - 1));
        
        this.player = this.listCharacters[this.playerIndexInList].GetComponent<SnapGridCharacter>();
        this.player.gameObject.transform.position = randomTransform.position;
        // Hack: calling UsePlayerControls() here bugs
    }

    public void SpawnRandomAIs()
    {
        for(int k = 0; k < 7; ++k)
        {
            if(k == this.playerIndexInList)
            {
                // Bypass the player
                continue;
            }

            int index = (int)(Random.Range(0, this.listSpawnPoints.Length - 1));
            Transform randomTransform = this.listSpawnPoints[index].transform;

            this.listCharacters[k].gameObject.transform.position = randomTransform.position;

            // Add in synchro conductor
            this.conductor.AddBrick(new Brick_SnapGridMoveAI(this.listCharacters[k].GetComponent<SnapGridAIController>()));
        }
    }

    public void OnPlayerKilledEvent()
    {
        // There is only one player, so on kill, game is over
        Debug.Log("OnPlayerKilledEvent received");
        countCharacters--;
        this.gameOverEvent.Raise();
        AkSoundEngine.PostEvent("Set_State_End", gameObject);
    }

    public void OnAIKilledEvent()
    {
        Debug.Log("OnPlayerKilledEvent received");
        countCharacters--;
        if(countCharacters == 4)
        {
            AkSoundEngine.PostEvent("Set_State_Phase2", gameObject);
        }
        else if(countCharacters == 2)
        {
            AkSoundEngine.PostEvent("Set_State_Phase3", gameObject);
        }
    }
}
