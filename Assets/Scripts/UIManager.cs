using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject GameScene;
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject GameOverPannel;

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

    }
    public void HideAllPannel()
    {

    }
}
