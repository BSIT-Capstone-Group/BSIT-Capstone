using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.LinearPlayModule.PlayerModule {
    [System.Serializable]
    public class Player {
        public float coin = 30.0f;
        public float hint = 3.0f;
        public float life = 3.0f;

    }

    public class PlayerController : MonoBehaviour {
        private Coroutine _convertCoinAsFuelCoroutine = null;

        public TextAsset playerFile;
        public Player player;

        public UIModule.UIController uiController;
        public ViewCameraModule.ViewCameraController viewCameraController;
        public LinearPlayModule.RoadModule.RoadController roadController;
        public VehicleModule.VehicleController vehicleController;

        public Vector3 startPositionOffset = Vector3.zero;
        public Vector3 middlePositionOffset = Vector3.zero;
        public Vector3 endPositionOffset = Vector3.zero;

        public UnityEvent<PlayerController, float> onCoinChange = new UnityEvent<PlayerController, float>();
        public UnityEvent<PlayerController, float> onHintChange = new UnityEvent<PlayerController, float>();
        public UnityEvent<PlayerController, float> onLifeChange = new UnityEvent<PlayerController, float>();
        public UnityEvent<PlayerController, Vector3, Vector3> onRespawn = new UnityEvent<PlayerController, Vector3, Vector3>();

        private void Awake() {
            this.setUpPlayer();

        }

        private void Start() {
            this.uiController.onFuelTopUp.AddListener(this.onUIFuelTopUp);
            this.viewCameraController.onUpdate.AddListener(this.onViewCameraControllerUpdate);
            this.setCoin(this.player.coin);
            this.setHint(this.player.hint);
            this.setLife(this.player.life);

        }

        private void FixedUpdate() {

        }

        public void onViewCameraControllerUpdate(
            ViewCameraModule.ViewCameraController vc,
            Vector3 position, Vector3 rotation
        ) {
            GameObject sizeModel = roadController.sizeModel;

            if(sizeModel) {
                MeshRenderer renderer = sizeModel.GetComponent<MeshRenderer>();
                Transform lmodel = roadController.transform.GetChild(
                    roadController.transform.childCount - 4
                );
                Transform fmodel = roadController.transform.GetChild(0);
                float lbound = lmodel.position.z - (renderer.bounds.size.z / 2);
                float fbound = fmodel.position.z;

                // print(this.transform.position.z + " >= " + lbound);

                if(this.transform.position.z <= fbound) {
                    vc.positionOffset = this.startPositionOffset;

                } else if(this.transform.position.z >= lbound) {
                    vc.positionOffset = this.endPositionOffset;

                } else {
                    vc.positionOffset = this.middlePositionOffset;

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

        public void setUpPlayer() {
			this.player = GameModule.GameController.currentMode.linearPlay.player;

        }

    }

}
