using Godot;
using Scripts.RecipeScripts;
using Scripts.ZombScripts;
using Scripts.GlassAttacks.Effects;
using Scripts.RecipeScripts.Condiments;

namespace Scripts.GlassAttacks {
    public abstract partial class GlassAttack : Node2D {

        [Export] protected AnimatedSprite2D gfx;
        [Export] protected Area2D hitBox;
        [Export] protected float damage;
        [Export] protected float duration = 0;

        public RecipeStruct recipe;
        private iGlassAttackEffect effect;

        public override void _Ready() {
            // AddChild(timer);
            hitBox.Connect("area_entered", new Callable(this, nameof(OnHit)));
            SetEffect();
            gfx.Play();
        }

        protected virtual void OnHit(Area2D area) {
            if (!(area.GetParent() is Zomb)) { 
                return;
            }
        }

        protected virtual void DealDamage(Zomb zomb) {
            effect.ApplyEffect(zomb);
            //zomb.health -= damage;
        }

        protected void SetEffect() {
            switch (recipe._condiment) {
                case CondimentEnum.Ice:
                    effect = new GlassEffectSlow();
                    break;
                case CondimentEnum.Lemon:
                    effect = new GlassEffectAcid();
                    break;
                case CondimentEnum.GrapeFruit:
                    effect = new GlassEffectExplosion();
                    break;
            }
        }
    }
}

