using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.ViewCameraModule {

    public class ViewCameraController : MonoBehaviour {
        public enum Mode {
            TOP, BOTTOM,
            FRONT, BACK,
            LEFT, RIGHT

        }

        private float _lastXPosition = 0.0f;
        private float _lastYPosition = 0.0f;
        private float _lastZPosition = 0.0f;

        public Transform target;
        public Mode view = Mode.TOP;
        public float distance = 25.0f;
        public float smoothTime = 0.50f;

        public bool lockXPosition = false;
        public bool lockYPosition = false;
        public bool lockZPosition = false;
        public Vector3 positionOffset = Vector3.zero;
        public Vector3 rotationOffset = Vector3.zero;

        public UnityEvent<ViewCameraController, Vector3, Vector3> onUpdate = new UnityEvent<ViewCameraController, Vector3, Vector3>();

        public void FixedUpdate() {
            this.update();
            
        }

        public void update() {
            Vector3 position = Vector3.zero;
            Vector3 rotation = Vector3.zero;

            float tx = !this.lockXPosition ? this.target.position.x : this._lastXPosition;
            float ty = !this.lockYPosition ? this.target.position.y : this._lastYPosition;
            float tz = !this.lockZPosition ? this.target.position.z : this._lastZPosition;

            if(!this.lockXPosition) this._lastXPosition = this.transform.position.x;
            if(!this.lockYPosition) this._lastYPosition = this.transform.position.y;
            if(!this.lockZPosition) {
                this._lastZPosition = this.transform.position.z;
                
            }

            switch(this.view) {
                case(Mode.TOP): {
                    position = new Vector3(tx, ty + this.distance, tz);
                    rotation = new Vector3(90.0f, 0.0f, 0.0f);
                    break;

                } case(Mode.BOTTOM): {
                    position = new Vector3(tx, ty + -this.distance, tz);
                    rotation = new Vector3(-90.0f, 0.0f, 0.0f);
                    break;
                    
                } case(Mode.FRONT): {
                    position = new Vector3(tx, ty, tz + this.distance);
                    rotation = new Vector3(180.0f, 0.0f, 180.0f);
                    break;
                    
                } case(Mode.BACK): {
                    position = new Vector3(tx, ty, tz + -this.distance);
                    rotation = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                    
                } case(Mode.LEFT): {
                    position = new Vector3(tx + this.distance, ty, tz);
                    rotation = new Vector3(0.0f, -90.0f, 0.0f);
                    break;
                    
                } case(Mode.RIGHT): {
                    position = new Vector3(tx + -this.distance, ty, tz);
                    rotation = new Vector3(0.0f, 90.0f, 0.0f);
                    break;
                    
                } default: {
                    break;

                }
            }

            position += this.positionOffset;
            rotation += this.rotationOffset;

            Vector3 pos = Vector3.zero;
            // this.transform.position = position;
            // this.transform.position = Vector3.Lerp(this.transform.position, position, this.smoothTime * Time.deltaTime);

            this.transform.position = Vector3.SmoothDamp(this.transform.position, position, ref pos, this.smoothTime * Time.fixedDeltaTime);
            this.transform.rotation = Quaternion.Euler(rotation);

            this.onUpdate.Invoke(this, this.transform.position, rotation);

        }

    }

}
