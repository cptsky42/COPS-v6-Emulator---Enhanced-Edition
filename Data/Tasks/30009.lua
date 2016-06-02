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

function processTask30009(client, idx)
    name = "StageGuard"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721100, 1) then

            text(client, "Since you get the token, I will teleport you to the starting point to continue the challenge. Are you ready?")
            link(client, "Yes. I am.", 1)
            link(client, "Wait a moment.", 255)
            pic(client, 7)
            create(client)

        else

            text(client, "Only you fight in the stage and get a Pass Token, can I send you back to challenge.")
            link(client, "I see.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "PassToken1") and (getMoney(client) >= 0) then

            spendItem(client, 721100, 1)
            move(client, 1040, 595, 383)

        end

    end

end
