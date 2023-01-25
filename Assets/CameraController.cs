using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform player;
    public float dampTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    void Update () {
        if (player == null) { return; }
        Vector3 point = gameObject.GetComponent<Camera>().WorldToViewportPoint(player.position);
        Vector3 delta = player.position - gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}
