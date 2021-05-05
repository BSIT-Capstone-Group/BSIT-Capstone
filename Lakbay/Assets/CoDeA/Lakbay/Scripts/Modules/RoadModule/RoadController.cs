using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.RoadModule {
    [System.Serializable]
    public class Road {
        public int length = 30;

    }

    public class RoadController : MonoBehaviour {
        private int _currentLength = 0;

        public TextAsset roadFile;
        // public QuestionModule.SetController setController;
        public GameObject model;
        [HideInInspector]
        public GameObject sizeModel;
        [HideInInspector]
        public GameObject startingLineModel;
        [HideInInspector]
        public GameObject finishLineModel;
        public Road road;

        private void Start() {
            this.setUp();

        }

        private void FixedUpdate() {
            if(this.road.length != this._currentLength) {
                this.updateSet();

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

            this.setUpModel();
            this.setUpSet();

            this._currentLength = this.road.length;

        }

        public void setUpModel() {
            // Vector3 modelSize = this.sizeModel.GetComponent<MeshRenderer>().bounds.size;

            // this.sizeModel = this.transform.Find("Road").gameObject;
            // this.startingLineModel = this.sizeModel.transform.Find("Back").gameObject;
            // this.finishLineModel = this.sizeModel.transform.Find("Front").gameObject;

            Vector3 modelSize = Vector3.zero;
            List<GameObject> models = new List<GameObject>();
            for(int i = 0; i < this.road.length; i++) {
                GameObject model = Instantiate<GameObject>(this.model);

                if(i == 0) {
                    this.sizeModel = model.transform.Find("Road").gameObject;
                    modelSize = this.sizeModel.GetComponent<MeshRenderer>().bounds.size;
                    
                }

                model.transform.position += new Vector3(0.0f, 0.0f, modelSize.z * i);
                model.transform.SetParent(this.transform);

                models.Add(model);

            }

            GameObject front = models[models.Count - 1].transform.Find("Road").Find("Front").gameObject;
            GameObject back = models[0].transform.Find("Road").Find("Back").gameObject;

            front.SetActive(true);
            back.SetActive(true);

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
