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

    public GameObject shieldPrefab; // Prefab del escudo
    public Transform[] shieldSpawnPoints; // Array de puntos de aparición del escudo

    public List<GameObject> destroyers; // Lista de objetos que pueden destruir muros

    public DestructibleObject destructibleWall; // Referencia al muro destructible
    public bool enableWallRegeneration = true; // Variable para habilitar/deshabilitar la regeneración del muro

    private void Start()
    {
        // Asegúrate de que el texto de resultado esté desactivado al inicio
        resultText.gameObject.SetActive(false);

        // Suscribirse al evento de cambio de puntaje
        OnPlayerScoreChanged += HandlePlayerScoreChanged;
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento al destruirse
        OnPlayerScoreChanged -= HandlePlayerScoreChanged;
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

    private void HandlePlayerScoreChanged(object sender, EventArgs e)
    {
        if (enableWallRegeneration && (scorePlayer == 3 || scorePlayer == 6 || scorePlayer == 9))
        {
            destructibleWall.Regenerate();
        }
    }

    private void InvokeShields()
    {
        foreach (Transform spawnPoint in shieldSpawnPoints)
        {
            Instantiate(shieldPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}