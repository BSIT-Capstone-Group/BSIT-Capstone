using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.GameModule {
    [CreateAssetMenu(fileName="Mode", menuName="Lakbay/Mode/New", order=1)]
    public class Mode : ScriptableObject {
        public LocalizedAsset<TextAsset> playerFile;
        public LocalizedAsset<TextAsset> vehicleFile;

        public LinearPlayModule.PlayerModule.Player player;
        public VehicleModule.Vehicle vehicle;

        public LinearPlay linearPlay;

    }

    [CreateAssetMenu(fileName="Linear Play", menuName="Lakbay/Mode/Play/Linear/New", order=1)]
    public class LinearPlay : ScriptableObject {
        [CreateAssetMenu(fileName="Level", menuName="Lakbay/Mode/Play/Linear/Level", order=1)]
        public class Level : ScriptableObject {
            public LocalizedAsset<TextAsset> roadFile;
            public LocalizedAsset<TextAsset> setFile;

            public LinearPlayModule.RoadModule.Road road;
            public LinearPlayModule.QuestionModule.Set set;

        }

        public List<Level> levels;

    }

    [CreateAssetMenu(fileName="Free-Roam Play", menuName="Lakbay/Mode/Play/Free-Roam/New", order=1)]
    public class FreeRoamPlay : ScriptableObject {
        public LocalizedAsset<TextAsset> playerFile;
        public LocalizedAsset<TextAsset> vehicleFile;

    }

}