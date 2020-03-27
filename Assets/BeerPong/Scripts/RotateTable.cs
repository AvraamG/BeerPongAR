using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTable : MonoBehaviour
{
    // the target the UI should "look at"
    [SerializeField] Transform target;

    /// <summary>
    /// Starts subscribe to the Event from the Island class
    /// </summary>
    void OnEnable()
    {
        target = Camera.main.transform;
        SetRotationOnUI();
    }
    /// <summary>
    /// when this game object is disabled it stops to subscribe to the Event
    /// </summary>
    private void OnDisable()
    {
        
    }

    /// <summary>
    /// sets the rotation on the new position when its set in the Island class
    /// </summary>
    void SetRotationOnUI()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(-direction);
        transform.rotation = rotation;       
    }
}
