using Godot;
using Scripts.ZombScripts;
namespace Scripts.GlassAttacks {
    public abstract partial class GlassAttack : Node2D {
        [Export] protected AnimatedSprite2D gfx;
        [Export] protected Area2D hitBox;
        [Export] protected float damage;

        public override void _Ready() {
            gfx.Play();
        }

        private void DealDamage(Zomb zomb) {
            //zomb.health -= damage;
        }
    }
}

