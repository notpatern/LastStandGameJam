using Scripts.RecipeScripts.Condiments;
using Scripts.RecipeScripts.Liquids;
using System.Collections.Generic;

namespace Scripts.RecipeScripts {
    public struct RecipeStruct {
        public float shakeValue;
        public List<LiquidEnum> liquids;
        public List<CondimentEnum> condiments;
    }
}

