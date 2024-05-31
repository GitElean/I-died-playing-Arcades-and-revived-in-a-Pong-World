using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public int hitPoints = 3; // Cantidad de impactos necesarios para destruir el objeto
    public GameManager gameManager;
    private Vector3 initialPosition; // Posición inicial del muro
    private int originalHitPoints; // Guardar los hit points originales

    private void Start()
    {
        originalHitPoints = hitPoints; // Guardar los hit points originales
        initialPosition = transform.position; // Guardar la posición inicial
        Debug.Log("Initial position set to: " + initialPosition);
    }

    // Método para manejar los impactos recibidos
    public void TakeHit()
    {
        hitPoints--;

        if (hitPoints <= 0)
        {
            DestroyObject();
        }
    }

    // Método para destruir el objeto
    private void DestroyObject()
    {
        gameObject.SetActive(false); // Desactivar el objeto en lugar de destruirlo
        Debug.Log("Wall destroyed: " + gameObject.activeSelf);
    }

    // Método para regenerar el muro
    public void Regenerate()
    {
        Debug.Log("Regenerating Wall");
        hitPoints = originalHitPoints; // Restaurar los hit points originales
        transform.position = initialPosition; // Restaurar la posición inicial
        Debug.Log("Restored position to: " + transform.position);

        // Asegurarse de que todos los componentes necesarios estén habilitados
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false;
        }

        gameObject.SetActive(true); // Reactivar el objeto
        Debug.Log("Wall regenerated: " + gameObject.activeSelf + ", Position: " + transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión es con uno de los objetos en el array destroyers
        foreach (GameObject destroyer in gameManager.destroyers)
        {
            if (collision.gameObject == destroyer)
            {
                TakeHit();
                break;
            }
        }
    }
}
