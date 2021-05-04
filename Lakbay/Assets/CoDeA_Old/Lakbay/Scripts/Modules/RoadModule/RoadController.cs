using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.RoadModule {
    [System.Serializable]
    public class Road {
        public int length = 30;

    }

    public class RoadController : Utilities.ExtendedMonoBehaviour {
        private int _currentLength = 0;

        public TextAsset roadFile;
        public QuestionModule.SetController setController;
        public GameObject model;
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
            TextAsset roadFile = Game.modeData.stage.roadFile ? Game.modeData.stage.roadFile : this.roadFile;
            this.road = Utilities.Helper.parseYAML<Road>(roadFile.ToString());

            this.updateSet();

        }

        public void updateSet() {
            Utilities.Helper.destroyChildren(this.transform);

            this.setUpModel();
            this.setUpSet();

            this._currentLength = this.road.length;

        }

        public void setUpModel() {
            Vector3 modelSize = this.model.GetComponent<MeshRenderer>().bounds.size;
        
            for(int i = 0; i < this.road.length; i++) {
                GameObject model = Instantiate<GameObject>(this.model);
                model.transform.position += new Vector3(0.0f, 0.0f, modelSize.z * i);

                Transform back = model.transform.GetChild(3);
                Transform front = model.transform.GetChild(2);

                if(i == 0) back.gameObject.SetActive(true);
                if(i == this.road.length - 1) front.gameObject.SetActive(true);

                model.transform.SetParent(this.transform);

            }


        }

        public void setUpSet() {
            Vector3 modelSize = this.model.GetComponent<MeshRenderer>().bounds.size;

            GameObject[] itemModels = this.setController.populate(this.setController.transform);
            float roadSize = modelSize.z * this.road.length;
            float spacing = roadSize / (itemModels.Length + 1);

            for(int i = 0; i < itemModels.Length; i++) {
                GameObject itemModel = itemModels[i];
                itemModel.transform.position = Vector3.zero;

                itemModel.transform.position += new Vector3(0.0f, 0.0f, (spacing * (i + 1)) - modelSize.z);

            }

        }

    }

}
