using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CoDe_A.Lakbay.Modules.GameModule;
using System.Linq;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.RoadModule {
    [System.Serializable]
    public class Road {
        public int length = 30;
        public int additionalStartingLength = 5;
        public int additionalEndingLength = 5;

    }

    public class RoadController : MonoBehaviour {
        private int _currentLength = 0;

        public TextAsset roadFile;
        // public QuestionModule.SetController setController;
        public GameObject model;
        [HideInInspector]
        public float modelSize = 0.0f;
        [HideInInspector]
        public GameObject startingLineModel;
        [HideInInspector]
        public GameObject finishLineModel;

        public GameObject particleHolder;

        public PlayerModule.PlayerController playerController;
        public List<SpawnerModule.SpawnerController> spawnerControllers = new List<SpawnerModule.SpawnerController>();

        public Road road;

        private void Start() {
            // this.sizeModel = this.model.transform.Find("Road").gameObject;
            if(GameController.currentModeData != null) this.setUpRoad(
                // GameController.linearPlayStage.roadFile
                GameController.currentLinearPlayLevel.roadFile
            );
            else if(this.roadFile) this.setUpRoad(this.roadFile);

        }

        private void Update() {
            if(this.road.length != this._currentLength) {
                this.setUpRoad(this.road);

            }

        }
        
        public void setUp() {
            // TextAsset roadFile = Game.modeData.stage.roadFile ? Game.modeData.stage.roadFile : this.roadFile;
            if(this.roadFile) {
                this.road = Utilities.Helper.parseYAML<Road>(roadFile.ToString());

            }

            this.updateSet();

        }

        public void updateSet() {
            Utilities.Helper.destroyChildren(this.transform);

            this.populate();
            // this.setUpSet();

            this._currentLength = this.road.length;

        }

        public void populate() {
            Utilities.Helper.destroyChildren(this.transform);

            Vector3 modelSize = Vector3.zero;
            List<GameObject> models = new List<GameObject>();

            int additionalRoadLength = this.road.additionalStartingLength + this.road.additionalEndingLength;
            int levelNumber = GameController.currentModeData.linearPlayData.levels.IndexOf(GameController.currentLinearPlayLevel) + 1;

            this.modelSize = Utilities.Helper.getChildren(
                this.model.transform.Find("Roads").transform
            ).Select<Transform, float>(
                (t) => t.GetComponent<MeshRenderer>().bounds.size.z
            ).Sum();

            for(int i = 0; i < this.road.length + additionalRoadLength; i++) {
                GameObject model = Instantiate<GameObject>(this.model);

                model.transform.position += new Vector3(0.0f, 0.0f, this.modelSize * i);
                model.transform.SetParent(this.transform);
                
                foreach(Transform c in Utilities.Helper.getChildren(model.transform.Find("Terrains"))) {
                    if(c.name.Equals($"{levelNumber}")) c.gameObject.SetActive(true);
                    else c.gameObject.SetActive(false);

                }

                models.Add(model);

            }

            GameObject front = models[models.Count - 1].transform.Find("Blocks").Find("Front").gameObject;
            GameObject back = models[0].transform.Find("Blocks").Find("Back").gameObject;

            this.finishLineModel = front;
            this.startingLineModel = back;
            this.spawnerControllers = Utilities.Helper.getChildren(
                models[models.Count - 1].transform.Find("Spawners").gameObject
            ).Select<GameObject, SpawnerModule.SpawnerController>(
                (go) => go.GetComponent<SpawnerModule.SpawnerController>()
            ).ToList();

            front.SetActive(true);
            back.SetActive(true);

            if(this.spawnerControllers.Count != 0) this.spawnerControllers[0].transform.parent.gameObject.SetActive(true);

            foreach(SpawnerModule.SpawnerController sc in this.spawnerControllers) {
                sc.targetPosition = back.transform.position;
                sc.playerController = this.playerController;
                sc.particleHolder = this.particleHolder;
                sc.startSpawning();

            }

            this._currentLength = this.road.length;

        }

        public void setUpRoad(TextAsset roadFile) {
            this.setUpRoad(Utilities.Helper.parseYAML<Road>(roadFile.text));

        }

        public void setUpRoad(Road road) {
            this.road = road;
            this.populate();

        }

        public void setUpSet() {
            // Vector3 modelSize = this.model.GetComponent<MeshRenderer>().bounds.size;

            // GameObject[] itemModels = this.setController.populate(this.setController.transform);
            // float roadSize = modelSize.z * this.road.length;
            // float spacing = roadSize / (itemModels.Length + 1);

            // for(int i = 0; i < itemModels.Length; i++) {
            //     GameObject itemModel = itemModels[i];
            //     itemModel.transform.position = Vector3.zero;

            //     itemModel.transform.position += new Vector3(0.0f, 0.0f, (spacing * (i + 1)) - modelSize.z);

            // }

        }

    }

}
