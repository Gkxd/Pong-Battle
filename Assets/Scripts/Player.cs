using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int playerNumber; // 0 or 1
    public Gradient playerColor;

    public float playerSpeed;

    private float playerColorTime;

    private Material playerMaterial;

    private Vector3 position;

    void Start() {
        playerMaterial = transform.Find("Visual").GetComponent<Renderer>().material;
        position = transform.localPosition;
    }

    void Update() {
        if (playerNumber == 0) {
            float scale = Input.GetAxis("Player1_Horizontal") < 0 ? 0.2f : 1;
            position.y += Input.GetAxis("Player1_Vertical") * playerSpeed * Time.deltaTime * scale;
        }
        else {
            float scale = Input.GetAxis("Player2_Horizontal") > 0 ? 0.2f : 1;
            position.y += Input.GetAxis("Player2_Vertical") * playerSpeed * Time.deltaTime * scale;
        }

        if (playerColorTime > 0) {
            playerColorTime -= 4 * Time.deltaTime;
        }

        position.y = Mathf.Clamp(position.y, -5, 4);
        transform.localPosition = position;
        playerMaterial.color = playerColor.Evaluate(playerColorTime);
    }

    void OnCollisionEnter(Collision collision) {
        playerColorTime = 1;
    }
}
