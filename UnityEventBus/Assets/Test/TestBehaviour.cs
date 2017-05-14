using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventBus.API;
using UnityEventBus.Core;

public class TestBehaviour : MonoBehaviour {

    public EventBus eventBus;

	// Use this for initialization
	void Awake()
    {
        eventBus = new SyncEventBus();
        eventBus.Register(this);
	}
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            eventBus.Post("Started");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            eventBus.Post("GetHit");
        }

    }

    [Subscribe("Started")]
    public void OnStarted1(string message)
    {
        Debug.Log("TestBehaviour::OnStarted1: " + message);
    }

    [Subscribe("Started")]
    public void OnStarted2(string message)
    {
        Debug.Log("TestBehaviour::OnStarted2:" + message);
    }

    [Subscribe("GetHit")]
    public void OnGetHit(string message)
    {
        Debug.Log("TestBehaviour::OnGetHit:" + message);
    }

}
