using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallKnightScript : MonoBehaviour
{
    public Transform ball; // Referencia al objeto de la pelota
    public float moveSpeed = 5f; // Velocidad de movimiento del paddle
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispara el proyectil
    public float fireRate = 1f; // Frecuencia de disparo
    public float projectileSpeed = 10f; // Velocidad del proyectil

    private float nextFireTime = 0f;

    void Update()
    {
        MovePaddle();
        FireProjectile();
    }

    void MovePaddle()
    {
        if (ball != null)
        {
            // Calcular la dirección hacia la pelota en el eje Y
            Vector3 direction = ball.position - transform.position;
            direction = new Vector3(0, direction.y, 0).normalized; // Solo nos interesa el movimiento en el eje Y

            // Calcular la nueva posición del paddle
            Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

            // Limitar el movimiento del paddle dentro de los límites de la pantalla
            newPosition.y = Mathf.Clamp(newPosition.y, -6.5f, 9f);

            // Aplicar la nueva posición al paddle
            transform.position = newPosition;
        }
    }

    void FireProjectile()
    {
        if (Time.time >= nextFireTime)
        {
            // Crear el proyectil en el punto de disparo con la rotación del firePoint
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}
