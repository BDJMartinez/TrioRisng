using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public int SpawnCount {  get => spawnCount; private set => spawnCount = value; }
    public int SpawnListCurrentIndex { get => spawnListCurrentIndex; set => spawnListCurrentIndex = value; }

    [SerializeField] GameObject[] spawnObjects;
    [SerializeField] public int numToSpawn;
    [SerializeField] float spawnTime;
    [SerializeField] Transform[] spawnPos;
    public List<GameObject> spawnList;

    private int spawnCount;
    private int spawnListCurrentIndex = 0;

    bool isSpawning;
    bool startSpawning;
    public bool isObjectiveSpawner;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startSpawning && spawnCount < numToSpawn && !isSpawning)
        {
            StartCoroutine(spawn());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startSpawning = true;
        }
    }

    IEnumerator spawn()
    {
        isSpawning = true;
        int transformArrayPosition = Random.Range(0, spawnPos.Length);
        int objectArrayPosition = Random.Range(0, spawnObjects.Length);
        GameObject instantiated = Instantiate(spawnObjects[objectArrayPosition], spawnPos[transformArrayPosition].position, spawnPos[transformArrayPosition].rotation);
        spawnList.Insert(SpawnListCurrentIndex, instantiated);
        SpawnListCurrentIndex++;
        instantiated.transform.SetParent(transform);
        SpawnCount++;
        isSpawning=false;





        yield return new WaitForSeconds(spawnTime);


    }

}
