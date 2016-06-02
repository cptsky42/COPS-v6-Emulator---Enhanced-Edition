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

function useItem725002(self, client)
    name = "Tornado"
    face = 1

    if getSoul(client) < 160 then

        text(client, "Sorry, you cannot learn this spell before your Spirit is 160. Please train harder.")
        link(client, "I see.", 255)
        pic(client, 3)
        create(client)

    else

        if hasMagic(client, 1002, -1) then

            text(client, "You have learned Celestial Bolt.")
            link(client, "I see.", 255)
            pic(client, 3)
            create(client)

        else

            if hasMagic(client, 1001, 3) then

                awardMagic(client, 1002, 0)
                text(client, "You have learned Tornado.")
                link(client, "I see.", 255)
                pic(client, 3)
                create(client)
                deleteItem(self)

            else

                text(client, "Sorry, you cannot learn this spell before you practice Fire to level 3.")
                link(client, "I see.", 255)
                pic(client, 3)
                create(client)

            end

        end

    end

end
