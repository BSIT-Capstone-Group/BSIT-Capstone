using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using SimpleInput = UnityEngine.Input;

namespace CoDeA.Lakbay.Modules.PlayerModule {
    [System.Serializable]
    public class Player {
        public float coins = 30.0f;
        public float firstAid = 3.0f;

    }

    public class PlayerController : Utilities.ExtendedMonoBehaviour {
        private Coroutine _coroutine = null;

        [HideInInspector]
        public bool canConvertCoinAsFuel = false;

        public float coin = 0.0f;
        public float point = 0.0f;
        public float firstAid = 0.0f;

        public UIModule.UIController uiController;
        public VehicleModule.VehicleController vehicleController;
        public RoadModule.RoadController roadController;

        private void Start() {
            this.setCoin(this.coin);
            this.setPoint(this.point);
            this.setFirstAid(this.firstAid);

        }

        private void FixedUpdate() {
            this.update();

        }

        private void Update() {
            this.canConvertCoinAsFuel = SimpleInput.GetAxis("RefillFuel") > 0.0f ? true : false;

            if(this.canConvertCoinAsFuel && this._coroutine == null) {
                this._coroutine = StartCoroutine(this.convertCoinAsFuel());

            } else if(this._coroutine != null && !this.canConvertCoinAsFuel) {
                StopCoroutine(this._coroutine);
                this._coroutine = null;

            }

        }

        public IEnumerator convertCoinAsFuel() {
            float time = 0.25f;
            while(this.coin != 0.0f && this.vehicleController.fuel != this.vehicleController.maxFuel) {
                this.setCoin(Mathf.Max(this.coin - 1, 0.0f));
                this.vehicleController.fuel = Mathf.Min(this.vehicleController.fuel + 1, this.vehicleController.maxFuel);

                yield return new WaitForSeconds(time);

                time = Mathf.Max(time - 0.05f, 0.05f);

            }

        }

        public void update() {
            if(this.vehicleController.fuel == 0.0f) {
                if(this.firstAid > 0.0f) {
                    Transform[] children = Utilities.Helper.getChildren(this.roadController.transform);
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

                    this.vehicleController.refillFuel(this.vehicleController.maxFuel * 0.5f, this.vehicleController.maxFuel);
                    this.setFirstAid(this.firstAid - 1.0f);
                    this.respawnAt(resetPosition);

                } else {
                    Game.loadScene(0);

                }

            } else {

            }

            if(this.vehicleController.transform.position.y < this.roadController.transform.position.y) {
                Game.loadScene(0);

            }

        }

        public void respawnAt(Vector3 position, Vector3 rotation, float yOffset) {
            Rigidbody rigidbody = this.vehicleController.GetComponent<Rigidbody>();

            this.vehicleController.canRecordDistanceCovered = false;
            this.vehicleController.canRecordFuelDistanceCovered = false;
            rigidbody.Sleep();

            rigidbody.MovePosition(position + (Vector3.up * 1.2f));
            rigidbody.MoveRotation(Quaternion.Euler(Vector3.zero));

            this.vehicleController.canRecordDistanceCovered = true;
            this.vehicleController.canRecordFuelDistanceCovered = true;
            rigidbody.WakeUp();

        }

        public void respawnAt(Vector3 position, Vector3 rotation) {
            this.respawnAt(position, rotation, 1.2f);

        }

        public void respawnAt(Vector3 position) {
            this.respawnAt(position, Vector3.zero);

        }

        public void setCoin(float value) {
            this.coin = value;
            this.uiController.coinText.SetText(value.ToString("N0"));

        }

        public void setPoint(float value) {
            this.point = value;
            this.uiController.pointText.SetText(value.ToString("N0"));

        }

        public void setFirstAid(float value) {
            this.firstAid = value;
            this.uiController.firstAidText.SetText(value.ToString("N0"));

        }

    }

}
