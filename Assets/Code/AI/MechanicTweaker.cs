using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class MechanicTweaker {

    public string targetClass;

    private bool hasToggled = false;
    private bool hasSelected = false;
    private string targetField;

    public MechanicTweaker(string targetClass) {
        targetField = pickField();
        hasSelected = true;
    }

    public void tweak() {
        toggleValue(targetField);
    }


    /// <summary>
    /// Toggle a randomly selected field
    /// </summary>
    public void tweakRandomField() {
        string fieldName = pickField();
        toggleValue(fieldName);
    }

    /// <summary>
    /// Randomly select a field from the target class
    /// </summary>
    /// <returns></returns>
    public string pickField() {
        Type targetType = Type.GetType(targetClass);
        FieldInfo[] fi = targetType.GetFields();
        return fi[UnityEngine.Random.Range(0, fi.Length)].ToString();
    }

    /// <summary>
    /// Toggle the field value between two states
    /// </summary>
    /// <param name="field"></param>
    public void toggleValue(string field) {
        FieldInfo fi = typeof(ParameterToggle).GetField(field);
        object v = fi.GetValue(null);
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
