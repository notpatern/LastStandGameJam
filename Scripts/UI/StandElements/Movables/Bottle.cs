using Godot;
using Scripts.RecipeScripts.Liquids;
using Scripts.UI.StandElements.Helper;

namespace Scripts.UI.StandElements.Movables {
    public partial class Bottle : Movable, iLiquidDispenser {

        [Export] Area2D pourArea;
        LiquidDispenser dispenser;
        LiquidEnum liquid = LiquidEnum.Lemonade;

        public override void _Ready() {
            base._Ready();
            dispenser = new LiquidDispenser(this, pourArea, liquid, 10);
        }

        public override void _Process(double delta) {
            base._Process(delta);
            dispenser.PourLiquid(liquid);
        }
    }
}

