using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scripts.ZombScripts
{
    [GlobalClass]
    public partial class ZombsManager : Resource
    {
        int NumWave = 1;
        int WaveSize = 50;
        List<Zomb> zombs = new List<Zomb>();
        [Export] PackedScene[] spawners;


        float progressiveDifficulty;

        public void Start()
        {
            SpawnWave();
        }

        public List<Zomb> SpawnWave()
        {
            for (int i = 0; i < WaveSize; i++)
            {

            }
            return zombs;
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

            float lastWaveSinceHit, currentWaveNumber, zombsMaxDist,playerEfficiency,playerSkill;
            
            currentWaveNumber = NumWave;


        }
        public void Update(double delta)
        {
            Collision();
        }
    }
}
