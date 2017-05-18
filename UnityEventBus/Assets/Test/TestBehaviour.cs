
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
    public void OnStarted(EventArgument e)
    {
        Debug.Log("TestBehaviour::OnStarted: " + e.Data);
    }

    [Subscribe("GetHit")]
    public void OnGetHit(EventArgument e)
    {
        Debug.Log("TestBehaviour::OnGetHit:" + e.Data);
    }

}
