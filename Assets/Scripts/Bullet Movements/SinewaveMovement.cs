using UnityEngine;
using System.Collections;

public class SinewaveMovement : _AbstractMovement {

    private float frequency;

    private Vector3 forward;
    private Vector3 up;

    private float time;

    public SinewaveMovement(float frequency, Vector3 forward, Vector3 up) {
        this.frequency = frequency;
        this.forward = forward;
        this.up = up;
    }

    public override Vector3 updateVelocity(Vector3 currentVelocity, float deltaTime) {
        time += deltaTime;
        return forward + up * Mathf.Sin(time * Mathf.PI * 2 * frequency);
    }
}
