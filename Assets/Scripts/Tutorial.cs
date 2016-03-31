using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {


    public static bool player1Moved;
    public static bool player2Moved;

    public static bool player1Shot;
    public static bool player2Shot;

    public static bool player1Special;
    public static bool player2Special;

    public static Tutorial instance;

	void OnEnable () {
        instance = this;
	}
}
