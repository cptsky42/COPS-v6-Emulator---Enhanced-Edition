# COPS v6 Emulator - Enhanced Edition
Copyright © 2010 - 2012, 2014 - 2015 Jean-Philippe Boivin

Overview
--------

COPS v6 Emulator is a C# emulator of the MMORPG Conquer Online 2.0, targeting the version 5017. The emulator is developed using Visual Studio 2010 and the .NET framework v4.0.

Originally developed for COPS v6 - The Return of the Legend between 2010 and 2012, the source has been released to the public in 2013, a year after the shutdown of the server. In late 2014, I (JP) decided to rework the source following my work on COPS v7 Emulator. The new version is now targetting the version 4330 of the game.

Features
--------

COPS v6 Emulator is a fully featured server.

What's New?
-----------

What's new in the enhanced version ?

Globally :

- Follow only one naming convention.
- Code is fully documented.
- All P/Invokes are dropped to run on Mono (it should work).
- All useless unsafe code is removed.
- All code from the CO2_CORE_DLL library has been added directly to the project and refactored.
- All message classes has been refactored to use an inheritance model and accessors.
- The multithreading (and thread-safeness) has been revisited.
- The code has been revisited to use Linq when useful.
- A real logging system is available (using log4net).
- The servers can be ran as a service (using the ServiceStub process).


AccServer :

- The painful SVR file format has been removed. INI files are used instead.
- The server is now storing accounts in a MySQL database instead of XML files.
- The passwords are now securely stored using PBKDF2.
- The server is now separated from the game servers (the token is passed through MySQL).
- The server is supporting IP2Location databases for geolocalization of players.


MsgServer :

+ Database module
  - The server is now using MySQL instead of XML files.
+ Scripting module
  - The scripting engine has been converted to Lua, instead of a proprietary format (KCOSS, KCS).
  - The scripting engine now covers NPCs, monsters (on death), items, *events* and *traps*.
  - All scripts are based on the cq_action and cq_task tables of TQ Digital.
+ Map module
  - A proper map manager has been implemented.
  - The DMap parsing is now done by the server itself.
    * Parallel loading of DMap files
    * Shared data among all game maps
+ Generator module
  - The generator module is now based on the algorithm used by TQ Digital
+ Items
  - Items are now scripted instead of being hard-coded.
  - Dropped items are now generated based on the algorithm used by TQ Digital.
+ Guilds
  - Guilds have been fully reimplemented.
  - Allies, enemies and branches are now supported for guilds.
+ StrRes
  - String resources are now updated using reflection instead of a less efficient dictionnary.
  
Note 1. The database is based on the COPS v7 Emulator layout. It means that it could be used with COPS v7 Emulator with minor changes. As the COPS v7 Emulator database's layout is based on TQ's one, a server using TQ binaries could relatively easily be ported to COPS v6 Emulator too.

Note 2. The Lua scripting system is based on the base implemented in the COPS v7 Emulator. Although it is way more complete, it should be possible to reimplement the new functions in COPS v7 Emulator and run the scripts in any emulator.

Supported systems
-----------------

The emulator has been tested on Microsoft Windows 7 and Microsoft Windows Server 2008 R2.
It should work on any system supporting the .NET framework or the Mono framework.
