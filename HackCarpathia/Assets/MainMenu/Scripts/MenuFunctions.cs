using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    [Header("Ustawienia Sceny Początkowej")]
    [SerializeField] private int sceneMain = 1;

    [Header("Zarządzanie UI")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject settingsUI;

    private void Awake()
    {
        mainMenuUI.SetActive(true);
        settingsUI.SetActive(false);
    }

    private void Update()
    {
        if (settingsUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            closeSettings();
        }
    }

    public void playGame()
    {
        SceneManager.LoadScene(sceneMain);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void openSettings()
    {
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void closeSettings()
    {
        settingsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
