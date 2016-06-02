--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:52 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask300653(client, idx)
    name = "Grandpa"
    face = 1

    if (idx == 0) then

        text(client, "You are on the off-island. If you want to leave this island, the Boatman near the in-island (381,32) may help you.")
        link(client, "Thanks.", 1)
        link(client, "Just passing by.", 255)
        pic(client, 12)
        create(client)

    elseif (idx == 1) then

        text(client, "The monsters on the in-island are very ferocious. Please be careful.")
        link(client, "Teleport me to in-island.", 2)
        pic(client, 12)
        create(client)

    elseif (idx == 2) then

        move(client, 1219, 1016, 1292)

    end

end
