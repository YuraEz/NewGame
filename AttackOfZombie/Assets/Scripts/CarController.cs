using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public Transform[] waypoints; // Массив для хранения всех точек, которые куб должен посетить
    
    private int currentWaypointIndex = 0; // Индекс текущей точки
    public float moveSpeed = 5f; // Скорость движения куба
    //public float rotationSpeedFactor = 2.0f; // Фактор скорости вращения
    private ZombieController Zombies;

    void Start()
    {
        // Начинаем движение к первой точке (стартовой точке)
        MoveToWaypoint(waypoints[currentWaypointIndex]);
        Zombies = ZombieController.Instance;
    }


    void Update()
    {
        // Перемещаем куб к текущей точке
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // Проверяем, достиг ли куб текущей точки
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            //waypoints[currentWaypointIndex].gameObject.SetActive(false);
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

        // Вычисляем кватернион для плавного вращения
        //Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Плавно вращаем куб к следующей точке
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
    }
}
