using Scripts.RecipeScripts.Condiments;
using Scripts.RecipeScripts.Liquids;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace Scripts.RecipeScripts {
    public struct RecipeStruct {
        public float _shakeValue;
        public List<LiquidEnum> _liquids;
        public List<CondimentEnum> _condiments;

        public RecipeStruct(float shakeValue, List<LiquidEnum> liquids, List<CondimentEnum> condiment) {
            _shakeValue = shakeValue;
            _liquids = new List<LiquidEnum>(liquids);
            _condiments = new List<CondimentEnum>(condiment);
        }

        public bool Equals(RecipeStruct other, float difficultyMargin = 0) {
            bool sameShakeValue = other._shakeValue < _shakeValue + difficultyMargin && other._shakeValue > _shakeValue - difficultyMargin;
            bool sameLiquids = !_liquids.Except(other._liquids).Any();
            bool sameCondiments = !_condiments.Except(other._condiments).Any();
            return sameShakeValue && sameLiquids && sameCondiments;
        }
    }
}

