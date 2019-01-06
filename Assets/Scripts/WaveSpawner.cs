using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    private GameObject spawnObject;
    private Transform spawnTransform;
    public GameObject enemyPrefab;

    public float timeBetweenEnemy = 0.5f;
    public float timeBetweenWave = 5f;
    public float countdown = 3f;

    private int waveIndex = 0;
    private int enemiesToSpawn = 0;

    public Text waveCountdownTest;

    // Start is called before the first frame update
    void Start()
    {
        spawnObject = GameObject.FindWithTag("Respawn");
        spawnTransform = spawnObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWave;
        }

        countdown -= Time.deltaTime;
        if (countdown >= 0f)
        {
            waveCountdownTest.text = Mathf.Ceil(countdown).ToString();
        }
    }

    IEnumerator SpawnWave()
    {
        Debug.Log("Wave #" + waveIndex + " starting");
        waveIndex++;
        waveCountdownTest.text = "";
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemy);
        }
        
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnTransform.position, spawnTransform.rotation);
    }
}
