using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] int numberOfChunks = 4;

    [Header("Prefabs")]
    [SerializeField] GameObject lastGroundChunk;
    [SerializeField] List<GameObject> groundChunks = new List<GameObject>();
    [SerializeField] List<GameObject> housePrefab = new List<GameObject>();
    [SerializeField] Transform groundChunkStartPos;
    public Transform nextChunkPos;
    
    void Start() {
        MapInstantiate();
    }

    void MapInstantiate() {
        for(int i = numberOfChunks; i >= 0; i--) {
            if(i == numberOfChunks) {
                GameObject newChunk = Instantiate(
                    groundChunks[0], 
                    groundChunkStartPos.position, 
                    groundChunks[0].transform.rotation);
                GroundChunk groundChunk = newChunk.GetComponent<GroundChunk>();

                nextChunkPos = groundChunk.nextChunkPos;

                for(int j = groundChunk.housePos.Count - 1; j >= 0; j--) {
                    HouseInstantiate(groundChunk.housePos[j]);
                }
            }
            else if(i == 0) {
                Instantiate(lastGroundChunk, nextChunkPos.position, lastGroundChunk.transform.rotation);
            }
            else {
                int chunkNumber = Random.Range(0, groundChunks.Count);

                GameObject newChunk = Instantiate(
                    groundChunks[chunkNumber], 
                    nextChunkPos.position, 
                    groundChunks[chunkNumber].transform.rotation);
                GroundChunk groundChunk = newChunk.GetComponent<GroundChunk>();

                nextChunkPos = groundChunk.nextChunkPos;

                for(int j = groundChunk.housePos.Count - 1; j >= 0; j--) {
                    HouseInstantiate(groundChunk.housePos[j]);
                }
            }
        }
    }
    void HouseInstantiate(Transform spawnPos) {
        int houseNumber = Random.Range(0, housePrefab.Count);

        Instantiate(housePrefab[houseNumber], spawnPos.position, housePrefab[houseNumber].transform.rotation);
    }
}
