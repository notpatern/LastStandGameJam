using Godot;
using Scripts.UI.StandScripts;
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
        [Export] PackedScene spawner;
        List<ZombSpawner> spawners = new List<ZombSpawner>();
        Node2D parentAnchorNode = new Node2D();
        float progressiveDifficulty;
        public Node2D zombsTarget;

        public void Start(Node2D parent, Node2D target)
        {
            zombsTarget = target;
            parent.AddChild(parentAnchorNode);
            AddSpawnPoint();
            SpawnWave();
            GetZombies();
        }

        public List<Zomb> SpawnWave()
        {
            for (int i = 0; i < spawners.Count; i++)
            {
                spawners[i].spawnsList = SpawnOTron_3000();
                spawners[i].StartWave();
            }
            return zombs;
        }

        public List<Zomb> SpawnOTron_3000()
        {
            List<Zomb> spawns = new List<Zomb>();
            int spawnsPerSpawner = WaveSize/spawners.Count;
            int iteration = 0;
            Parallel.For(0, spawnsPerSpawner, i =>
            {
                if(iteration < 5)
                {
                    DefaultZomb zomb = new DefaultZomb();
                    spawns.Add(zomb);
                    iteration++;
                }
                else
                {
                    BigZomb bigZomb = new BigZomb();
                    spawns.Add(bigZomb);
                    iteration = 0;
                }

            });

            return spawns; 
        }

        public List<ZombSpawner> AddSpawnPoint()
        {
            ZombSpawner newSpawner = spawner.Instantiate<ZombSpawner>();
            newSpawner.zombsTarget = zombsTarget;
            spawners.Add(newSpawner);
            parentAnchorNode.AddChild(newSpawner);
            return spawners;
        }



        public void Collision()
        {
            foreach (Zomb zomb in zombs)
            {
                foreach(Zomb otherZomb in zombs)
                {
                    float distanceToNeighboringZomb = otherZomb.GlobalPosition.DistanceTo(zomb.GlobalPosition);
                    if (distanceToNeighboringZomb < zomb.radius * zomb.Scale.X)
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
        
        public void GetZombies()
        {
            zombs.Clear();
            for (int i = 0; i < spawners.Count; i++)
            {
                zombs.AddRange(spawners[i].GetSpawns());
            }
        }



        private StandInfo stand;

        public void GetStandInfo(StandInfo stand)
        {
            this.stand = stand;
            SetDifficulty();
        }

        

        public float SetDifficulty()
        {
            /* progressive difficulty params:
             * last wave since hit on stand
             * current wave number
             * zombie max dist
             * player efficiency
             * how many people were served correctly/incorrectly
             */

            float lastWaveSinceHit, currentWaveNumber, zombsMaxDist, specialZombsMaxDist, playerEfficiency, playerSkill, standHealth, standMaxHealth;

            lastWaveSinceHit = stand.lastWaveSinceHitOnStand;
            playerSkill = stand.playerZombKillEfficiency;
            playerEfficiency = stand.playerBarmanEfficiency;
            standHealth = stand.standHealth;
            standMaxHealth = stand.standMaxHealth;

            currentWaveNumber = NumWave;
            zombsMaxDist = 0; //to Change
            specialZombsMaxDist = 0;//to change
            progressiveDifficulty = ((currentWaveNumber + lastWaveSinceHit) * (Mathf.Abs(playerSkill - playerEfficiency) * (zombsMaxDist/specialZombsMaxDist))) - ( standMaxHealth - standHealth);

            return progressiveDifficulty;
        }



        public void Update(double delta)
        {
            Collision();
            GetZombies();
        }
    }
}
