using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CoDeA.Lakbay.Modules.GameModule;

namespace CoDeA.Lakbay.Modules.LinearPlayModule.QuestionModule {
    [System.Serializable]
    public class Set {
        public string name = "";

        public List<Item> items = new List<Item>();

    }

    public class SetController : MonoBehaviour {
        private int _currentItemsCount = 0;

        [HideInInspector]
        public ItemController currentItemController = null;

        public TextAsset setFile;
        public Set set;

        public GameObject itemModel;

        // public List<GameObject> triggerAgents = new List<GameObject>();
        public PlayerModule.PlayerController playerController;

        public UIModule.UIController uIController;
        public RoadModule.RoadController roadController;

        public List<ItemController> itemControllers = new List<ItemController>();

        public float score {
            get => this.itemControllers.Select<ItemController, float>(
                (ic) => ic.answeredCorrectly ? 1.0f : 0.0f
            ).Sum();
        }

        public float maxScore { get => this.itemControllers.Count; }

        private void Start() {
            if(GameController.currentMode != null) this.setUpSet(
                GameController.linearPlayStage.set
            );
            else if(this.setFile) this.setUpSet(this.setFile);

        }

        private void Update() {
            if(this.set.items.Count != this._currentItemsCount) {
                this.setUpSet(this.set);

            }

        }

        public void populate() {
            RoadModule.RoadController rc = this.roadController;
            if(!rc.sizeModel) return;

            Utilities.Helper.destroyChildren(this.transform);
            Vector3 modelSize = rc.sizeModel.GetComponent<MeshRenderer>().bounds.size;

            // GameObject[] itemModels = this.setController.populate(this.setController.transform);
            float roadSize = modelSize.z * rc.road.length;
            float spacing = roadSize / (this.set.items.Count + 1);
            float offsetSpacing = modelSize.z * this.roadController.road.additionalStartingLength;

            for(int i = 0; i < this.set.items.Count; i++) {
                Item item = this.set.items[i];
                GameObject itemModel = Instantiate<GameObject>(this.itemModel, this.transform);
                // itemModel.transform.position = Vector3.zero;

                itemModel.transform.position += new Vector3(0.0f, 0.0f, ((spacing * (i + 1)) - modelSize.z) + offsetSpacing);

                ItemController ic = itemModel.AddComponent<ItemController>();

                ic.onAnswer.AddListener(this.playerController.onItemAnswer);

                ic.setController = this;
                ic.playerController = this.playerController;
                ic.uiController = this.uIController;
                ic.setUpItem(item);

            }

            this._currentItemsCount = this.set.items.Count;

        }

        public void setUpSet(TextAsset setFile) {
            this.setUpSet(Utilities.Helper.parseYAML<Set>(setFile.text));

        }

        public void setUpSet(Set set) {
            this.set = set;
            this.populate();

        }

    }

}
