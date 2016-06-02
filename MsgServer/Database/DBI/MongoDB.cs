// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using MongoDB.Driver;

namespace COServer
{
    public static partial class Database
    {
        private static MongoClient sClient = null;
        private static MongoServer sServer = null;
        private static MongoDatabase sDatabase = null;

        public static bool SetupMongo(String aHost, String aDatabase, String aUsername, String aPassword)
        {
            try
            {
                sLogger.Debug("[MONGO] Trying to connect to {0}...", aHost);

                if (!String.IsNullOrWhiteSpace(aUsername) && !String.IsNullOrWhiteSpace(aPassword))
                    sClient = new MongoClient(String.Format("mongodb://{0}:{1}@{2}/{3}",
                        aUsername, aPassword, aHost, aDatabase));
                else
                    sClient = new MongoClient(String.Format("mongodb://{0}/{1}",
                        aHost, aDatabase));

                sServer = sClient.GetServer();
                sServer.Connect();

                sDatabase = sServer.GetDatabase("zfserver");

                return true;
            }
            catch (MongoConnectionException exc)
            {
                sLogger.Error("[MONGO] {0}", exc.Message);
                return false;
            }
        }

        public static MongoCollection<T> GetCollection<T>(String aName)
        {
            return sDatabase.GetCollection<T>(aName);
        }
    }
}
