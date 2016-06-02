--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/21/2015 1:36:21 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function useItem725031(self, client)
    name = "Boreas"
    face = 1

    if hasMagic(client, 5050, -1) then

        text(client, "You have learned this skill.")
        link(client, "I see.", 255)
        pic(client, 3)
        create(client)

    else

        awardMagic(client, 5050, 0)
        text(client, "You`ve comprehended the elements of Boreas. As you gain more experiences in using poleaxe, it`ll be more and more powerful.")
        link(client, "I see.", 255)
        pic(client, 3)
        create(client)
        deleteItem(self)

    end

end
