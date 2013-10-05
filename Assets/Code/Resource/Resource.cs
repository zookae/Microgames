using UnityEngine;
using System.Collections;

public enum ResourceType {
    NumDestoyed,
    Health,
    Score
}

public class Resource : MonoBehaviour {

    /// <summary>
    /// Name of the resource
    /// </summary>
    public string name;

    /// <summary>
    /// Type of the resource
    /// </summary>
    public ResourceType resourcetype;

    /// <summary>
    /// Current value
    /// </summary>
    public float value;

    /// <summary>
    /// Minimum allowed value
    /// </summary>
    public float minValue = 0.0f;

    /// <summary>
    /// Maximum allowed value
    /// </summary>
    public float maxValue = Mathf.Infinity;

    /// <summary>
    /// Update resource value, clamping to fit within range
    /// </summary>
    /// <param name="rtype"></param>
    /// <param name="newValue"></param>
    public void ChangeValue(float newValue) {
        if (newValue < minValue) {
            value = minValue;
        }
        else if (newValue > maxValue) {
            value = maxValue;
        }
        else {
            value = newValue;
        }
    }
}
