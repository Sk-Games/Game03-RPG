using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;

        }


        private void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionSchedular>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;

        }

        private void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }


    }

}