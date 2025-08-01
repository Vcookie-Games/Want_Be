using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SirenMyst
{
    public class CrushZone : SirenMonoBehaviour
    {
        [SerializeField] private bool playerInside = false;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.playerInside = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.playerInside = false;
            }
        }

        public bool IsPlayerInside()
        {
            return this.playerInside;
        }
    }
}
