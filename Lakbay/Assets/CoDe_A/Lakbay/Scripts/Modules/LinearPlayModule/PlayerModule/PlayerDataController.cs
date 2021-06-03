using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.PlayerModule {
    public class PlayerDataController : MonoBehaviour {
        public static float coin = 0.0f;
        public static float life = 0.0f;
        public static float hint = 0.0f;

        public static List<Tuple<int, int>> levelScores = new List<Tuple<int, int>>{null, null, null};
        public static int totalScore => levelScores.Select((x) => x.Item1).Sum();
        public static int totalMaxScore => levelScores.Select((x) => x.Item2).Sum();
        public static float totalTime = 0.0f;
        public static bool passed => totalScore >= (totalMaxScore * 0.80f);
        
        public PlayerController playerController;

        private void Awake() {
            if(!playerController) playerController = this.GetComponent<PlayerController>();

        }

        private void Update() {
            PlayerController pc = this.playerController;

            coin = pc.player.coin;
            life = pc.player.life;
            hint = pc.player.hint;

            int index = GameModule.GameController.currentLinearPlayLevelIndex;
            if(index != -1) {
                levelScores[index] = new Tuple<int, int>(pc.setController.score, pc.setController.maxScore);

            }

            totalTime = pc.timer.time;

        }

    }

}
