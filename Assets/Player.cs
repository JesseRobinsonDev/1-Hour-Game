using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Game game;

    private Rigidbody2D rb;

    public float jumpForce;
    public float moveForce;
    public float spinForce;
    public bool isJumping;
    public float jumpCooldown;
    public float slowTimeAmount;
    public bool isShooting;
    public float shootCooldown;

    public GameObject bulletSpawner;
    public GameObject bulletPrefab;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A) && !isJumping) {
            LaunchLeft();
            StartCoroutine(JumpCooldown());
        } else if (Input.GetKeyDown(KeyCode.D) && !isJumping) {
            LaunchRight();
            StartCoroutine(JumpCooldown());
        } else if (Input.GetMouseButtonDown(0)) {
            SlowTime();
        } else if (Input.GetMouseButtonUp(0)) {
            UnSlowTime();
            if (!isShooting) {
                Shoot();
                StartCoroutine(ShootCooldown());
            }
        }
    }

    private void LaunchLeft() {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.AddForce(Vector2.left * moveForce, ForceMode2D.Impulse);
        rb.AddTorque(spinForce, ForceMode2D.Force);
    }
    private void LaunchRight() {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.AddForce(Vector2.right * moveForce, ForceMode2D.Impulse);
        rb.AddTorque(-spinForce, ForceMode2D.Force);
    }

    private IEnumerator JumpCooldown() {
        isJumping = true;
        yield return new WaitForSeconds(jumpCooldown);
        isJumping = false;
    }

    private void SlowTime() {
        Time.timeScale = slowTimeAmount;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void UnSlowTime() {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void Shoot() {
        GameObject bog = Instantiate(bulletPrefab, bulletSpawner.transform.position, transform.rotation);
        bog.transform.Rotate(new Vector3(0f, 0f, -90f), Space.World);
        Bullet bullet = bog.GetComponent<Bullet>(); 
        bullet.StartLifespan();
    }

    private IEnumerator ShootCooldown() {
        isShooting = true;
        yield return new WaitForSeconds(shootCooldown);
        isShooting = false;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        GameObject collided = col.gameObject;
        if (collided.layer == 6) {
            Target target = collided.GetComponent<Target>();
            target.DestroyTarget();
            game.Gameover(0);
            game.cam.player = null;
            Destroy(gameObject);
        }
    }
}
