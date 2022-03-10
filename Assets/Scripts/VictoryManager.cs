using UnityEngine;
using UnityEngine.SceneManagement;
public class VictoryManager : MonoBehaviour
{

    public GameObject VictoryUI;

    public static VictoryManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of VictoryManager");
            return;
        }

        instance = this;
    }


    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
