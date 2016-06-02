--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:43 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10054(client, idx)
    name = "GeneralPeace"
    face = 1

    if (idx == 0) then

        if getLevel(client) < 60 then

            text(client, "This is the way to Desert City. The monsters there are much stronger than you. Are you sure you want to go there?")
            link(client, "No, I shall not go.", 255)
            link(client, "Please teleport me there.", 1)
            pic(client, 7)
            create(client)

        else

            text(client, "This is the way to the Desert City. Although you are excellent, it is dangerous to go ahead.")
            link(client, "I see.", 1)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 1) then

        move(client, 1000, 971, 666)

    end

end
