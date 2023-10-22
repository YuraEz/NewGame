using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1.0f;
    void Update()
    {
        Destroy(gameObject, destroyDelay);
    }
}
