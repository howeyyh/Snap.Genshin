using System;
using System.Collections.Generic;

namespace DGP.Genshin.Data
{
    internal class DataManager
    {
        public Dictionary<string,Material> SpecialMaterials { get;private set; } = new Dictionary<string, Material>();
        public Dictionary<string, Material> MonsterMaterials { get; private set; } = new Dictionary<string, Material>();
        public Dictionary<string, Weapon> Weapons { get; private set; } = new Dictionary<string, Weapon>();
        public Dictionary<string, Character> Characters { get; private set; } = new Dictionary<string, Character>();

        private void Initialize()
        {
            this.InitializeSpecialMaterials();//before characters
            this.InitializeMonsterMaterials();

            this.InitializeWeapons();
            this.InitializeArtifacts();
            this.InitializeCharacters();
        }

        private void InitializeSpecialMaterials()
        {
            SpecialMaterials.Add("小灯草", new Material("小灯草", "Data/Images/Material/Special/small_lamp_grass.png"));

        }

        private void InitializeMonsterMaterials()
        {
            MonsterMaterials.Add("箭镞", new Material("箭镞", "Data/Images/Material/Monster/firm_arrowhead.png"));
        }

        private void InitializeWeapons() => this.Weapons = new Dictionary<string, Weapon>()
            {
            };
        private void InitializeCharacters()
        {
            this.Characters = new Dictionary<string, Character>();
            Characters.Add("安柏", new Character()
            {
                Title = "安柏",
                MaxHP = 9461,
                ATK = 223,
                DEF = 601,
                ATKPercent = 0.24,
                Materials = new MaterialGroup()
                {
                    Material1 = SpecialMaterials["小灯草"],
                    Material2 = MonsterMaterials["箭镞"],
                }
            }); 

        }

        private void InitializeArtifacts()
        {
           
        }

        #region 单例
        private static DataManager instance;
        private static readonly object _lock = new object();
        private DataManager()
        {
            Initialize();
        }
        public static DataManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new DataManager();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion
    }
}
