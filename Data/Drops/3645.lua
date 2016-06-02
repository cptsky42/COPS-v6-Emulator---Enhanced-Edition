--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/10/2015 3:10:24 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function Monster3645_OnDie(self, client)
    name = "BeastSatan"

    spawnMonster(client, 3646, "FurySatan", 0, 0, 1700, 337, 341, 5606, 0)
    sendSysMsg(client, "Satan has taken the form of FurySatan.", 2005)

end
