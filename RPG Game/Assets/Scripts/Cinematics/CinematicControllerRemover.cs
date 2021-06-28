using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Controller;

namespace RPG.Cinematic
{
    public class CinematicControllerRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += enableControl;
            player = GameObject.FindWithTag("Player");
        }
        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void enableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}

