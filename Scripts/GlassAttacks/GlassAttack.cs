using Godot;
using Scripts.RecipeScripts;
using Scripts.ZombScripts;
using Scripts.GlassAttacks.Effects;

namespace Scripts.GlassAttacks {
    public abstract partial class GlassAttack : Node2D {
        [Export] protected AnimatedSprite2D gfx;
        [Export] protected Area2D hitBox;
        [Export] protected float damage;

        public RecipeStruct recipe;
        private iGlassAttackEffect effect;

        public override void _Ready() {
            gfx.Play();
        }

        private void DealDamage(Zomb zomb) {
            //zomb.health -= damage;
        }

        private void SetEffect() {

        }
    }
}

