using UnityEngine;
using UnityEngine.Assertions;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverHUD;
    public Animation backgroundAnim;
    public Animation cardAnim;

    private void Start()
    {
        Assert.IsNotNull(this.backgroundAnim, "Missing asset");
        Assert.IsNotNull(this.cardAnim, "Missing asset");
        Assert.IsNotNull(this.gameOverHUD, "Missing asset");

        // Default
        this.gameOverHUD.SetActive(false);
    }

    public void DisplayGameOverHUD()
    {
        Debug.Log("DisplayGameOverHUD");

        this.gameOverHUD.SetActive(true);
        this.backgroundAnim.Play();
        this.cardAnim.Play();
    }

    public void HideGameOverHUD()
    {
        Debug.Log("HideGameOverHUD");
        this.gameOverHUD.SetActive(false);
    }
}
