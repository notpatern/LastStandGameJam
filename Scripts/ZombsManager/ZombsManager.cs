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
        int killCount;
        List<Zomb> zombs = new List<Zomb>();
        [Export] PackedScene spawner;
        List<ZombSpawner> spawners = new List<ZombSpawner>();
        Node2D parentAnchorNode = new Node2D();
        float progressiveDifficulty;
        public Node2D zombsTarget;

        public void Start(Node2D parent, Node2D target)
        {
            progressiveDifficulty = 1000;
            zombsTarget = target;
            parent.AddChild(parentAnchorNode);
            AddSpawnPoint();
            SpawnWave();
            GetZombies();
        }

        public List<Zomb> SpawnWave()
        {
            GD.Print(progressiveDifficulty);
            killCount = 0;
            for (int i = 0; i < spawners.Count; i++)
            {
                spawners[i].spawnsList = SpawnOTron_3000();
                spawners[i].StartWave();
            }

            NumWave++;
            return zombs;
        }

        public List<Zomb> SpawnOTron_3000()
        {
            List<Zomb> spawns = new List<Zomb>();
            int spawnsPerSpawner = (int)progressiveDifficulty/spawners.Count;
            int iteration = 0;
            for(int i = 0;i < spawnsPerSpawner;i++)
            {
                if (progressiveDifficulty > 0)
                {
                    GD.Print(progressiveDifficulty);
                    if (iteration < 5)
                    {
                        DefaultZomb zomb = new DefaultZomb();
                        spawns.Add(zomb);
                        iteration++;
                        progressiveDifficulty -= 3;
                    }
                    else if (progressiveDifficulty > 40)
                    {
                        BigZomb bigZomb = new BigZomb();
                        spawns.Add(bigZomb);
                        iteration = 0;
                        progressiveDifficulty -= 10;
                    }
                    else if (progressiveDifficulty > 20 && iteration > 1)
                    {
                        Zombling zombling = new Zombling();
                        for (int j = 0; j < 10; j++)
                        {
                            spawns.Add(zombling);
                        }

                        progressiveDifficulty -= 20;
                    }
                    else
                    {
                        iteration = 0;
                    }
                }
            };

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
                        zomb.GlobalPosition += CalculateForce(otherZomb.repulsionForce,distanceToNeighboringZomb,zomb.radius) * GetDirectionToOtherZomb(zomb.GlobalPosition, otherZomb.GlobalPosition) * -1;
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
            int currentZombNum = zombs.Count;
            zombs.Clear();
            for (int i = 0; i < spawners.Count; i++)
            {
                zombs.AddRange(spawners[i].GetSpawns());
            }

            killCount += currentZombNum - zombs.Count;
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

            float lastWaveSinceHit, currentWaveNumber, playerEfficiency, playerSkill, standHealth, standMaxHealth, playerCurrency;

            lastWaveSinceHit = stand.lastWaveSinceHitOnStand;
            playerSkill = stand.playerZombKillEfficiency;
            playerEfficiency = stand.playerBarmanEfficiency;
            standHealth = stand.standHealth;
            standMaxHealth = stand.standMaxHealth;
            playerCurrency = CurrencyManager.playerMoney;

            currentWaveNumber = NumWave;
            progressiveDifficulty = ((currentWaveNumber + lastWaveSinceHit)) * ((Mathf.Abs(playerSkill - playerEfficiency)) - playerCurrency / ( standMaxHealth - standHealth));

            return progressiveDifficulty;
        }



        public void Update(double delta)
        {
            Collision();
            GetZombies();
        }
    }
}
