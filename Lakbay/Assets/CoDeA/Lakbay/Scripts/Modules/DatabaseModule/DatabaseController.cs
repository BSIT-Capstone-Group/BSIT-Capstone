using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using UnityEngine;
using CoDeA.Lakbay.Modules.LinearPlayModule;

namespace CoDeA.Lakbay.Modules.DatabaseModule {
    [System.Serializable]
    public class Mode {
        public string name = "";

        public LinearPlay linearPlay = null;

    }

    [System.Serializable]
    public class Play {
        // public string name = "";

    }

    [System.Serializable]
    public class LinearPlay : Play {
        [System.Serializable]
        public class Stage {
            // public string name = "";

            public LinearPlayModule.RoadModule.Road road;
            public LinearPlayModule.QuestionModule.Set set;

        }

        // public new string name = "Linear Play";

        public LinearPlayModule.PlayerModule.Player player = null;
        public VehicleModule.Vehicle vehicle = null;
        public List<Stage> stages = new List<Stage>();

    }

    public class DatabaseController : MonoBehaviour {
        public static Mode nonProMode = null;
        public static Mode proMode = null;

        private void Start() {
            DatabaseController.loadNonProMode();
            DatabaseController.loadProMode();

        }

        public static Mode loadMode(string modeName) {
            LinearPlayModule.PlayerModule.Player player = null;
            player = Utilities.Helper.parseYAML<LinearPlayModule.PlayerModule.Player>(
                Resources.Load<TextAsset>(
                    $"Text Files/Modes/{modeName}/Linear Play/player"
                ).text
            );

            VehicleModule.Vehicle vehicle = null;
            vehicle = Utilities.Helper.parseYAML<VehicleModule.Vehicle>(
                Resources.Load<TextAsset>(
                    $"Text Files/Modes/{modeName}/Linear Play/vehicle"
                ).text
            );

            List<LinearPlay.Stage> stages = new List<LinearPlay.Stage>();

            for(int i = 1; i <= 3; i++) {
                stages.Add(
                    DatabaseController.loadLinearPlayStage(modeName, i)
                );

            }

            LinearPlay linearPlay = new LinearPlay();
            linearPlay.player = player;
            linearPlay.stages = stages;
            linearPlay.vehicle = vehicle;

            Mode mode = new Mode();
            mode.name = $"{modeName}";
            mode.linearPlay = linearPlay;

            return mode;

        }

        public static LinearPlay.Stage loadLinearPlayStage(string modeName, int stageNumber) {
            LinearPlay.Stage stage = new LinearPlay.Stage();
            stage.road = Utilities.Helper.parseYAML<LinearPlayModule.RoadModule.Road>(
                Resources.Load<TextAsset>(
                    $"Text Files/Modes/{modeName}/Linear Play/Stages/{stageNumber}/road"
                ).text
            );
            stage.set = Utilities.Helper.parseYAML<LinearPlayModule.QuestionModule.Set>(
                Resources.Load<TextAsset>(
                    $"Text Files/Modes/{modeName}/Linear Play/Stages/{stageNumber}/set"
                ).text
            );

            return stage;

        }

        public static void loadNonProMode() {
            DatabaseController.nonProMode = DatabaseController.loadMode("Non-Pro");

        }

        public static void loadProMode() {
            DatabaseController.proMode = DatabaseController.loadMode("Pro");

        }

    }

}
