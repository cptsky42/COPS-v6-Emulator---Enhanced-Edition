--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:13 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask1153(client, idx)
    name = "EastGeneral"
    face = 1

    if (idx == 0) then

        text(client, "I am EastGeneral. This is the first floor of Labyrinth. If you want to enter the next floor, you should give me a")
        text(client, "SkyToken. Or do you want to go back to Twin City?")
        link(client, "To the next floor.", 1)
        link(client, "To Twin City.", 2)
        link(client, "I will stay here.", 255)
        pic(client, 31)
        create(client)

    elseif (idx == 1) then

        if hasItem(client, 721537, 1) then

            text(client, "The next floor is more dangerous. You should be more careful.")
            link(client, "Thank you very much.", 3)
            pic(client, 31)
            create(client)

        else

            text(client, "You haven`t got a SkyToken, I cannot teleport you there.")
            link(client, "I will stay here.", 255)
            pic(client, 31)
            create(client)

        end

    elseif (idx == 2) then

        move(client, 1002, 430, 380)
        setRecordPos(client, 1002, 430, 380)

    elseif (idx == 3) then

        spendItem(client, 721537, 1)
        move(client, 1352, 27, 222)
        setRecordPos(client, 1002, 430, 380)

    end

end
