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

function processTask30022(client, idx)
    name = "EvilHawk"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721154, 1) then

            text(client, "Oh, you were sent by Tientsin. Congratulations! This is the third floor. I will give you the PassLetter.")
            link(client, "Thanks.", 1)
            pic(client, 9)
            create(client)

        else

            if getLevel(client) < 110 then

                text(client, "Please come after your level is above 110.")
                link(client, "I see.", 255)
                pic(client, 9)
                create(client)

            else

                text(client, "This is the third floor. I may teleport you to the next pass directly, or to the battle stage. Are you ready?")
                link(client, "Yes, I am.", 2)
                link(client, "Wait a moment.", 255)
                pic(client, 9)
                create(client)

            end

        end

    elseif (idx == 1) then

        spendItem(client, 721154, 1)
        awardItem(client, "721155", 1)
        text(client, "You deserve it. Will you stay to challenge the Sky Pass or will you leave?")
        link(client, "I will continue the SkyPass.", 255)
        link(client, "I will leave here.", 3)
        pic(client, 9)
        create(client)

    elseif (idx == 2) then

        if (rand(client, 3) < 1) then

            move(client, 1040, 436, 224)
            text(client, "Congratulations! You passed! I will send you to the next floor.")
            link(client, "Thanks.", 255)
            pic(client, 9)
            create(client)

        else

            move(client, 1040, 277, 488)

        end

    elseif (idx == 3) then

        move(client, 1012, 170, 158)

    end

end
