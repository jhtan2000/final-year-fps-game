using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    public int lifeTime = 2;

    void Start()
    {

    }

    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
