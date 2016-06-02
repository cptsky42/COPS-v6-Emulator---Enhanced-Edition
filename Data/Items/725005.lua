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

function useItem725005(self, client)
    name = "FastBlade"
    face = 1

    if hasSkill(client, 410, 5) then

        if hasMagic(client, 1045, -1) then

            text(client, "You have learned Fast Attack.")
            link(client, "I see.", 255)
            pic(client, 3)
            create(client)

        else

            awardMagic(client, 1045, 0)
            text(client, "You have learned Fast Blade.")
            link(client, "I see.", 255)
            pic(client, 3)
            create(client)
            deleteItem(self)

        end

    else

        text(client, "Sorry, you cannot learn this skill before you practice your blade to level 5. Please train harder.")
        link(client, "I see.", 255)
        pic(client, 3)
        create(client)

    end

end
