using Godot;
using Scripts.ZombScripts;
using System;

namespace Scripts.ZombScripts;
public partial class ZombSpawner : Node2D
{
    [Export] AnimatedSprite2D zombsSprites;
    [Export] Node2D zombsTarget;
    [Export] PackedScene packedZomb;
    public override void _Ready()
    {

    }

    public Zomb CreateZomb(float pDifficulty, AnimatedSprite2D pZombSkins)
    {
        Zomb currentZomb = (Zomb)packedZomb.Instantiate();
        currentZomb.speed = currentZomb.speed * GetRandomSpeedFactor();
        currentZomb.zombTexture = pZombSkins.SpriteFrames.GetFrameTexture(pZombSkins.Animation, 0);
        currentZomb.GlobalPosition += ExtraMaths.GetRandomDirection();
        currentZomb.target = zombsTarget;
        currentZomb.Scale *= 0.5f;

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
