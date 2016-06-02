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

function useItem725003(self, client)
    name = "Cure"
    face = 1

    if getSoul(client) < 30 then

        text(client, "Sorry, you cannot learn this spell before you are spirit 30. Please train harder.")
        link(client, "I see.", 255)
        pic(client, 3)
        create(client)

    else

        if hasMagic(client, 1005, -1) then

            text(client, "You have learned Cure.")
            link(client, "I see.", 255)
            pic(client, 3)
            create(client)

        else

            awardMagic(client, 1005, 0)
            text(client, "You have learned Cure.")
            link(client, "I see.", 255)
            pic(client, 3)
            create(client)
            deleteItem(self)

        end

    end

end
