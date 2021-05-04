using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDeA_Old.Lakbay.Modules.WheelModule {
    public class WheelController : Utilities.ExtendedMonoBehaviour {
        [System.Serializable]
        public class Properties {
            public bool canAccelerate;
            public bool canSteer;
            public bool canBreak;
            public float maxSteerAngle = 30.0f;
            public float maxMotorTorque = 1500.0f;
            public float maxBrakeTorque = 1500.0f;
            
            public Vector3 rotation;

        }

        [HideInInspector]
        public new WheelCollider collider;
        public Transform model;
        public Properties properties;

        private void Start() {
            this.collider = this.transform.GetComponent<WheelCollider>();

        }

        public void updateModel() {
            if(this.model == null) return;
        
            Vector3 position;
            Quaternion rotation;

            this.collider.GetWorldPose(out position, out rotation);

            rotation *= Quaternion.Euler(this.properties.rotation);
        
            this.model.position = position;
            this.model.rotation = rotation;

        }

        public void steer(float steerAngle) {
            if(this.properties.canSteer) this.collider.steerAngle = steerAngle;

        }

        public void accelerate(float motorTorque) {
            if(this.properties.canAccelerate) this.collider.motorTorque = motorTorque;

        }

        public void brake(float brakeTorque) {
            if(this.properties.canBreak) this.collider.brakeTorque = brakeTorque;

        }

    }

}
