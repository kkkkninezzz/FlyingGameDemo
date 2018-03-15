using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flight;

[RequireComponent(typeof(AbstractFlight))]
public class FlyingController3 : MonoBehaviour {
    private AbstractFlight flight;
	// Use this for initialization
	void Start () {
        flight = GetComponent<AbstractFlight>();
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
            // Debug.Log("按下WS: " + Input.GetAxis(InputConstans.MFB));
            flight.RoteUD(Input.GetAxis(InputConstans.MFB));
        }
        if (Input.GetButton(InputConstans.RLR))
        {
            //按下AD
            //Debug.Log("按下AD: " + Input.GetAxis(InputConstans.RLR));
             flight.RoteLR(Input.GetAxis(InputConstans.RLR));
            //flight.MoveLR(Input.GetAxis(InputConstans.RLR));
        }

        flight.Operational();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("进入墙体了");
    }
}
