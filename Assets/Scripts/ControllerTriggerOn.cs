using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControllerTriggerOn : MonoBehaviour {

    public SteamVR_TrackedObject tracked_controller;


    // Use this for initialization
    void Start () {
        tracked_controller = this.transform.gameObject.GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        var device_controller = SteamVR_Controller.Input((int)tracked_controller.index);

        if (device_controller.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("トリガーを浅く引いた");

            // Laser(Cube)にアクセス
            var LaserPointerScript = GetComponent<SteamVR_LaserPointer>();
            GameObject laser = LaserPointerScript.pointer;

            // Laserと衝突しているGameObjectを取得
            var ColliderScript = laser.GetComponent<LaserPoniterCollider>();
            GameObject _collidingObject = ColliderScript.collidingObject;

            if (_collidingObject == null)
            {
                Debug.Log("No Colliding!");
            }
            else
            {
                _collidingObject.GetComponent<Button>().onClick.Invoke();
            }

        }

    }
}
