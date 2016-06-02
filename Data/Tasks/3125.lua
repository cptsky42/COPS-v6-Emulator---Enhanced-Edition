--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:28 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask3125(client, idx)
    name = "HighShelf"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Do you want this HighShelf? It costs only 8000 silvers.")
            link(client, "Yes.", 1)
            link(client, "No.", 255)
            pic(client, 188)
            create(client)

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 8000 then

                text(client, "You do not have 8000 silvers with you.")
                link(client, "I am sorry.", 255)
                pic(client, 188)
                create(client)

            else

                if (getItemsCount(client) <= 39) then

                    spendMoney(client, 8000)
                    awardItem(client, "721180", 1)
                    sendSysMsg(client, "You got a HighShelf", 2005)

                else

                    text(client, "Please prepare one slot in your inventory.")
                    link(client, "Alright.", 255)
                    pic(client, 188)
                    create(client)

                end

            end

        end

    end

end
