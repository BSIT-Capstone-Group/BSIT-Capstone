using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.UIModule {
    public class UIController : MonoBehaviour {
        [Header("Indicators")]
        public TMP_Text modeText;
        public TMP_Text stageText;
        public TMP_Text coinText;
        public TMP_Text hintText;
        public TMP_Text lifeText;
        public Slider fuelBar;

        [Header("Controls")]
        public Button leftSteer;
        public Button rightSteer;
        public Button accelerate;
        public Button brake;

        [Header("Question")]
        public TMP_Text timeText;
        public GameObject imagesPanel;
        public GameObject imageButton;
        public GameObject maximizedImagePanel;
        public Image maximizedImage;
        public GameObject questionPanel;
        public TMP_Text questionText;
        public GameObject choicesPanel;
        public GameObject choiceButton;
        public Button hintButton;

        [Header("Post Stage")]
        public GameObject postStagePanel;
        public Button nextStageButton;

        [Header("Post Linear Play")]
        public GameObject postLinearPlayPanel;
        public TMP_Text totalTimeText;
        public Button freeRoamButton;

        [Header("Notifications")]
        public Utilities.Notification notification;
        public Utilities.Notification pointNotification;

        [Header("Controllers")]
        public VehicleModule.VehicleController vehicleController;
        public PlayerModule.PlayerController playerController;

        [Header("Events")]
        public UnityEvent<UIController, float> onFuelTopUp = new UnityEvent<UIController, float>();

        private void Awake() {
            this.playerController.onCoinChange.AddListener(this.onPlayerControllerCoinChange);
            this.playerController.onHintChange.AddListener(this.onPlayerControllerHintChange);
            this.playerController.onLifeChange.AddListener(this.onPlayerControllerLifeChange);

            this.vehicleController.onFuelChange.AddListener(this.onVehicleControllerFuelChange);

        }

        private void Update() {
            float rf = SimpleInput.GetAxis("RefillFuel");
            this.onFuelTopUp.Invoke(this, rf);

            if(GameModule.GameController.currentMode != null) {
                this.modeText.SetText(GameModule.GameController.currentModeType == GameModule.GameController.Mode.NON_PRO ? "Non-Pro" : "Pro");

                if(GameModule.GameController.linearPlayStage != null) {
                    int i = GameModule.GameController.currentMode.linearPlay.stages.IndexOf(GameModule.GameController.linearPlayStage);
                    int l = GameModule.GameController.currentMode.linearPlay.stages.Count;
                    this.stageText.SetText($"{i + 1} / {l}");

                }
            
            }

        }
        
        public void onPlayerControllerCoinChange(PlayerModule.PlayerController pc, float value) {
            this.coinText.SetText(pc.player.coin.ToString("N0"));

        }

        public void onPlayerControllerHintChange(PlayerModule.PlayerController pc, float value) {
            this.hintText.SetText(pc.player.hint.ToString("N0"));

        }

        public void onPlayerControllerLifeChange(PlayerModule.PlayerController pc, float value) {
            this.lifeText.SetText(pc.player.life.ToString("N0"));

        }

        public void onVehicleControllerFuelChange(VehicleModule.VehicleController vc, float value) {
            this.fuelBar.maxValue = 1.0f;
            this.fuelBar.minValue = 0.0f;
            this.fuelBar.value = vc.vehicle.fuel / vc.vehicle.maxFuel;

        }

        public void setQuestionText(QuestionModule.Question question) {
            this.questionText.SetText(question.text.Trim());

        }

        public GameObject[] setChoiceButtons(params QuestionModule.Choice[] choices) {
            Utilities.Helper.destroyChildren(this.choicesPanel.transform);

            List<GameObject> gameObjects = new List<GameObject>();
            foreach(QuestionModule.Choice choice in choices) {
                GameObject cb = Instantiate<GameObject>(this.choiceButton, this.choicesPanel.transform);
                gameObjects.Add(cb);
                TMP_Text t = cb.transform.GetChild(0).GetComponent<TMP_Text>();

                t.SetText(choice.text.Trim());

                if(Debug.isDebugBuild && choice.correct) {
                    t.color = new Color(0, 255, 0);

                }

            }

            return gameObjects.ToArray();

        }

        public GameObject[] setImageButtons(params Sprite[] sprites) {
            this.imagesPanel.SetActive(false);
            Utilities.Helper.destroyChildren(this.imagesPanel.transform);

            if(sprites.Length > 0) this.imagesPanel.SetActive(true);

            List<GameObject> gameObjects = new List<GameObject>();

            foreach(Sprite sprite in sprites) {
                GameObject go = UIController.Instantiate<GameObject>(this.imageButton, this.imagesPanel.transform);
                gameObjects.Add(go);
                
                go.GetComponent<Image>().sprite = sprite;

            }

            return gameObjects.ToArray();

        }

        public GameObject[] setImageButtons(params string[] paths) {
            List<Sprite> sprites = new List<Sprite>();

            foreach(string path in paths) {
                string npath = $"Images/{path}";
                sprites.Add(
                    Resources.Load<Sprite>(npath)
                );

            }

            return this.setImageButtons(sprites.ToArray());

        }

        public void setMaximizedImage(Sprite sprite) {
            this.maximizedImagePanel.SetActive(false);

            if(sprite) this.maximizedImagePanel.SetActive(true);

            this.maximizedImage.sprite = sprite;

        }

    }

}
