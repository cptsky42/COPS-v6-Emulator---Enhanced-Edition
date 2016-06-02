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

function useItem720028(self, client)
    name = "DBScroll"
    face = 1

    if (getItemsCount(client) <= 30) then

        spendItem(client, 720028, 1)

        awardItem(client, "1088000", 1)

        awardItem(client, "1088000", 1)

        awardItem(client, "1088000", 1)

        awardItem(client, "1088000", 1)

        awardItem(client, "1088000", 1)

        awardItem(client, "1088000", 1)

        awardItem(client, "1088000", 1)

        awardItem(client, "1088000", 1)

        awardItem(client, "1088000", 1)

        awardItem(client, "1088000", 1)

        sendSysMsg(client, "You open the DragonScroll and get 10 Dragonballs.", 2005)

    else

        text(client, "Please prepare 10 slots in your inventory before you open it.")
        link(client, "Ok, thanks.", 255)
        pic(client, 29)
        create(client)

    end

end
