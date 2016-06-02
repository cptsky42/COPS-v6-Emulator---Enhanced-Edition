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

function useItem720027(self, client)
    name = "MeteorScroll"
    face = 1

    if (getItemsCount(client) <= 30) then

        spendItem(client, 720027, 1)

    else

        text(client, "Please prepare 10 slots in your inventory before you open it.")
        link(client, "Ok, thanks.", 255)
        pic(client, 29)
        create(client)

    end

end
