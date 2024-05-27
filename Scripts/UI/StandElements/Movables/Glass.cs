using Godot;
using Scripts.UI.StandElements.Usable;
using Scripts.RecipeScripts.Liquids;
using Scripts.RecipeScripts.Condiments;
using System.Collections.Generic;
using System;
using Scripts.RecipeScripts;

namespace Scripts.UI.StandElements.Movables {
    public partial class Glass : Movable {
        public float shakeValue;
        public List<LiquidEnum> liquids;
        public List<CondimentEnum> condiments;
        private iGlassState stateGlass;

        public RecipeStruct GetRecipeFromContents() {
            return new RecipeStruct(shakeValue, liquids, condiments);
        }
    }
}

