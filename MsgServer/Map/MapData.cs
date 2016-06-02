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
using System.Text;

namespace COServer
{
    /// <summary>
    /// Represent a map's passage to another one.
    /// </summary>
    public struct Passage
    {
        /// <summary>
        /// The X position of the passage.
        /// </summary>
        public UInt16 PosX;
        /// <summary>
        /// The Y position of the passage.
        /// </summary>
        public UInt16 PosY;
        /// <summary>
        /// The index of the passage.
        /// </summary>
        public int Index;
    }

    public class MapData
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(MapData));

        private const int MAX_PATH = 260;

        private const int MAP_TERRAIN = 1;
        private const int MAP_TERRAIN_PART = 2;
        private const int MAP_SCENE = 3;
        private const int MAP_COVER = 4;
        private const int MAP_ROLE = 5;
        private const int MAP_HERO = 6;
        private const int MAP_PLAYER = 7;
        private const int MAP_PUZZLE = 8;
        private const int MAP_3DSIMPLE = 9;
        private const int MAP_3DEFFECT = 10;
        private const int MAP_2DITEM = 11;
        private const int MAP_3DNPC = 12;
        private const int MAP_3DOBJ = 13;
        private const int MAP_3DTRACE = 14;
        private const int MAP_SOUND = 15;
        private const int MAP_2DREGION = 16;
        private const int MAP_3DMAGICMAPITEM = 17;
        private const int MAP_3DITEM = 18;
        private const int MAP_3DEFFECTNEW = 19;

        /// <summary>
        /// The version of the file format.
        /// </summary>
        private UInt32 mVersion;
        private UInt32 mData;

        /// <summary>
        /// The width (number of cell) of the map.
        /// </summary>
        private UInt16 mWidth;
        /// <summary>
        /// The height (number of cell) of the map.
        /// </summary>
        private UInt16 mHeight;
        /// <summary>
        /// All the cells of the map.
        /// </summary>
        private UInt16[,] mCells;
        /// <summary>
        /// All the passages of the map.
        /// </summary>
        private List<Passage> mPassages;

        /// <summary>
        /// The width (number of cell) of the map.
        /// </summary>
        public UInt16 Width { get { return mWidth; } }
        /// <summary>
        /// The height (number of cell) of the map.
        /// </summary>
        public UInt16 Height { get { return mHeight; } }
        /// <summary>
        /// All the cells of the map.
        /// </summary>
        public UInt16[,] Cells { get { return mCells; } }
        /// <summary>
        /// All the passages of the map.
        /// </summary>
        public Passage[] Passages { get { return mPassages.ToArray(); } }

        /// <summary>
        /// Load a DMap as a MapData object containing cells and passages.
        /// </summary>
        /// <param name="aOutdata">A reference to the outputed MapData</param>
        /// <param name="aPath">The path of the DMap file</param>
        /// <returns>True on success, false otherwise.</returns>
        public static bool Load(out MapData aOutdata, String aPath)
        {
            bool success = true;

            MapData data = new MapData();

            if (File.Exists(aPath))
            {
                using (FileStream stream = new FileStream(aPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.GetEncoding(1252)))
                    {
                        success = data.LoadMapData(reader);
                    }
                }
            }
            else
            {
                sLogger.Error("Can't load the DMap file at {0} as the file doesn't exist.", aPath);
                success = false;
            }

            aOutdata = success ? data : null;
            return success;
        }

        private MapData()
        {
            mVersion = 0;
            mData = 0;

            mWidth = 0;
            mHeight = 0;
            mCells = null;
            mPassages = new List<Passage>();
        }

        /// <summary>
        /// Load the map data.
        /// </summary>
        private bool LoadMapData(BinaryReader aReader)
        {
            bool success = true;

            mVersion = aReader.ReadUInt32();
            mData = aReader.ReadUInt32();

            String strVersion = null;
            if (mVersion == 0x50414D44)
            {
                mVersion = 0;
                mData = 0;

                aReader.BaseStream.Seek(0, SeekOrigin.Begin);
                strVersion = Encoding.ASCII.GetString(aReader.ReadBytes(8)).Trim(new char[] { '\0' });
            }

            // skipping puzzle file...
            aReader.BaseStream.Seek(sizeof(SByte) * MAX_PATH, SeekOrigin.Current);

            UInt32 width = 0, height = 0;
            width = aReader.ReadUInt32();
            height = aReader.ReadUInt32();

            if (strVersion == null)
                sLogger.Info("TQ Digital Data Map : v{0} (data={1}) with [{2}, {3}] cells.",
                    mVersion, mData, width, height);
            else
                sLogger.Info("TQ Digital Data Map : {0} with [{1}, {2}] cells.",
                    strVersion, width, height);

            if (width < UInt16.MaxValue && height < UInt16.MaxValue)
            {
                mWidth = (UInt16)width;
                mHeight = (UInt16)height;
            }
            else
            {
                sLogger.Error("Size overflow for the map.");
                success = false;
            }

            mCells = new UInt16[mWidth, mHeight];
            for (UInt16 y = 0; success && y < mHeight; ++y)
            {
                UInt32 computedChecksum = 0, checksum = 0;
                for (UInt16 x = 0; success && x < mWidth; ++x)
                {
                    UInt16 mask = 0, terrain = 0;
                    Int16 altitude = 0;

                    mask = aReader.ReadUInt16();
                    terrain = aReader.ReadUInt16();
                    altitude = aReader.ReadInt16();

                    computedChecksum += (UInt32)((mask * (terrain + y + 1)) +
                        ((altitude + 2) * (x + 1 + terrain)));

                    // to reduce memory usage, insert a small imperfection in the height value
                    mCells[x, y] = (UInt16)((altitude & 0xFFFE) | (mask != 1 ? 1 : 0));
                }
                checksum = aReader.ReadUInt32();

                if (computedChecksum != checksum)
                {
                    sLogger.Error("Invalid checksum for the block of cells for the position y={0}", y);
                    success = false;
                }
            }

            if (success)
                success = LoadPassageData(aReader);

            //if (success && version == 1003)
            //    success = LoadRegionData(aReader);

            if (success)
                success = LoadLayerData(aReader);

            // The rest are LAYER_SCENE, but useless for a server.
            // I'll not implement the rest as it would only slow down the loading.

            return success;
        }

        /// <summary>
        /// Load all passages data.
        /// </summary>
        private bool LoadPassageData(BinaryReader aReader)
        {
            bool success = true;

            Int32 count = aReader.ReadInt32();

            sLogger.Info("Found {0} passages for the map.", count);

            for (Int32 i = 0; success && i < count; ++i)
            {
                Passage passage = new Passage();
                UInt32 posX = 0, posY = 0;

                posX = aReader.ReadUInt32();
                posY = aReader.ReadUInt32();
                passage.Index = aReader.ReadInt32();

                if (posX < UInt16.MaxValue && posX < UInt16.MaxValue)
                {
                    passage.PosX = (UInt16)posX;
                    passage.PosY = (UInt16)posY;
                }
                else
                {
                    sLogger.Error("Size overflow for the position of the passage.");
                    success = false;
                }

                if (success)
                {
                    sLogger.Debug("Added passage {0} at ({1}, {2}).",
                        passage.Index, passage.PosX, passage.PosY);
                    mPassages.Add(passage);
                }
            }

            return success;
        }

        /// <summary>
        /// Load all regions data.
        /// </summary>
        private bool LoadRegionData(BinaryReader aReader)
        {
            bool success = true;

            Int32 count = aReader.ReadInt32();

            sLogger.Info("Found {0} regions for the map.", count);

            if (success)
            {
                sLogger.Warn("Regions are not supported yet.");
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Load all layers data.
        /// </summary>
        private bool LoadLayerData(BinaryReader aReader)
        {
            bool success = true;

            Int32 count = aReader.ReadInt32();

            sLogger.Info("Found {0} layers for the map.", count);

            for (Int32 i = 0; success && i < count; ++i)
            {
                Int32 type = aReader.ReadInt32();

                switch (type)
                {
                    case MAP_COVER: // 2DMapCoverObj
                        {
                            // Do nothing with it...
                            sLogger.Debug("Found a 2D map cover object layer. Skipping.");
                            aReader.BaseStream.Seek(416, SeekOrigin.Current);

                            if (mVersion == 1005)
                                aReader.BaseStream.Seek(4, SeekOrigin.Current);

                            break;
                        }
                    case MAP_TERRAIN: // 2DMapTerrainObj
                        {
                            // A class could be good... But useless for a server implementation.

                            String filename = Encoding.GetEncoding(1252).GetString(aReader.ReadBytes(MAX_PATH)).TrimEnd();
                            filename = filename.Replace('\\', '/');

                            // Stupid TQ can't properly zero-fill their buffer in C/C++...
                            int index = filename.IndexOf('\0');
                            if (index != -1)
                                filename = filename.Substring(0, index);

                            UInt32 startX = 0, startY = 0;
                            startX = aReader.ReadUInt32();
                            startY = aReader.ReadUInt32();

                            sLogger.Debug("Found a 2D map terrain object at ({0}, {1}). Loading scene file '{2}'",
                                startX, startY, filename);

                            if (File.Exists(filename))
                            {
                                using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                                {
                                    using (BinaryReader reader = new BinaryReader(stream))
                                    {
                                        Int32 partsCount = reader.ReadInt32();
                                        sLogger.Debug("Found {0} parts.", partsCount);

                                        // the server only need the new cells info, so it will be merged
                                        // and the objects will be deleted...
                                        // it is useless to parse most of the parts info...
                                        for (Int32 j = 0; success && j < partsCount; ++j) // all parts
                                        {
                                            UInt32 width = 0, height = 0;
                                            UInt32 sceneOffsetX = 0, sceneOffsetY;
                                            reader.BaseStream.Seek(256, SeekOrigin.Current); // AniFile
                                            reader.BaseStream.Seek(64, SeekOrigin.Current); // AniTitle
                                            reader.BaseStream.Seek(sizeof(UInt32), SeekOrigin.Current); // PosOffset(X)
                                            reader.BaseStream.Seek(sizeof(UInt32), SeekOrigin.Current); // PosOffset(Y)
                                            reader.BaseStream.Seek(sizeof(UInt32), SeekOrigin.Current); // AniInterval
                                            width = reader.ReadUInt32();
                                            height = reader.ReadUInt32();
                                            reader.BaseStream.Seek(sizeof(Int32), SeekOrigin.Current); // Thick
                                            sceneOffsetX = reader.ReadUInt32();
                                            sceneOffsetY = reader.ReadUInt32();
                                            reader.BaseStream.Seek(sizeof(Int32), SeekOrigin.Current); // HeightOffset

                                            for (UInt32 y = 0; success && y < height; ++y)
                                            {
                                                for (UInt32 x = 0; success && x < width; ++x)
                                                {
                                                    UInt32 mask = 0, terrain = 0;
                                                    Int32 altitude = 0;

                                                    mask = reader.ReadUInt32();
                                                    terrain = reader.ReadUInt32();
                                                    altitude = reader.ReadInt32();

                                                    UInt32 posX = ((startX + sceneOffsetX) + x) - width;
                                                    UInt32 posY = ((startY + sceneOffsetY) + y) - height;
                                                    if (posX < UInt16.MaxValue && posX < UInt16.MaxValue)
                                                    {
                                                        // to reduce memory usage, insert a small imperfection in the height value
                                                        mCells[posX, posY] = (UInt16)((altitude & 0xFFFE) | (mask != 0 ? 0: 1));
                                                    }
                                                    else
                                                    {
                                                        sLogger.Error("Size overflow for the position of the scene part.");
                                                        success = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                sLogger.Error("Can't load the scene file at '{0}' as the file doesn't exist.",
                                    filename);
                                success = false;
                            }

                            break;
                        }
                    case MAP_SOUND: // MapSound
                        {
                            // Do nothing with it...
                            sLogger.Debug("Found a map sound layer. Skipping.");
                            aReader.BaseStream.Seek(276, SeekOrigin.Current);
                            break;
                        }
                    case MAP_3DEFFECT: // 3DMapEffect
                        {
                            // Do nothing with it...
                            sLogger.Debug("Found a 3D map effect layer. Skipping.");
                            aReader.BaseStream.Seek(72, SeekOrigin.Current);
                            break;
                        }
                    case MAP_3DEFFECTNEW: // 3DMapEffectNew
                        {
                            // Do nothing with it...
                            sLogger.Debug("Found a 3D map effect (new) layer. Skipping.");
                            aReader.BaseStream.Seek(96, SeekOrigin.Current);
                            break;
                        }
                    default:
                        {
                            sLogger.Error("Found a layer of type {0} at offset {1}, which is unknown.",
                                type, aReader.BaseStream.Position);
                            success = false;
                            break;
                        }
                }
            }

            return success;
        }
    }
}
