using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestoryOwn : MonoBehaviour
{
    public float delay;

    private void Start()
    {
        Invoke("DestoryOwn", delay);
    }

    void DestoryOwn()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
