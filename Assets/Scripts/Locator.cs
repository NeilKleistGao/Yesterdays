using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class Locator : MonoBehaviour {
    [SerializeField] private Vector2 coordinates;

    private Vector3 Locate() {
        float longtitude = coordinates.x * Mathf.PI / 180F;
        float latitude = coordinates.y * Mathf.PI / 180F;

        float x = Mathf.Cos(latitude) * Mathf.Sin(longtitude);
        float y = Mathf.Sin(latitude);
        float z = -Mathf.Cos(latitude) * Mathf.Cos(longtitude);
        return new Vector3(x, y, z);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, 114514F * Locate());
    }
}
#endif
