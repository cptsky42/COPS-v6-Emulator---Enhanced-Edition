--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:56 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600061(client, idx)
    name = "RichmanZhang"
    face = 1

    if (idx == 0) then

        text(client, "Hi, my friend, I am selling a variety of goods. What are you interested?")
        link(client, "What do you have?", 1)
        link(client, "I have what you want.", 2)
        link(client, "No. Thanks.", 255)
        pic(client, 100)
        create(client)

    elseif (idx == 1) then

        text(client, "Anything. You can name it. For the sake of safety, I never display them. Just tell me what you want.")
        link(client, "Refined Bone Bracelet.", 3)
        link(client, "No. Thanks.", 255)
        pic(client, 100)
        create(client)

    elseif (idx == 2) then

        text(client, "If you have 3 emeralds and 4 gold ores of rate 3 at least, I will give you a refined Bone Bracelet in return.")
        link(client, "Let`s exchange now.", 4)
        link(client, "Let me think it over.", 255)
        pic(client, 100)
        create(client)

    elseif (idx == 3) then

        text(client, "Yes, I have it. It is very rare, and I am not going to sell it.")
        link(client, "But I need it badly.", 5)
        link(client, "Oh, forget it.", 255)
        pic(client, 100)
        create(client)

    elseif (idx == 4) then

        if hasItem(client, 1080001, 3) then

            if hasItems(client, 1072052, 1072059, 4) then

                spendItems(client, 1072052, 1072059, 4)
                spendItem(client, 1080001, 3)
                awardItem(client, "152126", 1)

            else

                text(client, "Sorry, you do not have 3 emeralds and 4 gold ores of rate 3 at least.")
                link(client, "Oh, I see.", 255)
                pic(client, 100)
                create(client)

            end

        else

            text(client, "Sorry, you do not have 3 emeralds and 4 gold ores of rate 3 at least.")
            link(client, "Oh, I see.", 255)
            pic(client, 100)
            create(client)

        end

    elseif (idx == 5) then

        text(client, "If that is the case, you can barter with me. I need 3 emeralds and 4 gold ores of rate 3 at least.")
        text(client, "I will exchange the bracelet for those items. How about it?")
        link(client, "It is a deal.", 2)
        link(client, "Forget it.", 255)
        pic(client, 100)
        create(client)

    end

end
