using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float immobilizeDuration = 0.25f;
    public float invulnerabilityDuration = 1f;

    private Vector3 targetPosition;

    private void Start()
    {
        // Establecer la dirección hacia la izquierda
        targetPosition = transform.position + Vector3.left * 1000f; // Un valor grande para que siga moviéndose hacia la izquierda
    }

    private void Update()
    {
        // Mover el proyectil hacia la izquierda usando MoveTowards
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftPaddle"))
        {
            Paddle playerController = other.GetComponent<Paddle>();
            if (playerController != null)
            {
                if (!playerController.IsInvulnerable)
                {
                    playerController.Immobilize(immobilizeDuration);
                    playerController.SetInvulnerability(invulnerabilityDuration);
                }
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("RightPaddle"))
        {
            Destroy(gameObject);
        }
    }
}
