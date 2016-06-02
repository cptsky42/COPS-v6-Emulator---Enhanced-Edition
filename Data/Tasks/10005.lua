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

function processTask10005(client, idx)
    name = "Blacksmith"
    face = 1

    if (idx == 0) then

        text(client, "I am selling different weapons in different city. In order to slay the enemies, you had better equip the best weapons.")
        link(client, "How to buy and sell?", 1)
        link(client, "Consult others.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 1) then

        text(client, "Before you buy an item, you`d better check its stats. Red stats means you cannot wear it until all stats are white.")
        text(client, "Click on me to open my shop window, and then right click on an items to buy it. To equip it, just right click on it.")
        text(client, "If you want to sell an item, you may click on me, then drag and drop your item into my slots. I have multipage items.")
        link(client, "How to repair?", 2)
        link(client, "Consult others.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 2) then

        text(client, "Disarm your item, click on a shopkeeper, then click on repair button and your item. The better the quality, the higher the fee.")
        link(client, "What are super items?", 3)
        link(client, "Consult others.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 3) then

        text(client, "Items are graded as normal, refined, unique, elite and super. NPC sells only normal items. Mobs gives better ones.")
        text(client, "That is all. If you have not talked to other NPCs, you had better have a chat with them so that you can learn more.")
        link(client, "Thanks.", 255)
        pic(client, 9)
        create(client)

    end

end
