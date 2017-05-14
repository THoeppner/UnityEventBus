using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventBus.Core;

public class TestBehaviour2 : MonoBehaviour {

    public TestBehaviour behaviour;

	// Use this for initialization
	void Start ()
    {
        behaviour.eventBus.Register(this);
	}
	
    [Subscribe("Started")]
    public void OnStarted1(string message)
    {
        Debug.Log("TestBehaviour2::OnStarted1: " + message);
    }

    [Subscribe("GetHit")]
    public void OnGetHit(string message)
    {
        Debug.Log("TestBehaviour2::OnGetHit:" + message);
    }

}
