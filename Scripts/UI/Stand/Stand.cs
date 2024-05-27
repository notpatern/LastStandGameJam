using Godot;

namespace Scripts.UI.StandScripts {
    public partial class Stand : Node2D {

        [Export] private int startHealth;
        public int health;
        public int money = 0;

        public override void _Ready() {
            health = startHealth;
        }
    }
}
