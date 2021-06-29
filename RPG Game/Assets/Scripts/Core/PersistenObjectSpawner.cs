using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistenObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistenObjectPrefab;

        static bool hasSpawne = false;
        private void Awake()
        {
            if (hasSpawne) return;

            SpawnPresistentObject();

            hasSpawne = true;
        }

        private void SpawnPresistentObject()
        {
            GameObject presistentObject = Instantiate(persistenObjectPrefab);
            DontDestroyOnLoad(presistentObject);
        }
    }
}

