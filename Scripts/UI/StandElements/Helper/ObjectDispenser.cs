using Godot;
using System;

namespace Scripts.UI.StandElements.Helper
{
    public partial class ObjectDispenser : Node2D
    {
        [Export] PackedScene sceneToSpawn;
        [Export] CollisionShape2D detectionAreaShape;
        Vector2 detectionArea;
        string inputEvent; //define before using

        bool isHoldingObject;
        public override void _Ready() {
            detectionArea = detectionAreaShape.Scale;
        }
        public override void _Process(double pDelta) {
            if (Input.IsActionJustPressed(inputEvent)
                && !isHoldingObject
                && GetGlobalMousePosition().x < detectionAreaShape.GlobalPosition.x + detectionArea.x / 2
                && GetGlobalMousePosition().x > detectionAreaShape.GlobalPosition.x - detectionArea.x / 2
                && GetGlobalMousePosition().y < detectionAreaShape.GlobalPosition.y + detectionArea.y / 2
                && GetGlobalMousePosition().y > detectionAreaShape.GlobalPosition.y - detectionArea.y / 2) {

                SpawnScene(sceneToSpawn, GetGlobalMousePosition());
            }
        }

        private void SpawnScene(PackedScene pSceneToSpawn, Vector2 pPosToSpawn, int pNumberToSpawn = 1) {
            Node2D lSpawnedObj;
            for (int i = 0; i < pNumberToSpawn; i++) {
                lSpawnedObj = (Node2D)pSceneToSpawn.Instance();
                AddChild(lSpawnedObj);
                lSpawnedObj.GlobalPosition = pPosToSpawn;
            }
        }
    }
}