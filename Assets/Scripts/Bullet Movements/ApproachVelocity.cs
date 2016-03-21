using UnityEngine;
using System.Collections;

public class ApproachVelocity : _AbstractMovement {

    private Vector3 targetVelocity;

    public ApproachVelocity(Vector3 velocity) {
        targetVelocity = velocity;
    }

    public override Vector3 updateVelocity(Vector3 currentVelocity, float deltaTime) {
        return Vector3.Lerp(currentVelocity, targetVelocity, deltaTime);
    }
}
