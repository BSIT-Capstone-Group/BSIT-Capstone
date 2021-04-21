using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.Question {
    public class SetController : Utilities.ExtendedMonoBehaviour {
        public bool shuffledItems = true;
        public bool shuffledItemChoices = true;
        public TextAsset setFile;
        public GameObject itemModel;

        public Player.PlayerController playerController;
        public Vehicle.VehicleController vehicleController;
        public UI.UIController uIController;
        public List<GameObject> triggerAgents = new List<GameObject>();
        public List<ItemController> itemControllers = new List<ItemController>();

        public float point {
            get {
                if(this.itemControllers.Count == 0.0f) return 0.0f;

                float point = 0.0f;

                foreach(ItemController ic in this.itemControllers) {
                    point += ic.answeredCorrectly ? 1 : 0;

                }

                return point;

            }

        }

        public void shuffleItems() {
            if(!this.shuffledItems) return;

            System.Random random = new System.Random();
            Question.ItemController[] aitems = Utilities.Helper.shuffle<Question.ItemController>(
                random, this.itemControllers.ToArray()
            );
            this.itemControllers.Clear();
            this.itemControllers.AddRange(new List<Question.ItemController>(aitems));

        }

        public void shuffleItemChoices() {
            if(!this.shuffledItemChoices) return;

            foreach(Question.ItemController ic in this.itemControllers) {
                ic.shuffleChoices();

            }

        }

        public GameObject[] populate(Transform parent) {
            this.itemControllers.Clear();
            List<GameObject> itemModels = new List<GameObject>();
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(this.setFile.ToString());

            foreach(Item item in items) {
                GameObject itemModel = Instantiate<GameObject>(this.itemModel);
                ItemController itemController = itemModel.AddComponent<ItemController>();

                itemController.setController = this;
                itemController.item = item;
                itemController.uiController = this.uIController;
                itemController.triggerAgents = this.triggerAgents;
                itemController.playerController = this.playerController;
                itemController.vehicleController = this.vehicleController;

                Utilities.Timer timer = itemModel.AddComponent<Utilities.Timer>();
                itemController.timer = timer;

                itemModels.Add(itemModel);
                this.itemControllers.Add(itemController);
                
            }

            this.shuffleItems();
            itemModels = this.itemControllers.ConvertAll(
                new System.Converter<ItemController, GameObject>(
                    (ItemController ic) => {
                        if(parent) ic.transform.SetParent(parent);
                        return ic.gameObject;
                    }
                )
            );
            this.shuffleItemChoices();

            return itemModels.ToArray();

        }

        public GameObject[] populate() {
            return this.populate(null);

        }

    }

}
