using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Camera mainCamera; // Asegúrate de asignar la cámara principal en el inspector
    public float miny = 0;
    public float maxy = 0;

    private bool isImmobilized = false;
    private bool isInvulnerable = false;
    private float immobilizeEndTime = 0f;
    private float invulnerabilityEndTime = 0f;

    private void Update() // Usa Update para obtener el input del mouse
    {
        HandleMovement();
        HandleTimers();
    }

    private void HandleMovement()
    {
        if (isImmobilized)
            return;

        MoveWithMouse();
    }

    private void MoveWithMouse()
    {
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane);
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        // Limita la posición Y del paddle para que no exceda los límites superior e inferior.
        float limitedY = Mathf.Clamp(mouseWorldPosition.y, -6.5f, 9f);

        // Mantiene las posiciones X y Z constantes (ajusta Z si es necesario).
        transform.position = new Vector3(transform.position.x, limitedY, transform.position.z);
    }

    private void HandleTimers()
    {
        if (isImmobilized && Time.time >= immobilizeEndTime)
        {
            isImmobilized = false;
        }

        if (isInvulnerable && Time.time >= invulnerabilityEndTime)
        {
            isInvulnerable = false;
        }
    }

    public void Immobilize(float duration)
    {
        isImmobilized = true;
        immobilizeEndTime = Time.time + duration;
    }

    public void SetInvulnerability(float duration)
    {
        isInvulnerable = true;
        invulnerabilityEndTime = Time.time + duration;
    }

    public bool IsInvulnerable
    {
        get { return isInvulnerable; }
    }
}
