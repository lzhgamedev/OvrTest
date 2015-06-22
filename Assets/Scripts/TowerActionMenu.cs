using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerActionMenu : MonoBehaviour {

	List<GameObject> ActionButtons;
	//ToDo: set button delegate
	//http://stackoverflow.com/questions/29414765/onclick-listener-delegation-on-ui-button
	public int MaxActionCount;

	public int DisplayCount;

	float ButtonHeight;
	float ButtonVerticalInterval;
	float ButtonZ;
	// Use this for initialization
	void Start () {
		ActionButtons = new List<GameObject>();
		for(int i = 0; i < MaxActionCount; ++i) {
			var button = Instantiate(Resources.Load("ActionButton")) as GameObject;
			button.transform.parent = transform;
			ActionButtons.Add(button);
			button.SetActive(false);
		}
		ButtonHeight = ActionButtons[0].GetComponent<RectTransform>().rect.height;
		ButtonZ = -10;
		ButtonVerticalInterval = ButtonHeight * 1.2f;

		ShowActionButtons(DisplayCount);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ShowActionButtons(int num) {
		if(num < 1)
			return;
		var VerticalOffset = (num - 1) * ButtonVerticalInterval * 0.5f;
		for(int i = 0; i < num; ++i) {
			ActionButtons[i].SetActive(true);
			ActionButtons[i].GetComponent<RectTransform>().transform.localPosition = new Vector3 (0, i * ButtonVerticalInterval - VerticalOffset, ButtonZ);
		}
	}


}
