using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Question {
    [System.Serializable]
    public class Item {
        [HideInInspector]
        public bool correct = false;

        public string question;
        public List<string> choices = new List<string>();
        public int correctChoiceIndex;

        public bool answer(int choiceIndex) => this.correctChoiceIndex == choiceIndex;

        public void shuffleChoices() {
            string correctChoice = this.choices[this.correctChoiceIndex];

            System.Random random = new System.Random();
            string[] aitems = Utilities.Helper.shuffle<string>(random, this.choices.ToArray());
            this.choices = new List<string>(aitems);
            this.correctChoiceIndex = this.choices.IndexOf(correctChoice);

        }

    }

}
