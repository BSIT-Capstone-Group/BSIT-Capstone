using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CoDeA.Lakbay.Modules.UIModule {
    public class UIController : Utilities.ExtendedMonoBehaviour {
        [Header("Indicators")]
        public TMP_Text modeText;
        public TMP_Text stageText;
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
        public GameObject imagesPanel;
        public GameObject imageButton;
        public GameObject maximizedImagePanel;
        public Image maximizedImage;
        public GameObject questionModal;
        public TMP_Text questionText;
        public GameObject choicesPanel;
        public GameObject choiceButton;

        [Header("Notification")]
        public Utilities.Notification notification;

        [Header("Controllers")]
        public VehicleModule.VehicleController vehicleController;
        public PlayerModule.PlayerController playerController;

        public UnityEvent<UIController, float> onFuelTopUp = new UnityEvent<UIController, float>();

        private void Awake() {
            this.questionModal.SetActive(false);

            // this.vehicleController.onFuelChange.AddListener(this.onVehicleFuelChange);
            this.playerController.onCoinChange.AddListener(this.onPlayerCoinChange);
            this.playerController.onFirstAidChange.AddListener(this.onPlayerFirstAidChange);
            this.playerController.onPointChange.AddListener(this.onPlayerPointChange);

        }

        private void Update() {
            this.updateFuelBar();

            float rf = SimpleInput.GetAxis("RefillFuel");
            this.onFuelTopUp.Invoke(this, rf);

            if(Game.modeData != null) {
                this.modeText.SetText(Game.mode == Game.Mode.NON_PRO ? "Non-Pro" : "Pro");

                if(Game.modeData.stage != null) {
                    int i = Game.modeData.stages.IndexOf(Game.modeData.stage);
                    int l = Game.modeData.stages.Count;
                    this.stageText.SetText($"{i + 1} / {l}");

                }
            
            }
            
        }

        public void updateFuelBar() {
            VehicleModule.VehicleController vc = this.vehicleController;
            this.fuelBar.value = Mathf.SmoothStep(this.fuelBar.value, vc.vehicle.fuel / vc.vehicle.maxFuel, 0.5f);

        }

        public void onPlayerCoinChange(PlayerModule.PlayerController pc, float value) {
            this.coinText.SetText(pc.coin.ToString("N0"));

        }

        public void onPlayerPointChange(PlayerModule.PlayerController pc, float value) {
            this.pointText.SetText(pc.point.ToString("N0"));

        }

        public void onPlayerFirstAidChange(PlayerModule.PlayerController pc, float value) {
            this.firstAidText.SetText(pc.firstAid.ToString("N0"));

        }

        public GameObject[] setChoiceButtons(params string[] texts) {
            Utilities.Helper.destroyChildren(this.choicesPanel.transform);

            List<GameObject> gameObjects = new List<GameObject>();
            foreach(string text in texts) {
                GameObject cb = Instantiate<GameObject>(this.choiceButton, this.choicesPanel.transform);
                gameObjects.Add(cb);
                TMP_Text t = cb.transform.GetChild(0).GetComponent<TMP_Text>();

                t.SetText(text.Trim());

            }

            return gameObjects.ToArray();

        }

        public GameObject[] setChoiceTexts(List<string> texts) => this.setChoiceButtons(texts.ToArray());
        
        public void setQuestionText(string text) {
            this.questionText.SetText(text.Trim());

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
                sprites.Add(AssetDatabase.LoadAssetAtPath<Sprite>(path));
                // Addressables.LoadAssetAsync<Sprite>(path).Completed += (h) => {
                //     if(h.Status == AsyncOperationStatus.Succeeded) {
                //         sprites.Add(h.Result);

                //     }
                // };

            }

            return this.setImageButtons(sprites.ToArray());

        }

        public void setMaximizedImage(Sprite sprite) {
            this.maximizedImagePanel.SetActive(false);

            if(sprite) this.maximizedImagePanel.SetActive(true);

            this.maximizedImage.sprite = sprite;

        }

        public void setMaximizedImage(string path) {
            this.setMaximizedImage(AssetDatabase.LoadAssetAtPath<Sprite>(path));

        }

    }

}
