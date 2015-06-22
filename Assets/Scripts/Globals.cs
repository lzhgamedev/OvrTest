using UnityEngine;
using System.Collections;

public class Globals{

	static IMediator Mediator;
	// Use this for initialization
	public static void InitSubsystem () {
		Mediator = new IMediator();
	}
	
	// Update is called once per frame
	public static IMediator GetMediator () {
		return Mediator;
	}
}
