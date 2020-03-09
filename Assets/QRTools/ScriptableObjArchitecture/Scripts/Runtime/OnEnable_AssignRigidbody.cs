using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Variables
{
    public class OnEnable_AssignRigidbody : MonoBehaviour
    {
        public RigidBodyVariable refRigidbody;

        private void OnEnable()
        {
            refRigidbody.Value = GetComponent<Rigidbody>();
            Destroy(this);
        }
    }
}
