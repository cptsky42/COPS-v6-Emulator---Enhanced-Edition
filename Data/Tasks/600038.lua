--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:55 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600038(client, idx)
    name = "LonelyGhost"
    face = 1

    if (idx == 0) then

        text(client, "We have been trapped here for many years. Few challengers have a chance to arrive here. You are a real conqueror.")
        link(client, "Who are you?", 1)
        pic(client, 9)
        create(client)

    elseif (idx == 1) then

        text(client, "We were from an old Kingdom. Our rivals made the mystic tactics. We failed to break through them and died.")
        link(client, "I see.", 2)
        pic(client, 9)
        create(client)

    elseif (idx == 2) then

        text(client, "I am trapped here and cannot get revived. Since you have passed the tactics, would you like to help me get reborn?")
        link(client, "How can I help you?", 3)
        pic(client, 9)
        create(client)

    elseif (idx == 3) then

        text(client, "We cannot get reborn unless someone would like to take us out. One person can take only one ghost at a time. Will you help me?")
        link(client, "No problem.", 4)
        link(client, "Sorry, I am very busy.", 5)
        pic(client, 9)
        create(client)

    elseif (idx == 4) then

        text(client, "You can choose three Meteors or the MoonBox. But what you will get from MoonBox depends on your luck. Which one do you prefer?")
        link(client, "Three meteors.", 6)
        link(client, "MoonBox.", 7)
        link(client, "Let me think it over.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 5) then

        text(client, "You only need to spare a little time. I want to get reborn very much. Please kindly have a mercy for me.")
        link(client, "I shall help you.", 4)
        link(client, "I want to leave here.", 8)
        pic(client, 9)
        create(client)

    elseif (idx == 6) then

        if (getItemsCount(client) <= 37) then

            spendItem(client, 721072, 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            move(client, 1042, 23, 24)

        else

            text(client, "Please prepare three vacancy in your Bag.")
            text(client, "I see. 0")
            pic(client, 9)
            create(client)

        end

    elseif (idx == 7) then

        spendItem(client, 721072, 1)
        move(client, 1042, 23, 24)
        awardItem(client, "721090", 1)

    elseif (idx == 8) then

        text(client, "Since you do not want to help me, I cannot force you. If you are ready to leave here, I shall be glad to teleport you out.")
        link(client, "I am ready.", 9)
        pic(client, 9)
        create(client)

    elseif (idx == 9) then

        spendItem(client, 721072, 1)
        move(client, 1042, 23, 24)

    end

end
