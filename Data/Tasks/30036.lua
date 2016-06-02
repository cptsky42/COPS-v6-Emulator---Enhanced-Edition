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

function processTask30036(client, idx)
    name = "StageGuard"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721103, 1) then

            text(client, "With this pass token, you may challenge the sky pass again. Would you like me to teleport you back?")
            link(client, "Yes, please.", 1)
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

        if hasTaskItem(client, "PassToken4") and (getMoney(client) >= 0) then

            spendItem(client, 721103, 1)
            move(client, 1040, 436, 224)

        end

    end

end
