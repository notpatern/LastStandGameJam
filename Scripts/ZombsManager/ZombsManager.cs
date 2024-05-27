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
        int WaveSize = 5;
        List<Zomb> zombs = new List<Zomb>();
        [Export] AnimatedSprite2D zombsSprites;
        [Export] PackedScene packedZomb;
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
                GD.Print("spawned " + i + " zombs.");
            }
            return zombs;
        }

        public Zomb CreateZomb(float pDifficulty, AnimatedSprite2D pZombSkins)    
        {
            Zomb currentZomb = (Zomb)packedZomb.Instantiate();
            currentZomb.speed = 1f;
            currentZomb.health = 1;
            currentZomb.damage = 2;
            currentZomb.zombTexture = pZombSkins.SpriteFrames.GetFrameTexture(pZombSkins.Animation, 0);

            return currentZomb;
        }

        public void Collision()
        {
            Parallel.For(0, WaveSize, i =>
            {

                

            });
        }

        public void SetDifficulty()
        {

        }

        public void Update()
        {

        }
    }
}
