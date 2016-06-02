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

function useItem725001(self, client)
    name = "Fire"
    face = 1

    if getSoul(client) < 80 then

        text(client, "Sorry, you cannot learn this spell before you are spirit 80. Please train harder.")
        link(client, "I see.", 255)
        pic(client, 3)
        create(client)

    else

        if hasMagic(client, 1001, -1) then

            text(client, "You have learned Astral Fire bolt.")
            link(client, "I see.", 255)
            pic(client, 3)
            create(client)

        else

            if hasMagic(client, 1000, 4) then

                awardMagic(client, 1001, 0)
                text(client, "You have learned Fire.")
                link(client, "I see.", 255)
                pic(client, 3)
                create(client)
                deleteItem(self)

            else

                text(client, "Sorry, you cannot learn this spell before you practice Thunder to level 4.")
                link(client, "I see.", 255)
                pic(client, 3)
                create(client)

            end

        end

    end

end
