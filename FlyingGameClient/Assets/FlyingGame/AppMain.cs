using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGF;

public class AppMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debugger.EnableLog = true;
        Debugger.EnableSave = true;

        Debugger.Log("Test");
        this.Log("ttttt");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
