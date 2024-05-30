using Godot;
using Scripts.GlassAttacks;
using Scripts.ZombScripts;

namespace Scripts.GlassAttacks {
    public partial class GlassAttackConcentrated : GlassAttack {

        [Export] private float damageMultiplier = 2f;

        public override void _Ready() {
            base._Ready();
            damage = damage * damageMultiplier;
        }
        protected override void OnHit(Area2D area) {
            base.OnHit(area); //checks if area;GetParent() is Zomb.
            Zomb zomb = (Zomb)area.GetParent();
            DealDamage(zomb);
        }
    }
}
