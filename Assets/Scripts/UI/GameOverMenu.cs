using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameManager gameManager;
    public CharacterCardUI[] listCharacterCards;

    public GameObject gameOverHUD;
    public Animation backgroundAnim;
    public Animation cardAnim;

    private void Start()
    {
        Assert.IsNotNull(this.gameManager, "Missing asset");
        Assert.IsNotNull(this.listCharacterCards, "Missing asset");

        Assert.IsNotNull(this.backgroundAnim, "Missing asset");
        Assert.IsNotNull(this.cardAnim, "Missing asset");
        Assert.IsNotNull(this.gameOverHUD, "Missing asset");

        // Default
        this.gameOverHUD.SetActive(false);
    }

    public void DisplayGameOverHUD()
    {
        Debug.Log("DisplayGameOverHUD");

        // Set cards content (in an ugly way, with ugly dependency with gamemanager)
        Assert.IsTrue(this.listCharacterCards.Length >= this.gameManager.GetListCharacters().Count, "Invalid data");
        for (int k = 0; k < this.listCharacterCards.Length; ++k)
        {
            this.listCharacterCards[k].SetCharacterData(this.gameManager.GetListCharacters()[k].GetCharacterData());
            if (this.gameManager.GetListCharacters()[k].IsKilled())
            {
                this.listCharacterCards[k].DisplayCross();
            }
        }

        this.gameOverHUD.SetActive(true);
        this.backgroundAnim.Play();
        this.cardAnim.Play();
    }

    public void HideGameOverHUD()
    {
        Debug.Log("HideGameOverHUD");
        this.gameOverHUD.SetActive(false);
    }

    public void GoBackToWelcomePage()
    {
        Debug.Log("Go back to welcome page");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
