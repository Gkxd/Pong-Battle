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
    }

    void OnEnable() {
        position = transform.localPosition;
    }

    void Update() {
        if (playerNumber == 0) {
            float scale = 1;
            if (Input.GetAxis("Player1_Defend") > 0) {
                scale = 0.25f;
            }
            else if (Input.GetAxis("Player1_Shot") > 0) {
                scale = 0.5f;
            }

            Vector3 movement = new Vector3(Input.GetAxis("Player1_Horizontal"), Input.GetAxis("Player1_Vertical"), 0).normalized * playerSpeed * Time.deltaTime * scale;

            position += movement;
        }
        else {
            float scale = 1;
            if (Input.GetAxis("Player2_Defend") > 0) {
                scale = 0.25f;
            }
            else if (Input.GetAxis("Player2_Shot") > 0) {
                scale = 0.5f;
            }

            Vector3 movement = new Vector3(Input.GetAxis("Player2_Horizontal"), Input.GetAxis("Player2_Vertical"), 0).normalized * playerSpeed * Time.deltaTime * scale;

            position += movement;
        }

        if (playerColorTime > 0) {
            playerColorTime -= 4 * Time.deltaTime;
        }

        position.y = Mathf.Clamp(position.y, -5, 4);
        if (playerNumber == 0) {
            position.x = Mathf.Clamp(position.x, -8.88f, -6);
        }
        else if (playerNumber == 1) {
            position.x = Mathf.Clamp(position.x, 6, 8.88f);
        }
        transform.localPosition = position;
        playerMaterial.color = playerColor.Evaluate(playerColorTime);
    }

    void OnCollisionEnter(Collision collision) {
        playerColorTime = 1;
    }
}
