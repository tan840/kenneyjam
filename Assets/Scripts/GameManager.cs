using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum GameState 
    {
        MainMenu,
        GameScene,
        Pause,
        GameOver
    }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        CurrentState = GameState.MainMenu;
    }
    public void SetState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.GameScene:
                break;
            case GameState.Pause:
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
        Debug.Log("Current State: " + newState);
    }
}
