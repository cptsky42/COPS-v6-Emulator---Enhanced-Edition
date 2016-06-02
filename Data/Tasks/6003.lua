--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:37 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask6003(client, idx)
    name = "PrisonOfficer"
    face = 1

    if (idx == 0) then

        text(client, "Welcome, Welcome to our BotJail travel tour. If you are level 14 or below, I will show you to the BotJail to ")
        text(client, "take a look around for free. Do you wanna go?")
        text(client, "")
        link(client, "Yes, please send me there.", 1)
        link(client, "No thanks.", 255)
        pic(client, 57)
        create(client)

    elseif (idx == 1) then

        if getLevel(client) < 15 then

            move(client, 6002, 29, 72)
            setUserStats(client, 6, 2, getUserStats(6, 2) + 1, true)

        else

            text(client, "Sorry, you are level 15 or above, so I can not send you to the botjail.")
            link(client, "Oh, what a pity!", 255)
            pic(client, 57)
            create(client)

        end

    end

end
