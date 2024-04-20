using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scorePlayer, scoreRival;
    public ScoreText playerScoreText, rivalScoreText; // Corregido: Añadido el punto y coma

    // Corregido: Eliminado el punto y coma después de la definición del parámetro
    public void OnScoreZoneReached(int id)
    {
        if (id == 1)
            scorePlayer++;

        if (id == 2)
            scoreRival++;

        UpdateScores();
        //gameUI.HighlightScore(id);
        //CheckWin();
    }

    public void UpdateScores()
    {
        playerScoreText.SetScore(scorePlayer);
        rivalScoreText.SetScore(scoreRival);
    }
}
