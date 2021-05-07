using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.LinearPlayModule.QuestionModule {
    [System.Serializable]
    public class Question {
        public List<string> images = new List<string>();
        public string text = "";

    }

    [System.Serializable]
    public class Choice {
        public string text = "";
        public bool correct = false;

    }

    [System.Serializable]
    public class Item {
        public Question question;
        public List<Choice> choices = new List<Choice>();
        
        public float minCoinGain = 5.0f;
        public float maxCoinGain = 30.0f;
        public float minFuelDeduction = 7.0f;
        public float maxFuelDeduction = 15.0f;

    }

    public class ItemController : MonoBehaviour {
        [HideInInspector]
        public Choice answeredChoice = null;
        public bool answeredCorrectly {
            get => this.answeredChoice != null && this.answeredChoice.correct;
        }

        public SetController setController;
        public Item item;

        public UnityEvent<ItemController, Choice> onAnswer = new UnityEvent<ItemController, Choice>();

        public bool checkAnswer(Choice choice) => choice != null ? choice.correct : false;

        public void answer(Choice choice) {
            this.onAnswer.Invoke(this, choice);
            // if(this.checkAnswer(choice)) {


            // } else {


            // }

        }

        // public

    }

}
