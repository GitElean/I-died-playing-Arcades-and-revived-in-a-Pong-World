using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody rb;
    public float maxInitialAngle = 0.87f;
    public float moveSpeed = 0f;
    public float speedIncrease = 1f; // Incremento de velocidad al colisionar con un paddle
    public float startX = 0f;
    public float startY = 2f;
    private string lastHitPaddleTag = null;

    private float lastHitTime = 0f;
    private float hitCooldown = 0.1f;

    public float maxYVelocityChange = 330023423f;

    // Variable pública para habilitar/deshabilitar la habilidad de cambio de dirección
    public bool isYDirectionChangeEnabled = false;

    // Cosas para el parry
    private bool isParryActive = false;
    private float parrySpeedMultiplier = 2f; // Multiplicador de velocidad durante el parry
    private bool canParry = false; // Indica si el jugador puede intentar un parry

    private Renderer renderer;

    // Variables para la habilidad de lanzar la pelota en X
    public bool isLaunchXEnabled = false; // Habilitar/deshabilitar la habilidad de lanzamiento en X
    public int maxLaunchXUses = 3; // Máximo de usos permitidos
    private int currentLaunchXUses = 0; // Contador de usos actuales

    // Start is called before the first frame update
    void Start()
    {
        initialPush();
        renderer = GetComponent<Renderer>();
    }

    private void initialPush()
    {
        float initialAngle = Random.Range(-maxInitialAngle, maxInitialAngle);
        int xDir = Random.value < 0.5f ? 1 : -1;
        Vector3 direction = new Vector3(xDir, initialAngle, 0);
        rb.velocity = direction * moveSpeed;
        Debug.Log("Initial push: " + rb.velocity);
    }

    private void resetBallPosition()
    {
        float posY = Random.Range(-startY, startY);
        Vector3 position = new Vector3(startX, posY, 14.75f);
        Debug.Log("Asignando nueva posición de la pelota: " + position);
        transform.position = position;
    }

    private void resetBall()
    {
        resetBallPosition();
        initialPush();
        ResetParry();
        currentLaunchXUses = 0; // Resetear el contador de usos al reiniciar la pelota
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision should be ignored
        if (collision.gameObject.tag == lastHitPaddleTag)
        {
            return; // Ignore this collision
        }

        if (collision.gameObject.CompareTag("RightPaddle") && isParryActive)
        {
            ResetParry();
        }

        if (collision.gameObject.CompareTag("LeftPaddle") || collision.gameObject.CompareTag("RightPaddle"))
        {
            Vector3 currentVelocity = rb.velocity;

            // Calculate the impact point relative to the center of the paddle
            float relativeImpactPosition = 5 * (collision.contacts[0].point.y - collision.transform.position.y) / (collision.collider.bounds.size.y / 2);
            float deltaYVelocity = relativeImpactPosition * maxYVelocityChange;

            // Flip the X velocity to send it toward the other player and adjust Y velocity based on where it hit the paddle
            currentVelocity.x = -1.05f * currentVelocity.x;
            currentVelocity.y += deltaYVelocity;

            // Increase the speed of the ball
            currentVelocity *= (1 + speedIncrease);

            // Apply the calculated velocity to the ball
            rb.velocity = currentVelocity;

            // Check for parry activation
            if (Input.GetKey(KeyCode.Space))
            {
                // Double the velocity for parry
                rb.velocity *= parrySpeedMultiplier;
                isParryActive = true;
                renderer.material.color = Color.blue;
            }

            // Reset the parry flag until the next opportunity to parry
            canParry = false;

            // Store the tag of the paddle that was last hit
            lastHitPaddleTag = collision.gameObject.tag;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ScoreZone scoreZone = other.GetComponent<ScoreZone>();
        if (scoreZone)
        {
            gameManager.OnScoreZoneReached(scoreZone.ID);
            resetBall();
        }
    }

    public void ResetParry()
    {
        if (isParryActive)
        {
            rb.velocity /= parrySpeedMultiplier;
            isParryActive = false;
            renderer.material.color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Invert Y direction when Q is pressed and the ability is enabled
        if (isYDirectionChangeEnabled && Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 currentVelocity = rb.velocity;
            currentVelocity.y = -currentVelocity.y;
            rb.velocity = currentVelocity;
            Debug.Log("Y direction inverted: " + rb.velocity);
        }

        // Launch ball in X direction when E is pressed and the ability is enabled
        if (isLaunchXEnabled && Input.GetKeyDown(KeyCode.E) && currentLaunchXUses < maxLaunchXUses)
        {
            Vector3 currentVelocity = rb.velocity;
            currentVelocity.y = 0; // Set Y velocity to 0
            currentVelocity.x = Mathf.Sign(currentVelocity.x) * moveSpeed; // Ensure the ball moves in the X direction at moveSpeed
            rb.velocity = currentVelocity;
            currentLaunchXUses++;
            Debug.Log("Ball launched in X direction: " + rb.velocity + " | Uses left: " + (maxLaunchXUses - currentLaunchXUses));
        }
    }
}