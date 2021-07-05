using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickUp : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSecond(respawnTime));
            }
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
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(isShow);
            }    
        }
    }
}
