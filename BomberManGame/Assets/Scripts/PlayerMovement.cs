using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bombPrefab;
    public int playerId;
    Rigidbody2D myBody;
    Vector2 movement;
    Animator anim;

    public bool alreadyDead = false;
    bool bombDropped = false;

    float horizontal;
    float vertical;
    Dictionary<string, KeyCode> controlsDict;

    //Powers Variable
    public int explosionPower = 1;
    public bool multipleBombs = false;
    public bool remoteDetonator = false;

    // public bool powerUpActivated = false;

    bool ActiveExplosionPower = false;
    bool ActiveMoveSpeedPower = false;
    float explosionPowerTimer = 0f;
    float multipleBombsPowerTimer = 0f;
    float remoteDetonatorPowerTimer = 0f;
    float moveSpeedPowerTimer = 0f;
    float AllPowerUpTime = 10f;
    void Awake () {
        myBody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
    }

    void Start () {
        InitialiseControls ();
    }

    void InitialiseControls () {
        controlsDict = PlayerControls.GetPlayerControls (playerId - 1);
    }

    void OnEnable () {
        EventManager.OnBombExploded += MyBombExploded;
    }

    void OnDisable () {
        EventManager.OnBombExploded -= MyBombExploded;
    }

    void MyBombExploded (int id) {
        if (id == playerId)
            bombDropped = false;
    }

    void Update () {
        if (!alreadyDead) {
            if (Input.GetKeyDown (controlsDict["bomb"]) && (!bombDropped || multipleBombs)) {
                //TODO://Check if at same tile bomb exists, then dont instantiate.

                Component[] bombs;

                bombs = FindObjectsOfType<Bomb> ();
                bool flag = true;
                int selfBombCount = 0;
                foreach (Bomb bomb in bombs) {
                    if (bomb.transform.position.x == Mathf.Round (transform.position.x) && bomb.transform.position.y == Mathf.Round (transform.position.y)) {
                        flag = false;
                    }
                    if (bomb.playerId == playerId) {
                        selfBombCount++;
                    }
                }
                bool remoteBombNotAlreadyPlaced = !remoteDetonator ? true : (selfBombCount < 1) ? true : false;
                if (flag && remoteBombNotAlreadyPlaced) {
                    GameObject go = Instantiate (bombPrefab,
                        (Vector3.right * Mathf.Round (transform.position.x)) + (Vector3.up * Mathf.Round (transform.position.y)),
                        Quaternion.identity
                    );
                    bombDropped = true;
                    go.GetComponent<Bomb> ().SetPlayerID (playerId, explosionPower);
                    if (remoteDetonator) {
                        go.GetComponent<Bomb> ().explodeTime = 99999f;
                    }

                }
            }

            if (remoteDetonator && Input.GetKeyDown (controlsDict["remoteBomb"])) {
                Component[] bombs;

                bombs = FindObjectsOfType<Bomb> ();
                bool flag = true;
                foreach (Bomb bomb in bombs) {
                    if (bomb.playerId == playerId) {
                        bomb.Explode ();
                    }
                }

            }

            if (ActiveExplosionPower) {
                explosionPowerTimer -= Time.deltaTime;
                if (explosionPowerTimer < 0f) {
                    ActiveExplosionPower = false;
                    explosionPower = 1;
                }
            }
            if (ActiveMoveSpeedPower) {
                moveSpeedPowerTimer -= Time.deltaTime;
                if (moveSpeedPowerTimer < 0f) {
                    ActiveMoveSpeedPower = false;
                    moveSpeed = 5f;
                }
            }
            if (multipleBombs) {
                multipleBombsPowerTimer -= Time.deltaTime;
                if (multipleBombsPowerTimer < 0f) {
                    multipleBombs = false;
                }
            }
            if (remoteDetonator) {
                remoteDetonatorPowerTimer -= Time.deltaTime;
                if (remoteDetonatorPowerTimer < 0f) {
                    //detroy his bombs when the power runs out//or maybe switch to 3sec delay in future.
                    Component[] bombs;

                    bombs = FindObjectsOfType<Bomb> ();
                    foreach (Bomb bomb in bombs) {
                        if (bomb.playerId == playerId) {
                            bomb.Explode ();
                        }
                    }
                    remoteDetonator = false;
                }
            }

            horizontal = Input.GetKey (controlsDict["left"]) && Input.GetKey (controlsDict["right"]) ? 0f : Input.GetKey (controlsDict["left"]) ? -1f : Input.GetKey (controlsDict["right"]) ? 1f : 0f;
            vertical = Input.GetKey (controlsDict["bottom"]) && Input.GetKey (controlsDict["up"]) ? 0f : Input.GetKey (controlsDict["bottom"]) ? -1f : Input.GetKey (controlsDict["up"]) ? 1f : 0f;
            movement = (((Vector2.right * horizontal + (Vector2.up * vertical))) * moveSpeed * Time.deltaTime);
            anim.SetFloat ("Vspeed", movement.y * 10);
            anim.SetFloat ("Hspeed", movement.x * 10);

        }
    }

    void FixedUpdate () {
        myBody.MovePosition (myBody.position + movement);
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (!alreadyDead) {
            if (other.name.Contains ("Explode") && !alreadyDead) {

                alreadyDead = true;
                // anim.SetTrigger("aw");
                // // anim.StopPlayback();
                // anim.Play("New State");
                anim.SetFloat ("Vspeed", 2500f);
                anim.SetFloat ("Hspeed", 2500f);

                // Destroy(gameObject);

            }
            if (other.name.Contains ("Power")) {
                int x = other.name.Contains ("Power_LongBomb") ? 0 : other.name.Contains ("Power_MoreBomb") ? 1 : other.name.Contains ("Power_RemoteControl") ? 2 : 3;
                switch (x) {
                    case 0:
                        explosionPower += 2;
                        explosionPowerTimer = AllPowerUpTime;
                        ActiveExplosionPower = true;
                        break;
                    case 1:
                        multipleBombs = true;
                        multipleBombsPowerTimer = AllPowerUpTime;
                        break;
                    case 2:
                        remoteDetonator = true;
                        remoteDetonatorPowerTimer = AllPowerUpTime;
                        break;
                    case 3:
                        moveSpeed = 10f;
                        moveSpeedPowerTimer = AllPowerUpTime;
                        ActiveMoveSpeedPower = true;
                        break;
                    default:
                        break;
                }
                // StartCoroutine();
                Destroy (other.transform.gameObject);
            }
        }
    }

    //TODO: removing the power.
    public void DeathOfBomberman () {
        Destroy (gameObject);

        EventManager.CheckGameOver ();
    }
}