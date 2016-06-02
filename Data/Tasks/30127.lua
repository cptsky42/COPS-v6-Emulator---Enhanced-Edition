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

function processTask30127(client, idx)
    name = "CellGuard"
    face = 1

    if (idx == 0) then

        text(client, "Do you want to leave?")
        link(client, "Yes.", 1)
        link(client, "I`d like to stay here.", 255)
        pic(client, 53)
        create(client)

    elseif (idx == 1) then

        move(client, 1061, 548, 297)

    end

end
