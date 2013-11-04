using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;

public class MechanicTweaker<T> : MonoBehaviour where T : Component {

    public string targetClass;

    // TODO : back to private
    public bool hasToggled = false;
    public bool hasSelected = false;
    public string targetField;

    public List<string> fields;
    public FieldInfo[] fis;

    public int fieldIndex;

    public List<T> entityComponents;

    //public MechanicTweaker(string targetClass) {
    //    targetField = pickField();
    //    hasSelected = true;
    //}

    void Awake() {
        Type targetType = Type.GetType(targetClass);
        fis = targetType.GetFields();

        fields = new List<string>();
        T[] mechanics = gameObject.GetComponents(typeof(T)) as T[];
        entityComponents.AddRange(mechanics);
        foreach (T mech in mechanics) {
            foreach (FieldInfo fi in fis) {
                fields.Add(fi.ToString());
                Debug.Log("[MechanicTweaker] got field " + fi);
                Debug.Log("[MechanicTweaker] got field " + fi.GetValue(mech));
            }
        }
        
        targetField = pickField();
        //hasSelected = true;
    }

    public void tweak() {
        Debug.Log("[MechanicTweaker] called tweak()");
        toggleValue(targetField);
    }


    /// <summary>
    /// Toggle a randomly selected field
    /// </summary>
    public void tweakRandomField() {
        string fieldName = pickField();

        Debug.Log("[MechanicTweaker] tweaking field : " + fieldName);

        toggleValue(fieldName);
    }

    public void setTargetField() {
        targetField = pickField();
    }

    /// <summary>
    /// Randomly select a field from the target class
    /// </summary>
    /// <returns></returns>
    public string pickField() {
        //Type targetType = Type.GetType(targetClass);
        //FieldInfo[] fi = targetType.GetFields();
        int index = UnityEngine.Random.Range(0, fis.Length);
        Debug.Log("[MechanicTweaker.pickField] picked field index : " + index);
        Debug.Log("[MechanicTweaker.pickField] got field: " + fis[index]);
        fieldIndex = index;
        return fis[index].ToString();
    }

    /// <summary>
    /// Toggle the field value between two states
    /// </summary>
    /// <param name="field"></param>
    public void toggleValue(string field) {
        Debug.Log("[MechanicTweaker] toggling value of field : " + field);
        //FieldInfo fi = Type.GetType(targetClass).GetField(field);
        FieldInfo fi = fis[fieldIndex];

        Debug.Log("[MechanicTweaker] toggling value of : " + fi.ToString());
        object v = fi.GetValue(Type.GetType(targetClass));
        if (v is int) {
            fi.SetValue(this, toggleInt((int)v));
        }
        else if (v is float) {
            fi.SetValue(this, toggleFloat((float)v));
        }
        else if (v is bool) {
            fi.SetValue(this, !((bool)v));
        }
        hasToggled = !hasToggled;
    }

    /// <summary>
    /// Toggle an integer value by doubling or halving
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private int toggleInt(int value) {
        if (hasToggled) {
            return value / 2;
        }
        return value * 2;
    }

    /// <summary>
    /// Toggle a float value by doubling or halving
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private float toggleFloat(float value) {
        if (hasToggled) {
            return value / 2.0f;
        }
        return value * 2.0f;
    }
}
