using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.SpawnerModule {
    public class SpawnerController : MonoBehaviour {
        private Coroutine _repeatedlySpawnCoroutine = null;

        public PlayerModule.PlayerController playerController;
        public Vector3 targetPosition = Vector3.zero;
        public GameObject particleHolder;
        public ParticleSystem despawnParticle;
        // public 
        public bool paused = false;
        public float chance = 0.25f;
        public List<float> timeDifferences = new List<float>() {2.0f};
        public List<SpawnController> spawns = new List<SpawnController>();

        public List<SpawnController> currentSpawns = new List<SpawnController>();

        private void Start() {
            // this.startSpawning();

        }

        private void FixedUpdate() {
            this.moveSpawns();

        }

        public void spawn(SpawnController spawn) {
            GameObject go = Instantiate<GameObject>(spawn.gameObject, this.transform);
            SpawnController sc = go.GetComponent<SpawnController>();
            // sc.transform.rotation = this.transform.rotation;
            sc.spawnerController = this;
            sc.playerController = this.playerController;
            this.currentSpawns.Add(sc);

        }

        public void moveSpawns() {
            foreach(SpawnController spawn in this.currentSpawns) {
                spawn.move(spawn.transform.position - (Vector3.forward * 2.0f * Time.fixedDeltaTime));

                // if(spawn.transform.position == this.)

            }

        }

        public void despawn(SpawnController spawn) {
            this.currentSpawns.Remove(spawn);
            Destroy(spawn.gameObject);
            print($"despawn: {spawn.name}");

        }

        public void playParticle(SpawnController spawn) {
            GameObject go = Instantiate<GameObject>(this.despawnParticle.gameObject, this.particleHolder.transform);
            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            go.transform.position = spawn.transform.position;
            go.transform.rotation = spawn.transform.rotation;
            ps.Play();
            Destroy(go.gameObject, ps.main.duration);

        }

        public IEnumerator repeatedlySpawn() {
            while(true) {
                yield return new WaitForSeconds(
                    Utilities.Helper.pickRandom<float>(this.timeDifferences.ToArray())
                );

                if(!this.paused) {
                    SpawnController spawn = Utilities.Helper.pickRandom<SpawnController>(this.spawns.ToArray());

                    if(Random.value <= this.chance) this.spawn(spawn);

                }

            }

        }

        public void startSpawning() {
            if(this._repeatedlySpawnCoroutine != null) return;

            this.resumeSpawning();

            this._repeatedlySpawnCoroutine = StartCoroutine(this.repeatedlySpawn());

        }

        public void pauseSpawning() {
            this.paused = true;

            foreach(SpawnController spawn in this.currentSpawns) {
                spawn.GetComponent<Rigidbody>().isKinematic = true;

            }

        }

        public void resumeSpawning() {
            this.paused = false;

            foreach(SpawnController spawn in this.currentSpawns) {
                spawn.GetComponent<Rigidbody>().isKinematic = false;

            }

        }

        public void stopSpawning() {
            if(this._repeatedlySpawnCoroutine == null) return;

            StopCoroutine(this._repeatedlySpawnCoroutine);

            this.pauseSpawning();

            List<SpawnController> spawns = this.currentSpawns.ToList();
            foreach(SpawnController spawn in spawns) {
                this.playParticle(spawn);
                this.despawn(spawn);

            }

        }

    }

}
