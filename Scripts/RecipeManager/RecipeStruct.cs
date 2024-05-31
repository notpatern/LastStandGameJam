using Scripts.RecipeScripts.Condiments;
using Scripts.RecipeScripts.Liquids;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace Scripts.RecipeScripts {
    public struct RecipeStruct {
        public bool _shook;
        //public List<LiquidEnum> _liquids;
        public float alcoholValue;
        public float lemonadeValue;
        public CondimentEnum _condiment;

        public RecipeStruct(bool shook, float alcoholValue, float lemonadeValue, CondimentEnum condiment) {
            _shook = shook;
            this.alcoholValue = alcoholValue;
            this.lemonadeValue = lemonadeValue;
            _condiment = condiment;
        }

        public bool Equals(RecipeStruct other, float margin) {
            bool sameShakeValue = _shook == other._shook ;
            bool sameAlcohol = alcoholValue < other.alcoholValue + margin && alcoholValue > other.alcoholValue - margin;
            bool sameLemonade = lemonadeValue < other.lemonadeValue + margin && lemonadeValue > other.lemonadeValue - margin;
            bool sameCondiment = _condiment == other._condiment ;
            return sameShakeValue && sameAlcohol && sameLemonade && sameCondiment;
        }
    }
}

