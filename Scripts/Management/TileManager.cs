using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tiles;

    [SerializeField]
    private Transform playerPosition;

    private float zSpawnPosition = 0;
    private float tileLength = 40;
    private int numberOfTiles = 5;

    private Queue<GameObject> spawnedTiles = new Queue<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        SpawnTile(0);
        for (int i = 0; i < numberOfTiles - 1; i++)
        {
            SpawnTile(Random.Range(0, tiles.Length));
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (playerPosition.position.z - tileLength > zSpawnPosition - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tiles.Length));
            Destroy(spawnedTiles.Dequeue());
        }

    }

    private void SpawnTile(int tileIndex)
    {
        GameObject tile = Instantiate(tiles[tileIndex], transform.forward * zSpawnPosition, transform.rotation);
        zSpawnPosition += tileLength;
        spawnedTiles.Enqueue(tile);
    }
}
