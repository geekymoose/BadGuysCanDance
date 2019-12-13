using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class WelcomeMenu : MonoBehaviour
{
    public void Start()
    {
        AkSoundEngine.PostEvent("Play_SW_Music", gameObject);
        AkSoundEngine.PostEvent("Set_State_Menu", gameObject);
    }

    public void StartGame()
    {
        Debug.Log("Start game");
        AkSoundEngine.PostEvent("Stop_SW_Music", gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        // TODO Quit sound
        //AkSoundEngine.PostEvent("TODO", gameObject);
        Application.Quit();
    }
}
