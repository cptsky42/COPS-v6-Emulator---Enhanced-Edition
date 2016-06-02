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

function processTask126(client, idx)
    name = "Assistant"
    face = 1

    if (idx == 0) then

        text(client, "Are you heading for silver mine? The rock monsters are very fierce. You cannot enter this cave before you are level 70.")
        link(client, "Please teleport me there.", 1)
        link(client, "Just passing by.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 1) then

        if getLevel(client) < 70 then

            text(client, "Sorry, you are not allowed to enter this cave before you are level 70.")
            link(client, "I see.", 255)
            pic(client, 9)
            create(client)

        else

            move(client, 1026, 138, 103)

        end

    end

end
