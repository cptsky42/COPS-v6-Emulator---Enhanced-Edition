--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 4/1/2015 7:36:05 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30000(client, idx)
    name = "Matchmaker"
    face = 1

    if (idx == 0) then

        text(client, "The fate brings lovers together. I hope that all can get married and live happily with their lover. What can I do for you?")
        link(client, "I want to get married.", 1)
        pic(client, 6)
        create(client)

    elseif (idx == 1) then

        text(client, "Marriage is the promise forever. Are you sure that you want to spend the rest of your life with your lover?")
        link(client, "I want to get married.", 2)
        link(client, "I prefer being single.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 2) then

        if (getMoney(client) >= 0) and (not isMarried(client)) then

            if getLevel(client) < 20 then

                text(client, "Sorry, you cannot get married before you are level 20 or above.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                text(client, "Are you ready to propose?")
                link(client, "Yeah. I am ready.", 3)
                link(client, "Let me think it over.", 255)
                pic(client, 6)
                create(client)

            end

        end

    elseif (idx == 3) then

        if (getMoney(client) >= 0) and (not isMarried(client)) then

            postCmd(client, 1067)
            text(client, "Please click on your lover to propose.")
            link(client, "OK.", 255)
            pic(client, 6)
            create(client)

        end

    end

end
