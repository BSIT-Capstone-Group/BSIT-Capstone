using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using CoDe_A.Lakbay.Modules.GameModule;
// using CoDe_A.Lakbay.Modules.GameModule;
using System;
using TMPro;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.PlayerModule {
    [System.Serializable]
    public class Player {
        public float coin = 30.0f;
        public float hint = 3.0f;
        public float life = 3.0f;

        public float maxLifeIntegrity = 3.0f;
        public float lifeIntegrity = 3.0f;

    }

    public class PlayerController : MonoBehaviour {
        private Coroutine _convertCoinAsFuelCoroutine = null;

        public TextAsset playerFile;
        public Player player;

        public UIModule.UIController uiController;
        public ViewCameraModule.ViewCameraController viewCameraController;
        public LinearPlayModule.RoadModule.RoadController roadController;
        public VehicleModule.VehicleController vehicleController;
        public QuestionModule.SetController setController;
        public Utilities.Timer timer;

        public UnityEvent<PlayerController, float> onCoinChange = new UnityEvent<PlayerController, float>();
        public UnityEvent<PlayerController, float> onHintChange = new UnityEvent<PlayerController, float>();
        public UnityEvent<PlayerController, float> onLifeChange = new UnityEvent<PlayerController, float>();
        public UnityEvent<PlayerController, float> onLifeIntegrityChange = new UnityEvent<PlayerController, float>();
        public UnityEvent<PlayerController, Vector3, Vector3> onRespawn = new UnityEvent<PlayerController, Vector3, Vector3>();

        private void Awake() {
            GameController.setDayPhase();
            if(GameController.dayPhase == GameController.DayPhase.EVENING) {
                this.vehicleController.turnOnHeadlights();

            } else this.vehicleController.turnOffHeadlights();

            this.uiController.onFuelTopUp.AddListener(this.onUIFuelTopUp);
            this.viewCameraController.onUpdate.AddListener(this.onViewCameraControllerUpdate);
            this.onHintChange.AddListener((pc, f) => {
                TMP_Text hintText = this.uiController.hintButton.GetComponentInChildren<TMP_Text>();
                hintText.SetText($"Hint ({pc.player.hint})");
            });

            this.vehicleController.onFuelChange.AddListener(this.onVehicleControllerFuelChange);

        }

        private void Start() {
            this.timer.start();

            if(GameController.currentModeData != null) this.setUpPlayer(
                GameController.currentModeData.linearPlayData.playerFile
            );
            else if(this.playerFile) this.setUpPlayer(this.playerFile);

        }

        private void Update() {
            // print(this.timer.time);
            if(this.roadController.finishLineModel) {
                float tp = this.roadController.finishLineModel.transform.position.z;
                float cp = this.transform.position.z;
                this.uiController.progressBar.value = cp / tp;

            }

        }

        private void OnCollisionEnter(Collision collider) {
            this.triggerFinishLine(collider);

        }

        public void triggerFinishLine(Collision collider) {
            if(this.roadController) {
                GameObject fm = this.roadController.finishLineModel;
                if(fm && fm.transform == collider.transform) {
                    int level = GameController.currentLinearPlayLevelIndex + 1; 
                    GameModule.LinearPlayData.Level l = GameController.forwardLinearPlayLevel();

                    GameController.pause();
                    this.vehicleController.sleep();
                    this.uiController.postStagePanel.SetActive(true);
                    this.uiController.nextStageButton.onClick.RemoveAllListeners();
                    // this.uiController.endGameButton.onClick.RemoveAllListeners();
                    this.uiController.nextStageButton.onClick.AddListener(this.onNextStage);
                    this.timer.pause();

                    int score = this.setController.score, maxScore = this.setController.maxScore;
                    this.uiController.scoreText.SetText($"{score} / {maxScore}");

                    this.uiController.nextStageButton.gameObject.SetActive(true);
                    this.uiController.endGameButton.gameObject.SetActive(false);
                    // TMP_Text text = this.uiController.scoreDescriptionPanel.GetComponentInChildren<TMP_Text>();
                    // text.gameObject.SetActive(true);
                    this.uiController.levelText.SetText(level + "");
                    this.uiController.scoreDescriptionText.SetText(this.setController.scoreDescription);
                    
                    for(int i = 0; i < this.uiController.starsPanel.transform.childCount; i++) {
                        var go = this.uiController.starsPanel.transform.GetChild(i);
                        if(i < this.setController.star) go.gameObject.SetActive(true);
                        else go.gameObject.SetActive(false);

                    }
                    // this.vehicleController.sleep();
                    // this.uiController.postLinearPlayPanel.SetActive(true);
                    // this.uiController.freeRoamButton.onClick.RemoveAllListeners();
                    // this.uiController.freeRoamButton.onClick.AddListener(this.onNextPhase);
                    // string text = this.timer.time.ToString("N0") + "s";
                    // text += $", {this.setController.score}/{this.setController.maxScore} pts";
                    // this.uiController.totalTimeText.SetText(text);

                    if(level == 3) {
                        this.uiController.viewAssessmentButton.gameObject.SetActive(true);
                        this.uiController.nextStageButton.gameObject.SetActive(false);
                        this.timer.stop();

                        var levels = GameController.currentModeData.linearPlayData.levels;

                        for(int i = 0; i < 3; i++) {
                            string ls = $"{PlayerDataController.levelScores[i].Item1} / {PlayerDataController.levelScores[i].Item2}";
                            this.uiController.levelScoreTexts[i].SetText(ls);

                        }

                        this.uiController.remainingLifeText.SetText(PlayerDataController.life + "");
                        this.uiController.remainingHintText.SetText(PlayerDataController.hint + "");
                        this.uiController.remainingCoinText.SetText(PlayerDataController.coin + "");

                        TimeSpan ts = TimeSpan.FromSeconds(PlayerDataController.totalTime);
                        string tsstr = ts.ToString(@"\:mm\:ss"); 

                        this.uiController.totalScoreText.SetText($"{PlayerDataController.totalScore} / {PlayerDataController.totalMaxScore}");
                        this.uiController.totalTimeText.SetText($"{tsstr}");

                        bool passed = PlayerDataController.passed;

                        this.uiController.crossmarkImage.gameObject.SetActive(!passed);
                        this.uiController.failedText.gameObject.SetActive(!passed);

                        this.uiController.checkmarkImage.gameObject.SetActive(passed);
                        this.uiController.passedText.gameObject.SetActive(passed);

                        this.uiController.freeRoamButton.gameObject.SetActive(passed);
                        this.uiController.retryButton.gameObject.SetActive(!passed);

                        this.uiController.freeRoamButton.onClick.RemoveAllListeners();
                        this.uiController.freeRoamButton.onClick.AddListener(() => {
                            GameController.resume();
                            GameController.loadScene(1);
                        });

                    } else {
                        this.uiController.nextStageButton.gameObject.SetActive(true);
                        this.uiController.viewAssessmentButton.gameObject.SetActive(false);

                    }

                }

            }

        }

        public void onVehicleControllerFuelChange(VehicleModule.VehicleController vc, float value) {
            if(value == 0.0f) {
                if(this.player.life > 0.0f) {
                    this.respawn();
                    this.useLife();

                } else {
                    GameController.loadScene(1);

                }

            }

        }

        public void onNextStage() {
            GameController.resume();
            this.setLifeIntegrity(this.player.maxLifeIntegrity);
            this.vehicleController.setFuel(
                Mathf.Min(this.vehicleController.vehicle.fuel + (this.player.coin * 0.25f), this.vehicleController.vehicle.maxFuel)
            );

            GameModule.LinearPlayData.Level l = GameController.currentLinearPlayLevel;
            this.vehicleController.respawn(
                this.vehicleController.initialPosition + (Vector3.up * 2.0f),
                this.vehicleController.initialRotation
            );
            this.roadController.setUpRoad(l.roadFile);
            this.setController.setUpSet(l.setFile);

            GameController.setDayPhase();
            if(GameController.dayPhase == GameController.DayPhase.EVENING) {
                this.vehicleController.turnOnHeadlights();
                
                foreach(GameObject go in this.roadController.lights) go.SetActive(true);

            } else this.vehicleController.turnOffHeadlights();

            this.vehicleController.wakeUp();
            this.uiController.postStagePanel.SetActive(false);
            this.timer.resume();

        }

        public void onNextPhase() {
            GameController.loadScene(1);

        }

        public void onItemAnswer(QuestionModule.ItemController ic, QuestionModule.Choice choice) {
            string message = "";
            string svalue = "";

            if(choice == null) {
                message = "Time's Up! Fuel got reduced by {0}.";

            }

            if(ic.checkAnswer(choice)) {
                // float maxCoinGain = 30.0f, minCoinGain = 5.0f;
                float maxCoinGain = ic.item.maxCoinGain, minCoinGain = ic.item.minCoinGain;
                float coinGain = (
                    maxCoinGain * (1.0f - ic.timer.progress)
                );
                coinGain = Mathf.Max(coinGain, minCoinGain);
                coinGain = Convert.ToInt32(coinGain);
                float currentCoin = ic.playerController.player.coin + coinGain;

                this.setCoin(currentCoin);

                string cg = coinGain.ToString("N0");
                message = "Correct Answer! Coins increased by {0}.";
                svalue = cg;

                this.uiController.pointNotification.show("1", 1.5f);

            } else {

                // float maxFuelDeduction = this.vehicleController.maxFuel * 0.15f;
                // float minFuelDeduction = this.vehicleController.maxFuel * 0.05f;
                float maxFuelDeduction = ic.item.maxFuelDeduction;
                float minFuelDeduction = ic.item.minFuelDeduction;
                float fuelDeduction = maxFuelDeduction * ic.timer.progress;
                fuelDeduction = Mathf.Max(maxFuelDeduction * ic.timer.progress, minFuelDeduction);
                float newFuel = this.vehicleController.vehicle.fuel - fuelDeduction;

                this.vehicleController.setFuel(newFuel);

                string fd = fuelDeduction.ToString("N0");
                message = choice != null ? "Wrong Answer! Fuel got reduced by {0}." : message;
                svalue = fd;

            }
            
            this.uiController.notification.show(
                String.Format(message, svalue),
                3.5f
            );

            // print(ic.answeredCorrectly + " " + this.setController.score);
            // foreach(var icc in this.setController.itemControllers) {
            //     print(icc.answeredCorrectly);

            // }
            // print("===========================");

        }

        public void onViewCameraControllerUpdate(
            ViewCameraModule.ViewCameraController vc,
            Vector3 position, Vector3 rotation
        ) {

            if(roadController.road != null) {
                Transform lmodel = roadController.transform.GetChild(
                    roadController.transform.childCount - 1
                );
                Transform fmodel = roadController.transform.GetChild(0);
                float lbound = lmodel.position.z - (roadController.modelSize * 7);
                float fbound = fmodel.position.z - (roadController.modelSize * 2.25f);

                if(vc.transform.position.z <= fbound) {
                    vc.move(new Vector3(
                        vc.transform.position.x,
                        vc.transform.position.y,
                        fbound
                    ), 0.0f);

                } else if(vc.transform.position.z >= lbound) {
                    vc.move(new Vector3(
                        vc.transform.position.x,
                        vc.transform.position.y,
                        lbound
                    ), 0.0f);

                }

            }

        }

        public IEnumerator convertCoinAsFuel() {
            float time = 0.25f;
            while(this.player.coin != 0.0f && this.vehicleController.vehicle.fuel != this.vehicleController.vehicle.maxFuel) {
                float coin = Mathf.Max(this.player.coin - 1, 0.0f);
                float fuel = Mathf.Min(this.vehicleController.vehicle.fuel + 1, this.vehicleController.vehicle.maxFuel);
                this.setCoin(coin);
                this.vehicleController.setFuel(fuel);

                yield return new WaitForSeconds(time);

                time = Mathf.Max(time - 0.05f, 0.05f);

            }

        }

        public void onUIFuelTopUp(UIModule.UIController uc, float value) {
            bool canConvertCoinAsFuel = value > 0.0f ? true : false;
            if(canConvertCoinAsFuel && this._convertCoinAsFuelCoroutine == null) {
                this._convertCoinAsFuelCoroutine = this.StartCoroutine(this.convertCoinAsFuel());

            } else if(this._convertCoinAsFuelCoroutine != null && !canConvertCoinAsFuel) {
                this.StopCoroutine(this._convertCoinAsFuelCoroutine);
                this._convertCoinAsFuelCoroutine = null;

            }

        }

        public void setCoin(float value) {
            this.player.coin = value;
            this.onCoinChange.Invoke(this, value);

        }

        public void setHint(float value) {
            this.player.hint = value;
            this.onHintChange.Invoke(this, value);

        }

        public void setLife(float value) {
            this.player.life = value;
            this.onLifeChange.Invoke(this, value);

        }

        public void setLifeIntegrity(float value) {
            this.player.lifeIntegrity = value;
            this.onLifeIntegrityChange.Invoke(this, value);

        }

        public void setUpPlayer(TextAsset playerFile) {
            this.setUpPlayer(Utilities.Helper.parseYAML<Player>(playerFile.text));

        }

        public void setUpPlayer(Player player) {
            this.player = player;
            this.setCoin(this.player.coin);
            this.setHint(this.player.hint);
            this.setLife(this.player.life);
            this.setLifeIntegrity(this.player.lifeIntegrity);

        }

        public void respawn() {
            VehicleModule.VehicleController vc = this.vehicleController;
            QuestionModule.ItemController ic = this.setController.currentItemController;

            if(ic == null) {
                vc.respawn(vc.initialPosition + (Vector3.up * 2.0f), vc.initialRotation);

            } else {
                vc.respawn(ic.transform.position + (Vector3.up * 2.0f), vc.initialRotation);

            }

        }

        public void useLife() {
            this.setLife(this.player.life - 1.0f);
            this.vehicleController.setFuel(
                (this.vehicleController.vehicle.fuel / this.vehicleController.vehicle.maxFuel) +
                this.vehicleController.vehicle.maxFuel * 0.5f
            );

        }

    }

}
