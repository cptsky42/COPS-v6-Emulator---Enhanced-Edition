--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:11 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask45(client, idx)
    name = "Mark.Controller"
    face = 1

    if (idx == 0) then

        text(client, "Do you want to leave the market? I can teleport you for free.")
        link(client, "Yeah. Thanks.", 1)
        link(client, "No, I shall stay here.", 255)
        pic(client, 156)
        create(client)

    elseif (idx == 1) then

        move(client, getRecordMap(client), getRecordX(client), getRecordY(client))

    end

end
