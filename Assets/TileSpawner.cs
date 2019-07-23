using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] GameObject tilePrefabs;
    [SerializeField] int tileCap = 8;
    [SerializeField] float timeUntilNextTile = 2f;

    bool nextTileReady = true;
    List<GameObject> activeTile = new List<GameObject>();
    Vector3 newTilePos = new Vector3();
    void Start()
    {
        for(int i = 0; i < tileCap; i++)
        {
            CreateNewTile();
        }
    }
    void Update()
    {
        if(nextTileReady)
        {
            StartCoroutine(AutoGenerateTile());
        }
    }
    private void CreateNewTile()
    {
        GameObject newTile = Instantiate(tilePrefabs, newTilePos, Quaternion.identity) as GameObject;
        newTile.transform.parent = this.transform;
        //Take size of prefab to get new location for next tile
        Vector3 tileSize = newTile.GetComponent<BoxCollider>().bounds.size;
        newTilePos.z += tileSize.z;
        activeTile.Add(newTile);
    }
    IEnumerator AutoGenerateTile()
    {
        nextTileReady = false;
        GameObject lastTile = activeTile[0];
        activeTile.RemoveAt(0);
        Destroy(lastTile);
        yield return new WaitForSeconds(timeUntilNextTile);
        CreateNewTile();
        nextTileReady = true;
    }
}
