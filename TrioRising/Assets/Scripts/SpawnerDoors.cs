using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDoors : MonoBehaviour
{
    [SerializeField] float speed;
    public float openAngle;
    public float closeAngle;
    public Vector3 doorDir;

    private bool isOpen;
    public bool enemyInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isOpen = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isOpen = false;
        }


    }




}
