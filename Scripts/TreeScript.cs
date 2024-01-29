using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public GameObject itemToSpawn;
    public float movementSpeed = 2f; // Geschwindigkeit, mit der sich das Item bewegt
    private bool hasSpawned = false; // Um sicherzustellen, dass das Item nur einmal gespawnt wird
    private GameObject spawnedItem;

    private PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasSpawned)
        {
            MoveSpawnedItemTowardsPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasSpawned)
        {
            pm = collision.gameObject.GetComponent<PlayerMovement>();
            if (pm.currentState == PlayerState.roll)
            {
                SpawnItem();
            }
        }
    }

    void SpawnItem()
    {
        if (itemToSpawn != null)
        {
            spawnedItem = Instantiate(itemToSpawn, transform.position, Quaternion.identity);
            hasSpawned = true;
        }
    }

    void MoveSpawnedItemTowardsPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && spawnedItem != null)
        {
            Vector3 direction = (player.transform.position - spawnedItem.transform.position).normalized;
            spawnedItem.transform.Translate(direction * movementSpeed * Time.deltaTime);
        }
    }
}
