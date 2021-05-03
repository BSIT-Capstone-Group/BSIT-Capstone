using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
// using SimpleInput = UnityEngine.Input;

namespace CoDeA.Lakbay.Modules.PlayerModule {
    [System.Serializable]
    public class Player {
        public float coin = 30.0f;
        public float hint = 3.0f;
        public float life = 3.0f;

    }

    public class PlayerController : Utilities.ExtendedMonoBehaviour {
        private Coroutine _coroutine = null;

        public TextAsset playerFile;

        public UIModule.UIController uiController;
        public VehicleModule.VehicleController vehicleController;
        public RoadModule.RoadController roadController;
        public QuestionModule.SetController setController;
        public Player player;

        public UnityEvent<PlayerController, float> onCoinChange = new UnityEvent<PlayerController, float>();
        public UnityEvent<PlayerController, float> onPointChange = new UnityEvent<PlayerController, float>();
        public UnityEvent<PlayerController, float> onLifeChange = new UnityEvent<PlayerController, float>();
        public UnityEvent<PlayerController, Vector3, Vector3> onRespawn = new UnityEvent<PlayerController, Vector3, Vector3>();

        private void Start() {
            this.setUpPlayer();
            this.setCoin(this.player.coin);
            this.setHint(this.player.hint);
            this.setLife(this.player.life);

            this.vehicleController.onFuelChange.AddListener(this.onVehicleFuelChange);
            this.uiController.onFuelTopUp.AddListener(this.onUIFuelTopUp);

        }

        private void FixedUpdate() {
            this.update();

        }

        private void Update() {

        }

        private void OnCollisionEnter(Collision collision) {
            Transform parent = collision.transform.parent;
            Transform child = collision.transform;
            if(child && child.name.Equals("Front")) {
                Game.Stage nextStage = Game.modeData.forwardStage();

                if(nextStage != null) {
                    Rigidbody rigidbody = this.vehicleController.GetComponent<Rigidbody>();

                    this.vehicleController.canRecordDistanceCovered = false;
                    this.vehicleController.canRecordFuelDistanceCovered = false;
                    rigidbody.Sleep();

                    this.setController.setUpSet();
                    this.roadController.setUp();

                    this.respawnAt(this.vehicleController.initialPosition);

                    this.vehicleController.canRecordDistanceCovered = true;
                    this.vehicleController.canRecordFuelDistanceCovered = true;
                    rigidbody.WakeUp();

                    return;

                } else {
                    Game.Loader.loadScene(1);

                }


            }

        }

        public void setUpPlayer() {
            TextAsset playerFile = Game.modeData.playerFile ? Game.modeData.playerFile : this.playerFile;
            this.player = Utilities.Helper.parseYAML<Player>(playerFile.ToString());

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
            if(canConvertCoinAsFuel && this._coroutine == null) {
                this._coroutine = this.StartCoroutine(this.convertCoinAsFuel());

            } else if(this._coroutine != null && !canConvertCoinAsFuel) {
                this.StopCoroutine(this._coroutine);
                this._coroutine = null;

            }

        }

        public void onVehicleFuelChange(VehicleModule.VehicleController vc, float value) {
            if(vc.vehicle.fuel == 0.0f) {
                if(this.player.life != 0.0f) {
                    this.respawnAt(this.getRespawnPosition());
                    vc.setFuel(Convert.ToInt32(vc.vehicle.maxFuel * 0.5f));
                    this.setLife(this.player.life - 1.0f);

                } else {
                    Game.Loader.loadScene(1);

                }

            }

        }

        public void update() {

        }

        public Vector3 getRespawnPosition() {
            Transform[] children = Utilities.Helper.getChildren(this.setController.transform);
            List<Transform> lchildren = new List<Transform>(children);
            lchildren.Reverse();
            Transform[] rchildren = new List<Transform>(lchildren).ToArray();

            Vector3 resetPosition = this.vehicleController.initialPosition;
            foreach(Transform qi in rchildren) {
                if(qi.CompareTag("Question Item") && !qi.gameObject.activeSelf) {
                    resetPosition = qi.position;
                    break;

                }

            }

            return resetPosition;
            
        }

        public void respawnAt(Vector3 position, Vector3 rotation, float yOffset) {
            Rigidbody rigidbody = this.vehicleController.GetComponent<Rigidbody>();
            Vector3 rposition = position + (Vector3.up * 1.2f);
            Vector3 rrotation = rotation;

            this.vehicleController.canRecordDistanceCovered = false;
            this.vehicleController.canRecordFuelDistanceCovered = false;
            rigidbody.Sleep();

            rigidbody.MovePosition(rposition);
            rigidbody.MoveRotation(Quaternion.Euler(rrotation));

            this.vehicleController.canRecordDistanceCovered = true;
            this.vehicleController.canRecordFuelDistanceCovered = true;
            rigidbody.WakeUp();

            this.onRespawn.Invoke(this, rposition, rrotation);

        }

        public void respawnAt(Vector3 position, Vector3 rotation) {
            this.respawnAt(position, rotation, 1.2f);

        }

        public void respawnAt(Vector3 position) {
            this.respawnAt(position, Vector3.zero);

        }

        public void setCoin(float value) {
            this.player.coin = value;
            this.onCoinChange.Invoke(this, value);

        }

        public void setHint(float value) {
            this.player.hint = value;
            this.onPointChange.Invoke(this, value);

        }

        public void setLife(float value) {
            this.player.life = value;
            this.onLifeChange.Invoke(this, value);

        }

    }

}
