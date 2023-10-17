using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public static ZombieController Instance;
    public Transform[] spawnPoints;
    public GameObject zombiePrefab; // ������ �����

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnRandomZombies(int count)
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points are not initialized or empty.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length); // ���������� ��������� ������ ����� ������
            Vector3 spawnPosition = spawnPoints[randomIndex].position; // �������� ������� ��� ������

            Instantiate(zombiePrefab, spawnPosition, Quaternion.identity); // ������� ����� � �������� �������
        }
    }
}
