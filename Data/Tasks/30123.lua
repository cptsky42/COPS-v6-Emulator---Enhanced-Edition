--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:50 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30123(client, idx)
    name = "JailGuard"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721156, 1) then

            text(client, "Will you go to Violet Jail?")
            link(client, "Yes.", 1)
            pic(client, 53)
            create(client)

        else

            text(client, "Without my master`s passport, you can`t access to the prison.")
            link(client, "Then forget it.", 255)
            pic(client, 53)
            create(client)

        end

    elseif (idx == 1) then

        text(client, "Please don`t blame me for not reminding you. Monsters in the Jail are fierce.")
        link(client, "Thank you.", 2)
        pic(client, 53)
        create(client)

    elseif (idx == 2) then

        spendItem(client, 721156, 1)
        move(client, 1061, 447, 251)

    end

end
