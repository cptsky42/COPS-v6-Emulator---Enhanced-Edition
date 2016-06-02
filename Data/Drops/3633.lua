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

function Monster3633_OnDie(self, client)
    name = "SwiftDevil"

    if getMetempsychosis(client) == 1 and getLevel(client) >= 120 then

        if getUserStats(client, 61, 0) >= 1 then


        else

            if getUserStats(client, 61, 15) == 1 then

                setUserStats(client, 61, 15, 0, true)
                dropItem(self, client, 722726)

            end

        end

    end

end
