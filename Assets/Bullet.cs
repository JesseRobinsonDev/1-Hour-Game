using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody2D rb;
    public float bulletSpeed;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rb.AddForce(transform.up.normalized * bulletSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        StopAllCoroutines();
        GameObject collided = col.gameObject;
        if (collided.layer == 6) {
            Target target = collided.GetComponent<Target>();
            target.DestroyTarget();
        }
        Destroy(gameObject);
    }

    public void StartLifespan() {
        StartCoroutine(StartLifespanRoutine());
    }

    private IEnumerator StartLifespanRoutine() {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
