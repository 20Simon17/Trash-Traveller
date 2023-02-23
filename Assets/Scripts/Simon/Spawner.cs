using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Variabler
    [Header("Spawning")]
    public GameObject objectToSpawn;
    public int AmountToSpawn;   
    
    [Header("Spawnpoints")]
    public GameObject[] spawnpointsArray; //alla spawnpoints vi lagt in i unity
    private List<GameObject> spawnpointsList;

    int randomSpawnpoint;
    #endregion

    private void Start()
    {
        spawnpointsList = new List<GameObject>(spawnpointsArray); 
        //kopierar min spawnpoint array till en lista som jag kan ändra hur jag vill

        for (int i = 0; i < AmountToSpawn; i++) //for loop för att spawna fiender på spawnpoints i scenen
        {
            randomSpawnpoint = Random.Range(0, spawnpointsList.Count);
            Instantiate(objectToSpawn, spawnpointsList[randomSpawnpoint].transform.position, Quaternion.identity);

            spawnpointsList.RemoveAt(randomSpawnpoint);
        }
    }
}
