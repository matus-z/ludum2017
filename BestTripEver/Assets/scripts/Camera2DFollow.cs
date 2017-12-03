using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {

    // the target the camera should follow (usually the player)
    public Transform target;

    // the camera distance (z position)
    public float distance = -10f;

    // damping is the amount of time the camera should take to go to the target
    public float damping = 5.0f;

    void Start () {
    }

    void Update () {

        // get the position of the target (AKA player)
        Vector3 wantedPosition = target.TransformPoint(0, 0, distance);

        // set the camera to go to the wanted position in a certain amount of time
        transform.position = Vector3.Lerp (transform.position, wantedPosition, Mathf.Clamp(Time.deltaTime * damping, 0, 1));
    }
}