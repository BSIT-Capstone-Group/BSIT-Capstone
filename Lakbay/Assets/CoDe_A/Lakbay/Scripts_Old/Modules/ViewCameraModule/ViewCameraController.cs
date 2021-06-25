using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace CoDe_A_Old.Lakbay.Modules.ViewCameraModule {
    [ExecuteInEditMode]
    public class ViewCameraController : MonoBehaviour {
        public enum Mode {
            TOP, BOTTOM,
            FRONT, BACK,
            LEFT, RIGHT

        }

        private bool _started = false;

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

        private void Start() {
            _started = true;

        }

        public void FixedUpdate() {
            this.update();
            
        }

        private void Update() {
            if(!_started) update();

        }

        public void update() {
            Vector3 position = Vector3.zero;
            Vector3 rotation = Vector3.zero;

            float tx = this.target.position.x;
            float ty = this.target.position.y;
            float tz = this.target.position.z;

            if(!this.lockXPosition) this._lastXPosition = this.transform.position.x;
            if(!this.lockYPosition) this._lastYPosition = this.transform.position.y;
            if(!this.lockZPosition) this._lastZPosition = this.transform.position.z;

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

            if(this.lockXPosition) position.x = this._lastXPosition;
            if(this.lockYPosition) position.y = this._lastYPosition;
            if(this.lockZPosition) position.z = this._lastZPosition;

            this.move(position);
            this.transform.rotation = Quaternion.Euler(rotation);

            this.onUpdate.Invoke(this, this.transform.position, rotation);

        }

        public void move(Vector3 position, float smoothTime) {
            Vector3 pos = Vector3.zero;
            // this.transform.position = Vector3.SmoothDamp(this.transform.position, position, ref pos, smoothTime);
            this.transform.position = Vector3.Lerp(this.transform.position, position, 10.0f * Time.fixedDeltaTime);

        }

        public void move(Vector3 position) {
            this.move(position, this.smoothTime * Time.fixedDeltaTime);

        }

    }

}
