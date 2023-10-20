using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float moveSpeed;
    public float attackPerSecond;
    public float attackPower;
    public static ZombieController Instance;
    private CarController Car;
    public float Healths;

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
        print("Ты движаешься");
        transform.position = Vector3.MoveTowards(transform.position, Car.transform.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damageAmount)
    {
        Healths -= damageAmount;
        if (Healths < 0)
        {
            Debug.Log("Ты убил зомби!");
            Destroy(gameObject);

        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackPerSecond);
            if (Vector3.Distance(transform.position, Car.transform.position) < 5f) 
            {
                print("Тебя Бьют зомби");
                Car.TakeDamage(attackPower);
            }
        }
    }

}
