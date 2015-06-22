using UnityEngine;
using System.Collections;

public class TowerCollision : MonoBehaviour, IClickable {

	public delegate void ClickDelegate(bool Toggle);
	public ClickDelegate OnTowerClick;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClicked() {
		Debug.Log(gameObject + "TowerCollision OnClicked");
		OnTowerClick(true);
	}

	public void OnUnClicked() {
		Debug.Log(gameObject + "TowerCollision OnUnClicked");
		OnTowerClick(false);
	}
}
