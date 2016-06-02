--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:38 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask7000(client, idx)
    name = "GuildKeeper"
    face = 1

    if (idx == 0) then

        text(client, "Are you going to leave here?")
        link(client, "Yes.", 1)
        link(client, "No. Wait a moment.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 1) then

        move(client, 1002, 430, 380)

    end

end
