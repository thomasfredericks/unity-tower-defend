using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float enemySpawnInterval = 1f;

    public GameObject enemyPrefab;

    public GameObject player;
    float enemySpawnLastTime = 0f;

    public Camera mainCamera;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - enemySpawnLastTime >= enemySpawnInterval)
        {
            enemySpawnLastTime = Time.time;

            // Calculate the mainCamera's boundaries in world coordinates
            float cameraHeight = mainCamera.orthographicSize * 2;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            float xMin = mainCamera.transform.position.x - (cameraWidth / 2f);
            float xMax = mainCamera.transform.position.x + (cameraWidth / 2f);
            float yMin = mainCamera.transform.position.y - (cameraHeight / 2f);
            float yMax = mainCamera.transform.position.y + (cameraHeight / 2f);

            // Define how far outside the screen the enemy should spawn
            const float BufferDistance = 0f;

            // Calculate the edges of the spawn zone
            float spawnXMin = xMin - BufferDistance;
            float spawnXMax = xMax + BufferDistance;
            float spawnYMin = yMin - BufferDistance;
            float spawnYMax = yMax + BufferDistance;

            int side = Random.Range(0, 3);
            Vector3 spawnPosition;

            if (side == 0) // Left side
            {
                // Fix X to the buffer edge, randomize Y within the mainCamera's vertical range
                float randomY = Random.Range(yMin, yMax);
                spawnPosition = new Vector3(spawnXMin, randomY, 0f);
            }
            else if (side == 1) // Right side
            {
                // Fix X to the buffer edge, randomize Y
                float randomY = Random.Range(yMin, yMax);
                spawnPosition = new Vector3(spawnXMax, randomY, 0f);
            }
            else if (side == 2) // Top side
            {
                // Fix Y to the buffer edge, randomize X
                float randomX = Random.Range(xMin, xMax);
                spawnPosition = new Vector3(randomX, spawnYMax, 0f);
            } else
            {
                // Fallback to center if something goes wrong
                spawnPosition = new Vector3(0f, 0f, 0f);
            }

            /*            else // Bottom side (side == 3)
                       {
                           // Fix Y to the buffer edge, randomize X
                           float randomX = Random.Range(xMin, xMax);
                           spawnPosition = new Vector3(randomX, spawnYMin, 0f);
                       } */

            GameObject instance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Vector3 pointTowards = player.transform.position - instance.transform.position;
            float angle = Mathf.Atan2(pointTowards.y, pointTowards.x) * Mathf.Rad2Deg;
            instance.transform.rotation = Quaternion.Euler(0, 0, angle);

            Enemy enemyScript = instance.GetComponent<Enemy>();
            enemyScript.target = player.transform;

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            Vector2 direction = (
                player.transform.position - instance.transform.position
            ).normalized;

            rb.velocity = direction * 1f;
        }
    }
}
