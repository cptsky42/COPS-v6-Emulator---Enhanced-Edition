--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:40 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10007(client, idx)
    name = "Armorer"
    face = 1

    if (idx == 0) then

        text(client, "Glad to meet you. I am selling different armors in different city. To level up quickly, you had better equip the best armors.")
        link(client, "How to buy and sell?", 1)
        link(client, "Consult others.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 1) then

        text(client, "Right click on an armor to buy it. Drag it to the shop window to sell it. Different armors give different stats.")
        text(client, "That is all. If you have not talked to other NPCs, you had better have a chat with them so that you can learn more.")
        link(client, "I see. Thanks.", 255)
        pic(client, 7)
        create(client)

    end

end
