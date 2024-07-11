using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int health;
    private int maxHealth = 5;
    private float healthPercentage = 1;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        health -= 1;
        healthPercentage = (float)health / (float)maxHealth;
        Messenger<float>.Broadcast(GameEvent.HEALTH_CHANGED, (float)healthPercentage);
        
        Debug.Log("Health: " + health);
        if (health == 0)
        {
            Debug.Break();
        }
    }
}
