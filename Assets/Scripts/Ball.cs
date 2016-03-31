using UnityEngine;
using System.Collections;

using UsefulThings;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrailRenderer))]
public class Ball : MonoBehaviour {
    public GameObject ballDeathPrefab;
    public Gradient trailColor;
    public float speed;

    private int lastTouchedPlayer = -1;
    private int bonusPoints;

    private new Rigidbody rigidbody;

    private Vector3 velocity;
    private GameObject visual;
    private TrailRenderer trailRenderer;
    private GameObject effect1;
    private GameObject effect2;

    private bool initialized;
    private bool destroyed;
    private float trailColorTime = 0.5f;
    private float targetTrailColorTime = 0.5f;

    private float effect1Scale;
    private float effect2Scale;
    private float effect1TargetScale;
    private float effect2TargetScale;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
        visual = transform.Find("Visual").gameObject;
        effect1 = transform.Find("Effect1").gameObject;
        effect2 = transform.Find("Effect2").gameObject;

        transform.localScale = new Vector3(0, 0, 1);

        effect1Scale = effect1TargetScale = 7;
        effect2Scale = effect2TargetScale = 8;

        GameState.BallCount++;
    }

    public void randomizeDirection() {
        if (rigidbody == null) {
            rigidbody = GetComponent<Rigidbody>();
        }

        float direction = (Random.Range(-30, 30) + (Random.value < 0.5 ? 0 : 180)) * Mathf.Deg2Rad;

        rigidbody.velocity = velocity = new Vector3(Mathf.Cos(direction), Mathf.Sin(direction), 0) * speed;

        initialized = true;
    }

    void Update() {
        Color c = trailColor.Evaluate(trailColorTime);

        trailRenderer.material.SetColor("_Color", c);

        trailColorTime = Mathf.Lerp(trailColorTime, targetTrailColorTime, 10 * Time.deltaTime);

        effect1.transform.localScale = new Vector3(effect1Scale, effect1Scale, 1);
        effect2.transform.localScale = new Vector3(effect2Scale, effect2Scale, 1);


        effect1Scale = Mathf.Lerp(effect1Scale, effect1TargetScale, 10 * Time.deltaTime);
        effect2Scale = Mathf.Lerp(effect2Scale, effect2TargetScale, 10 * Time.deltaTime);

        effect1TargetScale = Mathf.Lerp(effect1TargetScale, 7, 5 * Time.deltaTime);
        effect2TargetScale = Mathf.Lerp(effect2TargetScale, 8, 5 * Time.deltaTime);

        if (initialized && rigidbody.velocity.sqrMagnitude < 0.1f) {
            destroyBall();
        }

        if (transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 10 || transform.position.y < -10) {
            Destroy(gameObject, 5);
        }
    }

    void OnCollisionEnter(Collision collision) {
        effect1TargetScale += 2;
        effect2TargetScale -= 2;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Vector3 collisionNormal = collision.contacts[0].normal;

            if (Vector3.Dot(collisionNormal, Vector3.up) == 0) {
                Player player = collision.gameObject.GetComponent<Player>();
                lastTouchedPlayer = player.playerNumber;

                Vector3 newDirection = transform.position - player.transform.position;
                newDirection.y *= 0.5f;
                if (newDirection.sqrMagnitude < 1) {
                    newDirection.Normalize();
                }
                rigidbody.velocity = velocity = speed * newDirection;
                speed = velocity.magnitude;

                if (speed > 30) {
                    TimeController.SlowDownTime();
                }

                bonusPoints++;

                for (int i = 0; i < 5; i++) {
                    GameState.SpawnPoint(transform.position, lastTouchedPlayer);
                }

                targetTrailColorTime = lastTouchedPlayer;
                trailRenderer.time = Mathf.Clamp(bonusPoints * 0.1f, 0.1f, 5);

                SfxManager.PlaySfxBallHitPlayer();
            }
            else {
                destroyBall();
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) {

            if (collision.gameObject.CompareTag("Goal")) {
                destroyBall();
            }
            else {
                Vector3 collisionNormal = collision.contacts[0].normal;
                Vector3 projection = Vector3.Project(velocity, collisionNormal);
                rigidbody.velocity = velocity -= 2 * projection;

                if (rigidbody.velocity.sqrMagnitude > 900) {
                    for (int i = 0; i < 3; i++) {
                        GameState.SpawnPoint(transform.position, lastTouchedPlayer);
                    }
                    CameraShake.ShakeCamera(Mathf.Min(velocity.sqrMagnitude / 1000, 0.5f), 0.1f);
                    SfxManager.PlaySfxBallHitWall();
                }
                else {
                    if (lastTouchedPlayer != -1) {
                        GameState.SpawnPoint(transform.position, lastTouchedPlayer);
                    }
                    SfxManager.PlaySfxBallHitWall(0.15f);
                }
            }
        }
    }

    private IEnumerator destroyBallAfterDelay() {
        yield return new WaitForSeconds(0.5f);
        destroyBall();
    }

    private void destroyBall() {
        if (!destroyed) {
            destroyed = true;
            Destroy(gameObject, 5);

            GameObject deathEffect = (GameObject)Instantiate(ballDeathPrefab, transform.position, Quaternion.identity);

            int player = transform.position.x > 0 ? 0 : 1;


            ParticleSystem deathParticles = deathEffect.GetComponent<ParticleSystem>();
            deathParticles.startColor = trailColor.Evaluate(player);

            for (int i = 0; i < 10 + 5*bonusPoints; i++) {
                GameState.SpawnPoint(transform.position, player);
            }

            SfxManager.PlaySfxBallHitWall();
            CameraShake.ShakeCamera(1, 0.3f);

            GameState.BallCount--;

            rigidbody.velocity = Vector3.zero;
            transform.position = transform.position + Vector3.back * 100;

            visual.SetActive(false);
            enabled = false;
        }
    }
}
