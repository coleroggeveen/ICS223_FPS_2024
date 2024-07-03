using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingIguana : MonoBehaviour
{
    private float iguanaSpeed = 3.0f;
    private float obstacleRange = 9.0f;

    private Animator anim;

    private float turn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = iguanaSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Determine if headed for obstacle
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Test for collision
        if (Physics.SphereCast(ray, 0.5f, out hit))
        {

            // Is there an obstacle in the way to warrant a turn?
            if (hit.distance < obstacleRange)
            {
                // If turn value is not set (0), decide on left or right turn
                if (Mathf.Approximately(turn, 0.0f))
                {
                    // Flip a coin 1 or 0 (left or right)
                    turn = Random.Range(0, 2) == 0 ? -0.75f : 0.75f;
                }
                // Blending will cause iguana to move forward and turn at the same time
                // Turn quick, move slow
                Move(turn, 0.1f);
            }
            else // No obstacle, ok to move forward without turning
            {
                float forwardSpeed = Random.Range(0.5f, 1.0f);
                turn = 0.0f;

                // No blending because iguana not turning
                Move(turn, forwardSpeed);
            }
        }
    }

    private void Move(float turn, float forward)
    {
        float dampTime = 0.2f;
        if (anim != null)
        {
            anim.SetFloat("Turn", turn, dampTime, Time.deltaTime);
            anim.SetFloat("Forward", forward, dampTime, Time.deltaTime);
        }
    }
}
