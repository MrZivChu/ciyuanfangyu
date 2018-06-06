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

    RangeCheckManager rangeCheckManager = null;
    void Awake()
    {
        GameObject go = GameObject.Find("Manager");
        if (go != null)
            rangeCheckManager = go.GetComponent<RangeCheckManager>();
    }

    private void Start()
    {
        
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
                //print(transform.parent.parent.parent.name + " leftIndex = " + index);
                rangeScript.leftIndex = index;
            }
            else if (part == Part.right)
            {
                //print(transform.parent.parent.parent.name + " rightIndex = " + index);
                rangeScript.rightIndex = index;
            }

            CheckRange();
        }
    }

    void CheckRange()
    {
        if (rangeCheckManager != null)
        {
            rangeCheckManager.Check();
        }
    }
}
