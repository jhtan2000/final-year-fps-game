using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        Vector3 pos = target.position - offset;
        transform.position = new Vector3(target.position.x, pos.y, target.position.z);
    }
}
