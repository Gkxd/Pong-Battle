using UnityEngine;
using System.Collections;

public class SwitchUIScreens : MonoBehaviour {
    public static float lastTimeTriggered = -1;


    public GameObject screenA;
    public GameObject screenB;

    public GameObject screenA2;

    public GameObject deactivateA;
    public GameObject deactivateB;

    void Update() {
        if (Time.time - lastTimeTriggered > 0.5f) {
            if (Input.GetAxis("Player1_Shot") > 0 || Input.GetAxis("Player2_Shot") > 0) {
                screenA.SetActive(true);

                if (screenA2) {
                    screenA2.SetActive(true);
                }

                lastTimeTriggered = Time.time;
                gameObject.SetActive(false);

                if (deactivateA) deactivateA.SetActive(false);
                if (deactivateB) deactivateB.SetActive(false);

                SfxManager.PlaySfxUiSwitch();
            }
            else if ((screenB != null) && (Input.GetAxis("Player1_Defend") > 0 || Input.GetAxis("Player2_Defend") > 0 || Input.GetAxis("Player1_Special") > 0 || Input.GetAxis("Player2_Special") > 0)) {
                screenB.SetActive(true);
                gameObject.SetActive(false);

                lastTimeTriggered = Time.time;
                gameObject.SetActive(false);

                SfxManager.PlaySfxUiSwitch();
            }
        }
    }
}
