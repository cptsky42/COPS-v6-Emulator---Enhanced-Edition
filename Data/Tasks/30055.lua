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

function processTask30055(client, idx)
    name = "Daniel"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721109, 1) then

            text(client, "Congratulations! You have gained the prize token of Sky Pass. I wil send you to claim the prize.")
            link(client, "Thanks.", 1)
            pic(client, 8)
            create(client)

        else

            text(client, "This is the way to Sky Pass. Are you here to challenge the Sky Pass?")
            link(client, "Please show me the way.", 2)
            link(client, "Just passing by.", 255)
            pic(client, 8)
            create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "SkyPrizeToken") and (getMoney(client) >= 0) then

            move(client, 1040, 192, 250)

        end

    elseif (idx == 2) then

        text(client, "The Sky Pass was designed by my master Maggie. She trapped the strongest monsters in the Pass to test out the real hero.")
        link(client, "Can I have a try?", 3)
        pic(client, 8)
        create(client)

    elseif (idx == 3) then

        text(client, "Sure. Everyone has a chance to challenge the pass. If you pass 5 floors at a time, you will see our great master.")
        link(client, "What does it look like?", 4)
        link(client, "I got to go. Goodbye.", 255)
        pic(client, 8)
        create(client)

    elseif (idx == 4) then

        text(client, "The Pass consists of 5 floors, and each floor is guarded by the monsters of different level.")
        link(client, "How can I pass?", 5)
        pic(client, 8)
        create(client)

    elseif (idx == 5) then

        text(client, "If you are lucky enough, the guard will teleport you to the next floor; or you will be teleported to the battle stage.")
        link(client, "Give me some advice.", 6)
        pic(client, 8)
        create(client)

    elseif (idx == 6) then

        text(client, "Player of level 90 or above can choose from two battle stages. It is more possible to bypass the battle in the tougher stage.")
        link(client, "I would like to try.", 7)
        link(client, "I see. Thanks. Bye.", 255)
        pic(client, 8)
        create(client)

    elseif (idx == 7) then

        move(client, 1040, 595, 383)
        setRecordPos(client, 1020, 566, 565)

    end

end
