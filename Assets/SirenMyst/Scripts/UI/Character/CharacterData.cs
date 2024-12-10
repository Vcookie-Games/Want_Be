using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SirenMyst
{
    [CreateAssetMenu]
    public class CharacterData : ScriptableObject
    {
        public Character[] character;

        public int CharacterCount
        {
            get { return character.Length; }
        }
        
        public Character GetCharacter(int index)
        {
            return character[index];
        }
    }
}
