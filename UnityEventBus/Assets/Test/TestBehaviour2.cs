
using UnityEngine;
using UnityEventBus.API;
using UnityEventBus.Attributes;


public class TestBehaviour2 : MonoBehaviour {

    public TestBehaviour behaviour;

	// Use this for initialization
	void Start ()
    {
        behaviour.eventBus.Register(this);
	}
	
    [Subscribe("Started")]
    public void OnStarted(EventArgument e)
    {
        Debug.Log("TestBehaviour2::OnStarted: " + e.Data);
    }

    [Subscribe("GetHit")]
    public void OnGetHit(EventArgument e)
    {
        Debug.Log("TestBehaviour2::OnGetHit:" + e.Data);
    }

    [Subscribe("Delayed")]
    public void OnDelayed(EventArgument e)
    {
        Debug.Log("TestBehaviour2::OnDelayed: " + e.Data);
    }

}
