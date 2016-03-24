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

        targetVolume = volume = 1;
	}
	
	void Update () {
        if (Time.time - LastUltTime < 8) {
            targetVolume = 0.5f;
        }
        else {
            targetVolume = 1;
        }

        volume = Mathf.Lerp(volume, targetVolume, Time.deltaTime);

        audioSource.volume = volume;
	}
}
