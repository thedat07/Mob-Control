/*
	作成者　野村
	更新者　―
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixed_fps : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Application.targetFrameRate = 60;
    }
}
