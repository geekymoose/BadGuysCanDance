using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public GameEvent gameOverEvent;

    public Transform[] listSpawnPoints;
    public GameObject characterPrefab;

    private Conductor conductor;
    private SnapGridCharacter player;
    private int countCharacters = 8; // Hard coded to 8 characters
    
    private bool areAIspaned = false;
    private float spawningAIsAccumulator = 0.0f;
    private float spawningAIsAfterSeconds = 5.0f; // Hard coded. AIs spawns in X seconds


    private void Start()
    {
        this.conductor = this.GetComponent<Conductor>();

        Assert.IsNotNull(this.gameOverEvent, "Missing asset");
        Assert.IsTrue(this.listSpawnPoints.Length != 0, "Missing asset");
        Assert.IsNotNull(this.characterPrefab, "Missing asset");
        Assert.IsNotNull(this.conductor, "Missing asset");

        this.SpawnRandomPlayer();

        // TODO Start game sound (TMP)
        AkSoundEngine.PostEvent("Set_State_Phase1", gameObject);
    }

    private void Update()
    {
        if(this.player != null && !this.player.IsPlayerControlled())
        {
            // Hack: See SpawnRandomPlayer
            this.player.UsePlayerControls();
        }

        // Ugly but ok for now
        if (!this.areAIspaned)
        {
            this.spawningAIsAccumulator += Time.deltaTime;
            if(this.spawningAIsAccumulator >= this.spawningAIsAfterSeconds)
            {
                this.areAIspaned = true;
                this.SpawnRandomAIs();
            }
        }
    }

    public void SpawnRandomPlayer()
    {
        int index = (int)(Random.Range(0, this.listSpawnPoints.Length - 1));
        Transform randomTransform = this.listSpawnPoints[index].transform;
        GameObject playerObj = Instantiate(characterPrefab, randomTransform.position, randomTransform.rotation);
        this.player = playerObj.GetComponent<SnapGridCharacter>();
        // Cannot use this here (playerObj is not fully ready I guess.
        // playerObj.GetComponent<SnapGridCharacter>().UsePlayerControls();
    }

    public void SpawnRandomAIs()
    {
        // Nb AI == total - 1 because one player
        for(int k = 0; k < 7; ++k)
        {
            int index = (int)(Random.Range(0, this.listSpawnPoints.Length - 1));
            Transform randomTransform = this.listSpawnPoints[index].transform;
            GameObject characterAI = Instantiate(characterPrefab, randomTransform.position, randomTransform.rotation);

            // Add in synchro conductor
            this.conductor.AddBrick(new Brick_SnapGridMoveAI(characterAI.GetComponent<SnapGridAIController>()));
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
