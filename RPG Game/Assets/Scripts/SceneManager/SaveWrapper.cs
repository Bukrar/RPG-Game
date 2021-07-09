using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SaveWrapper : MonoBehaviour
    {
        const string defaulSaveFile = "save";

        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene()
        {
            yield return GetComponent<SaveSystem>().LoadLastScene(defaulSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(1.8f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Load()
        {
            GetComponent<SaveSystem>().Load(defaulSaveFile);
        }

        public void Save()
        {
            GetComponent<SaveSystem>().Save(defaulSaveFile);
        }
    }
}
