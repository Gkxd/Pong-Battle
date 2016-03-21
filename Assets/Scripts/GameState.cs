using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameState : MonoBehaviour {

    private static GameState instance;

    public static bool ShouldDeleteBall { get; set; } // Flag used to delete all on screen balls

    public static GameObject Player1 {
        get { if (instance) return instance.player1; return null; }
    }
    public static GameObject Player2 {
        get { if (instance) return instance.player2; return null; }
    }

    public static int Player1Hp {
        get { if (instance) return instance.player1Hp; return -1; }
        set {
            if (instance) {
                instance.player1Hp = Mathf.Clamp(value, 0, 100);
                instance.player1HpBar.localScale = new Vector3(instance.player1Hp / 100f, 1, 1);
            }
        }
    }
    public static int Player2Hp {
        get { if (instance) return instance.player2Hp; return -1; }
        set {
            if (instance) {
                instance.player2Hp = Mathf.Clamp(value, 0, 100);
                instance.player2HpBar.localScale = new Vector3(instance.player2Hp / 100f, 1, 1);
            }
        }
    }
    public static int Player1Mp {
        get { if (instance) return instance.player1Mp; return -1; }
        set {
            if (instance) {
                instance.player1Mp = Mathf.Clamp(value, 0, 100);
                instance.player1MpBar.localScale = new Vector3(instance.player1Mp / 100f, 1, 1);
            }
        }
    }
    public static int Player2Mp {
        get { if (instance) return instance.player2Mp; return -1; }
        set {
            if (instance) {
                instance.player2Mp = Mathf.Clamp(value, 0, 100);
                instance.player2MpBar.localScale = new Vector3(instance.player2Mp / 100f, 1, 1);
            }
        }
    }
    public static int BallCount {
        get { if (instance) return instance.ballCount; return -1; }
        set { if (instance) instance.ballCount = value; }
    }



    public static void SpawnPoint(Vector3 position, int player) {
        if (instance) {
            Point point = ((GameObject)Instantiate(instance.pointPrefab, position, Quaternion.identity)).GetComponent<Point>();
            point.setTarget(player);
        }
    }

    public static PlayerBullet SpawnPlayerBullet(Vector3 position, int player) {
        if (instance) {
            PlayerBullet bullet = ((GameObject)Instantiate(instance.playerBulletPrefab, position, Quaternion.identity)).GetComponent<PlayerBullet>();
            bullet.playerNumber = player;
            return bullet;
        }
        return null;
    }

    public static Ball AddBall() {
        if (instance) {
            return ((GameObject)Instantiate(instance.ballPrefab)).GetComponent<Ball>();
        }
        return null;
    }

    public GameObject ballPrefab;
    public GameObject pointPrefab;
    public GameObject playerBulletPrefab;

    public GameObject player1;
    public GameObject player2;

    public RectTransform player1HpBar;
    public RectTransform player2HpBar;
    public RectTransform player1MpBar;
    public RectTransform player2MpBar;

    private int ballCount;

    private int player1Hp;
    private int player2Hp;

    private int player1Mp;
    private int player2Mp;

    private Coroutine ballSpawnRoutine;

    void Start() {
        instance = this;

        player1Hp = player2Hp = 100;
        player1Mp = player2Mp = 0;

        ballSpawnRoutine = StartCoroutine(spawnRandomBallAtCenter());
    }

    private IEnumerator spawnRandomBallAtCenter(float speed = 6, float delay = 1) {
        while (true) {
            if (ballCount < 1) {
                Ball b = AddBall();
                b.speed = speed;
                yield return new WaitForSeconds(delay);
                b.randomizeDirection();
            }
            yield return null;
        }
    }
}
