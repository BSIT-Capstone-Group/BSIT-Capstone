using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hajono.Lakbay.Modules.Player {
    public class PlayerController : Utilities.ExtendedMonoBehaviour {
        public float coin = 0.0f;
        public float point = 0.0f;
        public float firstAid = 0.0f;

        public UI.UIController uiController;
        public Vehicle.VehicleController vehicleController;
        public Road.RoadController roadController;

        private void Start() {
            this.setCoin(this.coin);
            this.setPoint(this.point);
            this.setFirstAid(this.firstAid);

        }

        private void FixedUpdate() {
            this.update();

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

                    this.vehicleController.transform.position = resetPosition;
                    // this.vehicleController.transform.position = Vector3.MoveTowards(
                    //     this.vehicleController.transform.position,
                    //     resetPosition,
                    //     Time.fixedDeltaTime
                    // );

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
