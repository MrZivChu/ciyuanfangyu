﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour {
    void Start() {
        DontDestroyOnLoad(gameObject);
    }
}
