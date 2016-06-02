--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:13 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask1156(client, idx)
    name = "NorthGeneral"
    face = 1

    if (idx == 0) then

        text(client, "I am NorthGeneral. This is the fourth floor. If you want to go back to Twin City. I can help you.")
        link(client, "To Twin City.", 1)
        link(client, "I will stay here.", 255)
        pic(client, 31)
        create(client)

    elseif (idx == 1) then

        move(client, 1002, 430, 380)
        setRecordPos(client, 1002, 430, 380)

    end

end
