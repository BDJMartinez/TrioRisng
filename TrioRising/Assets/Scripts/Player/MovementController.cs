using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.Player
{
    public class MovementController : MonoBehaviour
    {
        #region PLAYER_COMPONENTS
        [Header(" ---- Player Components ---- ")]
        [SerializeField] private PlayerController Owner;
        [SerializeField] private CharacterController Controller;
        #endregion



        #region LIFECYCLE_METHODS
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion
    } 
}
