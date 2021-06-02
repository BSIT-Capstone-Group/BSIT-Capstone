using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using CoDe_A.Lakbay.Modules.GameModule;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.QuestionModule {
    [System.Serializable]
    public class Set {
        public string name = "";

        public List<Item> items = new List<Item>();

        public Dictionary<IEnumerable<int>, List<string>> scoring = new Dictionary<IEnumerable<int>, List<string>>();

        public int score => this.items.FindAll((i) => i.answeredCorrectly).Count;
        public int maxScore => this.items.Count;

    }

    public class SetController : MonoBehaviour {
        private int _currentItemsCount = 0;

        [HideInInspector]
        public ItemController currentItemController = null;

        public bool shuffledItems = true;
        public bool shuffledItemChoices = true;

        public TextAsset setFile;
        public Set set;

        public GameObject itemModel;

        // public List<GameObject> triggerAgents = new List<GameObject>();
        public PlayerModule.PlayerController playerController;

        public UIModule.UIController uiController;
        public RoadModule.RoadController roadController;

        public List<ItemController> itemControllers = new List<ItemController>();

        public int score {
            get => this.itemControllers.Select<ItemController, int>(
                (ic) => ic.answeredCorrectly ? 1 : 0
            ).Sum();
        }

        public int maxScore { get => this.itemControllers.Count; }

        public string scoreDescription {
            get {
                return Utilities.Helper.pickRandom<string>(this.set.scoring[this.scoringKey].ToArray());

            }
        }

        public IEnumerable<int> scoringKey => this.set.scoring.Keys.ToList().Find((e) => e.Contains(score));

        public int star {
            get {
                var o = this.set.scoring.OrderBy((kvp) => kvp.Key.ToList().Last());
                var os = o.ToList();
                foreach(var o_ in os) {
                    if(o_.Key.Equals(this.scoringKey)) return os.IndexOf(o_);

                }

                return 0;

            }
        }

        private void Start() {
            if(GameController.currentModeData != null) this.setUpSet(
                GameController.currentLinearPlayLevel.setFile
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
            if(rc.modelSize == 0.0f) return;

            this.itemControllers.Clear();
            Utilities.Helper.destroyChildren(this.transform);

            // GameObject[] itemModels = this.setController.populate(this.setController.transform);
            float roadSize = rc.modelSize * rc.road.length;
            float spacing = roadSize / (this.set.items.Count + 1);
            float offsetSpacing = rc.modelSize * this.roadController.road.additionalStartingLength;

            for(int i = 0; i < this.set.items.Count; i++) {
                Item item = this.set.items[i];
                GameObject itemModel = Instantiate<GameObject>(this.itemModel, this.transform);
                // itemModel.transform.position = Vector3.zero;

                itemModel.transform.position += new Vector3(0.0f, 0.0f, ((spacing * (i + 1)) - rc.modelSize) + offsetSpacing);

                ItemController ic = itemModel.AddComponent<ItemController>();

                ic.onAnswer.AddListener(this.playerController.onItemAnswer);

                ic.setController = this;
                ic.playerController = this.playerController;
                ic.uiController = this.uiController;
                ic.shuffledChoices = this.shuffledItemChoices;
                ic.setUpItem(item);

                this.itemControllers.Add(ic);

            }

            this._currentItemsCount = this.set.items.Count;

        }

        public void setUpSet(TextAsset setFile) {
            this.setUpSet(Utilities.Helper.parseYAML<Set>(setFile.text));

        }

        public void setUpSet(Set set) {
            if(this.shuffledItems) {
                set.items = Utilities.Helper.shuffle<Item>(set.items.ToArray()).ToList();

            }

            this.set = set;
            this.populate();

        }

    }

}
