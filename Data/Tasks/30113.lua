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

function processTask30113(client, idx)
    name = "Soldier"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721135, 1) then

            text(client, "If he is not in the castle, you may find him near the village of southeastern Maple Forest. The fire rats are gathering there.")
            link(client, "Thanks a lot.", 255)
            pic(client, 14)
            create(client)

        else

            if hasItem(client, 721134, 1) then

                text(client, "Are you here to give him the official letter? He has gone hunting and will not come back soon.")
                link(client, "Where can I find him?", 1)
                link(client, "What a bad luck.", 255)
                pic(client, 14)
                create(client)

            else

                text(client, "Our General has gone hunting alone. We are here to guard the castle.")
                link(client, "I see.", 255)
                pic(client, 14)
                create(client)

            end

        end

    elseif (idx == 1) then

        text(client, "It is hard to say. He often goes hunting near the village of southeastern Maple Forest. You can try your luck there.")
        link(client, "Thanks.", 2)
        pic(client, 14)
        create(client)

    elseif (idx == 2) then

        awardItem(client, "721135", 1)
        text(client, "You had better take my letter with you, or he will not talk to you.")
        link(client, "Thank you.", 255)
        pic(client, 14)
        create(client)

    end

end
