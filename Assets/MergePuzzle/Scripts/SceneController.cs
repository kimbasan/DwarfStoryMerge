using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private PartyController heroParty;

    private void Awake()
    {
        Time.timeScale = 1f;
        if (tutorialPanel!= null)
        {
            if (PlayerPrefs.GetInt(Constants.TUTORIAL_BOOL) == 0) {
            
                tutorialPanel.SetActive(true);
            }
        }

        if (heroParty != null)
        {
            heroParty.HeroDied += HeroParty_HeroDied;
        }
    }

    private void HeroParty_HeroDied(object sender, System.EventArgs e)
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMetaScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCoreScene()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CleanSaveFile()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HideTutorial()
    {
        PlayerPrefs.SetInt(Constants.TUTORIAL_BOOL, 1);
        tutorialPanel.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
}
