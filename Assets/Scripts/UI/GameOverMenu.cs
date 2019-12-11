using UnityEngine;
using UnityEngine.Assertions;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverHUD;

    private void Start()
    {
        Assert.IsNotNull(this.gameOverHUD, "Missing asset");

        // Default
        this.gameOverHUD.SetActive(false);
    }

    public void DisplayGameOverHUD()
    {
        Debug.Log("DisplayGameOverHUD");
        this.gameOverHUD.SetActive(true);
    }

    public void HideGameOverHUD()
    {
        Debug.Log("HideGameOverHUD");
        this.gameOverHUD.SetActive(false);
    }
}
