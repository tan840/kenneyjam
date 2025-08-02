using System.Collections.Generic;
using TMPro;
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
        GameOver,
        GameWon
    }
    public GameState CurrentState { get; private set; }
    public int HumanKills { get => humanKills; set => humanKills = value; }
    public int ZombieKills { get => zombieKills; set => zombieKills = value; }

    UIManager m_UIManager;

    [SerializeField] int humanKills = 0;
    [SerializeField] int zombieKills = 0;

    [SerializeField] TMP_Text humanKillText;
    [SerializeField] TMP_Text zombieKillText;

    public List<Character> humans = new List<Character>();
    public List<Character> zombies = new List<Character>();
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
        //CurrentState = GameState.MainMenu;
       SetState(GameState.MainMenu);
        Application.targetFrameRate = 60;
    }
    public void SetState(GameState newState)
    {
        CurrentState = newState;
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
            case GameState.GameWon:
                m_UIManager.ShowGameWonScene();
                break;
            default:
                break;
        }
        //Debug.Log("Current State: " + newState);
    }
    public void RegisterCharacter(Character character)
    {
        if (character.isZombie)
            zombies.Add(character);
        else
            humans.Add(character);
    }

    public void UnregisterCharacter(Character character)
    {
        if (character.isZombie)
            zombies.Remove(character);
        else
            humans.Remove(character);
    }
    public void AddKill(bool isZombie)
    {
        if (isZombie)
        {
            zombieKills--;
            UpdateUI();
        }
        else
        {
            humanKills--;
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if (humanKillText) humanKillText.text = $"{humanKills}";
        if (zombieKillText) zombieKillText.text = $"{zombieKills}";
        if(zombieKills <= 0)
        {
            SetState(GameState.GameOver);
        }
        else if (humanKills <= 0)
        {
            SetState(GameState.GameWon);
        }
    }

}
