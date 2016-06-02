--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:50 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30161(client, idx)
    name = "FurnitureNPC"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Welcome to Twin City Furniture Store. Currently, you have limited selection, but more furniture will come in soon.")
            link(client, "I wanna have a look.", 1)
            link(client, "I am not interested.", 255)
            pic(client, 188)
            create(client)

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            move(client, 1511, 52, 70)

        end

    end

end
