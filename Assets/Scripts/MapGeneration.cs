using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] int numberOfChunks = 4;

    [Header("Prefabs")]
    [SerializeField] GameObject lastGroundChunk;
    [SerializeField] List<GameObject> groundChunks = new List<GameObject>();
    [SerializeField] List<GameObject> housePrefab = new List<GameObject>();
    [SerializeField] GameObject bobHouse;
    [SerializeField] Transform groundChunkStartPos;
    public Transform nextChunkPos;
    private bool isBobHouse;

    [Header("Other")]
    [SerializeField] GameObject gamblingUI;
    [SerializeField] TMP_InputField gamblingMoneyInputField;
    
    void Start() {
        MapInstantiate();
    }

    void MapInstantiate() {
        for(int i = numberOfChunks; i >= 0; i--) {
            //First Chunk
            if(i == numberOfChunks) {
                GameObject newChunk = Instantiate(
                    groundChunks[0], 
                    groundChunkStartPos.position, 
                    groundChunks[0].transform.rotation);
                GroundChunk groundChunk = newChunk.GetComponent<GroundChunk>();

                nextChunkPos = groundChunk.nextChunkPos;

                for(int j = groundChunk.housePos.Count - 1; j >= 0; j--) {
                    HouseInstantiate(groundChunk.housePos[j], i);
                }
            }
            else if(i == 0) {
                lastGroundChunk.transform.position = nextChunkPos.position;
            }
            //Rest of the chunks
            else {
                int chunkNumber = Random.Range(0, groundChunks.Count);

                GameObject newChunk = Instantiate(
                    groundChunks[chunkNumber], 
                    nextChunkPos.position, 
                    groundChunks[chunkNumber].transform.rotation);
                GroundChunk groundChunk = newChunk.GetComponent<GroundChunk>();

                nextChunkPos = groundChunk.nextChunkPos;

                for(int j = groundChunk.housePos.Count - 1; j >= 0; j--) {
                    HouseInstantiate(groundChunk.housePos[j], i);
                }
            }
        }
    }
    void HouseInstantiate(Transform spawnPos, int chunkNumber) {
        if(bobHouse && !isBobHouse) {
            if(chunkNumber == 1) {
                Instantiate(bobHouse, spawnPos.position, bobHouse.transform.rotation, spawnPos);
                isBobHouse = true;
            }
            else if(Random.Range(0, 6) == 1) {
                Instantiate(bobHouse, spawnPos.position, bobHouse.transform.rotation, spawnPos);
                isBobHouse = true;
            }
            else {
                int houseNumber = Random.Range(0, housePrefab.Count);

                Instantiate(housePrefab[houseNumber], spawnPos.position, housePrefab[houseNumber].transform.rotation, spawnPos);
            }
        }
        else {
            int houseNumber = Random.Range(0, housePrefab.Count);

            Instantiate(housePrefab[houseNumber], spawnPos.position, housePrefab[houseNumber].transform.rotation, spawnPos);
        }
    }
}
