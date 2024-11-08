using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private List<GameObject> _affectedObjects = new List<GameObject>();

    private bool _isAffected;
    
    public IReadOnlyList<GameObject> AffectedObject => _affectedObjects;

    public bool IsAffected { get => _isAffected; private set => _isAffected = value;  }

    // Adds the entering object the affect list if it isn't already present
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is the list; If so, do noithing
        if (_affectedObjects.Contains(other.gameObject))
            return;

        _affectedObjects.Add(other.gameObject);         // Add new object to the affected list 
        IsAffected = _affectedObjects.Count > 0;        // Update the affected state
    }

    // Removes the exting object from the affected list
    private void OnTriggerExit(Collider other)
    {
        _affectedObjects.Remove(other.gameObject);      // Remove the object from the affected list
        IsAffected = _affectedObjects.Count > 0;        // Update the isAffected state
    }
}
