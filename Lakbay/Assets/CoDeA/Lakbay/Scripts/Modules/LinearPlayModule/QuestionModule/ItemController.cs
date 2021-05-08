using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using CoDeA.Lakbay.Modules.GameModule;

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
        public static readonly float TIMER_ADDITIONAL_TIME = 4.0f;

        [HideInInspector]
        public Choice answeredChoice = null;
        public bool answeredCorrectly {
            get => this.answeredChoice != null && this.answeredChoice.correct;
        }

        public TextAsset itemFile;
        public SetController setController;
        public Item item;
        
        public UIModule.UIController uiController;
        public Utilities.Timer timer;
        public List<GameObject> triggerAgents = new List<GameObject>();

        public UnityEvent<ItemController, Choice> onAnswer = new UnityEvent<ItemController, Choice>();

        public bool checkAnswer(Choice choice) => choice != null ? choice.correct : false;

        private void Start() {
            if(this.itemFile) this.setUpItem(this.itemFile);

        }

        public void answer(Choice choice) {
            this.onAnswer.Invoke(this, choice);
            // if(this.checkAnswer(choice)) {


            // } else {


            // }

        }

        public void setUpTimer() {
            if(this.item == null) return;

            this.timer = this.gameObject.GetComponent<Utilities.Timer>();
            if(!this.timer) this.timer = this.gameObject.AddComponent<Utilities.Timer>();

            float totalCharacters = this.item.question.text.Length;
            List<float> choicesCharacters = this.item.choices.ConvertAll(
                new Converter<Choice, float>((Choice c) => c.text.Length)
            );

            totalCharacters += choicesCharacters.Sum();
            float pTimeDuration = totalCharacters / Utilities.Helper.READING_CHARACTER_PER_SECOND;
            float timeDuration = pTimeDuration + ItemController.TIMER_ADDITIONAL_TIME;

            this.timer.startOnPlay = false;
            this.timer.timeDuration = timeDuration;
            this.timer.onRun.AddListener(this.onTimerRun);
            this.timer.onStop.AddListener(this.onTimerStop);

            this.timer.start();

        }

        public void onTimerRun(Utilities.Timer timer) {
            this.uiController.timeText.SetText(this.timer.timeRemaining.ToString("N1") + "s");
            
        }

        public void onTimerStop(Utilities.Timer timer) {
            this.answer(null);
            
        }
        
        public void setUpItem(TextAsset itemFile) {
            this.setUpItem(Utilities.Helper.parseYAML<Item>(itemFile.text));

        }

        public void setUpItem(Item item) {
            this.item = item;

        }

    }

}
