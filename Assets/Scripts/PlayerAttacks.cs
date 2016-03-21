using UnityEngine;
using System.Collections;

public class PlayerAttacks : MonoBehaviour {

    public int playerNumber;
    public Color color;

    private float cooldown;
    private float specialCooldown;

    private Transform visual;
    private BoxCollider collider;

    private float size;
    private float targetSize;
    private float offset;
    private float targetOffset;

    private Vector3 paddlePosition;
    private Vector3 paddleScale;
    private Vector3 colliderScale;

    void Start() {
        visual = transform.Find("Visual");
        collider = GetComponent<BoxCollider>();

        paddlePosition = visual.localPosition;
        paddleScale = visual.localScale;
        colliderScale = collider.size;

        size = targetSize = 1;
        offset = targetOffset = 0;
    }

    void Update() {
        size = Mathf.Lerp(size, targetSize, Time.deltaTime * 10);
        offset = Mathf.Lerp(offset, targetOffset, Time.deltaTime * 10);

        visual.localPosition = paddlePosition + Vector3.up * offset;
        visual.localScale = Vector3.Scale(paddleScale, new Vector3(1, size, 1));
        collider.center = Vector3.up * offset;
        collider.size = Vector3.Scale(colliderScale, new Vector3(1, size, 1));

        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
            if (cooldown < 0) {
                cooldown = 0;
            }
        }

        if (specialCooldown > 0) {
            specialCooldown -= Time.deltaTime;
            if (specialCooldown < 0) {
                specialCooldown = 0;
            }
        }

        if (playerNumber == 0) {
            if (Input.GetAxis("Player1_Horizontal") < 0) {
                if (Input.GetAxis("Player1_Vertical") > 0) {
                    targetSize = 1;
                    targetOffset = 3;
                }
                else if (Input.GetAxis("Player1_Vertical") < 0) {
                    targetSize = 1;
                    targetOffset = -3;
                }
                else {
                    targetSize = 1.5f;
                    targetOffset = 0;
                }
            }
            else {
                targetSize = 1;
                targetOffset = 0;

                if (Input.GetAxis("Player1_Shot") > 0 && cooldown == 0) {

                    PlayerBullet bullet = GameState.SpawnPlayerBullet(transform.position, playerNumber);


                    Vector3 initialVelocity = new Vector3(Random.Range(-3, 0.2f), Random.Range(-2.5f, 2.5f), 0);
                    Vector3 targetVelocity = new Vector3(Random.Range(6, 8), Random.Range(-0.5f, 0.5f) + Input.GetAxis("Player1_Vertical"), 0);

                    bullet.velocity = initialVelocity;
                    bullet.movement = new ApproachVelocity(targetVelocity);
                    bullet.color = color;

                    if (Input.GetAxis("Player1_Vertical") != 0) {
                        cooldown += 0.1f;
                    }
                    else {
                        cooldown += 0.2f;
                    }
                }
            }

            if (Input.GetAxis("Player1_Special") > 0 && specialCooldown == 0) {
                if (GameState.Player1Mp == 100) {
                    SfxManager.PlaySfxPinkUltimate();
                    StartCoroutine(player1Ultimate());

                    GameState.Player1Mp -= 100;
                    specialCooldown += 8;
                }
                else if (GameState.Player1Mp >= 25) {
                    SfxManager.PlaySfxPinkSpecial();
                    StartCoroutine(player1Special());

                    GameState.Player1Mp -= 25;
                    specialCooldown += 0.5f;
                }
            }
        }
        else {
            if (Input.GetAxis("Player2_Horizontal") > 0) {
                if (Input.GetAxis("Player2_Vertical") > 0) {
                    targetSize = 0.75f;
                    targetOffset = 2.5f;
                }
                else if (Input.GetAxis("Player2_Vertical") < 0) {
                    targetSize = 0.75f;
                    targetOffset = -2.5f;
                }
                else {
                    targetSize = 2f;
                    targetOffset = 0;
                }
            }
            else {
                targetSize = 1;
                targetOffset = 0;

                if (Input.GetAxis("Player2_Shot") > 0 && cooldown == 0) {
                    float speed = 7;

                    for (int i = 1; i < 6; i++) {
                        for (int j = 0; j < i; j++) {
                            float angle;
                            angle = ((13 + 2 * Input.GetAxis("Player2_Vertical")) * (j + 0.5f) - 13 * i / 2) * Mathf.Deg2Rad;

                            PlayerBullet bullet = GameState.SpawnPlayerBullet(transform.position, playerNumber);
                            bullet.color = color;

                            bullet.velocity = new Vector3(-Mathf.Cos(angle), Mathf.Sin(angle), 0) * (speed - i * 0.5f);
                        }
                    }

                    cooldown += 1;
                }
            }

            if (Input.GetAxis("Player2_Special") > 0 && specialCooldown == 0) {

                if (GameState.Player2Mp == 100) {
                    SfxManager.PlaySfxBlueUltimate();

                    StartCoroutine(player2Ultimate());

                    GameState.Player2Mp = 0;
                    specialCooldown += 8;
                }
                else if (GameState.Player2Mp >= 20) {
                    SfxManager.PlaySfxBlueSpecial();
                    StartCoroutine(player2Special(transform.position.y));

                    GameState.Player2Mp -= 20;
                    specialCooldown += 0.5f;
                }
            }
        }
    }

    private IEnumerator player1Special() {
        for (int i = 0; i < 50; i++) {
            PlayerBullet bullet = GameState.SpawnPlayerBullet(new Vector3(-9, Random.Range(-5f, 4f), 0), playerNumber);
            bullet.velocity = new Vector3(Random.Range(5, 5.5f), Random.Range(-0.5f, 0.5f), 0);
            bullet.color = color;

            yield return new WaitForSeconds(0.07f);
        }
    }

    private IEnumerator player1Ultimate() {
        StartCoroutine(bulletStorm(100, 8));
        yield return new WaitForSeconds(2);
        StartCoroutine(bulletStorm(100, 6));
        yield return new WaitForSeconds(2);
        StartCoroutine(bulletStorm(100, 4));
        yield return new WaitForSeconds(1.6f);
        StartCoroutine(bulletStorm(200, 2.4f));
    }

    private IEnumerator bulletStorm(int amount, float totalTime) {
        for (int i = 0; i < amount; i++) {
            Vector3 position = new Vector3(-9, Random.Range(-5, 5), 0);
            PlayerBullet bullet = GameState.SpawnPlayerBullet(position, playerNumber);

            Vector3 offset = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
            Vector3 velocity = (GameState.Player2.transform.position - position + offset).normalized;

            bullet.velocity = velocity * Random.Range(4, 10);
            bullet.color = color;
            
            yield return new WaitForSeconds(totalTime / amount);
        }
    }


    private IEnumerator player2Special(float y) {
        for (int i = 0; i < 20; i++) {

            PlayerBullet bullet = GameState.SpawnPlayerBullet(new Vector3(9, y - 0.1f * i, 0), playerNumber);
            bullet.movement = new SinewaveMovement(1, Vector3.left * (5 + 0.1f * i), Vector3.up * 5);
            bullet.color = color;

            bullet = GameState.SpawnPlayerBullet(new Vector3(9, y + 0.1f * i, 0), playerNumber);
            bullet.movement = new SinewaveMovement(1, Vector3.left * (5 + 0.1f * i), Vector3.down * 5);
            bullet.color = color;

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator player2Ultimate() {
        for (int i = 0; i < 32; i++) {
            StartCoroutine(player2Special(GameState.Player1.transform.position.y));
            yield return new WaitForSeconds(0.25f);
        }
    }
}
