using UnityEngine;
using System.Collections;
using Ovr;
public class HmdTapDetector : MonoBehaviour
{
    public delegate void OnHmdTappedEventHandler();
    public static OnHmdTappedEventHandler OnHmdTapped = () => { };
    public float accelerometerThreshold = 200f;

	Hmd hmd;

	void Start()
	{
		Debug.Log("Start");
		//Hmd.Initialize();
		hmd = new Hmd(Hmd.Detect() - 1);
	}
	
	void Update()
    {
        // Ref: http://zabaglione.info/archives/462
        var state = hmd.GetTrackingState(0.0);
		if ((state.StatusFlags & (uint) Ovr.StatusBits.OrientationTracked) != 0) {
			//Debug.Log("tracked");
            var accel = OVRExtensions.ToVector3(state.RawSensorData.Accelerometer, false);
			//Debug.Log("accel"+accel);
            if (accel.sqrMagnitude > accelerometerThreshold) {
                OnHmdTapped();
				Debug.Log(accel.sqrMagnitude);
            }
        }
    }

	void OnDestroy()
	{
		Debug.Log("Destroy");
		hmd.Dispose();
		Hmd.Shutdown();
	}
}
