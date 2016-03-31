using UnityEngine;
using System.Collections;

using UsefulThings;

public class PlayerAttacks : MonoBehaviour {

    public int playerNumber;
    public Color color;
    public Color darkColor;
    public Color brightColor;

    public TimeKeeper pinkGradientTime;
    public TimeKeeper blueGradientTime;

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

    void OnEnable() {
        cooldown = 1;
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
            if (Input.GetAxis("Player1_Defend") > 0) {
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
                if (Input.GetAxis("Player1_Shot") > 0) {
                    targetSize = 0.3f;
                    targetOffset = 0;
                }
                else {
                    targetSize = 1;
                    targetOffset = 0;
                }

                if (Input.GetAxis("Player1_Shot") > 0 && cooldown == 0) {


                    /*
                    Vector3 initialVelocity = new Vector3(Random.Range(-3, 0.2f), Random.Range(-2.5f, 2.5f), 0);
                    Vector3 targetVelocity = new Vector3(Random.Range(6, 8), Random.Range(-0.5f, 0.5f) + Input.GetAxis("Player1_Vertical"), 0);

                    bullet.velocity = initialVelocity;
                    bullet.movement = new ApproachVelocity(targetVelocity);
                    */


                    if (GameState.Player1Hp < 10) {
                        for (int i = -2; i <= 2; i++) {

                            float angle = 12 * i * Mathf.Deg2Rad;

                            for (int j = 0; j < 3; j++) {
                                float angleOffset = -3 * j * Input.GetAxis("Player1_Vertical") * Mathf.Deg2Rad;

                                PlayerBullet bullet = GameState.SpawnPlayerBullet(transform.position, playerNumber);
                                Vector3 direction = new Vector3(Mathf.Cos(angle + angleOffset), Mathf.Sin(angle + angleOffset), 0);

                                bullet.velocity = direction * (4 + 0.5f * j);

                                setBulletColor(bullet);
                            }
                        }
                    }
                    else {
                        for (int i = -1; i <= 1; i++) {

                            float angle = 12 * i * Mathf.Deg2Rad;

                            for (int j = 0; j < 2; j++) {
                                float angleOffset = -3 * j * Input.GetAxis("Player1_Vertical") * Mathf.Deg2Rad;

                                PlayerBullet bullet = GameState.SpawnPlayerBullet(transform.position, playerNumber);
                                Vector3 direction = new Vector3(Mathf.Cos(angle + angleOffset), Mathf.Sin(angle + angleOffset), 0);

                                bullet.velocity = direction * (4 + 0.5f * j);

                                setBulletColor(bullet);
                            }
                        }
                    }

                    cooldown += 1;
                }
            }

            if (Input.GetAxis("Player1_Special") > 0 && specialCooldown == 0) {
                if (GameState.Player1Mp == 100) {
                    SfxManager.PlaySfxPinkUltimate();
                    VolumeDecreaseUltimate.LastUltTime = Time.time;
                    DarkenBackgroundUltimate.LastUltTime = Time.time;
                    pinkGradientTime.setTimeTo(-8);

                    StartCoroutine(player1Ultimate());

                    GameState.Player1Mp -= 100;
                    specialCooldown += 8;
                }
                else if (GameState.Player1Mp >= 25) {
                    SfxManager.PlaySfxPinkSpecial();
                    StartCoroutine(player1Special());

                    GameState.Player1Mp -= 25;
                    specialCooldown += 4;
                }
            }
        }
        else {
            if (Input.GetAxis("Player2_Defend") > 0 && cooldown == 0) {
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
                if (Input.GetAxis("Player2_Shot") > 0) {
                    targetSize = 0.3f;
                    targetOffset = 0;
                }
                else {
                    targetSize = 1;
                    targetOffset = 0;
                }

                if (Input.GetAxis("Player2_Shot") > 0 && cooldown == 0) {
                    float speed = 7;

                    if (GameState.Player2Hp < 10) {
                        for (int i = 1; i < 6; i++) {
                            for (int j = 0; j < i; j++) {
                                float angle;
                                angle = ((13 + 3 * Input.GetAxis("Player2_Vertical")) * (j + 0.5f) - 13 * i / 2) * Mathf.Deg2Rad;

                                PlayerBullet bullet = GameState.SpawnPlayerBullet(transform.position, playerNumber);
                                setBulletColor(bullet);

                                bullet.velocity = new Vector3(-Mathf.Cos(angle), Mathf.Sin(angle), 0) * (speed - i * 0.5f);
                            }
                        }
                    }
                    else {
                        for (int i = 1; i < 4; i++) {
                            for (int j = 0; j < i; j++) {
                                float angle;
                                angle = ((13 + 3 * Input.GetAxis("Player2_Vertical")) * (j + 0.5f) - 13 * i / 2) * Mathf.Deg2Rad;

                                PlayerBullet bullet = GameState.SpawnPlayerBullet(transform.position, playerNumber);
                                setBulletColor(bullet);

                                bullet.velocity = new Vector3(-Mathf.Cos(angle), Mathf.Sin(angle), 0) * (speed - i * 0.5f);
                            }
                        }
                    }

                    cooldown += 1;
                }
            }

            if (Input.GetAxis("Player2_Special") > 0 && specialCooldown == 0) {

                if (GameState.Player2Mp == 100) {
                    SfxManager.PlaySfxBlueUltimate();
                    VolumeDecreaseUltimate.LastUltTime = Time.time;
                    DarkenBackgroundUltimate.LastUltTime = Time.time;
                    blueGradientTime.setTimeTo(-8);

                    StartCoroutine(player2Ultimate(GameState.Player2.transform.position));

                    GameState.Player2Mp = 0;
                    specialCooldown += 8;
                }
                else if (GameState.Player2Mp >= 20) {
                    SfxManager.PlaySfxBlueSpecial();
                    StartCoroutine(player2Special(transform.position.y));

                    GameState.Player2Mp -= 20;
                    specialCooldown += 4;
                }
            }
        }
    }

    private void setBulletColor(PlayerBullet bullet) {
        bullet.trailColor = color;
        bullet.darkColor = darkColor;
        bullet.brightColor = brightColor;
    }

    private IEnumerator player1Special() {

        for (int i = 0; i < 10; i++) {

            for (int j = -2; j <= 2; j++) {
                PlayerBullet bullet = GameState.SpawnPlayerBullet(new Vector3(-9, GameState.Player1.transform.position.y + j * 0.3f, 0), playerNumber);
                bullet.velocity = new Vector3(15, 0, 0);
                bullet.GetComponent<TrailRenderer>().enabled = true;
                setBulletColor(bullet);

                bullet.movement = new WormMovement(2, new Vector3(0.5f, 0, 0), new Vector3(15, 0, 0));
            }


            yield return new WaitForSeconds(0.4f);
        }
    }

    private IEnumerator player1Ultimate() {
        float toggle = 1;

        StartCoroutine(bulletSlash(60, 0.2f, -8, -6, 8, 31, toggle *= -1));
        yield return new WaitForSeconds(2);

        StartCoroutine(bulletSlash(60, 0.2f, -8, -4, 8, 31, toggle *= -1));
        yield return new WaitForSeconds(1);
        StartCoroutine(bulletSlash(50, 0.2f, -8, -4, 8, 31, toggle *= -1));
        yield return new WaitForSeconds(1);

        for (int i = 0; i < 4; i++) {
            StartCoroutine(bulletSlash(60, 0.2f, -8, -2, 8, 37, toggle *= -1));
            yield return new WaitForSeconds(0.4f);
        }

        for (int i = 0; i < 8; i++) {
            StartCoroutine(bulletSlash(60, 0.1f, -8, 0, 8, 47, toggle *= -1));
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator bulletSlash(int amount, float totalTime, float minX, float maxX, float speed, float spin, float startY) {
        Vector3 start = new Vector3(Random.Range(minX, maxX), 5 * startY, 0);
        Vector3 end = new Vector3(Random.Range(minX, maxX), -5 * startY, 0);
        Vector3 dPos = (end - start) / amount;

        float angle = Random.Range(0, 2 * Mathf.PI);

        for (int i = 0; i < amount; i++) {
            PlayerBullet bullet = GameState.SpawnPlayerBullet(start + i * dPos, playerNumber);

            Vector3 velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * (speed + 0.3f * (i % 5));
            bullet.movement = new DelayMovement(1, velocity);
            bullet.GetComponent<TrailRenderer>().enabled = true;
            setBulletColor(bullet);

            if (i % 5 == 4) {
                angle += spin * Mathf.Deg2Rad;
            }
            else {
                angle += 2 * Mathf.Deg2Rad;
            }

            yield return new WaitForSeconds(totalTime / amount);
        }
    }

    private IEnumerator player2Special(float y, bool large = true) {
        for (int i = 0; i < 20; i++) {

            if (large) {
                PlayerBullet bullet = GameState.SpawnPlayerBullet(new Vector3(9, y - 0.1f * i, 0), playerNumber);
                bullet.movement = new SinewaveMovement(1, Vector3.left * (5 + 0.1f * i), Vector3.up * 10);
                bullet.GetComponent<TrailRenderer>().enabled = true;
                setBulletColor(bullet);
                bullet.size = 2;


                bullet = GameState.SpawnPlayerBullet(new Vector3(9, y + 0.1f * i, 0), playerNumber);
                bullet.movement = new SinewaveMovement(1, Vector3.left * (5 + 0.1f * i), Vector3.down * 10);
                bullet.GetComponent<TrailRenderer>().enabled = true;
                setBulletColor(bullet);
                bullet.size = 2;
            }
            else {
                PlayerBullet bullet = GameState.SpawnPlayerBullet(new Vector3(9, y - 0.1f * i, 0), playerNumber);
                bullet.movement = new SinewaveMovement(1, Vector3.left * (5 + 0.1f * i), Vector3.up * 5);
                setBulletColor(bullet);


                bullet = GameState.SpawnPlayerBullet(new Vector3(9, y + 0.1f * i, 0), playerNumber);
                bullet.movement = new SinewaveMovement(1, Vector3.left * (5 + 0.1f * i), Vector3.down * 5);
                setBulletColor(bullet);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator player2Ultimate(Vector3 position) {
        float angle = 0;
        for (int i = 0; i < 64; i++) {
            for (int j = 0; j < 12; j++) {
                float direction = (angle + j * 360/12f) * Mathf.Deg2Rad;
                Vector3 velocity = new Vector3(Mathf.Cos(direction), Mathf.Sin(direction), 0) * (2 + i * 0.01f);

                PlayerBullet bullet = GameState.SpawnPlayerBullet(position, playerNumber);
                bullet.velocity = velocity;
                bullet.movement = new CurvedMovement(i % 2 == 0 ? 30 : -30, 1.5f, 10);
                bullet.GetComponent<TrailRenderer>().enabled = true;
                setBulletColor(bullet);
            }
            angle += 7;
            yield return new WaitForSeconds(0.125f);
        }
    }
}
