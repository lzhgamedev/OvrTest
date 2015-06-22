using UnityEngine;
using System.Collections;

public class TowerLogic : MonoBehaviour {

	TowerCollision MyCollision;

	IMediator Mediator;
	// Use this for initialization
	void Start () {
		MyCollision = GetComponentInChildren<TowerCollision>();
		if(MyCollision != null) {
			MyCollision.OnTowerClick = OnTowerClick;
		}
		Mediator = Globals.GetMediator();
		Mediator.RegisterLogic(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTowerClick(bool Toggle) {
		Debug.Log(gameObject + "TowerLogic OnTowerClick" + Toggle);
		Mediator.TowerClicked(Toggle);
	}

}
