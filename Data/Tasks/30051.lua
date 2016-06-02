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

function processTask30051(client, idx)
    name = "TerminalGuard"
    face = 1

    if (idx == 0) then

        if getLevel(client) < 80 then

            text(client, "If you want to challenge this floor, you must be between level 80 and 100.")
            link(client, "I see.", 255)
            pic(client, 9)
            create(client)

        else

            if getLevel(client) < 101 then

                text(client, "You have a good luck. This is the last pass. You must prove your ability before you get the big prize. Are you ready?")
                link(client, "Sure.", 1)
                pic(client, 9)
                create(client)

            else

                text(client, "If you want to challenge this floor, you must be between level 80 and 100.")
                link(client, "I see.", 255)
                pic(client, 9)
                create(client)

            end

        end

    elseif (idx == 1) then

        text(client, "But you have to fight in the stage for a Pass Token. The guard may send you to claim the prize or send you back to me.")
        link(client, "Go ahead.", 2)
        link(client, "Wait a moment.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 2) then

        move(client, 1040, 252, 319)

    end

end
