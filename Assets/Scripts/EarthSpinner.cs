using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpinner : MonoBehaviour {
    [SerializeField] private Rigidbody earthRigidBody;

    private Vector2 mScreenPosition = Vector2.zero;
    private Vector2[] scalePositions = new Vector2[2];

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

            Vector2[] newPositions = new Vector2[2];
            for (int i = 0; i < 2; ++i) {
                var touch = Input.touches[i];
                if (touch.phase == TouchPhase.Moved) {
                    newPositions[i] = touch.position;
                }
                else {
                    newPositions[i] = scalePositions[i];
                }
            }

            float oldLeng = (scalePositions[1] - scalePositions[0]).magnitude;
            float newLeng = (newPositions[1] - newPositions[0]).magnitude;
            Scale(newLeng - oldLeng);
        }

        if (Input.GetMouseButtonDown(0)) {
            Stop();
            mScreenPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) {
            Rotate(Input.mousePosition);
        }

        Scale(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void Stop() {
        earthRigidBody.angularVelocity = Vector3.zero;
    }

    private void Rotate(Vector2 newPosition) {
        var sub = newPosition - mScreenPosition;
        mScreenPosition = newPosition;

        earthRigidBody.AddForceAtPosition(sub * Time.deltaTime * 20.0f, Vector3.back);
    }

    private void Scale(float delta) {
        float res = transform.localScale.x + delta;
        res = Mathf.Clamp(res, 1.0f, 2.0f);
        transform.localScale = new Vector3(res, res, res);
    }
}
