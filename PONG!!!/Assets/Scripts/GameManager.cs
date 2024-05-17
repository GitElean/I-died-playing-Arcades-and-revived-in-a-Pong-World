using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con elementos UI
using System; // Necesario para usar el tipo EventHandler
using TMPro;

public class GameManager : MonoBehaviour
{
    public int scorePlayer, scoreRival;
    public ScoreText playerScoreText, rivalScoreText;
    public TextMeshProUGUI resultText;

    public event EventHandler OnPlayerScoreChanged;

    private void Start()
    {
        // Asegúrate de que el texto de resultado esté desactivado al inicio
        resultText.gameObject.SetActive(false);

    }

    public void OnScoreZoneReached(int id)
    {
        if (id == 1)
        {
            scorePlayer++;
            OnPlayerScoreChanged?.Invoke(this, EventArgs.Empty); // Disparar el evento
        }
        else if (id == 2)
        {
            scoreRival++;
        }

        UpdateScores();
        CheckWinCondition();
    }

    public void UpdateScores()
    {
        playerScoreText.SetScore(scorePlayer);
        rivalScoreText.SetScore(scoreRival);
    }

    private void CheckWinCondition()
    {
        if (scorePlayer >= 11)
        {
            ShowEndGame("You Win");
        }
        else if (scoreRival >= 11)
        {
            ShowEndGame("You Lose");
        }
    }

    private void ShowEndGame(string message)
    {
        resultText.text = message; // Establece el mensaje de victoria o derrota
        resultText.gameObject.SetActive(true); // Muestra el texto
        Time.timeScale = 0; // Pausa el juego
    }

 
}
