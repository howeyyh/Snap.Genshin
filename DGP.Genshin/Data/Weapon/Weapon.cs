using DGP.Genshin.Data.Weapon.Passives;
using System;
using System.Text;

namespace DGP.Genshin.Data.Weapon
{
    public class Weapon
    {
        public uint ATK { get; set; }
        public Stat SubStat { get; set; } = Stat.None;
        public double SubStatValue { get; set; }
        public WeaponType Type { get; set; }
        public PassiveCollection Passives { get; set; }
        public Uri ImageUri { get; set; }
        public string WeaponName { get; set; }
        public int Star { get; set; } = 1;
        public WeaponMaterial Material { get; set; }
        public bool IsReleased { get; set; } = true;
        public Material DailyMaterial { get; set; }
        public Material EliteMaterial { get; set; }
        public Material MonsterMaterial { get; set; }
        public int RefineLevel { get; set; } = 1;

        public string Refine1
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (Passives != null)
                {
                    foreach (Passive passive in Passives)
                    {
                        string d = passive.Description;
                        if (passive.Values != null)
                        {
                            double v = passive.Values[0];
                            d = d.Replace("*value*", " " + v * 100 + "% ");
                        }
                        if (passive.Times != null)
                        {
                            d = d.Replace("*time*", " " + passive.Times[0].ToString() + " ");
                        }

                        sb.Append(d).Append('\n');
                    }
                }

                return sb.ToString();
            }
        }
        public string Refine2
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Passives != null)
                {
                    foreach (Passive passive in Passives)
                    {
                        string d = passive.Description;
                        if (passive.Values != null)
                        {
                            double v = passive.Values[1];
                            d = d.Replace("*value*", " " + v * 100 + "% ");
                        }
                        if (passive.Times != null)
                        {
                            d = d.Replace("*time*", " " + passive.Times[1].ToString() + " ");
                        }

                        sb.Append(d).Append('\n');
                    }
                }

                return sb.ToString();
            }
        }
        public string Refine3
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Passives != null)
                {
                    foreach (Passive passive in Passives)
                    {
                        string d = passive.Description;
                        if (passive.Values != null)
                        {
                            double v = passive.Values[2];
                            d = d.Replace("*value*", " " + v * 100 + "% ");
                        }
                        if (passive.Times != null)
                        {
                            d = d.Replace("*time*", " " + passive.Times[2].ToString() + " ");
                        }

                        sb.Append(d).Append('\n');
                    }
                }

                return sb.ToString();
            }
        }
        public string Refine4
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Passives != null)
                {
                    foreach (Passive passive in Passives)
                    {
                        string d = passive.Description;
                        if (passive.Values != null)
                        {
                            double v = passive.Values[3];
                            d = d.Replace("*value*", " " + v * 100 + "% ");
                        }
                        if (passive.Times != null)
                        {
                            d = d.Replace("*time*", " " + passive.Times[3].ToString() + " ");
                        }

                        sb.Append(d).Append('\n');
                    }
                }

                return sb.ToString();
            }
        }
        public string Refine5
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Passives != null)
                {
                    foreach (Passive passive in Passives)
                    {
                        string d = passive.Description;
                        if (passive.Values != null)
                        {
                            double v = passive.Values[4];
                            d = d.Replace("*value*", " " + v * 100 + "% ");
                        }
                        if (passive.Times != null)
                        {
                            d = d.Replace("*time*", " " + passive.Times[4].ToString() + " ");
                        }

                        sb.Append(d).Append('\n');
                    }
                }

                return sb.ToString();
            }
        }
    }
}
