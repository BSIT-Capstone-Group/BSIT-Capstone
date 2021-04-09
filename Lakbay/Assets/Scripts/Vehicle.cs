using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WheelProperties {
    public float maxSteerAngle = 30.0f;
    public float maxMotorTorque = 1500.0f;
    public float maxBrakeTorque = 1500.0f;
    public bool canAccelerate;
    public bool canSteer;
    public bool canBreak;
    
    public Vector3 rotation;
}

[System.Serializable]
public class Wheel {
    public WheelCollider collider;

    public WheelProperties properties;

    public Transform transform {
        get => this.collider.transform.childCount == 0 ? null : this.collider.transform.GetChild(0);
    }

    public void positionVisual() {
        if(this.transform == null) return;
     
        Vector3 position;
        Quaternion rotation;

        this.collider.GetWorldPose(out position, out rotation);

        rotation *= Quaternion.Euler(this.properties.rotation);
     
        this.transform.position = position;
        this.transform.rotation = rotation;
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

public class Vehicle : MonoBehaviour {
    [Tooltip("Wheel")]
    public List<Wheel> wheels;

    public bool shareWheelProperties = true;

    public WheelProperties properties;
     
    // finds the corresponding visual wheel
    // correctly applies the transform
    public void positionVisualWheels(WheelCollider collider) {
        if(collider.transform.childCount == 0) {
            return;
        }
     
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
     
    public void FixedUpdate() {
        float motorTorque = Input.GetAxis("Vertical");
        float brakeTorque = Input.GetAxis("Jump");
        float steerAngle = Input.GetAxis("Horizontal");

        // Debug.Log($"{motorTorque} {brakeTorque}");
     
        foreach(Wheel wheel in wheels) {
            float maxSteerAngle = this.shareWheelProperties ? this.properties.maxSteerAngle : wheel.properties.maxSteerAngle;
            float maxMotorTorque = this.shareWheelProperties ? this.properties.maxMotorTorque : wheel.properties.maxMotorTorque;
            float maxBrakeTorque = this.shareWheelProperties ? this.properties.maxBrakeTorque : wheel.properties.maxBrakeTorque;

            wheel.steer(steerAngle * maxSteerAngle);
            wheel.accelerate(motorTorque * maxMotorTorque);
            wheel.brake(brakeTorque * maxBrakeTorque);
            wheel.positionVisual();
        }
    }
}