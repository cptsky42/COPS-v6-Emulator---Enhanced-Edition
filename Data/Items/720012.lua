--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/21/2015 1:36:19 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function useItem720012(self, client)
    name = "Ginseng"
    face = 1

    if (getItemsCount(client) <= 37) then

        awardItem(client, "1002010", 1)
        awardItem(client, "1002010", 1)
        awardItem(client, "1002010", 1)
        spendItem(client, 720012, 1)

    else

        text(client, "Please leave 3 free slots in your inventory first.")
        link(client, "I see.", 255)
        pic(client, 9)
        create(client)

    end

end
