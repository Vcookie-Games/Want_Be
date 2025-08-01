using HoangVuCode;
using UnityEngine;

namespace SirenMyst
{
    public class WallCollision : SirenMonoBehaviour
    {
        public WallMovement wallMovement;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadWallMovement();
        }

        protected virtual void LoadWallMovement()
        {
            if (this.wallMovement != null) return;
            this.wallMovement = transform.parent.GetComponent<WallMovement>();
            Debug.LogWarning(transform.name + ": LoadWallMovement", gameObject);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (this.wallMovement != null && this.wallMovement.isClosing && col.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player touched wall while closing");
                if (this.wallMovement.crushZone != null && this.wallMovement.crushZone.IsPlayerInside())
                {
                    GameController.Instance.GameOver();
                }
            }
        }
    }
}