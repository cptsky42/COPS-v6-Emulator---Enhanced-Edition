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

function useItem720017(self, client)
    name = "ChantPill"
    face = 1

    if (getItemsCount(client) <= 37) then

        awardItem(client, "1002040", 1)
        awardItem(client, "1002040", 1)
        awardItem(client, "1002040", 1)
        spendItem(client, 720017, 1)

    else

        text(client, "Please leave 3 free slots in your inventory first.")
        link(client, "I see.", 255)
        pic(client, 9)
        create(client)

    end

end
