using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public enum View {
    TOP, BOTTOM,
    FRONT, BACK,
    LEFT, RIGHT
}

public class ViewCamera : MonoBehaviour {
    public Transform target;
    public View view = View.TOP;
    public float distance = 25.0f;
    public float smoothTime = 1.0f;

    public bool lockXPosition = false;
    public bool lockYPosition = false;
    public bool lockZPosition = false;
    public Vector3 positionOffset = Vector3.zero;
    public Quaternion rotationOffset = Quaternion.Euler(Vector3.zero);

    public void FixedUpdate() {
        Vector3 position = Vector3.zero;
        Vector3 rotation = Vector3.zero;

        float tx = !this.lockXPosition ? this.target.position.x : 0.0f;
        float ty = !this.lockYPosition ? this.target.position.y : 0.0f;
        float tz = !this.lockZPosition ? this.target.position.z : 0.0f;

        switch(this.view) {
            case(View.TOP): {
                position = new Vector3(tx, ty + this.distance, tz);
                rotation = new Vector3(90.0f, 0.0f, 0.0f);
                break;

            } case(View.BOTTOM): {
                position = new Vector3(tx, ty + -this.distance, tz);
                rotation = new Vector3(-90.0f, 0.0f, 0.0f);
                break;
                
            } case(View.FRONT): {
                position = new Vector3(tx, ty, tz + this.distance);
                rotation = new Vector3(180.0f, 0.0f, 180.0f);
                break;
                
            } case(View.BACK): {
                position = new Vector3(tx, ty, tz + -this.distance);
                rotation = new Vector3(0.0f, 0.0f, 0.0f);
                break;
                
            } case(View.LEFT): {
                position = new Vector3(tx + this.distance, ty, tz);
                rotation = new Vector3(0.0f, -90.0f, 0.0f);
                break;
                
            } case(View.RIGHT): {
                position = new Vector3(tx + -this.distance, ty, tz);
                rotation = new Vector3(0.0f, 90.0f, 0.0f);
                break;
                
            } default: break;
        }

        position += this.positionOffset;

        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);

        // Vector3 vectorZero = new Vector3(0, 0, 0);
        // transform.position = Vector3.SmoothDamp(transform.position, position, ref vectorZero, this.smoothTime);
        // transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(Quaternion.ToEulerAngles(transform.rotation), rotation, ref vectorZero, this.smoothTime));
    }
}
