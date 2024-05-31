using Godot;
using Scripts.RecipeScripts.Liquids;
using Scripts.UI.StandElements.Movables;

namespace Scripts.UI.StandElements.Helper {
    public class LiquidDispenser {

        iLiquidDispenser parent;
        Area2D detectionArea;
        float flowSpeed;

        public LiquidDispenser(iLiquidDispenser parent, Area2D detectionArea, LiquidEnum liquid, float flowSpeed) {
            this.parent = parent;
            this.detectionArea = detectionArea;
            this.flowSpeed = flowSpeed;
        }

        public void PourLiquid(LiquidEnum liquid) {
            foreach (Area2D glassArea in detectionArea.GetOverlappingAreas()) {
                glassArea.GetParent<Glass>().AddLiquid(liquid, flowSpeed);
            }
        }
    }
}
