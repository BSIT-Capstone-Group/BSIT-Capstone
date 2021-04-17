using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Question {
    public class Controller : Utilities.ExtendedMonoBehaviour {
        [HideInInspector]
        public bool triggered = false;

        public Question.Display display;
        public Question.Item item;
        public Utilities.Timer timer;
        public bool canTrigger = true;
        public List<Transform> triggerAgents;

        [HideInInspector]
        public Transform triggeringAgent;

        public void answer(int i) {
            if(this.item.answer(i)) {
                this.item.correct = true;
                Debug.Log("Correct Answer!");

            } else {
                this.item.correct = false;
                Debug.Log("Wrong Answer!");

            }

            this.canTrigger = false;
            this.triggeringAgent.GetComponent<Rigidbody>().isKinematic = false;
            this.triggeringAgent = null;
            this.display.hide();
            this.display.unset();
            this.display.controller = null;
            // Game.resume();

        }

        public void trigger(Collider collider) {
            if(this.canTrigger && this.triggerAgents.Contains(collider.transform)) {
                this.display.controller = this;
                this.display.set(this.item.question, this.item.choices.ToArray());
                this.display.show();
                this.triggered = true;
                this.timer.start();
                this.triggeringAgent = collider.transform;
                this.triggeringAgent.GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log("Question Reached!");
                // Game.pause();

            }

        }

        private void Update() {
            if(this.triggered) {
                if(this.timer.timeRemaining != 0) {
                    this.display.timeText.SetText(this.timer.timeRemaining.ToString("N1") + "s");

                }

                if(this.triggeringAgent && this.timer.timeRemaining == 0.0f) {
                    this.canTrigger = false;
                    this.triggeringAgent.GetComponent<Rigidbody>().isKinematic = false;
                    this.triggeringAgent = null;
                    this.display.hide();
                    this.display.unset();
                    this.display.controller = null;

                }

            }

        }

        private void OnTriggerEnter(Collider collider) {
            this.trigger(collider);

        }

    }

}
