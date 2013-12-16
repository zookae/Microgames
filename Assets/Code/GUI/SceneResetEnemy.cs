using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneResetEnemy : MonoBehaviour {

    public Transform enemyPrefab;
    private Transform player;
    private GameObject background;
    
    /// <summary>
    /// User-specified list of positions to place enemies along 2D plane
    /// </summary>
    public List<Vector2> enemyPositions;


    void Start() {
        player = (GameObject.Find("Player") as GameObject).transform;
        background = GameObject.Find("Background") as GameObject;
    }

    public void OnGUI() {
        if (GUI.Button(new Rect(Screen.width - 100, Screen.height - 70, 100, 30), "Reset enemies")) {
            Debug.Log("clicked to reset enemies");
            ResetEnemy();
        }
    }

    // TODO: GUI to send trace / save
    // TODO: GUI to reset to previous save

    public void ResetEnemy() {
        RemoveOfTag("Enemy"); // remove enemies
        RemoveOfTag("BulletBad"); // remove bullets
        BHParamSpawnEnemyDefault();
    }

    /// <summary>
    /// Destroy all entities with a given tag
    /// </summary>
    /// <param name="removeTag"></param>
    public void RemoveOfTag(string removeTag) {
        GameObject[] toRemove = GameObject.FindGameObjectsWithTag(removeTag);
        foreach (GameObject entity in toRemove) {
            GameObject.Destroy(entity);
        }
    }

    /// <summary>
    /// Spawn enemy prefab at desired positions
    /// </summary>
    public void BHParamSpawnEnemyDefault() {
        ParamChange setSpeed = GameState.Singleton.currentAction;
        foreach (Vector2 enemyPos in enemyPositions) {
            Transform enemy = Instantiate(enemyPrefab, new Vector3(enemyPos.x, enemyPos.y, 0), Quaternion.identity) as Transform;
            NPCShootTowardTarget shoot = enemy.gameObject.GetComponent<NPCShootTowardTarget>();
            shoot.moveTarget = player;
            shoot.bulletBounds = background;
        }
    }
}
