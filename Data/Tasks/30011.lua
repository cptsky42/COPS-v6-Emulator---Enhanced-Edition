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

function processTask30011(client, idx)
    name = "GreenSnake"
    face = 1

    if (idx == 0) then

        if getLevel(client) < 91 then

            text(client, "This is the second floor. I may teleport you to the next floor directly, or to the battle stage. Are you ready?")
            link(client, "Yes. I am.", 1)
            link(client, "Wait a moment.", 255)
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

            move(client, 1040, 492, 281)
            text(client, "Congratulations! You passed! I will send you to the next floor.")
            link(client, "Thanks.", 255)
            pic(client, 9)
            create(client)

        else

            move(client, 1040, 421, 440)

        end

    end

end
