using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class HmdInputModule : StandaloneInputModule
{
    private const float SWITCH_MOUSE_TO_HMD_TIME = 0.5f;

	public KeyCode hmdClickKey = KeyCode.Space;
    public enum Device
    {
        Mouse, HMD
    }

    public Device currentDevice = Device.Mouse;
    private Vector3 lastMousePosition_ = Vector3.zero;
    private float mouseOrKeyboardUnusedTime_ = 0f;
    private GameObject currentFocusedObject_ = null;

	static HmdInputModule _singleton;


	public override void ActivateModule()
    {
        base.ActivateModule();
        HmdTapDetector.OnHmdTapped += OnHmdTapped;
    }

    public override void Process()
    {
        CheckCurrentDevice();
        switch (currentDevice) {
            case Device.Mouse: base.Process(); break;
            case Device.HMD: ProcessHMD(); break;
        }
    }

    void CheckCurrentDevice()
    {
        mouseOrKeyboardUnusedTime_ += Time.deltaTime;
        if (Input.GetMouseButton(0) || Input.mousePosition != lastMousePosition_) {
            mouseOrKeyboardUnusedTime_ = 0f;
        }
        lastMousePosition_ = Input.mousePosition;
        if (mouseOrKeyboardUnusedTime_ > SWITCH_MOUSE_TO_HMD_TIME) {
            currentDevice = Device.HMD;
        } else {
            currentDevice = Device.Mouse;
            if (currentFocusedObject_ != null) {
				var exitData = GetMousePointerEventData().GetButtonState(0).eventData.buttonData;
				exitData.pointerEnter = currentFocusedObject_;
                ExecuteEvents.ExecuteHierarchy(currentFocusedObject_, exitData, ExecuteEvents.pointerExitHandler);
                currentFocusedObject_ = null;
            }
        }
    }

    void ProcessHMD()
    {

        var pointerData = GetMousePointerEventData();
		var underCursorObject = pointerData.GetButtonState(0).eventData.buttonData.pointerCurrentRaycast.gameObject;

        if (currentFocusedObject_ != underCursorObject) {
            if (currentFocusedObject_ != null) {
				var exitData = pointerData.GetButtonState(0).eventData.buttonData;
				exitData.pointerEnter = currentFocusedObject_;
                ExecuteEvents.ExecuteHierarchy(currentFocusedObject_, exitData, ExecuteEvents.pointerExitHandler);
            }
            if (underCursorObject != null) {
				var enterData = pointerData.GetButtonState(0).eventData.buttonData;
				enterData.pointerEnter = underCursorObject;
				ExecuteEvents.ExecuteHierarchy(underCursorObject, enterData, ExecuteEvents.pointerEnterHandler);
            }
            currentFocusedObject_ = underCursorObject;
        }
    }


    void OnHmdTapped()
    {
        if (currentFocusedObject_ != null) {
			var pointerData = GetMousePointerEventData().GetButtonState(0).eventData.buttonData;
			pointerData.pointerEnter = currentFocusedObject_;
            ExecuteEvents.ExecuteHierarchy(currentFocusedObject_, pointerData, ExecuteEvents.pointerDownHandler);
            StartCoroutine(Submit(pointerData));
        }
    }

    IEnumerator Submit(PointerEventData pointerData)
    {
        yield return new WaitForSeconds(0.5f);
        ExecuteEvents.ExecuteHierarchy(currentFocusedObject_, pointerData, ExecuteEvents.pointerUpHandler);
        ExecuteEvents.ExecuteHierarchy(currentFocusedObject_, pointerData, ExecuteEvents.submitHandler);
    }
}
