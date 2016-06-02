--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:52 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask350050(client, idx)
    name = "CelestialTao"
    face = 1

    if (idx == 0) then

        text(client, "Reborn level 70+ players can redistribute their attribute points at the cost of a dragonball.")
        link(client, "I will reallot my points.", 1)
        link(client, "Let me think it over.", 255)
        pic(client, 29)
        create(client)

    elseif (idx == 1) then

        if getMetempsychosis(client) == 0 then

            text(client, "You cannot reallot your ability points unless you are reborn and above level 70.")
            link(client, "I see.", 255)
            pic(client, 29)
            create(client)

        else

            if getLevel(client) < 70 then

                text(client, "Although you are reborn, you have not reached level 70. Please put in more efforts.")
                link(client, "I see.", 255)
                pic(client, 29)
                create(client)

            else

                if hasItem(client, 1088000, 1) then

                    moveToRebornMap(client)
                    spendItem(client, 1088000, 1)
                    text(client, "Congratulations! You can reallot your ability points.")
                    link(client, "Thanks.", 255)
                    pic(client, 29)
                    create(client)

                else

                    text(client, "Sorry, you do not have a dragonball.")
                    link(client, "I see.", 255)
                    pic(client, 29)
                    create(client)

                end

            end

        end

    end

end
