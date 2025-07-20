using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameScene;
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject gameOverPannel;
    [SerializeField] GameObject gameWonPannel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void ShowMainMenu()
    {
        HideAllPannel();
        mainMenu.gameObject.SetActive(true);
    }
    public void ReplayLevel()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ShowGameScene()
    {
        HideAllPannel();
        gameScene.gameObject.SetActive(true);
    }
    public void ShowPauseMenu()
    {
        HideAllPannel();
        panelPause.gameObject.SetActive(true);
    }
    public void ShowGameOverScene()
    {
        HideAllPannel();
        gameOverPannel.gameObject.SetActive(true);
    }
    public void ShowGameWonScene()
    {
        HideAllPannel();
        gameWonPannel.gameObject.SetActive(true);
    }
    public void HideAllPannel()
    {
        mainMenu.gameObject.SetActive(false);
        gameScene.gameObject.SetActive(false);
        panelPause.gameObject.SetActive(false);
        gameOverPannel.gameObject.SetActive(false);
    }
}
