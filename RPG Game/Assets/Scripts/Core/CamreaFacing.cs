using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CamreaFacing : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}

