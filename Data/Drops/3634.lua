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

function Monster3634_OnDie(self, client)
    name = "Banshee"

    if getMetempsychosis(client) == 1 and getLevel(client) >= 120 then

        if getUserStats(client, 61, 0) >= 1 then


        else

            getUserStats(client, 61, 16) == 1
            setUserStats(client, 61, 16, 0, true)
            dropItem(self, client, 722729)

        end

    end

end
