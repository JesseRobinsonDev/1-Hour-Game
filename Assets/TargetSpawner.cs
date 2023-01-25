using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {

    public bool spawningActive;
    public float spawnCooldown;
    public Vector2 areaWidth;
    public Vector2 areaHeight;
    public GameObject targetPrefab;
    public List<GameObject> targets;

    private void Start() {
        StartCoroutine(SpawnTarget());
    }

    private IEnumerator SpawnTarget() {
        yield return new WaitForSeconds(spawnCooldown);
        if (spawningActive) {
            float x = Random.Range(areaWidth.x, areaWidth.y);
            float y = Random.Range(areaHeight.x, areaHeight.y);
            GameObject target = Instantiate(targetPrefab, new Vector2(x, y), Quaternion.identity);
            targets.Add(target);
        }
        StartCoroutine(SpawnTarget());
    }

    public void DeleteTargets() {
        for (int i = 0; i < targets.Count; i++) {
            if (targets != null) {
                Destroy(targets[i]);
            }
        }
        targets.Clear();
    }
}
