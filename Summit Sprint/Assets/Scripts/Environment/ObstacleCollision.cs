using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided: " + other.name);

        if (other.CompareTag("Player"))
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            PlayerHit hitbox = other.GetComponent<PlayerHit>();
            hitbox.player.Crash(); 
        }
    }

}