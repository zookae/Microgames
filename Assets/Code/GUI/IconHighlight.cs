using UnityEngine;
using System.Collections;

public class IconHighlight : MonoBehaviour {
    
    /// <summary>
    /// An object in the scene.
    /// Other objects with this may have a handle to this.
    /// </summary>
    public GameObject obj;

    /// <summary>
    /// Offset from this object's position.
    /// Offset should be in the same space as this object 
    /// (e.g. if this object lives in screen space, offset should be in screen space)
    /// </summary>
    public Vector3 desiredOffset;

    void OnMouseOver() {
        if (obj.guiTexture != null) {
            obj.guiTexture.enabled = true;
        }

        // Adjust position
        if (obj.GetComponent<GUIElement>() != null) {
            obj.transform.position = this.gameObject.transform.position + desiredOffset;
        } else { // object lives in world space and requires an extra matrix mult.
            // XXX (kasiu): NOT WORKING, CHECK LOGIC
            obj.transform.position = Camera.main.ScreenToWorldPoint(this.gameObject.transform.position);
        }
    }

    void OnMouseExit() {
        if (obj.guiTexture != null) {
            // XXX (kasiu): I suspect this will break if colliders are on top of each other in some weird order.
            obj.guiTexture.enabled = false;
        }
    }
}
