using HoangVuCode;
using SirenMyst;
using System.Collections.Generic;
using UnityEngine;

namespace SirenMyst
{
    public class ConveyorBelt : MonoBehaviour
    {
        [SerializeField] protected Vector2 direction;
        [SerializeField] protected float speed;
        [SerializeField] protected float jumpForce;

        [SerializeField] protected List<GameObject> onBelt;

        protected virtual void FixedUpdate()
        {
            for (int i = 0; i < this.onBelt.Count; i++)
            {
                this.onBelt[i].GetComponent<Rigidbody2D>().AddForce(this.speed * this.direction);
                if (Input.GetMouseButtonDown(0)) 
                {
                    this.onBelt[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    GameController.Instance.GetPlayerController().RefreshMovement();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.onBelt.Add(collision.gameObject);
            GameController.Instance.GetPlayerController().StopMovement();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            this.onBelt.Remove(collision.gameObject);
            GameController.Instance.GetPlayerController().RefreshMovement();

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position,
                            (Vector2)transform.position + direction.normalized * 0.75f);
        }
    }
}
