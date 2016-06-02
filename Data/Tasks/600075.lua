--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:59 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600075(client, idx)
    name = "BoxerHuang"
    face = 1

    if (idx == 0) then

        text(client, "Although nothing will be consumed here, you cannot level up as fast as slaying the monsters. Shall I teleport you out?")
        link(client, "Yes, please.", 1)
        link(client, "No, thanks.", 255)
        pic(client, 84)
        create(client)

    elseif (idx == 1) then

        move(client, getRecordMap(client), getRecordX(client), getRecordY(client))

    end

end
