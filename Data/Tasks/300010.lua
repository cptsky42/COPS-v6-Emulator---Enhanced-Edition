--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:52 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask300010(client, idx)
    name = "Exorcist"
    face = 1

    if (idx == 0) then

        text(client, "The Ancient Devil was sealed in this island, The seal`s power is very weak now. The devil is gonna wake up. Can you help us?")
        link(client, "How can I help you?", 1)
        link(client, "Just passing by.", 255)
        pic(client, 87)
        create(client)

    elseif (idx == 1) then

        text(client, "First, get the 5 Amulets. Each amulet is protected by a Guard of different professions. Only if you are of the same")
        text(client, "profession, can you challenge the Guard. So you had better ask your friend for help. After you gather the 5 Amulets,")
        text(client, "click on the yellow marks on the ground to bring out the devil and its guards. Enable PK mode to kill them. Will you help us?")
        link(client, "Yes. I shall try.", 2)
        link(client, "Let me think it over.", 255)
        pic(client, 87)
        create(client)

    elseif (idx == 2) then

        move(client, 1052, 191, 232)

    end

end
