using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Assign your bullet prefab in the Inspector
    public GameObject bulletPrefab; 

    // Point where the bullet will spawn (e.g., the cannon's tip)
    public Transform firePoint;

    // Speed at which the bullet will travel
    public float bulletSpeed = 10f;

    float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Example: Shoot every 1 second (replace with your firing logic)
       if (Time.time - lastFireTime > 1)
         {
            lastFireTime = Time.time;
             Shoot();
          
        }
    }

    // Call this method when you want the tower to shoot
    public void Shoot()
    {
        // 1. Instantiate the bullet at the firePoint's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 2. Get the Rigidbody2D component from the instantiated bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // 3. Check if the Rigidbody2D exists before trying to use it
        if (rb != null)
        {
            // The firePoint's 'up' direction (green axis in the Scene view) 
            // is usually considered 'forward' for 2D sprites.
            // This is the direction the tower's transform is pointing.
            Vector2 direction = firePoint.up; 
            
            // 4. Apply a force or set a velocity in that direction
            rb.velocity = direction * bulletSpeed;
        }
        else
        {
            Debug.LogError("Bullet Prefab is missing a Rigidbody2D component!");
            Destroy(bullet); // Clean up the instantiated object if it's not set up correctly
        }

        // Optional: Destroy the bullet after a set time to prevent clutter
        // Destroy(bullet, 5f); 
    }


}
