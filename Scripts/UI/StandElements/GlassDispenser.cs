using Godot;
using System;

public partial class GlassDispenser : Node2D
{
    [Export] PackedScene glassFactory;
    [Export] Node2D spawnPoint;
    [Export] float distanceBeforeSpawnGlass;
    Node2D lastDispensedGlass;

    public override void _Ready() {
        lastDispensedGlass = SpawnObject(glassFactory, spawnPoint.GlobalPosition);
    }
    public override void _Process(double delta) {
        if (lastDispensedGlass != null) {
            if (lastDispensedGlass.GlobalPosition.DistanceTo(spawnPoint.GlobalPosition) > distanceBeforeSpawnGlass)
                lastDispensedGlass = SpawnObject(glassFactory, spawnPoint.GlobalPosition);
        }
    }
    private Node2D SpawnObject(PackedScene pSpawnable, Vector2 pPos) {

        Node2D lSpawnedObject;

        lSpawnedObject = (Node2D)pSpawnable.Instantiate();
        lSpawnedObject.AddChild(this);
        lSpawnedObject.GlobalPosition = pPos;

        return lSpawnedObject;
    }
}
