--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:45 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30061(client, idx)
    name = "GuardingKid"
    face = 1

    if (idx == 0) then

        text(client, "You are great to make it so far. Only one of us will send you to claim the prize. Please believe me.")
        link(client, "Alright. I believe you.", 1)
        link(client, "Let me think it over.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 1) then

        if (rand(client, 2) < 1) then

            moveNpc(client, 30061, 1041, 512, 322)
            move(client, 1041, 449, 247)
            text(client, "It will not be easy to claim the prize. The monsters ahead are tougher. Be careful.")
            link(client, "Thanks for reminding.", 255)
            pic(client, 10)
            create(client)

        else

            move(client, 1041, 449, 247)
            text(client, "It will not be easy to claim the prize. The monsters ahead are tougher. Be careful.")
            link(client, "Thanks for reminding.", 255)
            pic(client, 10)
            create(client)

        end

    end

end
