--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:44 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30001(client, idx)
    name = "GlumMonster"
    face = 1

    if (idx == 0) then

        if getLevel(client) < 91 then

            text(client, "This is 1st pass. I may teleport you to the next pass directly, or you will be teleported to the battle stage. Are you ready?")
            link(client, "Yeah.", 1)
            link(client, "Let me think it over.", 255)
            pic(client, 9)
            create(client)

        else

            text(client, "This floor is for those who are under level 90.")
            link(client, "I see.", 255)
            pic(client, 9)
            create(client)

        end

    elseif (idx == 1) then

        if (rand(client, 7) < 1) then

            move(client, 1040, 544, 331)
            text(client, "Congratulations! You have passed the first test. I am pleased to teleport you to the next pass.")
            link(client, "Thanks.", 255)
            pic(client, 9)
            create(client)

        else

            move(client, 1040, 469, 488)

        end

    end

end
