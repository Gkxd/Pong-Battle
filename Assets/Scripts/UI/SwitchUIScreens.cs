using UnityEngine;
using System.Collections;

public class SwitchUIScreens : MonoBehaviour {
    public static float lastTimeTriggered = -1;


    public GameObject screenA;
    public GameObject screenB;

    public GameObject screenA2;

    void Update() {
        if (Time.time - lastTimeTriggered > 0.5f) {
            if (Input.GetAxis("Player1_Shot") > 0 || Input.GetAxis("Player2_Shot") > 0) {
                screenA.SetActive(true);

                if (screenA2) {
                    screenA2.SetActive(true);
                }

                lastTimeTriggered = Time.time;
                gameObject.SetActive(false);
            }
            else if (Input.GetAxis("Player1_Special") > 0 || Input.GetAxis("Player2_Special") > 0) {
                screenB.SetActive(true);
                gameObject.SetActive(false);

                lastTimeTriggered = Time.time;
                gameObject.SetActive(false);
            }
        }
    }
}
