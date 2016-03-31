using UnityEngine;
using System.Collections;

public class WormMovement : _AbstractMovement {

    private float delay;
    private Vector3 velocityA;
    private Vector3 velocityB;

    private float time;

    public WormMovement(float delay, Vector3 velocityA, Vector3 velocityB) {
        this.delay = delay;
        this.velocityA = velocityA;
        this.velocityB = velocityB;
    }


    public override Vector3 updateVelocity(Vector3 currentVelocity, float deltaTime) {
        Vector3 velocity;
        if (time < delay) {
            velocity = Vector3.Lerp(currentVelocity, velocityA, deltaTime);
        }
        else {
            velocity = Vector3.Lerp(currentVelocity, velocityB, deltaTime);
        }
        time += deltaTime;
        return velocity;
    }

}
