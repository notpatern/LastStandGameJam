using Godot;
using System;

public partial class SolidDispenser : Node2D
{
    [Export] private bool glassDispenserMode;
    public override void _Ready() {
    }

    public override void _Process(double delta) {
    }

    private void SpawnObject(PackedScene pSpawnable, Vector2 pPos, int pNumber) {

        Node2D lSpawnedObject;

        for (int i = 0; i < pNumber; i++) {

            lSpawnedObject = (Node2D)pSpawnable.Instantiate();
            lSpawnedObject.AddChild(this);
            lSpawnedObject.GlobalPosition = pPos;
        }
    }
}
