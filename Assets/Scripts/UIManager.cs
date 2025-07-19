using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameScene;
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject gameOverPannel;

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
    public void HideAllPannel()
    {
        mainMenu.gameObject.SetActive(false);
        gameScene.gameObject.SetActive(false);
        panelPause.gameObject.SetActive(false);
        gameOverPannel.gameObject.SetActive(false);
    }
}
