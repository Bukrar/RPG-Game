using RPG.Attributes;
using RPG.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickUp : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon = null;
        [SerializeField] float healthToRestore = 0;
        [SerializeField] float respawnTime = 5;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                PickUp(other.gameObject);
            }
        }

        private void PickUp(GameObject subject)
        {
            if(weapon != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(weapon);
            }
            if(healthToRestore >0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore);
            }
            StartCoroutine(HideForSecond(respawnTime));
        }

        private IEnumerator HideForSecond(float seconds)
        {
            ShowWeapon(false);
            yield return new WaitForSeconds(seconds);
            ShowWeapon(true);
        }

        private void ShowWeapon(bool isShow)
        {
            GetComponent<Collider>().enabled = isShow;
            transform.GetChild(0).gameObject.SetActive(isShow);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(isShow);
            }
        }

        public bool HandleRaycast(PlayerController callingContrller)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickUp(callingContrller.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.PickUp;
        }
    }
}
