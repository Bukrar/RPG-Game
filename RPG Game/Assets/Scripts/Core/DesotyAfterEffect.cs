using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesotyAfterEffect : MonoBehaviour
{
    [SerializeField] GameObject targetToDestory = null;
    void Update()
    {
        if (!GetComponent<ParticleSystem>().IsAlive())
        {
            if (targetToDestory != null)
            {
                Destroy(targetToDestory);
            }
            else
            {
                Destroy(gameObject);
            }
          
        }
    }
}
