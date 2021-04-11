using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour {
    public bool canTrigger = true;
    public List<string> targetTags;
    public List<Transform> targetTransforms;
    public QuestionDisplay questionDisplay;

    public string question;
    public List<string> choices;
    public int correctChoiceIndex;

    public void answer(int choiceIndex) {
        Debug.Log(choiceIndex);
        if(this.correctChoiceIndex == choiceIndex) {
            Debug.Log("Correct Answer!");

        } else {
            Debug.Log("Wrong Answer!");

        }

        this.canTrigger = false;
        this.questionDisplay.hide();
        this.questionDisplay.unset();

        Game.resume();
    }

    public void OnTriggerEnter(Collider collider) {
        if(this.canTrigger) {
            foreach(string targetTag in this.targetTags) {
                if(collider.tag == targetTag) {
                }
            }

            foreach(Transform targetTransform in this.targetTransforms) {
                if(collider.transform == targetTransform) {
                    if(questionDisplay) {
                        questionDisplay.show();
                        questionDisplay.set(this, this.question, this.choices.ToArray());
                    }

                    Game.pause();

                    Debug.Log("Question Reached!");
                }
            }
        }
    }
}
