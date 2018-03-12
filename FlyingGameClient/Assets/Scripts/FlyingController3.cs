using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(transform.forward * Time.deltaTime * 10);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x - 2f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x + 2f, 0, 0);
        }
    }
}
