using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Question {
    [System.Serializable]
    public class Item {
        [HideInInspector]
        public bool correct = false;

        public string question;
        public List<string> choices = new List<string>();
        public int correctChoiceIndex;


        public bool answer(int choiceIndex) => this.correctChoiceIndex == choiceIndex;

    }

}
