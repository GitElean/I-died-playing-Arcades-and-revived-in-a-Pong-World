using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Camera mainCamera; // Asegúrate de asignar la cámara principal en el inspector
    public float miny =0;
    public float maxy =0;
    private void Update() // Usa Update para obtener el input del mouse
    {
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

}
