using UnityEngine;
using System.Collections;

public class CurvedMovement : _AbstractMovement {

    private Matrix4x4 transformMatrix;
    private float maxSpeed;

    public CurvedMovement(float rotation, float speedIncrease, float maxSpeed) {
        this.maxSpeed = maxSpeed;

        float radians = rotation * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radians) * speedIncrease;
        float sin = Mathf.Sin(radians) * speedIncrease;

        transformMatrix.SetRow(0, new Vector4(cos, sin, 0, 0));
        transformMatrix.SetRow(1, new Vector4(-sin, cos, 0, 0));
        transformMatrix.SetRow(2, Vector4.zero);
        transformMatrix.SetRow(3, Vector4.zero);
    }

    public override Vector3 updateVelocity(Vector3 currentVelocity, float deltaTime) {
        Vector3 newVelocity = transformMatrix.MultiplyVector(currentVelocity);

        if (newVelocity.sqrMagnitude > maxSpeed * maxSpeed) {
            newVelocity.Normalize();
            newVelocity *= maxSpeed;
        }

        return Vector3.Slerp(currentVelocity, newVelocity, deltaTime);
    }
}
