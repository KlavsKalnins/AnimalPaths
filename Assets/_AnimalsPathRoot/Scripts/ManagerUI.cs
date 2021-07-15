using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public static ManagerUI instance;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject guidePanel;

    private void OnEnable()
    {
        instance = this;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void SetPlayButtonStatus(bool active)
    {
        playButton.SetActive(active);
    }

    public void GuidePanel(bool active)
    {
        guidePanel.SetActive(active);
    }
}
