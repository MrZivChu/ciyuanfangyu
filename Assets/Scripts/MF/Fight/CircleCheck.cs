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


    private void OnTriggerEnter(Collider other)
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

            if (!CircleCheckManager.rangList.Contains(rangeScript))
            {
                CircleCheckManager.rangList.Add(rangeScript);
            }
        }
    }
}
