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

function processTask300652(client, idx)
    name = "MineSupervisor"
    face = 1

    if (idx == 0) then

        text(client, "Many kinds of Gems are buried in this mine, like Dragon, Phoenix, Rainbow and Kylin Gems. Will you enter this mine?")
        link(client, "Yeah.", 1)
        link(client, "Just passing by.", 255)
        pic(client, 12)
        create(client)

    elseif (idx == 1) then

        if getLevel(client) < 40 then

            text(client, "You cannot enter this mine before you are level 40 or above.")
            link(client, "I see.", 255)
            pic(client, 12)
            create(client)

        else

            move(client, 1218, 29, 70)

        end

    end

end
