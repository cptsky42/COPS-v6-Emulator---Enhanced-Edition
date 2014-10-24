// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using AMS.Profile;

namespace COServer
{
    public partial class Database
    {
        public class CRates
        {
            public Double Refined;
            public Double Unique;
            public Double Elite;
            public Double Super;
            public Double Craft;
            public Double Meteor;
            public Double DragonBall;
            public Double Money;
            public Double CPs;
            public Double Exp;
            public Double Socket;

            public CRates()
            {
                Refined = 1.0;
                Unique = 1.0;
                Elite = 1.0;
                Super = 1.0;
                Craft = 1.0;
                Meteor = 1.0;
                DragonBall = 1.0;
                Money = 1.0;
                CPs = 0.0;
                Exp = 1.0;
                Socket = 1.0;
            }

            public CRates(Xml AMSXml)
            {
                Refined = Double.Parse(AMSXml.GetValue("Rates", "Refined", "1.0"));
                Unique = Double.Parse(AMSXml.GetValue("Rates", "Unique", "1.0"));
                Elite = Double.Parse(AMSXml.GetValue("Rates", "Elite", "1.0"));
                Super = Double.Parse(AMSXml.GetValue("Rates", "Super", "1.0"));
                Craft = Double.Parse(AMSXml.GetValue("Rates", "Craft", "1.0"));
                Meteor = Double.Parse(AMSXml.GetValue("Rates", "Meteor", "1.0"));
                DragonBall = Double.Parse(AMSXml.GetValue("Rates", "DragonBall", "1.0"));
                Money = Double.Parse(AMSXml.GetValue("Rates", "Money", "1.0"));
                CPs = Double.Parse(AMSXml.GetValue("Rates", "CPs", "1.0"));
                Exp = Double.Parse(AMSXml.GetValue("Rates", "Exp", "1.0"));
                Socket = Double.Parse(AMSXml.GetValue("Rates", "Socket", "1.0"));
            }
        }

        public static CRates Rates;
    }
}
