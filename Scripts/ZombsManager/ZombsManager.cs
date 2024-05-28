using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scripts.ZombScripts
{
    [GlobalClass]
    public partial class ZombsManager : Node2D
    {
        int NumWave = 1;
        int WaveSize = 50;
        List<Zomb> zombs = new List<Zomb>();
        [Export] AnimatedSprite2D zombsSprites;
        [Export] PackedScene packedZomb;
        [Export] Node2D zombsTarget;
        float progressiveDifficulty;

        public override void _Ready()
        {
            Start();
        }

        public void Start()
        {
            SpawnWave();
        }

        public List<Zomb> SpawnWave()
        {
            for (int i = 0; i < WaveSize; i++)
            {
                zombs.Add(CreateZomb(progressiveDifficulty,zombsSprites));
                AddChild(zombs[i]);
            }
            return zombs;
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
            return randyRandom.RandfRange(1.2f,0.8f);
        }

        public void Collision()
        {
            foreach (Zomb zomb in zombs)
            {
                foreach(Zomb otherZomb in zombs)
                {
                    float distanceToNeighboringZomb = otherZomb.GlobalPosition.DistanceTo(zomb.GlobalPosition);
                    if (distanceToNeighboringZomb < zomb.radius)
                    {
                        if(CompareDirection(zomb,otherZomb) > 0.8f)
                        {
                            otherZomb.isAllowedToMove = false;
                            continue;
                        }
                        otherZomb.isAllowedToMove = true;
                        zomb.GlobalPosition += CalculateForce(zomb.repulsionForce,distanceToNeighboringZomb,zomb.radius) * GetDirectionToOtherZomb(zomb.GlobalPosition, otherZomb.GlobalPosition) * -1;
                    }
                }
            }
        }

        private float CompareDirection(Zomb zomb, Zomb otherZomb)
        {
            Vector2 otherZombVel = otherZomb.velocity.Normalized();
            Vector2 zombDirection = (zomb.GlobalPosition - otherZomb.GlobalPosition).Normalized();
            return otherZombVel.Dot(zombDirection);
        }

        private float CalculateForce(float zombForce, float distToZomb, float zombRepulsionRange)
        {
            float repulsionFactor = zombForce * ExtraMaths.Normalize(distToZomb,zombRepulsionRange);
            return repulsionFactor;
        }

        private Vector2 GetDirectionToOtherZomb(Vector2 zombPos, Vector2 otherZombPos)
        {
            Vector2 direction = zombPos.DirectionTo(otherZombPos);
            return direction;
        }

        public void SetDifficulty()
        {
            /* progressive difficulty params:
             * last wave since hit on stand
             * current wave number
             * zombie max dist
             * player efficiency
             * how many people were served correctly/incorrectly
             */

            float lastWaveSinceHit, currentWaveNumber;
            
            currentWaveNumber = NumWave;


        }



        private float DistToStand()
        {
            Vector2 spawnerPos = GlobalPosition;
            Vector2 standPos = zombsTarget.GlobalPosition;

            return spawnerPos.DistanceTo(standPos);
        }

        public override void _Process(double delta)
        {
            Update();
        }
        public void Update()
        {
            Collision();
        }
    }
}
