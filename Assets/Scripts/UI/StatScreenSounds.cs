using UnityEngine;
using System.Collections;

using UsefulThings;

public class StatScreenSounds : MonoBehaviour {
    void OnEnable() {
        StartCoroutine(playSounds());
    }

    private IEnumerator playSounds() {
        yield return new WaitForSeconds(1);
        CameraShake.ShakeCamera(1, 0.5f);
        SfxManager.PlaySfxBallHitWall();
        yield return new WaitForSeconds(1);
        CameraShake.ShakeCamera(1, 0.5f);
        SfxManager.PlaySfxBallHitWall();
    }
}
