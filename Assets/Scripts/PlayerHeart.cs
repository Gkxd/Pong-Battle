using UnityEngine;
using System.Collections;


public class PlayerHeart : MonoBehaviour {

    public int playerNumber; // 0 or 1
    public Gradient playerColor;

    private float playerColorTime;
    private Material playerMaterial;

    private float lastHitTime;

    void Start() {
        playerMaterial = transform.Find("Visual").GetComponent<Renderer>().material;
    }

    void Update() {
        playerMaterial.color = playerColor.Evaluate(playerColorTime);

        if (Time.time - lastHitTime < 2) {
            if (Time.time % 0.1f > 0.05f) {
                playerMaterial.color = playerMaterial.color * new Color(1, 1, 1, 0.5f);
            }
            else {
                playerMaterial.color = playerMaterial.color * new Color(1, 1, 1, 0.8f);
            }
        }

        if (playerColorTime > 0) {
            playerColorTime -= 4 * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (!GameState.IsGameOver) {
            PlayerBullet bullet = collider.GetComponent<PlayerBullet>();
            if (bullet.playerNumber != playerNumber) {
                if (Time.time - lastHitTime > 2) {
                    playerColorTime = 1;
                    if (playerNumber == 0) {
                        GameState.Player1Hp -= 3;
                    }
                    else {
                        GameState.Player2Hp -= 3;
                    }

                    //SfxManager.PlaySfxMissileSpawn();
                    SfxManager.PlaySfxHurt();
                }
            }
        }
    }
}