using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Part
{
    none = 0,
    left = 1,
    right = 2
}

public class CircleCheck : MonoBehaviour
{
    public Part part;

    CircleCheckManager circleCheckManager = null;
    void Awake()
    {
        GameObject go = GameObject.Find("Manager");
        if (go != null)
            circleCheckManager = go.GetComponent<CircleCheckManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("circleCheck"))
        {
            GameObject range = transform.parent.gameObject;
            int index = other.GetComponent<MappingCircle>().circleIndex;
            Range rangeScript = range.GetComponent<Range>();
            if (part == Part.left)
            {
                rangeScript.leftIndex = index;
            }
            else if (part == Part.right)
            {
                rangeScript.rightIndex = index;
            }
            CheckCircle();
        }
    }

    void CheckCircle()
    {
        if (circleCheckManager != null)
        {
            circleCheckManager.Check();
        }
    }
}
