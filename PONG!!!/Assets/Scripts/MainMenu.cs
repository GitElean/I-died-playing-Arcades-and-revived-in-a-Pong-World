using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;
    public GameObject optionsPanel;
    public List<Button> mainMenuButtons;
    public List<Button> controlsMenuButtons;
    public List<Button> optionsMenuButtons;

    private int currentIndex = 0; // Índice del botón actualmente seleccionado
    private List<Button> currentButtons;
    public Color selectedColor = Color.yellow; // Color para el botón seleccionado
    public Color defaultColor = Color.white;

    void Start()
    {
        Time.timeScale = 1.0f;
        currentButtons = mainMenuButtons;
        UpdateMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NavigateMenu(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            NavigateMenu(1);
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SelectOption();
        }

        Debug.Log(currentIndex + " " + currentButtons[0]);
    }

    void NavigateMenu(int direction)
    {
        if (currentButtons.Count == 0) return;

        currentButtons[currentIndex].GetComponentInChildren<Text>().color = defaultColor; // Restaurar el color del botón anterior

        currentIndex += direction;

        if (currentIndex < 0)
        {
            currentIndex = currentButtons.Count - 1;
        }
        else if (currentIndex >= currentButtons.Count)
        {
            currentIndex = 0;
        }

        UpdateMenu();
    }

    void UpdateMenu()
    {
        if (currentButtons.Count == 0) return;

        for (int i = 0; i < currentButtons.Count; i++)
        {
            if (i == currentIndex)
            {
                currentButtons[i].GetComponentInChildren<Text>().color = selectedColor;
            }
            else
            {
                currentButtons[i].GetComponentInChildren<Text>().color = defaultColor;
            }
        }
    }

    void SelectOption()
    {
        if (currentButtons.Count == 0) return;

        Button selectedButton = currentButtons[currentIndex];

        if (selectedButton != null)
        {
            selectedButton.onClick.Invoke(); // Invocar el evento onClick del botón seleccionado
        }
    }

    public void OnStartButton()
    {
        // Aquí puedes cargar la escena del juego, por ejemplo
        SceneManager.LoadScene("GameScene");
    }

    public void OnControlsButton()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
        optionsPanel.SetActive(false);
        currentButtons = controlsMenuButtons; // Actualizar los botones actuales
        currentIndex = 0;
        UpdateMenu();
    }

    public void OnOptionsButton()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        optionsPanel.SetActive(true);
        currentButtons = optionsMenuButtons; // Actualizar los botones actuales
        currentIndex = 0;
        UpdateMenu();
    }

    public void OnBackButton()
    {
        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        currentButtons = mainMenuButtons; // Volver a los botones del menú principal
        currentIndex = 0;
        UpdateMenu();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
