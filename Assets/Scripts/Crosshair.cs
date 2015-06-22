using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public float filter = 0.1f;
    public float crosshairDistance = 10f;

	IClickable LastCapturedActor;

	//debug
	public int lengthOfLineRenderer = 2;
	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	void Start() {
		/*
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
		*/
	}

    void Update()
    {
        
		var inputModule = UnityEngine.EventSystems.EventSystem.current.currentInputModule as HmdInputModule;
        if (inputModule && inputModule.currentDevice == HmdInputModule.Device.Mouse) {
            var mousePosition = new Vector3(Input.mousePosition.x , Input.mousePosition.y , crosshairDistance);
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

        } else {
            var centerPosition = new Vector3(0.5f, 0.5f, crosshairDistance);
            transform.position += (Camera.main.ViewportToWorldPoint(centerPosition) - transform.position) * filter;
        }
        transform.LookAt(Camera.main.transform);
        transform.Rotate(Camera.main.transform.up, 180);

		var CurrentCapturedActor = TryPickClickable();
		if(CurrentCapturedActor != null && CurrentCapturedActor != LastCapturedActor) {
			StopCoroutine("SelectTimer");
			StartCoroutine("SelectTimer", 1.0f);
		}
		else if(CurrentCapturedActor == null && LastCapturedActor != null) {
			StopCoroutine("SelectTimer");
			LastCapturedActor.OnUnClicked();
		}
		LastCapturedActor = CurrentCapturedActor;

		/*
		var direction = Camera.main.transform.forward;
		var origin = transform.position + 0.001f * direction;
		if(Input.GetMouseButtonDown(0)) {
			LineRenderer lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.SetPosition(0, origin);
			lineRenderer.SetPosition(1, origin + 100 * direction);
		}
		*/
    }

	IClickable TryPickClickable() {
		var direction = Camera.main.transform.forward;
		var origin = transform.position;
		//Debug.DrawLine(origin, origin + 100 * direction, Color.red);
		RaycastHit hit_info;
		//Ray hit_ray = Camera.main.ScreenPointToRay(0.5f, 0.5f, crosshairDistance);

		var mask = 1 << LayerMask.NameToLayer("Clickable");
		var Picked = Physics.Raycast(origin, direction, out hit_info, Mathf.Infinity, mask);
		//var Picked = Physics.Raycast( out hit_info);
		IClickable PickedCollider;
		if(Picked) {
			PickedCollider = hit_info.collider.GetComponent(typeof(IClickable)) as IClickable;
			//Debug.Log(hit_info.collider.GetComponent<TowerCollision>());
		} else {
			PickedCollider = null;
		}
		//Debug.Log(PickedCollider);
		if(PickedCollider != null) {
			return PickedCollider;
		} else {
			return null;
		}
	}

	IEnumerator SelectTimer(float time) {
		yield return new WaitForSeconds(time);
		Debug.Log("SelectTimer");
		LastCapturedActor.OnClicked();
	}
}
