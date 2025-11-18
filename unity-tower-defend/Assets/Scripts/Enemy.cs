using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   

  
    public GameObject target;
    // The speed of the enemy
    public float moveSpeed = 1f;

    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(75,200 );
        if ( Random.Range(0f,1f) >= 0.5f )
        {
            rotationSpeed = rotationSpeed * -1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
    
    
    // Update is called once per frame
    void Update()
    {

        //transform.rotation = Quaternion.Euler(0, transform., 0);
        transform.Rotate( new Vector3(0,0,rotationSpeed * Time.deltaTime ) );
        // 1. Check if the target exists
        if (target == null)
        {
            // Optional: Find the player if the target hasn't been set
            // target = GameObject.FindWithTag("Player").transform;
            return;
        }

        // 2. Calculate the distance to move this frame
        // Time.deltaTime ensures the movement is frame-rate independent
        float step = moveSpeed * Time.deltaTime;

        // 3. Move the current position towards the target position
        transform.position = Vector2.MoveTowards(
            transform.position, 
            target.transform.position, 
            step
        );
    }

}
