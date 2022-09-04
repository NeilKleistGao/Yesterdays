using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpinner : MonoBehaviour {
    private Vector2 mScreenPosition = Vector2.zero;

    void Start() {
        Input.multiTouchEnabled = true;
    }

    void Update() {
        if (Input.touchCount == 1) {
            var touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began) {
                mScreenPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) {
                Rotate(touch.position);
            }
        }
        else if (Input.touchCount == 2) { 
            // TODO: scale
        }

        if (Input.GetMouseButtonDown(0)) {
            mScreenPosition = Camera.main.WorldToScreenPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0)) {
            Rotate(Camera.main.WorldToScreenPoint(Input.mousePosition));
        }
    }

    private void Rotate(Vector2 newPosition) {
        var sub = newPosition - mScreenPosition;
        mScreenPosition = newPosition;

        if (sub.magnitude > 0.1f) {
            var angle = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.AngleAxis(angle + sub.x * Time.deltaTime, Vector3.up);
        }
    }
}
