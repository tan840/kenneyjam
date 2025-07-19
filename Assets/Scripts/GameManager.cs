using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

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
    UIManager m_UIManager;
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
        m_UIManager = UIManager.Instance;
        CurrentState = GameState.MainMenu;
        SetState(CurrentState);
    }
    public void SetState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                m_UIManager.ShowMainMenu();
                break;
            case GameState.GameScene:
                m_UIManager.ShowGameScene();
                break;
            case GameState.Pause:
                m_UIManager.ShowPauseMenu();
                break;
            case GameState.GameOver:
                m_UIManager.ShowGameOverScene();
                break;
            default:
                break;
        }
        Debug.Log("Current State: " + newState);
    }
}
