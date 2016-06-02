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

function processTask300654(client, idx)
    name = "Boatman"
    face = 1

    if (idx == 0) then

        text(client, "I am here to lead the people out of the island. Are you leaving the island.")
        link(client, "Yeah.", 1)
        link(client, "Just passing by.", 255)
        pic(client, 12)
        create(client)

    elseif (idx == 1) then

        move(client, 1213, 448, 272)

    end

end
