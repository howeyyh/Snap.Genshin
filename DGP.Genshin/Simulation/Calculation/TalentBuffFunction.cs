using DGP.Genshin.Data.Character;
using DGP.Genshin.Data.Weapon;

namespace DGP.Genshin.Simulation.Calculation
{
    public abstract class TalentBuffFunction
    {
        public Character Character { get; set; }
        public Weapon Weapon { get; set; }
        public abstract double Bonus();
    }

}
