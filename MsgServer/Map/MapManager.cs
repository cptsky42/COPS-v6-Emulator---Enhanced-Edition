// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace COServer
{
    /// <summary>
    /// Global map manager.
    /// </summary>
    public class MapManager
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(MapManager));

        /// <summary>
        /// The unique ID of the prison map.
        /// </summary>
        public static UInt32 PRISON_MAP_UID = 6002;
        /// <summary>
        /// The unique ID of the newbie map.
        /// </summary>
        public static UInt32 NEWBIE_MAP_UID = 1010;

        /// <summary>
        /// All game maps.
        /// </summary>
        private static readonly Dictionary<UInt32, GameMap> sGameMaps = new Dictionary<UInt32, GameMap>();
        /// <summary>
        /// All map data based on the UID.
        /// </summary>
        private static readonly Dictionary<UInt16, MapData> sMaps = new Dictionary<UInt16, MapData>();
        /// <summary>
        /// All map data based on the files.
        /// </summary>
        private static readonly Dictionary<String, MapData> sData = new Dictionary<String, MapData>();

        /// <summary>
        /// The latest used map ID.
        /// </summary>
        private static UInt32 sLastMapId = 20000;

        /// <summary>
        /// Get the next available ID.
        /// </summary>
        public static UInt32 GetNextMapId()
        {
            lock (sGameMaps)
            {
                while (sGameMaps.ContainsKey(sLastMapId))
                {
                    ++sLastMapId;

                    if (sLastMapId == 25000)
                        sLastMapId = 20000;
                }

                return sLastMapId;
            }
        }

        /// <summary>
        /// Get a game map based on its ID.
        /// </summary>
        /// <param name="aMapId">The map ID.</param>
        /// <param name="aOutMap">The GameMap object if found</param>
        /// <returns>True on success, false otherwise.</returns>
        public static bool TryGetMap(UInt32 aMapId, out GameMap aOutMap)
        {
            lock (sGameMaps)
            {
                return sGameMaps.TryGetValue(aMapId, out aOutMap);
            }
        }

        /// <summary>
        /// Create a new game map with the specified information.
        /// </summary>
        /// <param name="aMapId">The unique ID of the map</param>
        /// <param name="aInfo">The object containing the info</param>
        /// <returns>True on success, false otherwise</returns>
        public static bool CreateMap(UInt32 aMapId, GameMap.Info aInfo)
        {
            bool success = true;

            lock (sGameMaps)
            {
                if (!sGameMaps.ContainsKey(aMapId))
                {
                    if (sMaps.ContainsKey(aInfo.DocId))
                    {
                        GameMap map = new GameMap(aMapId, aInfo, sMaps[aInfo.DocId]);
                        sGameMaps.Add(aMapId, map);

                        sLogger.Info("Created game map {0} with data of {1}.",
                            map.Id, map.DocId);
                    }
                    else
                    {
                        sLogger.Error("Missing map data for doc ID {0}. The map {1} won't be created.",
                            aInfo.DocId, aMapId);
                        success = false;
                    }
                }
                else
                {
                    sLogger.Error("Duplicated map {0}.", aMapId);
                    success = false;
                }
            }

            return success;
        }

        /// <summary>
        /// Destroy the specified map.
        /// </summary>
        /// <param name="aMapId">The unique ID of the map</param>
        /// <returns>True on success, false otherwise</returns>
        public static bool DestroyMap(UInt32 aMapId)
        {
            bool success = true;

            lock (sGameMaps)
            {
                if (sGameMaps.ContainsKey(aMapId))
                {
                    success = sGameMaps.Remove(aMapId);
                }
                else
                {
                    sLogger.Error("Map {0} does not exist.", aMapId);
                    success = false;
                }
            }

            return success;
        }

        /// <summary>
        /// Create a dynamic map based on another map.
        /// </summary>
        /// <param name="aMapId">The unique ID of the map</param>
        /// <param name="aRefUID">The unique ID of the reference map</param>
        /// <returns>True on success, false otherwise</returns>
        public static bool LinkMap(UInt32 aMapId, UInt32 aRefUID)
        {
            bool success = true;

            lock (sGameMaps)
            {
                if (!sGameMaps.ContainsKey(aMapId))
                {
                    if (sGameMaps.ContainsKey(aRefUID))
                    {
                        throw new NotImplementedException();

                        //DynMap map = new DynMap(aMapId, sGameMaps[aRefUID]);
                        //sGameMaps.Add(aMapId, map);

                        //sLogger.Info("Created dynamic game map {0} based on {1}.",
                        //    map.Id, aRefUID);
                    }
                    else
                    {
                        sLogger.Error("Map {0} does not exist.", aRefUID);
                        success = false;
                    }
                }
                else
                {
                    sLogger.Error("Duplicated map {0}.", aMapId);
                    success = false;
                }
            }

            return success;
        }

        /// <summary>
        /// Load all data from the data map files.
        /// </summary>
        public static bool LoadData()
        {
            bool success = true;

            Dictionary<String, List<UInt16>> maps = new Dictionary<String, List<UInt16>>();

            if (!File.Exists(Program.RootPath + "/Database/GameMap.dat"))
            {
                sLogger.Error("File '{0}' does not exist.",
                    Program.RootPath + "/Database/GameMap.dat");
                return false;
            }

            using (FileStream stream = new FileStream(Program.RootPath + "/Database/GameMap.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream, Encoding.GetEncoding(1252)))
                {
                    Int32 count = reader.ReadInt32();

                    for (Int32 i = 0; i < count; ++i)
                    {
                        UInt32 mapId = reader.ReadUInt32();
                        String filename = Encoding.GetEncoding(1252).GetString(reader.ReadBytes(reader.ReadInt32())).TrimEnd();
                        UInt32 puzzleSize = reader.ReadUInt32();

                        filename = Program.RootPath + "/" + filename;
                        filename = filename.Replace('\\', '/');

                        if (!maps.ContainsKey(filename))
                            maps.Add(filename, new List<UInt16>());
                        maps[filename].Add((UInt16)mapId);
                    }
                }
            }

            sLogger.Info("Detected {0} core(s). Spawning {0} workers for loading maps...",
                Environment.ProcessorCount);

            Task<bool>[] tasks = new Task<bool>[Environment.ProcessorCount];
            for (int i = 0; i < tasks.Length; ++i)
            {
                tasks[i] = new Task<bool>(LoadData, maps);
                tasks[i].Start();
            }

            Task.WaitAll(tasks);

            foreach (Task<bool> task in tasks)
                success = success && task.Result;

            return success;
        }

        /// <summary>
        /// Lock for the concurrent load of data maps.
        /// </summary>
        private static object sWorkLock = new object();

        /// <summary>
        /// Concurrent load of data maps.
        /// </summary>
        private static bool LoadData(object aState)
        {
            Dictionary<String, List<UInt16>> maps = (aState as Dictionary<String, List<UInt16>>);

            bool success = true;

            sLogger.Info("Worker {0} starting.",
                Thread.CurrentThread.ManagedThreadId);

            while (success)
            {
                String filename;
                List<UInt16> mapIds;

                lock (sWorkLock)
                {
                    if (maps.Count == 0)
                        break;

                    var first = maps.First();
                    maps.Remove(first.Key);

                    filename = first.Key;
                    mapIds = first.Value;
                }

                MapData data = null;
                success = MapData.Load(out data, filename);

                if (success)
                {
                    sLogger.Info("Loaded map data at '{0}'.", filename);

                    lock (sData)
                    {
                        sData.Add(filename, data);
                        foreach (UInt16 mapId in mapIds)
                        {
                            sLogger.Info("Found already loaded map data for id={0}. Using {1}.",
                                mapId, filename);

                            sMaps.Add(mapId, data);
                        }
                    }
                }
                else
                {
                    sLogger.Warn("Could not find all files for loading the map data file '{0}'. Ignoring error.",
                        filename);
                    success = true;
                }

                Thread.Yield();
            }

            sLogger.Info("Worker {0} done. (success={1})",
                Thread.CurrentThread.ManagedThreadId, success);

            return success;
        }
    }
}