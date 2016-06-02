--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:11 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask420(client, idx)
    name = "Warden"
    face = 1

    if (idx == 0) then

        text(client, "What can I do for you?")
        link(client, "Please let me out.", 1)
        link(client, "Just passing by.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 1) then

        if checkTime(client, 5, "00 01") then

            move(client, 1002, 430, 380)
            text(client, "You can leave now. Please do not use macro or AFK processor any more, otherwise, your account may be deleted.")
            link(client, "I see.", 255)
            pic(client, 10)
            create(client)

        else

            if checkTime(client, 5, "30 31") then

                move(client, 1002, 430, 380)
                text(client, "You can leave now. Please do not use macro or AFK processor any more, otherwise, your account may be deleted.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            else

                text(client, "Sorry, I cannot release you. Please wait until it is time for half hourly pardon.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            end

        end

    end

end
