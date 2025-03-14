using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SirenMyst
{
    public class CharacterManager : SirenMonoBehaviour
    {
        public CharacterData characterDB;
        public TextMeshProUGUI nameText;
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

        public virtual void NextOption()
        {
            this.selectedOption++;

            if (this.selectedOption >= this.characterDB.CharacterCount)
            {
                this.selectedOption = 0;
            }

            this.UpdateCharacter(this.selectedOption);
            this.Save();
        }

        public virtual void BackOption()
        {
            this.selectedOption--;

            if (this.selectedOption < 0)
            {
                this.selectedOption = characterDB.CharacterCount - 1;
            }

            this.UpdateCharacter(this.selectedOption);
            this.Save();
        }
        protected virtual void UpdateCharacter(int selectedOption)
        {
            Character character = this.characterDB.GetCharacter(selectedOption);
            this.artworkSprite.sprite = character.characterSprite;
            this.nameText.text = character.characterName;
        }

        protected virtual void Load()
        {
            this.selectedOption = PlayerPrefs.GetInt("selectedOption");
        }
        protected virtual void Save()
        {
            PlayerPrefs.SetInt("selectedOption", selectedOption);
        }

        //public virtual void ChangeScene
    }
}
