using UnityEngine;
using System.Collections;

public abstract class _AbstractMovement {
    public abstract Vector3 updateVelocity(Vector3 currentVelocity, float deltaTime);
}
