using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.SpawnerModule {
    public interface ISpawn {
        void move(Vector3 position);
        void getHit(Vector3 position);

    }

    public class SpawnController : MonoBehaviour, ISpawn {
        public SpawnerController spawnerController;
        public PlayerModule.PlayerController playerController;
        public UnityEvent<SpawnController, Vector3> onHit = new UnityEvent<SpawnController, Vector3>();

        private void OnTriggerEnter(Collider collider) {
            if(this.playerController.transform == collider.transform) {
                this.getHit(this.transform.position);

            }

        }
        
        public void move(Vector3 position) {
            this.transform.position = Vector3.Lerp(this.transform.position, position, 2.0f);

        }

        public void getHit(Vector3 position) {
            this.onHit.Invoke(this, position);

        }

    }

}
