using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform[] waypoints; // Массив для хранения всех точек, которые куб должен посетить
    private int currentWaypointIndex = 0; // Индекс текущей точки
    public float moveSpeed = 5f; // Скорость движения куба
    private ZombieSpawner Zombies;
    public static CarController Instance;
    public float Healths;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Начинаем движение к первой точке (стартовой точке)
        MoveToWaypoint(waypoints[currentWaypointIndex]);
        Zombies = ZombieSpawner.Instance;
    }


    void Update()
    {
        // Перемещаем куб к текущей точке
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // Плавный поворот к следующей точке
        Vector3 targetDirection = waypoints[currentWaypointIndex].position - transform.position;

        //transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        //Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1.0f * Time.deltaTime);

        // Проверяем, достиг ли куб текущей точки
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1f)
        {
            Zombies.SpawnRandomZombies(3);

            // Если достиг, переходим к следующей точке
            currentWaypointIndex++;

            // Если достигли финиша, останавливаем движение
            if (currentWaypointIndex >= waypoints.Length)
            {
                Debug.Log("Дошли до финиша!");
                enabled = false; // Отключаем скрипт
                return;
            }

            // В противном случае, продолжаем двигаться к следующей точке
            MoveToWaypoint(waypoints[currentWaypointIndex]);
        }
    }

    private void MoveToWaypoint(Transform waypoint)
    {
        // Направляем куб к следующей точке
        Vector3 direction = (waypoint.position - transform.position).normalized;
        transform.forward = direction;
    }

    public void TakeDamage(float damageAmount)
    {
        Healths -= damageAmount;
        if (Healths < 0)
        {
            Debug.Log("Ты проиграл!");
        }
    }
}
