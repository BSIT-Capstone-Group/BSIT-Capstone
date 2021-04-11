using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    private Vector3 lastPosition = Vector3.zero;
    private float distanceCovered = 0.0f;
    private bool canRecordDistanceCovered = true;

    private float fuelDistanceCovered = 0.0f;
    private float refilledFuel = 0.0f;
    private bool canRecordFuelDistanceCovered = true;

    [Tooltip("Wheel")]
    public List<Wheel> wheels;

    public Transform fuelBar;
    public float initialFuel = 100.0f;
    public float maxFuel = 100.0f;
    public float fuelPerDistance = 0.10f;

    private float fuel = 0.0f;

    public bool shareWheelProperties = true;

    public WheelProperties properties;

    public void updateFuel() {
        float consumedFuel = this.distanceCovered * this.fuelPerDistance;
        float currentFuel = this.refilledFuel - consumedFuel;

        if(currentFuel < 0.0f) currentFuel = 0.0f;

        this.fuel = currentFuel;
    }

    public void updateSlider() {
        if(this.fuelBar != null) {
            Slider slider = this.fuelBar.GetComponent<Slider>();
            slider.maxValue = this.refilledFuel;
            slider.value = Mathf.SmoothStep(slider.value, this.fuel, 0.25f);
        }
    }

    public float recordDistanceCovered() {
        float distanceCovered = float.Parse(Vector3.Distance(this.transform.position, this.lastPosition).ToString("F2"));

        if(this.canRecordDistanceCovered) {
            this.distanceCovered += distanceCovered;
        }

        return distanceCovered;
    }

    public float recordFuelDistanceCovered() {
        float distanceCovered = float.Parse(Vector3.Distance(this.lastPosition, this.transform.position).ToString("F2"));

        if(this.canRecordFuelDistanceCovered) {
            this.fuelDistanceCovered += distanceCovered;
        }

        return distanceCovered;
    }

    public void refillFuel(float value) {
        this.fuelDistanceCovered = 0.0f;
        this.fuel = value;
        this.refilledFuel = value;
    }

    public void updateWheels() {
        float motorTorque = Input.GetAxis("Vertical");
        float brakeTorque = Input.GetAxis("Jump");
        float steerAngle = Input.GetAxis("Horizontal");
     
        foreach(Wheel wheel in wheels) {
            float maxSteerAngle = this.shareWheelProperties ? this.properties.maxSteerAngle : wheel.properties.maxSteerAngle;
            float maxMotorTorque = this.shareWheelProperties ? this.properties.maxMotorTorque : wheel.properties.maxMotorTorque;
            float maxBrakeTorque = this.shareWheelProperties ? this.properties.maxBrakeTorque : wheel.properties.maxBrakeTorque;

            if(this.fuel > 0.0f) {
                wheel.accelerate(motorTorque * maxMotorTorque);

                if(motorTorque != 0.0f) {
                    this.canRecordFuelDistanceCovered = true;
                    this.updateFuel();

                } else this.canRecordFuelDistanceCovered = false;
                
                // Debug.Log($"Distance Covered: {this.fuelDistanceCovered}, Fuel: {this.fuel}");

            } else {
                this.canRecordFuelDistanceCovered = false;
                wheel.accelerate(0.0f);
            }

            wheel.steer(steerAngle * maxSteerAngle);
            wheel.brake(brakeTorque * maxBrakeTorque);
            wheel.positionVisual();

        }

    }

    public void Start() {
        this.refillFuel(this.initialFuel);
        this.lastPosition = transform.position;
    }

    public void FixedUpdate() {
        this.recordDistanceCovered();
        this.recordFuelDistanceCovered();
        this.updateWheels();
        this.updateSlider();
            
        this.lastPosition = transform.position;
    }
}