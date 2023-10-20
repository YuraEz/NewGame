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

        // Преобразуем позицию мыши в луч в мировом пространстве
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        // Создаем переменную для хранения информации о пересечении луча с поверхностью
        RaycastHit hit;

        // Пересекаем луч с объектами на сцене
        if (Physics.Raycast(ray, out hit))
        {
            // Получаем точку пересечения луча с поверхностью
            Vector3 hitPoint = hit.point;

            // Игнорируем высоту точки пересечения, чтобы объект оставался на горизонтальной плоскости
            hitPoint.y = transform.position.y;

            // Получаем направление от объекта к точке пересечения луча
            Vector3 lookDirection = hitPoint - transform.position;

            // Используем Quaternion.LookRotation, чтобы повернуть объект в это направление
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
            print("Ты попал в зомби");
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
