using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

namespace Question {
    public class Display : MonoBehaviour {
        [HideInInspector]
        public Question.Controller controller;

        public TMP_Text questionText;
        public Transform choiceGroup;
        public Button choiceButton;
        [HideInInspector]
        public List<Button> choiceButtons = new List<Button>();

        private void Start() {
            this.hide();
            this.unset();
            
        }

        public void show() => this.gameObject.SetActive(true);

        public void hide() => this.gameObject.SetActive(false);

        public void set(string question, params string[] choices) {
            this.unset();

            this.questionText.SetText(question);

            int i = 0;
            foreach(string choice in choices) {
                Button choiceButton = Instantiate<Button>(this.choiceButton);

                choiceButton.transform.SetParent(this.choiceGroup);
                this.choiceButtons.Add(choiceButton);

                TMP_Text choiceText = choiceButton.transform.GetChild(0).GetComponent<TMP_Text>();
                choiceText.SetText(choice);

                Func<int, UnityEngine.Events.UnityAction> listener = (int i) => () => this.controller.answer(i);
                choiceButton.onClick.AddListener(listener(i));

                RectTransform rect = choiceButton.GetComponent<RectTransform>();
                rect.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                rect.localRotation = Quaternion.Euler(Vector3.zero);
                rect.localPosition = Vector3.zero;

                i++;

            }

        }

        public void unset() {
            this.questionText.SetText("");
            this.choiceButtons.Clear();
            Utilities.destroyChildren(this.choiceGroup);

        }

    }

}