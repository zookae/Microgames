using UnityEngine;
using System.Collections;

public class DestroyPrint : MonoBehaviour {

    void OnDestroy() {
        try {
            Debug.Log("total destroyed before: " + GameState.Singleton.resources[Resource.NumDestoyed]);
            Debug.Log(transform.GetInstanceID() + " was destroyed");
            GameState.Singleton.resources[Resource.NumDestoyed] = GameState.Singleton.resources[Resource.NumDestoyed] + 1;
            Debug.Log("total destroyed after: " + GameState.Singleton.resources[Resource.NumDestoyed]);
        } catch {
        }
    }
}
