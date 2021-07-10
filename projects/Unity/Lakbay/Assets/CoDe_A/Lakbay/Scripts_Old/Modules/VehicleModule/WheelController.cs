using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDe_A_Old.Lakbay.Modules.VehicleModule {
    public class WheelController : MonoBehaviour {
        [HideInInspector]
        public new WheelCollider collider;

        public string steerAxis = "SteeringWheel";
        public string accelerateAxis = "AccelerationAndBrake";
        public string reverseAxis = "ReverseAcceleration";
        public string brakeAxis = "AccelerationAndBrake";

        public bool canSteer = false;
        public bool canAccelerate = false;
        public bool canBrake = false;

        public float maxSteerAngle = 35.0f;
        public float maxMotorTorque = 15000.0f;
        public float maxBrakeTorque = 30000.0f;
        public float maxDeceleration = 500.0f;

        public VehicleController vehicleController;
        public GameObject model;
        public Vector3 modelRotation = Vector3.zero;

        private void Start() {
            this.setUp();

        }

        private void FixedUpdate() {
            if(this.vehicleController) return;

            float sf = this.getSteerAngleFactor(), bf = this.getBrakeTorqueFactor(),
            af = this.getMotorTorqueFactor(), df = sf;

            this.steer(sf);
            this.brake(bf);
            this.accelerate(af);

            if(bf == 0.0f) this.decelerate(af == 0.0f ? 1.0f : 0.0f);

            this.updateModel();

        }

        public void setUp() {
            if(!this.collider) this.collider = this.transform.GetComponent<WheelCollider>();
            if(!this.collider) this.collider = this.gameObject.AddComponent<WheelCollider>();

            GameObject go = this.model;

            if(this.transform.childCount == 0 && this.model) {
                go = GameObject.Instantiate<GameObject>(this.model, this.transform);
                go.transform.localScale = Vector3.one;
                go.transform.position = Vector3.zero;
                go.transform.rotation = Quaternion.Euler(Vector3.zero + this.modelRotation);

            } else {
                go = this.transform.GetChild(0).gameObject;

            }

            this.model = go;

        }
        
        public void steer(float factor) {
            if(!this.canSteer) return;

            this.collider.steerAngle = this.maxSteerAngle * factor;

        }
        
        public void accelerate(float factor) {
            if(!this.canAccelerate) return;

            this.collider.motorTorque = this.maxMotorTorque * factor;

        }

        public void decelerate(float factor) {
            if(!this.canAccelerate) return;

            this.collider.brakeTorque = this.maxDeceleration * factor;

        }
        
        public void brake(float factor) {
            if(!this.canBrake) return;

            this.collider.brakeTorque = this.maxBrakeTorque * factor;

        }

        public virtual float getSteerAngleFactor() {
            if(!this.canSteer) return 0.0f;

            float value = SimpleInput.GetAxis(this.steerAxis);
            value = value == 0.0f ? SimpleInput.GetAxis("Horizontal") : value;


            return value;

        }

        public virtual float getMotorTorqueFactor() {
            if(!this.canAccelerate) return 0.0f;

            float accelerate = Mathf.Min(SimpleInput.GetAxis(this.accelerateAxis), 0.0f) * -1;
            float raccelerate = Mathf.Min(SimpleInput.GetAxis(this.reverseAxis), 0.0f);
            float value = accelerate + raccelerate;
            value = value == 0.0f ? SimpleInput.GetAxis("Vertical") : value;

            return value;

        }

        public virtual float getBrakeTorqueFactor() {
            if(!this.canBrake) return 0.0f;

            float value = Mathf.Max(SimpleInput.GetAxis(this.brakeAxis), 0.0f);
            value = value == 0.0f ? SimpleInput.GetAxis("Jump") : value;

            return value;

        }

        public void updateModel() {
            if(this.model) {
                Quaternion q;
                Vector3 p;
                this.collider.GetWorldPose(out p, out q);

                this.model.transform.position = p;
                this.model.transform.rotation = q * Quaternion.Euler(this.modelRotation);
                this.model.transform.localScale = Vector3.one;
                
            }

        }

    }

}
