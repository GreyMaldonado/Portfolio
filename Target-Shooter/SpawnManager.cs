using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] targetPrefabs;
    private float spawnPosX = 0f;
    private float spawnPosY = 0f;
    private float spawnPosZ = 0f;
    public float spawnRangeY = 0f;
    public bool levelDone = false;
    public int targetCounter = -1;

    public float startDelay = 3f;
    public float spawnInterval = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPosX = transform.position.x;
        spawnPosZ = transform.position.z;

        //level = LevelManager.instance.getLevel();
        //targetCounter = 10 * level;
        //StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        if (targetCounter == 0 && levelDone == false)
        {
            //LevelManager.instance.incLevel();
            StopCoroutine(Spawner());
            levelDone = true;
            //Debug.Log("Level done");
        }


    }

    void SpawnRandomTarget()
    {
        int prefabIndex = Random.Range(0, targetPrefabs.Length);
        spawnPosY = transform.position.y + Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

        Instantiate(targetPrefabs[prefabIndex], spawnPos, targetPrefabs[prefabIndex].transform.rotation);
        targetCounter--;
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(startDelay);
        for(int i = targetCounter; i > 0; i--)
        {
            SpawnRandomTarget();
            yield return new WaitForSeconds(Random.Range(3f, spawnInterval));
        }
    }

    public void levelStart(int level)
    {
        targetCounter = 1 * level;
        StartCoroutine(Spawner());
        levelDone = false;
    }

    public bool levelStatus()
    {
        return levelDone;
    }
}
