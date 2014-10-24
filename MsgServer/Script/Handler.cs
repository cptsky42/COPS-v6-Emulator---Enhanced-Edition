using System;
using COServer.Network;
using COServer.Entities;
using Logik.Script;
using CO2_CORE_DLL;

namespace COServer.Script
{
    public partial class ScriptHandler
    {
        private static Boolean CheckRequirement(KCS.Requirement Requirement, Object Obj)
        {
            try
            {
                Client Client = (Obj as Client);
                if (Client == null)
                    return false;

                Player Player = Client.User;

                switch ((KCS.RequirementType)Requirement.Type)
                {
                    #region CurHP
                    case KCS.RequirementType.CurHP:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.CurHP < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.CurHP > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.CurHP <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.CurHP >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.CurHP == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.CurHP != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region MaxHP
                    case KCS.RequirementType.MaxHP:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.MaxHP < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.MaxHP > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.MaxHP <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.MaxHP >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.MaxHP == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.MaxHP != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region CurMP
                    case KCS.RequirementType.CurMP:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.CurMP < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.CurMP > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.CurMP <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.CurMP >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.CurMP == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.CurMP != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region MaxMP
                    case KCS.RequirementType.MaxMP:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.MaxMP < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.MaxMP > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.MaxMP <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.MaxMP >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.MaxMP == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.MaxMP != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region Money
                    case KCS.RequirementType.Money:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.Money < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.Money > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.Money <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.Money >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.Money == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.Money != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region Exp
                    case KCS.RequirementType.Exp:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.Exp < (ulong)Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.Exp > (ulong)Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.Exp <= (ulong)Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.Exp >= (ulong)Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.Exp == (ulong)Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.Exp != (ulong)Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region PkPoints
                    case KCS.RequirementType.PkPoints:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.PkPoints < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.PkPoints > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.PkPoints <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.PkPoints >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.PkPoints == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.PkPoints != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region Profession
                    case KCS.RequirementType.Profession:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.Profession < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.Profession > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.Profession <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.Profession >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.Profession == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.Profession != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region AddPoints
                    case KCS.RequirementType.AddPoints:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.AddPoints < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.AddPoints > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.AddPoints <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.AddPoints >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.AddPoints == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.AddPoints != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region Look
                    case KCS.RequirementType.Look:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.Look < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.Look > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.Look <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.Look >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.Look == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.Look != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region Level
                    case KCS.RequirementType.Level:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.Level < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.Level > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.Level <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.Level >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.Level == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.Level != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region Spirit
                    case KCS.RequirementType.Spirit:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.Spirit < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.Spirit > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.Spirit <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.Spirit >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.Spirit == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.Spirit != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region Vitality
                    case KCS.RequirementType.Vitality:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.Vitality < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.Vitality > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.Vitality <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.Vitality >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.Vitality == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.Vitality != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region Strength
                    case KCS.RequirementType.Strength:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.Strength < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.Strength > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.Strength <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.Strength >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.Strength == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.Strength != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region Agility
                    case KCS.RequirementType.Agility:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.Agility < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.Agility > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.Agility <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.Agility >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.Agility == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.Agility != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region Hair
                    case KCS.RequirementType.Hair:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.Hair < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.Hair > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.Hair <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.Hair >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.Hair == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.Hair != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region CPs
                    case KCS.RequirementType.CPs:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.CPs < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.CPs > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.CPs <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.CPs >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.CPs == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.CPs != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region VPs
                    case KCS.RequirementType.VPs:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.VPs < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.VPs > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.VPs <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.VPs >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.VPs == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.VPs != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region InvContains
                    case KCS.RequirementType.InvContains:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.Equal:
                                    {
                                        string[] Data = Requirement.StrValue.Split(':');
                                        if (Player.InventoryContains(Int32.Parse(Data[0]), Int32.Parse(Data[1])))
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        string[] Data = Requirement.StrValue.Split(':');
                                        if (!Player.InventoryContains(Int32.Parse(Data[0]), Int32.Parse(Data[1])))
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region InvCount
                    case KCS.RequirementType.InvCount:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.LessThan:
                                    {
                                        if (Player.ItemInInventory() < Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThan:
                                    {
                                        if (Player.ItemInInventory() > Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.LessThanOrEqual:
                                    {
                                        if (Player.ItemInInventory() <= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.GreaterThanOrEqual:
                                    {
                                        if (Player.ItemInInventory() >= Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.ItemInInventory() == Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.ItemInInventory() != Requirement.IntValue)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region SkillContains
                    case KCS.RequirementType.SkillContains:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.GetMagicByType((Int16)Requirement.IntValue) != null)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.GetMagicByType((Int16)Requirement.IntValue) == null)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                    #region ProfContains
                    case KCS.RequirementType.ProfContains:
                        {
                            switch ((KCS.Operator)Requirement.Operator)
                            {
                                case KCS.Operator.Equal:
                                    {
                                        if (Player.GetWeaponSkillByType((Int16)Requirement.IntValue) != null)
                                            return true;
                                        break;
                                    }
                                case KCS.Operator.Inequal:
                                    {
                                        if (Player.GetWeaponSkillByType((Int16)Requirement.IntValue) == null)
                                            return true;
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                }
                return false;
            }
            catch (Exception Exc) { Program.WriteLine(Exc.ToString()); return false; }
        }

        private static void GetReward(KCS.Reward Reward, Object Obj)
        {
            try
            {
                Client Client = (Obj as Client);
                if (Client == null)
                    return;

                Player Player = Client.User;

                switch ((KCS.RewardType)Reward.Type)
                {
                    #region CurHP
                    case KCS.RewardType.CurHP:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.CurHP += (UInt16)Reward.IntValue;
                                        if (Player.Team != null)
                                            World.BroadcastTeamMsg(Player.Team, MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.CurHP -= (UInt16)Reward.IntValue;
                                        if (Player.Team != null)
                                            World.BroadcastTeamMsg(Player.Team, MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.CurHP *= (UInt16)Reward.IntValue;
                                        if (Player.Team != null)
                                            World.BroadcastTeamMsg(Player.Team, MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.CurHP /= (UInt16)Reward.IntValue;
                                        if (Player.Team != null)
                                            World.BroadcastTeamMsg(Player.Team, MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.CurHP = (UInt16)Reward.IntValue;
                                        if (Player.Team != null)
                                            World.BroadcastTeamMsg(Player.Team, MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                            break;
                        }
                    #endregion
                    #region MaxHP
                    case KCS.RewardType.MaxHP:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.MaxHP += (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.MaxHP -= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.MaxHP *= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.MaxHP /= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.MaxHP = (UInt16)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.MaxHP, (MsgUserAttrib.Type.Life + 1)));
                            break;
                        }
                    #endregion
                    #region CurMP
                    case KCS.RewardType.CurMP:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.CurMP += (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.CurMP -= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.CurMP *= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.CurMP /= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.CurMP = (UInt16)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.CurMP, MsgUserAttrib.Type.Mana));
                            break;
                        }
                    #endregion
                    #region MaxMP
                    case KCS.RewardType.MaxMP:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.MaxMP += (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.MaxMP -= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.MaxMP *= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.MaxMP /= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.MaxMP = (UInt16)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.MaxMP, (MsgUserAttrib.Type.Mana + 1)));
                            break;
                        }
                    #endregion
                    #region Money
                    case KCS.RewardType.Money:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.Money += Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.Money -= Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.Money *= Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.Money /= Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.Money = Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                            break;
                        }
                    #endregion
                    #region Exp
                    case KCS.RewardType.Exp:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.Exp += (UInt64)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.Exp -= (UInt64)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.Exp *= (UInt64)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.Exp /= (UInt64)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.Exp = (UInt64)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, (Int64)Player.Exp, MsgUserAttrib.Type.Exp));
                            break;
                        }
                    #endregion
                    #region PkPoints
                    case KCS.RewardType.PkPoints:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.PkPoints += (Int16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.PkPoints -= (Int16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.PkPoints *= (Int16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.PkPoints /= (Int16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.PkPoints = (Int16)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.PkPoints, MsgUserAttrib.Type.PkPoints));
                            break;
                        }
                    #endregion
                    #region Profession
                    case KCS.RewardType.Profession:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.Profession += (Byte)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.Profession -= (Byte)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.Profession *= (Byte)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.Profession /= (Byte)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.Profession = (Byte)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.Profession, MsgUserAttrib.Type.Profession));
                            break;
                        }
                    #endregion
                    #region AddPoints
                    case KCS.RewardType.AddPoints:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.AddPoints += (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.AddPoints -= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.AddPoints *= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.AddPoints /= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.AddPoints = (UInt16)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.AddPoints, MsgUserAttrib.Type.AddPoints));
                            break;
                        }
                    #endregion
                    #region Look
                    case KCS.RewardType.Look:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.Look += (UInt32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.Look -= (UInt32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.Look *= (UInt32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.Look /= (UInt32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.Look = (UInt32)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.Look, MsgUserAttrib.Type.Look));
                            break;
                        }
                    #endregion
                    #region Level
                    case KCS.RewardType.Level:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.Level += (Byte)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.Level -= (Byte)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.Level *= (Byte)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.Level /= (Byte)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.Level = (Byte)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.Level, MsgUserAttrib.Type.Level));
                            break;
                        }
                    #endregion
                    #region Spirit
                    case KCS.RewardType.Spirit:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.Spirit += (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.Spirit -= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.Spirit *= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.Spirit /= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.Spirit = (UInt16)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.Spirit, MsgUserAttrib.Type.Spirit));
                            break;
                        }
                    #endregion
                    #region Vitality
                    case KCS.RewardType.Vitality:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.Vitality += (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.Vitality -= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.Vitality *= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.Vitality /= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.Vitality = (UInt16)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.Vitality, MsgUserAttrib.Type.Vitality));
                            break;
                        }
                    #endregion
                    #region Strength
                    case KCS.RewardType.Strength:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.Strength += (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.Strength -= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.Strength *= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.Strength /= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.Strength = (UInt16)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.Strength, MsgUserAttrib.Type.Strength));
                            break;
                        }
                    #endregion
                    #region Agility
                    case KCS.RewardType.Agility:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.Agility += (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.Agility -= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.Agility *= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.Agility /= (UInt16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.Agility = (UInt16)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.Agility, MsgUserAttrib.Type.Agility));
                            break;
                        }
                    #endregion
                    #region Hair
                    case KCS.RewardType.Hair:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.Hair += (Int16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.Hair -= (Int16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.Hair *= (Int16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.Hair /= (Int16)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.Hair = (Int16)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.Hair, MsgUserAttrib.Type.Hair));
                            break;
                        }
                    #endregion
                    #region CPs
                    case KCS.RewardType.CPs:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.CPs += (Int32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.CPs -= (Int32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.CPs *= (Int32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.CPs /= (Int32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.CPs = (Int32)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.CPs, MsgUserAttrib.Type.CPs));
                            break;
                        }
                    #endregion
                    #region VPs
                    case KCS.RewardType.VPs:
                        {
                            switch ((KCS.Operator)Reward.Operator)
                            {
                                case KCS.Operator.Addition:
                                    {
                                        Player.VPs += (Int32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Subtraction:
                                    {
                                        Player.VPs -= (Int32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Multiplication:
                                    {
                                        Player.VPs *= (Int32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.Divison:
                                    {
                                        Player.VPs /= (Int32)Reward.IntValue;
                                        break;
                                    }
                                case KCS.Operator.None:
                                    {
                                        Player.VPs = (Int32)Reward.IntValue;
                                        break;
                                    }
                            }
                            Player.Send(MsgUserAttrib.Create(Player, Player.CPs, MsgUserAttrib.Type.CPs));
                            break;
                        }
                    #endregion
                    #region AddItem
                    case KCS.RewardType.AddItem:
                        {
                            String[] Data = Reward.StrValue.Split(':');

                            Int32 Amount = Int32.Parse(Data[1]);
                            for (Int32 i = 0; i < Amount; i++)
                            {
                                String[] ItemParts = Data[0].Split('-');
                                Item Item = Item.Create(
                                    0,
                                    0,
                                    Int32.Parse(ItemParts[0]),
                                    Byte.Parse(ItemParts[1]),
                                    Byte.Parse(ItemParts[2]),
                                    Byte.Parse(ItemParts[3]),
                                    Byte.Parse(ItemParts[4]),
                                    Byte.Parse(ItemParts[5]),
                                    2,
                                    0,
                                    ItemHandler.GetMaxDura(Int32.Parse(ItemParts[0])),
                                    ItemHandler.GetMaxDura(Int32.Parse(ItemParts[0])));

                                Player.AddItem(Item, true);
                            }
                            break;
                        }
                    #endregion
                    #region DelItem
                    case KCS.RewardType.DelItem:
                        {
                            String[] Data = Reward.StrValue.Split(':');
                            Player.DelItem(Int32.Parse(Data[0]), Int32.Parse(Data[1]), true);
                            break;
                        }
                    #endregion
                    #region Teleport
                    case KCS.RewardType.Teleport:
                        {
                            String[] Data = Reward.StrValue.Split(':');
                            Player.Move(Int16.Parse(Data[0]), UInt16.Parse(Data[1]), UInt16.Parse(Data[2]));
                            break;
                        }
                    #endregion
                    #region AddSkill
                    case KCS.RewardType.AddSkill:
                        {
                            Magic Magic = Magic.Create(Player.UniqId, (Int16)Reward.IntValue, 0, 0, 0, false);
                            Player.AddMagic(Magic, true);
                            break;
                        }
                    #endregion
                    #region DelSkill
                    case KCS.RewardType.DelSkill:
                        {
                            Magic Magic = Player.GetMagicByType((Int16)Reward.IntValue);
                            Player.DelMagic(Magic, true);
                            break;
                        }
                    #endregion
                    #region AddProf
                    case KCS.RewardType.AddProf:
                        {
                            WeaponSkill WeaponSkill = WeaponSkill.Create(Player.UniqId, (Int16)Reward.IntValue, 0, 0, 0, false);
                            Player.AddWeaponSkill(WeaponSkill, true);
                            break;
                        }
                    #endregion
                    #region DelProf
                    case KCS.RewardType.DelProf:
                        {
                            WeaponSkill WeaponSkill = Player.GetWeaponSkillByType((Int16)Reward.IntValue);
                            Player.DelWeaponSkill(WeaponSkill, true);
                            break;
                        }
                    #endregion
                    #region RemoteControl
                    case KCS.RewardType.RemoteControl:
                        {
                            Player.Send(MsgAction.Create(Player, Reward.IntValue, MsgAction.Action.PostCmd));
                            break;
                        }
                    #endregion
                    #region RandOpt
                    case KCS.RewardType.RandOpt:
                        {
                            String[] Data = Reward.StrValue.Split(':');
                            Byte Var1 = (Byte)Int32.Parse(Data[0]);
                            Byte Var2 = (Byte)Int32.Parse(Data[1]);

                            Byte Opt = (Byte)MyMath.Generate(Math.Min(Var1, Var2), Math.Max(Var1, Var2));

                            Byte[] Buffer = new Byte[Kernel.MAX_BUFFER_SIZE];
                            Int32 Position = 0;

                            Position += SendText("Une valeur a été généré au hazard! Cliquez sur continuer...", Obj, ref Buffer, Position);
                            Position += SendOption(Opt, "Continuer...", Obj, ref Buffer, Position);
                            Position += SendFace(1, Obj, ref Buffer, Position);
                            Position += SendEnd(Obj, ref Buffer, Position);

                            Byte[] Tmp = new Byte[Position];
                            Array.Copy(Buffer, 0, Tmp, 0, Position);
                            Client.Send(Tmp);
                            break;
                        }
                    #endregion
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc.ToString()); }
        }

        public unsafe static Int32 SendText(String Text, Object Obj, ref Byte[] Buffer, Int32 Position)
        {
            Client Client = (Obj as Client);
            if (Client == null)
                return 0;

            Player Player = Client.User;

            Int32 Len = 0;
            for (Int16 x = 0; x < Text.Length; x += 250)
            {
                Byte Length = 250;
                if (x + 250 > Text.Length)
                    Length = (Byte)(Text.Length - x);

                Byte[] Msg = MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Text, Text.Substring(x, Length));
                fixed (Byte* pBuf = Buffer)
                    Kernel.memcpy(pBuf + Position + Len, Msg, Msg.Length);
                Len += Msg.Length;
            }
            return Len;
        }

        public unsafe static Int32 SendOption(Byte UniqId, String Text, Object Obj, ref Byte[] Buffer, Int32 Position)
        {
            Client Client = (Obj as Client);
            if (Client == null)
                return 0;

            Player Player = Client.User;

            Byte[] Msg = MsgDialog.Create(0, 0, UniqId, MsgDialog.Action.Link, Text);
            fixed (Byte* pBuf = Buffer)
                Kernel.memcpy(pBuf + Position, Msg, Msg.Length);
            return Msg.Length;
        }

        public unsafe static Int32 SendInput(Byte UniqId, String Text, Object Obj, ref Byte[] Buffer, Int32 Position)
        {
            Client Client = (Obj as Client);
            if (Client == null)
                return 0;

            Player Player = Client.User;

            Byte[] Msg = MsgDialog.Create(0, 0, UniqId, MsgDialog.Action.Edit, Text);
            fixed (Byte* pBuf = Buffer)
                Kernel.memcpy(pBuf + Position, Msg, Msg.Length);
            return Msg.Length;
        }

        public unsafe static Int32 SendFace(Int16 Face, Object Obj, ref Byte[] Buffer, Int32 Position)
        {
            Client Client = (Obj as Client);
            if (Client == null)
                return 0;

            Player Player = Client.User;

            Byte[] Msg = MsgDialog.Create(0, Face, 0xFF, MsgDialog.Action.Pic, null);
            fixed (Byte* pBuf = Buffer)
                Kernel.memcpy(pBuf + Position, Msg, Msg.Length);
            return Msg.Length;
        }

        public unsafe static Int32 SendEnd(Object Obj, ref Byte[] Buffer, Int32 Position)
        {
            Client Client = (Obj as Client);
            if (Client == null)
                return 0;

            Player Player = Client.User;

            Byte[] Msg = MsgDialog.Create(0, 0, 0xFF, MsgDialog.Action.Create, null);
            fixed (Byte* pBuf = Buffer)
                Kernel.memcpy(pBuf + Position, Msg, Msg.Length);
            return Msg.Length;
        }

        public unsafe static void SendData(Object Obj, Byte[] Data, Int32 Length)
        {
            Client Client = (Obj as Client);
            if (Client == null)
                return;

            Byte[] Buffer = new Byte[Length];
            fixed (Byte* pData = Data)
                Kernel.memcpy(Buffer, pData, Length);
            Client.Send(Buffer);
            Buffer = null;
        }
    }
}