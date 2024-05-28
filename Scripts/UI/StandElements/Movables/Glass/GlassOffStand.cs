
namespace Scripts.UI.StandElements.Movables.Glasses {
    public class GlassOffStand : GlassState {

        public GlassOffStand(Glass parentGlass) : base(parentGlass) {

        }

        public override void PickUp() {

        }

        public override void Drop() {
            if (HasUranium()) {
                //spawn the correct attack
            }
        }
    }
}

