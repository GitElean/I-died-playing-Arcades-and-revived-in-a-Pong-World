using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PaddleController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float speedIncreasePerPoint = 0.5f; // Cuánto incrementar la velocidad por cada punto
    public float upperBound = 4f;
    public float lowerBound = -4f;
    private int direction = 1;
    private GameManager gameManager;

    private void Start()
    {
        // Encontrar el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            // Suscribirse al evento de cambio de puntuación
            gameManager.OnPlayerScoreChanged += HandlePlayerScoreChanged;
        }
    }

    private void HandlePlayerScoreChanged(object sender, EventArgs e)
    {
        // Incrementar la velocidad del movimiento cada vez que la puntuación del jugador aumenta
        moveSpeed += speedIncreasePerPoint;
    }

    private void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime * direction, 0);

        if (transform.position.y >= upperBound && direction > 0)
        {
            direction = -1;
        }
        else if (transform.position.y <= lowerBound && direction < 0)
        {
            direction = 1;
        }
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento cuando el objeto sea destruido
        if (gameManager != null)
        {
            gameManager.OnPlayerScoreChanged -= HandlePlayerScoreChanged;
        }
    }
}
