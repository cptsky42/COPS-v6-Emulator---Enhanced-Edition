--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:11 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask43(client, idx)
    name = "CaptainLi"
    face = 1

    if (idx == 0) then

        text(client, "What can I do for you?")
        link(client, "Visit the jail.", 1)
        link(client, "Just passing by.", 255)
        pic(client, 2)
        create(client)

    elseif (idx == 1) then

        text(client, "Give me 1000 silver, I will teleport you there. If your PK points are 100+, you will be put into the jail.")
        link(client, "Here are 1000 silver.", 2)
        link(client, "If so, I will stay here.", 255)
        pic(client, 2)
        create(client)

    elseif (idx == 2) then

        spendMoney(client, 1000)
        move(client, 6000, 29, 72)

    end

end
