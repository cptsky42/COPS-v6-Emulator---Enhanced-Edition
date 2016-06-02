--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/21/2015 1:36:20 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function useItem725016(self, client)
    name = "NightDevil"
    face = 1

    if getLevel(client) < 70 then

        text(client, "Sorry, you cannot learn this spell before you are level 70.")
        link(client, "I see.", 255)
        pic(client, 3)
        create(client)

    else

        if hasMagic(client, 1360, -1) then

            text(client, "You have learned this spell.")
            link(client, "I see.", 255)
            pic(client, 3)
            create(client)

        else

            awardMagic(client, 1360, 0)
            text(client, "You have learned Night Devil.")
            link(client, "I see.", 255)
            pic(client, 3)
            create(client)
            deleteItem(self)

        end

    end

end
