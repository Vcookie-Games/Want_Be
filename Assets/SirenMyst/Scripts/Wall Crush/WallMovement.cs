using HoangVuCode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SirenMyst
{
    public class WallMovement : SirenMonoBehaviour
    {
        [SerializeField] protected Transform wallLeft;
        [SerializeField] protected Transform wallRight;
        [SerializeField] protected Transform leftStartPos;
        [SerializeField] protected Transform rightStartPos;
        [SerializeField] protected float distance = 3f;

        [SerializeField] protected float speed = 2f;
        
        [SerializeField] protected float wait = 1f;
        [SerializeField] protected float timer = 0f;

        public CrushZone crushZone;

        public bool isClosing;

        protected virtual void Update()
        {
            this.WallMoving();
        }

        protected virtual void WallMoving()
        {
            this.timer += Time.deltaTime;

            if (!this.isClosing)
            {
                this.wallLeft.position = Vector3.MoveTowards(this.wallLeft.position, this.leftStartPos.position + Vector3.right * this.distance, this.speed * Time.deltaTime);
                this.wallRight.position = Vector3.MoveTowards(this.wallRight.position, this.rightStartPos.position + Vector3.left * this.distance, this.speed * Time.deltaTime);
                
                if (Vector3.Distance(this.wallLeft.position, this.leftStartPos.position + Vector3.right * this.distance) < 0.01f)
                {
                    this.isClosing = true;
                    this.timer = 0f;
                }
            }
            else
            {
                this.wallLeft.position = Vector3.MoveTowards(this.wallLeft.position, this.leftStartPos.position, this.speed * Time.deltaTime);
                this.wallRight.position = Vector3.MoveTowards(this.wallRight.position, this.rightStartPos.position, this.speed * Time.deltaTime);

                if (Vector3.Distance(this.wallLeft.position, this.leftStartPos.position) < 0.01f && this.timer > this.wait)
                {
                    this.isClosing = false;
                    this.timer = 0f;
                }
            }
        }
    }
}
