using Scripts.RecipeScripts.Condiments;
using Scripts.RecipeScripts.Liquids;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace Scripts.RecipeScripts {
    public struct RecipeStruct {
        public bool _shook;
        public List<LiquidEnum> _liquids;
        public CondimentEnum _condiment;

        public RecipeStruct(bool shook, List<LiquidEnum> liquids, CondimentEnum condiment) {
            _shook = shook;
            _liquids = new List<LiquidEnum>(liquids);
            _condiment = condiment;
        }

        public bool Equals(RecipeStruct other) {
            bool sameShakeValue = _shook == other._shook ;
            bool sameLiquids = !_liquids.Except(other._liquids).Any();
            bool sameCondiment = _condiment == other._condiment ;
            return sameShakeValue && sameLiquids && sameCondiment;
        }
    }
}

