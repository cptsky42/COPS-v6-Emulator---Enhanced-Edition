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

function processTask10008(client, idx)
    name = "Pharmacist"
    face = 1

    if (idx == 0) then

        text(client, "Hi! I am selling all kinds of potions and City Gate Scrolls in the cities. I also sell fireworks and skill books in the market.")
        link(client, "What potions?", 1)
        link(client, "Consult others.", 255)
        pic(client, 1)
        create(client)

    elseif (idx == 1) then

        text(client, "Healing and mana potions. Healing potions can make you healthy, and mana potions will enable you to cast spells.")
        text(client, "That is all. If you have not talked to other NPCs, you had better have a chat with them so that you can learn more.")
        link(client, "I see. Thanks.", 255)
        pic(client, 1)
        create(client)

    end

end
