using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scorePlayer, scoreRival;
    public ScoreText playerScoreText, rivalScoreText; // Corregido: A�adido el punto y coma

    // Corregido: Eliminado el punto y coma despu�s de la definici�n del par�metro
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
