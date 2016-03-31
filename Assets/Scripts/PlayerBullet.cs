using UnityEngine;
using System.Collections;

using UsefulThings;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBullet : MonoBehaviour {

    public GameObject playerBulletDeathPrefab;
    public Color trailColor;


    public Curve colorChangeCurve;

    public Color darkColor;
    public Color brightColor;

    public int playerNumber;
    public float size = 1;

    public Vector3 velocity;

    public _AbstractMovement movement;

    protected new Rigidbody rigidbody;

    private bool destroyed;

    private TrailRenderer trailRenderer;
    private float lifeTime;

    private Material material;
    private float alpha;
    private float targetAlpha;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = velocity;

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.material.color = trailColor;

        transform.localScale = Vector3.forward;
        trailRenderer.startWidth = 0;

        material = transform.Find("Visual").GetComponent<Renderer>().material;

        if ((playerNumber == 0 && transform.position.x < -5) || (playerNumber == 1 && transform.position.x > 5)) {
            alpha = targetAlpha = 0.2f;
        }
        else {
            alpha = targetAlpha = 1;
        }

        //SfxManager.PlaySfxMissileSpawn();
    }

    void Update() {
        if (lifeTime <= 1) {
            float scale = lifeTime * 0.5f * size;
            float trailWidth = lifeTime * 0.12f * size;

            transform.localScale = new Vector3(scale, scale, 1);
            trailRenderer.startWidth = trailWidth;
        }

        if (destroyed) {
            trailRenderer.startWidth = Mathf.Lerp(trailRenderer.startWidth, 0, Time.deltaTime);
        }

        if (transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 9 || transform.position.y < -9) {
            Destroy(gameObject, 0.5f);
        }


        Color bulletColor = Color.Lerp(darkColor, brightColor, colorChangeCurve.Evaluate(lifeTime));

        if ((playerNumber == 0 && transform.position.x < -5) || (playerNumber == 1 && transform.position.x > 5)) {
            targetAlpha = 0.2f;
        }
        else {
            targetAlpha = 1;
        }

        alpha = Mathf.Lerp(alpha, targetAlpha, 5 * Time.deltaTime);
        bulletColor.a = alpha;

        material.color = bulletColor;
        lifeTime += Time.deltaTime;
    }

    void FixedUpdate() {
        if (!destroyed) {
            if (movement != null) {
                rigidbody.velocity = velocity = movement.updateVelocity(velocity, Time.deltaTime);
            }

            transform.localEulerAngles = Vector3.forward * Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        }
    }

    void OnTriggerEnter(Collider collider) {

        PlayerHeart p = collider.gameObject.GetComponent<PlayerHeart>();

        if (p.playerNumber != playerNumber) {
            GameObject deathEffect = (GameObject)Instantiate(playerBulletDeathPrefab, transform.position, Quaternion.identity);

            //deathEffect.GetComponent<ParticleSystem>().startColor = color;
            deathEffect.transform.Find("Particle System").GetComponent<ParticleSystem>().startColor = trailColor;

            Destroy(gameObject, 5);
            destroyed = true;
            //GameState.SpawnPoint(transform.position, playerNumber);

            rigidbody.velocity = velocity = Vector3.zero;
            transform.position = transform.position + Vector3.back * 100;
        }
    }
}
