using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallsColor : MonoBehaviour
{
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        // Obtiene el componente Renderer del objeto
        renderer = GetComponent<Renderer>();
    }

    // Este método se llama automáticamente cuando este objeto colisiona con otro
    private void OnCollisionEnter(Collision collision)
    {
        // Genera un color aleatorio y cambia el color del material a ese color
        Color randomColor = Random.ColorHSV();  // Genera un color completamente aleatorio
        renderer.material.color = randomColor;
    }
}
