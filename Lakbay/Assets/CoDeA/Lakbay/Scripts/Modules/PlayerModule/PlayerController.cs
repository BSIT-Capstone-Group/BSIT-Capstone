using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.PlayerModule {
    public class PlayerController : Utilities.ExtendedMonoBehaviour {
        private Coroutine coroutine = null;

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
            // print(this.canConvertCoinAsFuel);
            // Time.w
            // this.convertCoinAsFuel();
            if(this.canConvertCoinAsFuel && this.coroutine == null) {
                this.coroutine = StartCoroutine(this.convertCoinAsFuel());

            } else if(this.coroutine != null && !this.canConvertCoinAsFuel) {
                StopCoroutine(this.coroutine);
                this.coroutine = null;

            }

        }

        public IEnumerator convertCoinAsFuel() {
            while(this.coin != 0.0f && this.vehicleController.fuel != this.vehicleController.maxFuel) {
                this.setCoin(Mathf.Max(this.coin - 1, 0.0f));
                this.vehicleController.fuel = Mathf.Min(this.vehicleController.fuel + 1, this.vehicleController.maxFuel);

                yield return new WaitForSeconds(0.07f);

            }

        }

        public void update() {
            if(this.vehicleController.fuel == 0.0f) {
                if(this.firstAid > 0.0f) {
                    Transform[] children = Utilities.Helper.getChildren(this.roadController.transform);
                    List<Transform> lchildren = new List<Transform>(children);
                    lchildren.Reverse();
                    Transform[] rchildren = new List<Transform>(lchildren).ToArray();

                    this.vehicleController.canRecordDistanceCovered = false;
                    this.vehicleController.canRecordFuelDistanceCovered = false;
                    this.vehicleController.GetComponent<Rigidbody>().Sleep();

                    Vector3 resetPosition = this.vehicleController.initialPosition;
                    foreach(Transform qi in rchildren) {
                        if(qi.CompareTag("Question Item") && !qi.gameObject.activeSelf) {
                            resetPosition = qi.position;
                            break;

                        }

                    }

                    this.vehicleController.GetComponent<Rigidbody>().MovePosition(resetPosition + (Vector3.up * 1.2f));
                    this.vehicleController.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(Vector3.zero));

                    this.vehicleController.refillFuel(this.vehicleController.maxFuel * 0.5f, this.vehicleController.maxFuel);
                    this.vehicleController.canRecordDistanceCovered = true;
                    this.vehicleController.canRecordFuelDistanceCovered = true;
                    this.vehicleController.GetComponent<Rigidbody>().WakeUp();

                    this.setFirstAid(this.firstAid - 1.0f);

                } else {
                    Game.loadScene(0);

                }

            } else {

            }

            if(this.vehicleController.transform.position.y < this.roadController.transform.position.y) {
                Game.loadScene(0);

            }

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
