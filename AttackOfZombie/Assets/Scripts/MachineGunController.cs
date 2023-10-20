using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunController : MonoBehaviour
{

    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private Transform Canon;
    [SerializeField] private BulletController PrefabBullet;
    [SerializeField] private float ShotForce;
    private bool CanAttack = true;
    public float AttackForce;
    public float AttackRange;
    public float attackPower;
    public Camera mainCamera;

    private void Update()
    {

        Vector3 mousePosition = Input.mousePosition;

        // ����������� ������� ���� � ��� � ������� ������������
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        // ������� ���������� ��� �������� ���������� � ����������� ���� � ������������
        RaycastHit hit;

        // ���������� ��� � ��������� �� �����
        if (Physics.Raycast(ray, out hit))
        {
            // �������� ����� ����������� ���� � ������������
            Vector3 hitPoint = hit.point;

            // ���������� ������ ����� �����������, ����� ������ ��������� �� �������������� ���������
            hitPoint.y = transform.position.y;

            // �������� ����������� �� ������� � ����� ����������� ����
            Vector3 lookDirection = hitPoint - transform.position;

            // ���������� Quaternion.LookRotation, ����� ��������� ������ � ��� �����������
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        if (Input.GetMouseButton(0) && CanAttack)
        {
           Attack();
        }
    }

    private void AttackTimer()
    {
        CanAttack = true;
    }

    private void Attack()
    {
        if (!CanAttack) { return; }
        BulletController bullet = Instantiate(PrefabBullet, Canon.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(ShotForce * Canon.forward);


        print("Attacked");
        Ray ray = new Ray(Canon.position, Canon.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, AttackRange, EnemyMask))
        {
            print("�� ����� � �����");
            ZombieController Zombie = hit.transform.GetComponent<ZombieController>();
            Zombie.TakeDamage(attackPower);
            hit.rigidbody.AddForce(-hit.normal * AttackForce);
        }
        CanAttack = false;
        Invoke("AttackTimer", 0.1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Canon.position, Canon.forward * AttackRange);
    }
}
