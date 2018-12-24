﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour {

    public float RotationSpeed = 1.0f;
    
	

	void Update () {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationSpeed);
    }
}

