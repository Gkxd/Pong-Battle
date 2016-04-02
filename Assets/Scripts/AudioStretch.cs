using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioStretch : MonoBehaviour {

    AudioSource audio;

    void Start() {
        audio = GetComponent<AudioSource>();
    }
    void Update() {
        audio.pitch = Time.timeScale;
    }
}
