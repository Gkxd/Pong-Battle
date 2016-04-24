using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameState : MonoBehaviour {

    private static GameState instance;

    public static int WinCounter { get; private set; }
    public static bool IsGameOver { get; private set; }

    public static int Player1BallsHit { get; set; }
    public static int Player2BallsHit { get; set; }
    public static int Player1BulletsFired { get; set; }
    public static int Player2BulletsFired { get; set; }

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

                if (instance.player1Hp == 0) {
                    instance.endGame(1);
                }
            }
        }
    }
    public static int Player2Hp {
        get { if (instance) return instance.player2Hp; return -1; }
        set {
            if (instance) {
                instance.player2Hp = Mathf.Clamp(value, 0, 100);
                instance.player2HpBar.localScale = new Vector3(instance.player2Hp / 100f, 1, 1);

                if (instance.player2Hp == 0) {
                    instance.endGame(0);
                }
            }
        }
    }

    public static int Player1Mp {
        get { if (instance) return instance.player1Mp; return -1; }
        set {
            if (instance) {
                int oldValue = instance.player1Mp;

                instance.player1Mp = Mathf.Clamp(value, 0, 100);
                instance.player1MpBar.localScale = new Vector3(instance.player1Mp / 100f, 1, 1);

                if (instance.player1Mp == 100 && oldValue < 100) {
                    SfxManager.PlaySfxUltimateCharged();
                }
            }
        }
    }
    public static int Player2Mp {
        get { if (instance) return instance.player2Mp; return -1; }
        set {
            if (instance) {
                int oldValue = instance.player2Mp;

                instance.player2Mp = Mathf.Clamp(value, 0, 100);
                instance.player2MpBar.localScale = new Vector3(instance.player2Mp / 100f, 1, 1);

                if (instance.player2Mp == 100 && oldValue < 100) {
                    SfxManager.PlaySfxUltimateCharged();
                }
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
            if (player == 0) {
                Player1BulletsFired++;
            }
            else if (player == 1) {
                Player2BulletsFired++;
            }

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
    public GameObject player1DeathPrefab;
    public GameObject player2DeathPrefab;

    public GameObject player1;
    public GameObject player2;

    public GameObject hud;
    public GameObject game;
    public GameObject winScreen;
    public GameObject player1Wins;
    public GameObject player2Wins;

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

    void OnEnable() {
        instance = this;

        foreach (Transform child in game.transform) {
            if (child.gameObject.layer == LayerMask.NameToLayer("Ball")) {
                Destroy(child.gameObject);
            }
        }

        IsGameOver = false;

        Player1Hp = Player2Hp = 100;
        Player1Mp = Player2Mp = 0;

        if (WinCounter <= -3) {
            Player1Mp = 20 - (WinCounter + 3) * 10;
        }
        else if (WinCounter >= 3) {
            Player2Mp = 20 + (WinCounter - 3) * 10;
        }

        Player1BallsHit = Player2BallsHit = Player1BulletsFired = Player2BulletsFired = 0;


        player1.transform.localPosition = new Vector3(-8.5f, 0, 0);
        player2.transform.localPosition = new Vector3(8.5f, 0, 0);

        player1.SetActive(true);
        player2.SetActive(true);

        ballCount = 0;

        ballSpawnRoutine = StartCoroutine(spawnRandomBallAtCenter());
    }

    private IEnumerator spawnRandomBallAtCenter(float speed = 8, float delay = 2.9f) {
        while (true) {
            if (Player1Hp == 0 || Player2Hp == 0) {
                break;
            }

            if (ballCount < 1) {
                Ball b = AddBall();
                b.transform.parent = game.transform;
                b.speed = speed;
                yield return new WaitForSeconds(delay);
                b.randomizeDirection();
            }
            yield return null;
        }
    }

    private void endGame(int winningPlayer) {
        if (!IsGameOver) {
            GameObject winnerText = winningPlayer == 0 ? player1Wins : player2Wins;
            GameObject loserText = winningPlayer == 0 ? player2Wins : player1Wins;

            GameObject loserPlayer = winningPlayer == 0 ? player2 : player1;

            Instantiate(winningPlayer == 0 ? player2DeathPrefab : player1DeathPrefab, loserPlayer.transform.position, Quaternion.identity);

            loserPlayer.SetActive(false);

            winnerText.SetActive(true);
            loserText.SetActive(false);

            winScreen.SetActive(true);

            StopCoroutine(ballSpawnRoutine);

            SwitchUIScreens.lastTimeTriggered = Time.time + 5;

            IsGameOver = true;

            if (winningPlayer == 0) {
                if (WinCounter >= 0) {
                    WinCounter++;
                }
                else {
                    WinCounter = 0;
                }
            }
            else if (winningPlayer == 1) {
                if (WinCounter <= 0) {
                    WinCounter--;
                }
                else {
                    WinCounter = 0;
                }
            }

            SfxManager.PlaySfxPlayerDeath();
            TimeController.SlowDownTime(1);
        }
    }
}
