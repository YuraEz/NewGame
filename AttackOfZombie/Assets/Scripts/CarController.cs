using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public Transform[] waypoints; // ������ ��� �������� ���� �����, ������� ��� ������ ��������
    
    private int currentWaypointIndex = 0; // ������ ������� �����
    public float moveSpeed = 5f; // �������� �������� ����
    //public float rotationSpeedFactor = 2.0f; // ������ �������� ��������
    private ZombieController Zombies;

    void Start()
    {
        // �������� �������� � ������ ����� (��������� �����)
        MoveToWaypoint(waypoints[currentWaypointIndex]);
        Zombies = ZombieController.Instance;
    }


    void Update()
    {
        // ���������� ��� � ������� �����
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // ���������, ������ �� ��� ������� �����
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            //waypoints[currentWaypointIndex].gameObject.SetActive(false);
            Zombies.SpawnRandomZombies(3);

            // ���� ������, ��������� � ��������� �����
            currentWaypointIndex++;

            // ���� �������� ������, ������������� ��������
            if (currentWaypointIndex >= waypoints.Length)
            {
                Debug.Log("����� �� ������!");
                enabled = false; // ��������� ������
                return;
            }

            // � ��������� ������, ���������� ��������� � ��������� �����
            MoveToWaypoint(waypoints[currentWaypointIndex]);
        }
    }

    private void MoveToWaypoint(Transform waypoint)
    {
        // ���������� ��� � ��������� �����
        Vector3 direction = (waypoint.position - transform.position).normalized;
        transform.forward = direction;

        // ��������� ���������� ��� �������� ��������
        //Quaternion targetRotation = Quaternion.LookRotation(direction);

        // ������ ������� ��� � ��������� �����
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
    }
}
