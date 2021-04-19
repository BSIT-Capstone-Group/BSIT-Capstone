using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hajono.Lakbay.Modules.ViewCamera {

    public class ViewCameraController : Utilities.ExtendedMonoBehaviour {
        public enum Mode {
            TOP, BOTTOM,
            FRONT, BACK,
            LEFT, RIGHT

        }

        public Transform target;
        public Mode view = Mode.TOP;
        public float distance = 25.0f;
        public float smoothTime = 0.50f;

        public bool lockXPosition = false;
        public bool lockYPosition = false;
        public bool lockZPosition = false;
        public Vector3 positionOffset = Vector3.zero;
        public Vector3 rotationOffset = Vector3.zero;

        public void FixedUpdate() {
            this.update();
            
        }

        public void update() {
            Vector3 position = Vector3.zero;
            Vector3 rotation = Vector3.zero;

            float tx = !this.lockXPosition ? this.target.position.x : 0.0f;
            float ty = !this.lockYPosition ? this.target.position.y : 0.0f;
            float tz = !this.lockZPosition ? this.target.position.z : 0.0f;

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
                    
                } default: break;
            }

            position += this.positionOffset;
            rotation += this.rotationOffset;

            Vector3 pos = Vector3.zero;
            // this.transform.position = position;
            // this.transform.position = Vector3.Lerp(this.transform.position, position, this.smoothTime * Time.deltaTime);
            this.transform.position = Vector3.SmoothDamp(this.transform.position, position, ref pos, this.smoothTime * Time.fixedDeltaTime);
            this.transform.rotation = Quaternion.Euler(rotation);

        }

    }

}
