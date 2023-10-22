using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform[] waypoints; // ������ ��� �������� ���� �����, ������� ��� ������ ��������
    private int currentWaypointIndex = 0; // ������ ������� �����
    public float moveSpeed = 5f; // �������� �������� ����
  
    public static CarController Instance;
    public float Healths;
    public float rotationSpeed;

    private UIManager uiManager;
    private ZombieSpawner Zombies;
    private UIProgressBar progressBar;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // �������� �������� � ������ ����� (��������� �����)
        MoveToWaypoint(waypoints[currentWaypointIndex]);
        Zombies = ZombieSpawner.Instance;
        uiManager = UIManager.Instance;
        progressBar = UIProgressBar.Instance;
        progressBar.SetMaxValue(Healths);
    }


    void Update()
    {
        // ���������� ��� � ������� �����
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // ������� ������� � ��������� �����
        Vector3 targetDirection = waypoints[currentWaypointIndex].position - transform.position;

        //transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        //Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1.0f * Time.deltaTime);

        // ���������, ������ �� ��� ������� �����
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1f)
        {
            Zombies.SpawnRandomZombies(currentWaypointIndex);

            // ���� ������, ��������� � ��������� �����
            currentWaypointIndex++;

            // ���� �������� ������, ������������� ��������
            if (currentWaypointIndex >= waypoints.Length)
            {
                enabled = false;
                uiManager.ChangeScreen("Win");
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
        

        Quaternion LookRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, LookRot, Time.deltaTime * rotationSpeed);
        transform.forward = direction;
    }

    public void TakeDamage(float damageAmount)
    {
        Healths -= damageAmount;
        progressBar.SetValue(Healths);
        if (Healths < 0)
        {
            enabled = false;
            uiManager.ChangeScreen("Lose");
        }
    }
}
