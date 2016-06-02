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

function useItem721540(self, client)
    name = "AncestorBox"
    face = 1

    if (getItemsCount(client) <= 36) then

        spendItem(client, 721540, 1)
        action = randomAction(client, 1, 8)
        if action == 1 then
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
        elseif action == 2 then
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
        elseif action == 3 then
            awardItem(client, "1088000", 1)
        elseif action == 4 then
            awardItem(client, "700031", 1)
        elseif action == 5 then
            awardItem(client, "700001", 1)
        elseif action == 6 then
            awardItem(client, "700011", 1)
        elseif action == 7 then
            awardItem(client, "700062", 1)
        elseif action == 8 then
            awardItem(client, "700022", 1)
        end


    else

        sendSysMsg(client, "Please prepare at least four slots in your inventory.", 2005)

    end

end
