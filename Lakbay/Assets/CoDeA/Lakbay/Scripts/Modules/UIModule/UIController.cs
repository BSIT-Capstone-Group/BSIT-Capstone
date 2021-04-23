using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.UIModule {
    public class UIController : Utilities.ExtendedMonoBehaviour {
        [Header("Indicators")]
        public TMP_Text coinText;
        public TMP_Text pointText;
        public TMP_Text firstAidText;
        public Slider fuelBar;

        [Header("Controls")]
        public Button leftSteer;
        public Button rightSteer;
        public Button accelerate;
        public Button brake;

        [Header("Question")]
        public TMP_Text timeText;
        public GameObject questionModal;
        public TMP_Text questionText;
        public GameObject choicesPanel;
        public GameObject choiceButton;

        [Header("Notification")]
        public Utilities.Notification notification;

        private void Start() {
            this.questionModal.SetActive(false);

        }

    }

}
