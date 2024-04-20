using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 3f;

    private void FixedUpdate() // Usa FixedUpdate en lugar de Update para trabajar con la física
    {
        float moveVertical = Input.GetAxis("Movement"); // Obtiene el input vertical
        Move(moveVertical);
    }

    private void Move(float moveVertical)
    {
        // Crear un nuevo vector de movimiento, sólo afectando al eje Y
        Vector3 movement = new Vector3(0.0f, moveVertical, 0.0f);

        // Aplicar el movimiento al Rigidbody con el moveSpeed
        rb.velocity = movement * moveSpeed;
    }
}