using UnityEngine;
using UnityEngine.Assertions;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverHUD;

    private Animation anim;

    private void Start()
    {
        this.anim = this.GetComponent<Animation>();

        Assert.IsNotNull(this.anim, "Missing asset");
        Assert.IsNotNull(this.gameOverHUD, "Missing asset");

        // Default
        this.gameOverHUD.SetActive(false);
    }

    public void DisplayGameOverHUD()
    {
        Debug.Log("DisplayGameOverHUD");
        this.gameOverHUD.SetActive(true);
        this.anim.Play();
    }

    public void HideGameOverHUD()
    {
        Debug.Log("HideGameOverHUD");
        this.gameOverHUD.SetActive(false);
    }
}
