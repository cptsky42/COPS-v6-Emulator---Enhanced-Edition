--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:37 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask5004(client, idx)
    name = "MillionaireLee"
    face = 1

    if (idx == 0) then

        text(client, "Hunting meteors and dragonballs is an exciting thing. However, they also pile up in your inventories.")
        link(client, "Agreed. How do you deal with it?", 1)
        link(client, "I am poor and do not have the problem.", 255)
        pic(client, 29)
        create(client)

    elseif (idx == 1) then

        text(client, "I can pack dragonballs and meteors for you. Give me 10 meteors or 10 dragonballs, I will make it to a MeteorScroll")
        text(client, "or a DragonScroll that occupies only one slot. Just right click on it, it will return into 10 meteors or 10 dragonballs again.")
        link(client, "Cool. Please pack my meteors.", 2)
        link(client, "Cool. Please pack my dragonballs.", 3)
        pic(client, 29)
        create(client)

    elseif (idx == 2) then

        if hasItem(client, 1088001, 10) then

            spendItem(client, 1088001, 10)
            awardItem(client, "720027", 1)

        else

            text(client, "Sorry, you do not have 10 meteors.")
            link(client, "OK, I know.", 255)
            pic(client, 29)
            create(client)

        end

    elseif (idx == 3) then

        if hasItem(client, 1088000, 10) then

            spendItem(client, 1088000, 1)

            spendItem(client, 1088000, 1)

            spendItem(client, 1088000, 1)

            spendItem(client, 1088000, 1)

            spendItem(client, 1088000, 1)

            spendItem(client, 1088000, 1)

            spendItem(client, 1088000, 1)

            spendItem(client, 1088000, 1)

            spendItem(client, 1088000, 1)

            spendItem(client, 1088000, 1)

            awardItem(client, "720028", 1)

            sendSysMsg(client, "Your dragonballs have been packed successfully.", 2005)

        else

            text(client, "Sorry, you do not have 10 dragonballs.")
            link(client, "OK, I know.", 255)
            pic(client, 29)
            create(client)

        end

    end

end
