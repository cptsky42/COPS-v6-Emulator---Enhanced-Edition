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

function processTask30110(client, idx)
    name = "Mr.Leisure"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721131, 1) then

            text(client, "He looks like the other blacksmith, but he is a weapon maker, while the other is a weapon retailer.")
            link(client, "I see.", 255)
            pic(client, 56)
            create(client)

        else

            if getLevel(client) < 40 then

                text(client, "I am always ready to help other guys.")
                link(client, "You are a kind man.", 255)
                pic(client, 56)
                create(client)

            else

                text(client, "My old friend, Blacksmith Li in Ape Mountain, needs help. Will you do him a favor?")
                link(client, "Sure.", 1)
                link(client, "Sorry, I am too busy.", 255)
                pic(client, 56)
                create(client)

            end

        end

    elseif (idx == 1) then

        text(client, "Take my letter to him, and he will tell you more.")
        link(client, "OK.", 2)
        pic(client, 56)
        create(client)

    elseif (idx == 2) then

        awardItem(client, "721131", 1)

    end

end
