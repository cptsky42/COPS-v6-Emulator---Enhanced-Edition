--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:41 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10021(client, idx)
    name = "ArenaGuard"
    face = 1

    if (idx == 0) then

        text(client, "The arena is open. Welcome to challenge other people. The admission fee is only 50 silver.")
        text(client, "If you PK in the arena, you will not gain or lose any experience or equipment, and you will get revived at the place you die.")
        text(client, "The Kungfu circle is very dangerous, I suggest you PK in area.")
        link(client, "Enter the arena.", 1)
        link(client, "Just passing by.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 1) then

        if spendMoney(client, 50) then

            move(client, 1005, 51, 68)

        else

            text(client, "Sorry, you do not have 50 silver.")
            link(client, "I see.", 255)
            link(client, "Just passing by.", 255)
            pic(client, 7)
            create(client)

        end

    end

end
