using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject zombiePrefab; // Префаб зомби
    public static ZombieController Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //SpawnRandomZombies(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnRandomZombies(int count, Transform[] spawnPoints)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length); // Генерируем случайный индекс точки спавна
            Vector3 spawnPosition = spawnPoints[randomIndex].position; // Получаем позицию для спавна

            Instantiate(zombiePrefab, spawnPosition, Quaternion.identity); // Создаем зомби в заданной позиции
        }
    }
}
