using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    public Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameInvisible()
    {
        // Destroy the bullet once it leaves the player's view
        Destroy(gameObject); 
        // Optional: Add a Debug.Log here if you want to confirm it's working
        // Debug.Log("Bullet left the screen and was destroyed.");
    }
    
    // You might also want to destroy the bullet if it hits something
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the bullet upon impact
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}
