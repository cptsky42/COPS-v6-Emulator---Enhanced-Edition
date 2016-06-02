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

function processTask30044(client, idx)
    name = "TerminalGuard"
    face = 1

    if (idx == 0) then

        if getLevel(client) < 100 then

            text(client, "This floor is for those who are above level 100.")
            link(client, "I see.", 255)
            pic(client, 9)
            create(client)

        else

            text(client, "Welcome! This is the last floor. You are lucky to make it so far. It is tougher ahead. Do you want a try?")
            link(client, "Sure!", 1)
            pic(client, 9)
            create(client)

        end

    elseif (idx == 1) then

        text(client, "But you have to fight in the stage for a Pass Token. The guard may send you to claim the prize or send you back to me.")
        link(client, "Ok, let`s get started.", 2)
        link(client, "Wait a moment.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 2) then

        move(client, 1040, 204, 367)

    end

end
