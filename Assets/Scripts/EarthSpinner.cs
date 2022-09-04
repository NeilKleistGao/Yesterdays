using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpinner : MonoBehaviour {
    [SerializeField] private Rigidbody earthRigidBody;

    private Vector2 mScreenPosition = Vector2.zero;

    void Start() {
        Input.multiTouchEnabled = true;
    }

    void Update() {
        if (Input.touchCount == 1) {
            var touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began) {
                Stop();
                mScreenPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) {
                Rotate(touch.position);
            }
        }
        else if (Input.touchCount == 2) {
            Stop();
            // TODO: scale
        }

        if (Input.GetMouseButtonDown(0)) {
            Stop();
            mScreenPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) {
            Rotate(Input.mousePosition);
        }
    }

    private void Stop() {
        earthRigidBody.angularVelocity = Vector3.zero;
    }

    private void Rotate(Vector2 newPosition) {
        var sub = newPosition - mScreenPosition;
        mScreenPosition = newPosition;

        earthRigidBody.AddForceAtPosition(sub * Time.deltaTime * 20.0f, Vector3.back);
    }
}
