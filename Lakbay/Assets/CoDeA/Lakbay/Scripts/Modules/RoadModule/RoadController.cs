using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.RoadModule {
    public class RoadController : Utilities.ExtendedMonoBehaviour {
        private int _currentLength = 0;

        public QuestionModule.SetController setController;
        public GameObject model;
        public int length = 20;

        private void Start() {
            this.setUp();

        }

        private void FixedUpdate() {
            this.setUp();

        }
        
        public void setUp() {
            if(this.length != this._currentLength) {
                Utilities.Helper.destroyChildren(this.transform);

                this.setUpModel();
                this.setUpSet();

                this._currentLength = this.length;

            }

        }

        public void setUpModel() {
            Vector3 modelSize = this.model.GetComponent<MeshRenderer>().bounds.size;
        
            for(int i = 0; i < this.length; i++) {
                GameObject model = Instantiate<GameObject>(this.model);
                model.transform.position += new Vector3(0.0f, 0.0f, modelSize.z * i);

                model.transform.SetParent(this.transform);

            }


        }

        public void setUpSet() {
            Vector3 modelSize = this.model.GetComponent<MeshRenderer>().bounds.size;

            GameObject[] itemModels = this.setController.populate(this.transform);
            float roadSize = modelSize.z * this.length;
            float spacing = roadSize / (itemModels.Length + 1);

            for(int i = 0; i < itemModels.Length; i++) {
                GameObject itemModel = itemModels[i];
                itemModel.transform.position = Vector3.zero;

                itemModel.transform.position += new Vector3(0.0f, 0.0f, (spacing * (i + 1)) - modelSize.z);

            }

        }

    }

}
