
using UnityEngine;
using UnityEventBus.API;
using UnityEventBus.Attributes;
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
    public void OnStarted1(EventArgument e)
    {
        Debug.Log("TestBehaviour::OnStarted1: " + e.Data);
    }

    [Subscribe("Started")]
    public void OnStarted2(EventArgument e)
    {
        Debug.Log("TestBehaviour::OnStarted2:" + e.Data);
    }

    [Subscribe("GetHit")]
    public void OnGetHit(EventArgument e)
    {
        Debug.Log("TestBehaviour::OnGetHit:" + e.Data);
    }

}
