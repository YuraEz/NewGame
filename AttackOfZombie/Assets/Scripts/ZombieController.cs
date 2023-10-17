using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Transform CarTransform;
    public float moveSpeed;
    public float attackPerSecond;
    public float attackPower;

    public static ZombieController Instance;
    public Transform[] spawnPoints;
    public GameObject zombiePrefab; // ������ �����
    private CarController Car;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Car = CarController.Instance;
        StartCoroutine(Attack());
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, CarTransform.position, moveSpeed * Time.deltaTime);
        // ���������, ������ �� ��� ������� �����
        //if (Vector3.Distance(transform.position, CarTransform.position) < 3f)
        //{
        //    print("���� ���� �����");
        //    Car.TakeDamage(5);
        //}
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

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackPerSecond);
            if (Vector3.Distance(transform.position, CarTransform.position) < 5f)
            {
                print("���� ���� �����");
                Car.TakeDamage(attackPower);
            }
        }
    }

}
