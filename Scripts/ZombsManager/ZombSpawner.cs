using Godot;
using Scripts.ZombScripts;
using System;
using System.Collections.Generic;

namespace Scripts.ZombScripts;
public partial class ZombSpawner : Node2D
{
    public List<Zomb> spawnsList = new List<Zomb>();
    public List<Zomb> spawns = new List<Zomb>();
    [Export] AnimatedSprite2D zombsSprites;
    public Node2D zombsTarget;
    [Export] PackedScene packedZomb;
    public override void _Ready()
    {

    }

    public void StartWave()
    {
        foreach (Zomb zomb in spawnsList)
        {
            if(zomb is DefaultZomb)
            {
                AddChild(CreateDefaultZomb(zombsSprites));
            }
        }
    }

    public DefaultZomb CreateDefaultZomb(AnimatedSprite2D pZombSkins)
    {
        DefaultZomb currentZomb = (DefaultZomb)packedZomb.Instantiate();
        currentZomb.speed = currentZomb.speed * GetRandomSpeedFactor();
        currentZomb.zombTexture = pZombSkins.SpriteFrames.GetFrameTexture(pZombSkins.Animation, 0);
        currentZomb.GlobalPosition += ExtraMaths.GetRandomDirection();
        currentZomb.target = zombsTarget;
        currentZomb.Scale *= 0.5f;
        GD.Print("Spawned a default zomb");

        return currentZomb;
    }
    private float GetRandomSpeedFactor()
    {
        RandomNumberGenerator randyRandom = new RandomNumberGenerator();
        randyRandom.Randomize();
        return randyRandom.RandfRange(1.2f, 0.8f);
    }

    public float DistToStand()
    {
        Vector2 spawnerPos = GlobalPosition;
        Vector2 standPos = zombsTarget.GlobalPosition;

        return spawnerPos.DistanceTo(standPos);
    }

    public override void _Process(double delta)
    {

    }
}
