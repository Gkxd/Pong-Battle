using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class VolumeDecreaseUltimate : MonoBehaviour {

    public static float LastUltTime;

    private AudioSource audioSource;

    private float targetVolume;
    private float volume;

	void Start () {
        LastUltTime = -100;

        audioSource = GetComponent<AudioSource>();

        targetVolume = volume = 0.3f;
	}
	
	void Update () {
        if (Time.time - LastUltTime < 8) {
            targetVolume = 0.1f;
        }
        else {
            targetVolume = 0.3f;
        }

        volume = Mathf.Lerp(volume, targetVolume, Time.deltaTime);

        audioSource.volume = volume;
	}
}
