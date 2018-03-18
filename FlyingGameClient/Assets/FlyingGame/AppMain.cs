using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGF;
using Kurisu.Service.Core.Example;
using Kurisu.Service.Core;

public class AppMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debugger.EnableLog = true;

        Example exa = new Example();
        exa.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
