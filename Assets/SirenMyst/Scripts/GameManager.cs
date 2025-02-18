using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SirenMyst
{
    public class GameManager : SirenMonoBehaviour
    {
        public static bool isGameOver;
        [SerializeField] protected Transform gameOverScreen;
        protected override void Awake()
        {
            base.Awake();
            isGameOver = false;
        }

        protected virtual void Update()
        {
            this.IsGameOver();
        }

        protected virtual void IsGameOver()
        {
            if (!isGameOver) return;
            this.gameOverScreen.gameObject.SetActive(true);
        }

    }
}