using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SirenMyst
{
    public class PlayerSkin : SirenMonoBehaviour
    {
        public CharacterData characterDB;
        public SpriteRenderer artworkSprite;

        private int selectedOption = 0;

        protected override void Start()
        {
            base.Start();
            if (!PlayerPrefs.HasKey("selectedOption"))
            {
                this.selectedOption = 0;
            }
            else
            {
                this.Load();
            }
            this.UpdateCharacter(this.selectedOption);
        }

        protected virtual void UpdateCharacter(int selectedOption)
        {
            Character character = this.characterDB.GetCharacter(selectedOption);
            this.artworkSprite.sprite = character.characterSprite;
        }

        protected virtual void Load()
        {
            this.selectedOption = PlayerPrefs.GetInt("selectedOption");
        }
    }
}
