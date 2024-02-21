using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 0.5f;
        [SerializeField] float fadeInTime = 1.0f;
        [SerializeField] float fadeWaitTime = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            //if(sceneToLoad < 0)
            //{
             //   Debug.LogError("Scene To Load Not Set");
             //   yield break;
            //}

            DontDestroyOnLoad(gameObject);
            print("not working");
            //Fader fader = FindObjectOfType<Fader>();

            //yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            print("Scene Loaded");

            //Portal otherPortal = GetOtherPortal();
            //UpdatePlayer(otherPortal);
            //yield return new WaitForSeconds(fadeWaitTime);
            //yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPoertal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = otherPoertal.transform.position;
            player.transform.rotation = otherPoertal.transform.rotation;

        }

        private Portal GetOtherPortal()
        {
            ;
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != this.destination) continue;
                return portal;
            }
            return null;
        }
    }
}
