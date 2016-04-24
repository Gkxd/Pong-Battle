using UnityEngine;
using System.Collections;

using UsefulThings;

[RequireComponent(typeof(TimeKeeper))]
public class Point : MonoBehaviour {

    private int targetPlayer;


    private Vector3 target;
    private Vector3 velocity;

    private Color color;

    private Material material;
    private float scale;

    private bool pointAdded;

    private TimeKeeper tk;
    private float approachDelay;

    void Start() {
        tk = GetComponent<TimeKeeper>();
        material = GetComponent<Renderer>().material;

        scale = Random.Range(0.2f, 0.5f);
        transform.localScale = new Vector3(scale, scale, 1);

        float angle;

        if (transform.position.x > 0) {
            if (transform.position.y > 2) {
                angle = Random.Range(190, 250) * Mathf.Deg2Rad;
            }
            else if (transform.position.y < -2) {
                angle = Random.Range(100, 170) * Mathf.Deg2Rad;
            }
            else {
                angle = Random.Range(120, 240) * Mathf.Deg2Rad;
            }
        }
        else {
            if (transform.position.y > 2) {
                angle = Random.Range(-80, -10) * Mathf.Deg2Rad;
            }
            else if (transform.position.y < -2) {
                angle = Random.Range(10, 80) * Mathf.Deg2Rad;
            }
            else {
                angle = Random.Range(-60, 60) * Mathf.Deg2Rad;
            }
        }

        velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * Random.Range(0.5f, 3f);
        velocity.Scale(new Vector3(1, 1, 0) * 5);

        approachDelay = Random.value/2 + 0.5f;
    }

    void Update() {

        if (tk.lifeTime > approachDelay) {
            if ((transform.position - target).sqrMagnitude < 0.1f) {
                color.a = Mathf.Lerp(color.a, -1, 2 * Time.deltaTime);
                material.color = color;

                scale = Mathf.Lerp(scale, 2, 2 * Time.deltaTime);
                transform.localScale = new Vector3(scale, scale, 1);

                if (!pointAdded) {
                    Destroy(gameObject, 1);
                    pointAdded = true;


                    if (targetPlayer == 0) {
                        GameState.Player1Mp++;
                    }
                    else {
                        GameState.Player2Mp++;
                    }

                    SfxManager.PlaySfxGetPoint();
                }
            }
            else {
                transform.position += velocity * Time.deltaTime;

                velocity = (target - transform.position).normalized * 20;
            }
        }
        else {
            transform.position += velocity * Time.deltaTime;

            velocity = Vector3.Lerp(velocity, Vector3.zero, 3*Time.deltaTime);
        }
    }

    public void setTarget(int player) {
        if (material == null) {
            material = GetComponent<Renderer>().material;
        }

        targetPlayer = player;

        if (player == 0) {
            target = new Vector3(-8.5f, 4.5f, 0);
            color = new Color(1, 0.36f, 0.61f, 0.5f);
            material.color = color;
        }
        else if (player == 1) {
            target = new Vector3(8.5f, 4.5f, 0);
            color = new Color(0.27f, 0.57f, 1, 0.5f);
            material.color = color;
        }
    }
}
