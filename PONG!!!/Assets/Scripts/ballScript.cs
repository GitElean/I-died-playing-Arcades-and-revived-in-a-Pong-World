using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{   
    public GameManager gameManager;
    public Rigidbody rb;
    public float maxInitialAngle = 0.87f;
    public float moveSpeed = 2.0f;
    public float startX = 0f;
    public float startY = 2f;
    public float startZ = 0f;
    // Start is called before the first frame update
    void Start()
    {
        initialPush();
    }

    private void initialPush(){
        float initialAngle = Random.Range(-maxInitialAngle, maxInitialAngle);
        int xDir = Random.value < 0.5f ? 1 : -1;
        Vector3 direction = new Vector3(xDir, initialAngle, 0);
        rb.velocity = direction* moveSpeed;
    }

    private void resetBallPosition()
    {
        float posY = Random.Range(-startY, startY);
        Vector3 position = new Vector3(startX, posY, startZ);

        transform.position = position;
    }

    private void resetBall()
    {
        resetBallPosition();
        initialPush();
    }

    private void OnTriggerEnter(Collider collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone)
        {
            gameManager.OnScoreZoneReached(scoreZone.ID);
            resetBall();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
