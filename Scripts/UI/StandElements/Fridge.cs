using Godot;
using System;


namespace Scripts.UI.StandElements {

	public partial class Fridge : Node2D {

        [Export] private CollisionShape2D[] fridgeZones;
        [Export] private PackedScene[] elementsInFridge;

        string spawnInputName;

        public override void _Process(double delta) {
            if (Input.IsActionJustPressed(spawnInputName)) {
                foreach (CollisionShape2D lFridgeZone in fridgeZones) {

                    if (lFridgeZone.Shape.GetRect().HasPoint(GetGlobalMousePosition()))
                        SpawnObject(elementsInFridge[Array.IndexOf(fridgeZones, lFridgeZone)], GetGlobalMousePosition());
                }
            }
        }
        private void SpawnObject(PackedScene pSpawnable, Vector2 pPos, int pNumber = 1) {

            Node2D lSpawnedObject;

            for (int i = 0; i < pNumber; i++) {

                lSpawnedObject = (Node2D)pSpawnable.Instantiate();
                lSpawnedObject.AddChild(this);
                lSpawnedObject.GlobalPosition = pPos;
            }
        }
    }
}