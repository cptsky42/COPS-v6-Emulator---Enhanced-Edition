--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:50 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30158(client, idx)
    name = "Craftsman"
    face = 1

    if (idx == 0) then

        text(client, "What can I do for you?")
        link(client, "Do you have timber.", 1)
        link(client, "Just passing by.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 1) then

        text(client, "We are rebuilding the market and have stored a lot of timber.")
        link(client, "Can you give me some?", 2)
        link(client, "You have wasted too much.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 2) then

        text(client, "We are in urgent need of ores. You may exchange five ores for a piece of timber.")
        link(client, "Here are five ores.", 3)
        link(client, "I shall come later.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 3) then

        if hasItems(client, 1072010, 1072059, 5) then

            if spendItems(client, 1072010, 1072059, 5) then

                awardItem(client, "721171", 1)

            else

                text(client, "Sorry, you do not have five ores.")
                link(client, "I see.", 255)
                pic(client, 92)
                create(client)

            end

        else

            text(client, "Sorry, you do not have five ores.")
            link(client, "I see.", 255)
            pic(client, 92)
            create(client)

        end

    end

end
