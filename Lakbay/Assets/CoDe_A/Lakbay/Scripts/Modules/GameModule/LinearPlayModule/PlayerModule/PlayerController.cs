using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.GameModule.LinearPlayModule.PlayerModule {
    public interface IPowerUp {
        void activate(Player player);
        void deactivate(Player player);
        void buff(Player player);

    }

    [System.Serializable]
    public abstract class PowerUp : IPowerUp {
        public bool hasBuff => duration > 0.0f;

        public bool stackable = false;
        public float duration = 0.0f;

        public virtual void activate(Player player) {
            if(hasBuff) player.addBuff(this);

        }

        public virtual void deactivate(Player player) {}

        public virtual void buff(Player player) {}

    }

    [System.Serializable]
    public class ShieldPowerUp : PowerUp {
        public new float duration = 4.0f;

        public override void activate(Player player) {
            base.activate(player);

            player.invulnerable = true;

        }

        public override void deactivate(Player player) {
            player.invulnerable = false;

        }

        public override void buff(Player player) {
            player.invulnerable = false;

        }
        
    }

    [System.Serializable]
    public class SpeedBoosterPowerUp : PowerUp {

    }

    [System.Serializable]
    public class Player {
        public bool invulnerable = false;
        private List<PowerUp> _buffs = new List<PowerUp>();
        public List<PowerUp> buffs => Helper.clone(_buffs, true);

        public void addBuff(PowerUp powerUp) {
            if(!powerUp.stackable) {
                if(!_buffs.Contains(powerUp)) {
                    _buffs.Add(powerUp);

                }

            } else _buffs.Add(powerUp);

        }

        public void removeBuff(PowerUp powerUp) {
            if(!_buffs.Contains(powerUp)) return;

            _buffs.Remove(powerUp);

        }

    }

    public class PlayerController : CoreModule.Controller {


    }

}
