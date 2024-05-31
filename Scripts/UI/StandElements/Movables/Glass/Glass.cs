using Godot;
using Scripts.RecipeScripts;
using Scripts.UI.StandScripts;
using Scripts.RecipeScripts.Liquids;
using Scripts.RecipeScripts.Condiments;
using Scripts.UI.StandElements.Movables.Glasses;
using System.Collections.Generic;

namespace Scripts.UI.StandElements.Movables {
    public partial class Glass : Movable {

        public bool shook;
        //public List<LiquidEnum> liquids;
        public CondimentEnum condiment;

        //amount of each liquid in the bottle.
        private float alcoholValue = 0;
        private float lemonadeValue = 0;
        private const float MAX_LIQUID = 100;
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

        public void AddLiquid(LiquidEnum liquid, float flowSpeed) {
            //c'est pas swag mais pour debug plus tard c'est plus lisible que les autres solutions.
            switch (liquid) {
                case LiquidEnum.Lemonade:
                    if (alcoholValue + lemonadeValue < MAX_LIQUID) {
                        lemonadeValue += flowSpeed * (float)GetProcessDeltaTime();
                        lemonadeValue = Mathf.Clamp(lemonadeValue, 0, MAX_LIQUID);
                    }
                    break;
                case LiquidEnum.Alcohol:
                    if (alcoholValue + lemonadeValue < MAX_LIQUID) {
                        alcoholValue += flowSpeed * (float)GetProcessDeltaTime();
                        alcoholValue = Mathf.Clamp(alcoholValue, 0, MAX_LIQUID);
                    }
                    break;
            }
        }

        public override void PickedUpState(double delta) {
            base.PickedUpState(delta);
        }

        public override void DroppedState(double delta) {
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
            return new RecipeStruct(shook, alcoholValue, lemonadeValue, condiment);
        }
    }
}

