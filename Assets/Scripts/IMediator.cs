using UnityEngine;
using System.Collections;

public class IMediator {
	TowerView _View;
	TowerLogic _Logic;

	public void RegisterView(TowerView View) {
		_View = View;
	}

	public void RegisterLogic(TowerLogic Logic) {
		_Logic = Logic;
	}
		
	public void TowerClicked(bool Toggle) {
		_View.gameObject.SetActive(Toggle);
	}
}
