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

function processTask300651(client, idx)
    name = "Explorer"
    face = 1

    if (idx == 0) then

        text(client, "This is the way to the dangerous areas. Are you sure you will go ahead?")
        link(client, "Yeah.", 1)
        link(client, "Just passing by.", 255)
        pic(client, 12)
        create(client)

    elseif (idx == 1) then

        move(client, 1205, 1351, 1198)

    end

end
