using UnityEngine;
using System.Collections;

public class TowerView : MonoBehaviour {

	IMediator Mediator;
	// Use this for initialization
	void Start () {
		Mediator = Globals.GetMediator();
		Mediator.RegisterView(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
