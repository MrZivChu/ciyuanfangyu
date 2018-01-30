using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverMaterials : MonoBehaviour
{

    public GameObject diskBottom1;
    public GameObject diskBottom2;
    public GameObject diskBottom3;
    public GameObject diskBottom4;

    private void Start()
    {
        Utils.SetObjectEmissionColor(diskBottom1, new Color(0, 0, 0));
        Utils.SetObjectEmissionColor(diskBottom2, new Color(0, 0, 0));
        Utils.SetObjectEmissionColor(diskBottom3, new Color(0, 0, 0));
        Utils.SetObjectEmissionColor(diskBottom4, new Color(0, 0, 0));
    }
}
