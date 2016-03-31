using UnityEngine;
using System.Collections;

public class DelayMovement : _AbstractMovement {

    private float time;
    private float delay;

    private Vector3 velocity;

    public DelayMovement(float delay, Vector3 velocity) {
        this.delay = delay;
        this.velocity = velocity;
    }

    public override Vector3 updateVelocity(Vector3 currentVelocity, float deltaTime) {
        time += deltaTime;

        if (time > delay) {
            return velocity;
        }
        else {
            return velocity * 0.01f;
        }
    }
}
