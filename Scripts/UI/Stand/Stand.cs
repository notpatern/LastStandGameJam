using Godot;
using System;

namespace Scripts.UI.StandScripts {
    public struct StandInfo {

        public int lastWaveSinceHitOnStand;
        public float playerZombKillEfficiency;
        public float playerBarmanEfficiency;
        public int numCustomersServed;
        public float customerRatio;
        public int standHealth;
        public int standMaxHealth;
        public int numAltDrinksPrepared;

        public StandInfo(int lastWaveSinceHitOnStand, float playerZombKillEfficiency, float playerBarmanEfficiency, int numCustomersServed, float customerRatio, int standHealth, int standMaxHealth, int numAltDrinksPrepared) {
            this.lastWaveSinceHitOnStand = lastWaveSinceHitOnStand;
            this.playerZombKillEfficiency = playerZombKillEfficiency;
            this.playerBarmanEfficiency = playerBarmanEfficiency;
            this.numCustomersServed = numCustomersServed;
            this.customerRatio = customerRatio;
            this.standHealth = standHealth;
            this.standMaxHealth = standMaxHealth;   
            this.numAltDrinksPrepared = numAltDrinksPrepared;
        }
    }

    public partial class Stand : Node2D {

        [Export] private int startHealth;
        private int health;
        private int money = 0;
        public Area2D standArea;

        private int lastWaveSinceHitOnStand;
        private float playerZombKillEfficiency;
        private float playerBarmanEfficiency;
        private int numCustomersServed;
        private float customerRatio;
        private int standHealth;
        private int standMaxHealth;
        private int numAltDrinksPrepared;

        private Action<StandInfo> GetStandInfo;

        public override void _Ready() {
            health = startHealth;
        }

        public void Start() {
            GetStandInfo(GetStandInfoStruct());
        }

        public void BindGetStandInfo(Action<StandInfo> action) { 
            GetStandInfo += action;
        }

        public void GetCustomerInfo(float ratio) {
            customerRatio = ratio;
        }


        private StandInfo GetStandInfoStruct() {
            return new StandInfo(lastWaveSinceHitOnStand, playerZombKillEfficiency, playerBarmanEfficiency, numCustomersServed, customerRatio, standHealth, standMaxHealth, numAltDrinksPrepared);
        }
    }
}
