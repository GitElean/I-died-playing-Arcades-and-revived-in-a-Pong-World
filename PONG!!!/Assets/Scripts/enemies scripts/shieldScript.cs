using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del escudo
    public float moveRange = 2f; // Rango de movimiento del escudo
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        MoveShield();
    }

    private void MoveShield()
    {
        float newY = initialPosition.y + Mathf.PingPong(Time.time * moveSpeed, moveRange) - (moveRange / 2);
        transform.position = new Vector3(transform.position.x,newY , transform.position.z);
    }
}
