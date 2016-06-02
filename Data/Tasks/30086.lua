--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:48 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30086(client, idx)
    name = "BanditHead"
    face = 1

    if (idx == 0) then

        text(client, "It is our domain. If you do not want to die, you had better get out of here right now.")
        link(client, "Did you rob food carrier?", 1)
        link(client, "I got to go at once.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 1) then

        text(client, "We dare not rob the army. Only the unarmed Trade Caravans are our target.")
        link(client, "Who might do it?", 2)
        pic(client, 9)
        create(client)

    elseif (idx == 2) then

        text(client, "There is another group of bandits here who are stronger and bigger than us. I am afraid it was them who did it.")
        link(client, "I trust you.", 255)
        pic(client, 9)
        create(client)

    end

end
