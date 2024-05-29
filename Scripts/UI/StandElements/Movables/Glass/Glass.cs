using Godot;
using Scripts.RecipeScripts;
using Scripts.UI.StandScripts;
using Scripts.RecipeScripts.Liquids;
using Scripts.RecipeScripts.Condiments;
using Scripts.UI.StandElements.Movables.Glasses;
using System.Collections.Generic;

namespace Scripts.UI.StandElements.Movables {
    public partial class Glass : Movable {

        public float shakeValue;
        public List<LiquidEnum> liquids;
        public List<CondimentEnum> condiments;

        /// <summary>
        /// State representing wether the glass is 'on' the stand (preparation of recipes)
        /// or 'off' the stand (give glass to customer or drop on zombs).
        /// </summary>
        private GlassState glassState;
        public Stand stand;

        public override void _Process(double delta) {
            base._Process(delta);
            GlassStateHandler();
        }

        protected override void PickedUpState(double delta) {
            base.PickedUpState(delta);
        }

        protected override void DroppedState(double delta) {
            base.DroppedState(delta);
        }

        public override void SetPickedUp() {
            glassState.PickUp();
        }

        public override void SetDropped() {
            glassState.Drop();
        }

        public void SetOnStand() {
            glassState = new GlassOnStand(this);
        }

        public void SetOffStand() {
            glassState = new GlassOffStand(this);
        }

        private bool IsInStandArea() {
            return body.OverlapsArea(stand.standArea);
        }

        private void GlassStateHandler() {
            if (IsInStandArea() && glassState is GlassOffStand) {
                SetOnStand();
            }
            else if (!IsInStandArea() && glassState is GlassOnStand) {
                SetOffStand();
            }
        }

        public RecipeStruct GetRecipeFromContents() {
            return new RecipeStruct(shakeValue, liquids, condiments);
        }
    }
}

