using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowManager : MonoBehaviour
{
    public static FlowManager instance;

    private int CurrentScene;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GotoInit()
    {
        SceneManager.LoadScene(1);

        CurrentScene = 0;

    }
    public void GotoMenu()
    {
        SceneManager.LoadScene(1);

        CurrentScene = 2;
    }
    public void GotoInGame()
    {
        CurrentScene = 3;
        SceneManager.LoadScene(1);

    }

    public int GetCurrentScene()
    {
        return CurrentScene;
    }

    public enum SceneType
    {
        Init,
        Menu,
        Game
    }
}
