using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flight;

public class FlyingController3 : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        this.transform.Translate(transform.forward * Time.deltaTime * 10);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x - 2f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x + 2f, 0, 0);
        }*/

        if (Input.GetButton(InputConstans.MFB))
        {   //拉升
            //按下WS
            Debug.Log("按下WS: " + Input.GetAxis(InputConstans.MFB));
        }
        if (Input.GetButton(InputConstans.RLR))
        {
            //按下AD
            Debug.Log("按下AD: " + Input.GetAxis(InputConstans.RLR));
        }
        /*
        if (Input.GetButton(InputConstans.MUD))
        {
            Debug.Log("滑轮控制速度: " + Input.GetAxis(InputConstans.MUD));
        }
        */
        /*
        if (Input.GetButton(InputConstans.MLR))
        {

        }
        */
        /*
            flight.MoveLR(Input.GetAxis(InputConstans.MLR));


        if (Input.GetButtonDown(InputConstans.SLR))
        {

            flight.StuntLR(Input.GetAxis(InputConstans.SLR));

        }
        if (Input.GetButtonDown(InputConstans.SUD))
        {
            flight.StuntUD(Input.GetAxis(InputConstans.SUD));
        }

        if (!GCC)
        {
            if (Camera.main)
                GCC = Camera.main.GetComponent<GameCameraControl>();
            return;
        }
        if (GCC)
            GCC.IsS = flight.IsSing;
        */
    }
}
