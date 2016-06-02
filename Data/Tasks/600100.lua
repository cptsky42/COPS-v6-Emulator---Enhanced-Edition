--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:59 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600100(client, idx)
    name = "Alcoholist"
    face = 1

    if (idx == 0) then

        if hasItem(client, 723030, 1) then

            text(client, "Wow, your wine is so spicy. It must be fine wine. Can I have a look?")
            link(client, "Here you are.", 1)
            link(client, "Sorry, you can`t.", 255)
            pic(client, 67)
            create(client)

        else

            text(client, "Great people talk about ideas, average talk about things, and small people talk about wine.")
            link(client, "It sounds true.", 2)
            pic(client, 67)
            create(client)

        end

    elseif (idx == 1) then

        text(client, "It is HealthWine. My favorite. It would be much appreciated if you let me have the wine.")
        link(client, "Well...", 3)
        pic(client, 67)
        create(client)

    elseif (idx == 2) then

        text(client, "Thanks. I have been guarding Mystic Cave for ages. When I am at leisure, I will write poems and drink some wine.")
        link(client, "What is Mystic Cave?", 4)
        pic(client, 67)
        create(client)

    elseif (idx == 3) then

        text(client, "Do you have any requirements? Please tell me frankly.")
        link(client, "Can I enter Mystic Cave?", 5)
        link(client, "No, I will keep the wine.", 255)
        pic(client, 67)
        create(client)

    elseif (idx == 4) then

        text(client, "You`d better not enter it, it is dangerous. There are ferocious DevilKings of level 120 and 122. So I guard here,")
        text(client, "in case people brush in mistakenly.")
        link(client, "I can deal with them.", 6)
        link(client, "Luckily, I did not get in.", 255)
        pic(client, 67)
        create(client)

    elseif (idx == 5) then

        text(client, "Nothing is left except for poems and wine. You can go there if you like.")
        link(client, "Thanks a lot.", 7)
        pic(client, 67)
        create(client)

    elseif (idx == 6) then

        text(client, "It is my duty, I won`t let you in.")
        link(client, "I see.", 255)
        pic(client, 67)
        create(client)

    elseif (idx == 7) then

        spendItem(client, 723030, 1)
        setRecordPos(client, 1001, 673, 345)
        move(client, 1300, 312, 646)

    end

end
