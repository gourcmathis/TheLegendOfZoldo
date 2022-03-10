using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Inventory : MonoBehaviour
{
    public int gemsCount;
    public Text gemsCountText;

    public int keysCount;
    public Text keysCountText;

    public static Inventory instance;

    public LevelLoader levelLoader;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory");
            return;
        }

        instance = this;
    }

    public void AddGems(int count)
    {
        gemsCount += count;
        gemsCountText.text = gemsCount.ToString();
    }

    public void AddKeys(int count)
    {
        keysCount += count;
        keysCountText.text = keysCount.ToString();
    }

    public void UseKeys()
    {
        keysCount -= 1;
        keysCountText.text = keysCount.ToString();
    }

    public void Victory()
    {
        if(gemsCount == 10)
        {
            levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
            levelLoader.LoadLevel(2);
        }
        
    }
}
