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
    [Export] PackedScene packedDefaultZomb;
    [Export] PackedScene packedBigZomb;
    [Export] PackedScene packedZombling;
    public override void _Ready()
    {
    }

    public void StartWave()
    {
        foreach (Zomb zomb in spawnsList)
        {
            if(zomb is DefaultZomb)
            {
                DefaultZomb currentZomb = CreateDefaultZomb(zombsSprites);
                spawns.Add(currentZomb);
                AddChild(currentZomb);
            } else if(zomb is BigZomb)
            {
                BigZomb currentZomb = CreateBigZomb(zombsSprites);
                spawns.Add(currentZomb);
                AddChild(currentZomb);
            } else if (zomb is Zombling)
            {
                Zombling currentZomb = CreateZombling(zombsSprites);
                spawns.Add(currentZomb);
                AddChild(currentZomb);
            }
        }
    }

    public List<Zomb> GetSpawns() { return spawns; }

    public DefaultZomb CreateDefaultZomb(AnimatedSprite2D pZombSkins)
    {
        DefaultZomb currentZomb = (DefaultZomb)packedDefaultZomb.Instantiate();
        currentZomb.speed = currentZomb.speed * GetRandomSpeedFactor();
        currentZomb.zombTexture = pZombSkins.SpriteFrames.GetFrameTexture(pZombSkins.Animation, 0);
        currentZomb.GlobalPosition += ExtraMaths.GetRandomDirection();
        currentZomb.target = zombsTarget;
        currentZomb.Scale *= 0.5f;

        return currentZomb;
    }

    public BigZomb CreateBigZomb(AnimatedSprite2D pZombSkins)
    {
        BigZomb currentZomb = (BigZomb)packedBigZomb.Instantiate();
        currentZomb.speed = (currentZomb.speed/2) * GetRandomSpeedFactor();
        currentZomb.zombTexture = pZombSkins.SpriteFrames.GetFrameTexture(pZombSkins.Animation, 1);
        currentZomb.GlobalPosition += ExtraMaths.GetRandomDirection();
        currentZomb.target = zombsTarget;
        currentZomb.repulsionForce = 0.5f;
        currentZomb.Scale *= 1;

        return currentZomb;
    }

    public Zombling CreateZombling(AnimatedSprite2D pZombSkins)
    {
        Zombling currentZomb = (Zombling)packedZombling.Instantiate();
        currentZomb.speed = currentZomb.speed * 2;
        currentZomb.zombTexture = pZombSkins.SpriteFrames.GetFrameTexture(pZombSkins.Animation, 2);
        currentZomb.GlobalPosition += ExtraMaths.GetRandomDirection();
        currentZomb.target = zombsTarget;
        currentZomb.repulsionForce = 0.5f;
        currentZomb.Scale *= 0.25f;

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
