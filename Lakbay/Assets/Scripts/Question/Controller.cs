using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Question {
    public class Controller : MonoBehaviour {
        public Question.Display display;
        public Question.Item item;
        public bool canTrigger = true;
        public List<Transform> triggerAgents;

        public void answer(int i) {
            if(this.item.answer(i)) {
                this.item.correct = true;
                Debug.Log("Correct Answer!");

            } else {
                this.item.correct = false;
                Debug.Log("Wrong Answer!");

            }

            this.display.hide();
            this.display.unset();
            this.canTrigger = false;
            this.display.controller = null;
            Game.resume();

        }

        public void trigger(Collider collider) {
            if(this.canTrigger && this.triggerAgents.Contains(collider.transform)) {
                this.display.controller = this;
                this.display.set(this.item.question, this.item.choices.ToArray());
                this.display.show();
                Game.pause();
                Debug.Log("Question Reached!");

            }

        }

        private void OnTriggerEnter(Collider collider) {
            this.trigger(collider);

        }

    }

}
