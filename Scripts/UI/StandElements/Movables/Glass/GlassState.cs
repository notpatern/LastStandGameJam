
namespace Scripts.UI.StandElements.Movables.Glasses {
    public abstract class GlassState{

        public Glass _parentGlass;

        public GlassState(Glass parentGlass) {
            _parentGlass = parentGlass;
        }

        public virtual void CheckInStandArea() {

        }
        public virtual void PickUp() {

        }
        public virtual void Drop() {

        }
    }
}
