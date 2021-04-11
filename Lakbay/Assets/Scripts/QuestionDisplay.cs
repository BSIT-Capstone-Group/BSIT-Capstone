using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuestionDisplay : MonoBehaviour {
    public TMP_Text questionText;
    public GameObject choice;
    public Transform choicePanel;
    
    private Question question;

    public void spawnChoices(params string[] choices) {
        for(int i = 0; i < choices.Length; i++) {
            string choice = choices[i];

            GameObject choice_ = Instantiate<GameObject>(this.choice);
            Button choiceButton = choice_.GetComponent<Button>();
            Func<int, UnityEngine.Events.UnityAction> listener = x => () => this.question.answer(x);
            choiceButton.onClick.AddListener(listener(i));

            RectTransform rectTransform = choice_.GetComponent<RectTransform>();
            choice_.transform.SetParent(this.choicePanel);
            rectTransform.localRotation = Quaternion.Euler(Vector3.zero);
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            TMP_Text choiceText = choice_.transform.GetChild(0).GetComponent<TMP_Text>();
            choiceText.SetText(choice);
            
        }
    }

    public void despawnChoices() {
        for(int i = 1; i <= this.choicePanel.childCount; i++) {
            Transform child = this.choicePanel.GetChild(i - 1);
            GameObject.Destroy(child.gameObject);
        }
    }

    public void setQuestion(string question) {
        this.questionText.SetText(question);
    }

    public void clearQuestion() {
        this.questionText.SetText("");
    }

    public void set(Question question, string questionString, params string[] choices) {
        this.question = question;
        this.setQuestion(questionString);
        this.spawnChoices(choices);
    }

    public void unset() {
        this.clearQuestion();
        this.despawnChoices();
    }

    public void show() {
        this.gameObject.SetActive(true);
    }

    public void hide() {
        this.gameObject.SetActive(false);
    }

    public void Start() {
        this.hide();
    }
}
