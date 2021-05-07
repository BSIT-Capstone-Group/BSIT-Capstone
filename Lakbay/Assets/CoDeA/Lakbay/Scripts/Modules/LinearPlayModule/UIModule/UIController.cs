using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.LinearPlayModule.UIModule {
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

    }

}
