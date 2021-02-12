using DGP.Genshin.Helper;
using DGP.Genshin.Simulation.Calculation;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public abstract class Passive : Observable
    {


        public string Description { get; set; }
        public string DescriptionAll
        {
            get
            {
                string d = Description;
                if (Values != null)
                {
                    string v = string.Join("/", Values.Select(i => i * 100 + "%"));
                    d = d.Replace("*value*", "[" + v + "]");
                }
                if (Times != null)
                {
                    string t = string.Join("/", Times);
                    d = d.Replace("*time*", "[" + t + "]");
                }
                return d;
            }
        }
        public string DescriptionByRefineLevel
        {
            get
            {
                string d = Description;
                if (Values != null)
                {
                    //string v = string.Join("/", Values.Select(i => i * 100 + "%"));
                    string v = Values[refineLevel - 1] * 100 + "%";
                    d = d.Replace("*value*", " " + v + " ");
                }
                if (Times != null)
                {
                    string t = Times[refineLevel - 1].ToString();
                    d = d.Replace("*time*", " " + t + " ");
                }
                return d;
            }
        }
        public DoubleCollection Values { get; set; }
        protected double CurrentValue { get { return Values[RefineLevel - 1]; } }
        public DoubleCollection Times { get; set; }

        private int refineLevel = 1;
        public int RefineLevel
        {
            get => refineLevel; set
            {
                Set(ref refineLevel, value);
                OnPropertyChanged("DescriptionByRefineLevel");
            }
        }
        //for triggerable
        public bool IsTriggered { get; set; } = true;
        //for stackable
        public int MaxStack { get; set; } = 1;
        public int CurrentStack { get; set; } = 1;
        public Int32Collection Stacks
        {
            get
            {
                Int32Collection stacks = new Int32Collection();
                for (int i = 1; i <= MaxStack; i++)
                {
                    stacks.Add(i);
                }

                return stacks;
            }
        }
        //for conditional
        public double Rate { get; set; } = 1;
        public string ConditionText { get; set; }
        public Visibility ConditionVisibility { get; set; } = Visibility.Collapsed;
        public bool IsSatisfied { get; set; } = true;

        public abstract void Apply(Calculator c);
    }
    public class NormalPassive : Passive
    {
        public override void Apply(Calculator c)
        {
        }
    }
    public class HPPercentPassive : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered)
            {
                c.HP.BonusByPercent += CurrentValue * CurrentStack * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class HPToATK : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered)
            {
                c.Attack.Bonus += c.HP * CurrentValue * CurrentStack * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class DealedDMGBonus : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered)
            {
                c.DamageBonus += CurrentValue * CurrentStack * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class NormalChargedDealedDMGBonus : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered && (c.Target == Target.NormalAttack || c.Target == Target.NormalAttack))
            {
                c.DamageBonus += CurrentValue * CurrentStack * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class BaseATKPercentBonus : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered)
            {
                c.Attack.Base *= (1 + CurrentValue * CurrentStack) * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class ATKPercentBonus : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered)
            {
                c.Attack.BonusByPercent += CurrentValue * CurrentStack * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class ATKBonus : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered)
            {
                c.Attack.Bonus += CurrentValue * CurrentStack * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class ATKDEFPercentBonus : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered)
            {
                c.Attack.BonusByPercent += CurrentValue * CurrentStack * (IsSatisfied ? Rate : 1);
                c.Defence.BonusByPercent += CurrentValue * CurrentStack * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class ATKToDamage : Passive
    {
        public double Probability { get; set; } = 1;
        public override void Apply(Calculator c)
        {
            if (IsTriggered)
            {
                //将一击与额外伤害合并计算
                c.ATKToDMGRate = (1 + CurrentValue) * Probability * CurrentStack * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class CritRateBonus : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered)
            {
                c.CritRate += CurrentValue * CurrentStack * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class ElementSkillDealedDMGBonus : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered && c.Target == Target.ElementSkill)
            {
                c.DamageBonus += CurrentValue * CurrentStack * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class ElementSkillCritRateBonus : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered && c.Target == Target.ElementSkill)
            {
                c.CritRate += CurrentValue * CurrentStack * (IsSatisfied ? Rate : 1);
            }
        }
    }
    public class AttackSpeedBonus : Passive
    {
        public override void Apply(Calculator c)
        {
            if (IsTriggered && c.Target == Target.ElementSkill)
            {
                c.AttackSpeed += CurrentValue * (IsSatisfied ? Rate : 1);
            }
        }
    }
}
