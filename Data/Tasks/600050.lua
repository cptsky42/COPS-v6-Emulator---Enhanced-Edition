--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:55 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600050(client, idx)
    name = "Fortuneteller"
    face = 1

    if (idx == 0) then

        text(client, "Have you heard of Palace Method?")
        link(client, "Palace Method?", 1)
        link(client, "Just passing by.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 1) then

        text(client, "Are you interested? I would like to tell you more and hope you can work it out.")
        link(client, "Yeah, please go ahead.", 2)
        link(client, "Sorry, I am very busy.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 2) then

        text(client, "I discovered a mystic tactic a few days ago. I tried to work it out, but I failed and almost died from it.")
        link(client, "I`d like to have a try", 3)
        link(client, "Too dangerous.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 3) then

        text(client, "It is very dangerous. Are you sure you will try? If yes, I shall teleport you there. Talk to Maggie to enter the tactics.")
        link(client, "Yeah.", 4)
        link(client, "I changed my mind.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 4) then

        if getLevel(client) < 70 then

            text(client, "I don`t think your skill is strong enough to pass it. Do not take risks.")
            link(client, "I will come later.", 255)
            pic(client, 7)
            create(client)

        else

            if getLevel(client) < 95 then

                text(client, "You are very weak. I am afraid you will die from the tactics. Are you sure you will take this risk?")
                link(client, "Yeah.", 5)
                link(client, "I changed my mind.", 255)
                pic(client, 7)
                create(client)

            else

                move(client, 1042, 28, 33)

            end

        end

    elseif (idx == 5) then

        move(client, 1042, 28, 33)

    end

end
