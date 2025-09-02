using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetaEnemy : MonoBehaviour
{
    public GameObject metaScreen;
    public float hintRange;
    public float maxAlpha;

    void Start()
    {
        metaScreen.SetActive(false);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        Color color = metaScreen.GetComponent<Image>().color;
        if (distanceToPlayer <= hintRange)
        {
            metaScreen.SetActive(true);
            color.a = maxAlpha - (distanceToPlayer / hintRange);
        }
        else
        {
            metaScreen.SetActive(false);
        }

        if (color.a > maxAlpha)
        {
            color.a = maxAlpha;
        }

        if (color.a < .1f)
        {
            color.a = .1f;
        }

        metaScreen.gameObject.transform.GetComponent<Image>().color = color;
    }
}
