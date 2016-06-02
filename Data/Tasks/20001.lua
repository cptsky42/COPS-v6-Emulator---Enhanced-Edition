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

function processTask20001(client, idx)
    name = "SoldierLeader"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721211, 1) then

            text(client, "The life here is too dull and dangerous. I must go crazy if I stay any longer.")
            link(client, "I would rather die.", 255)
            pic(client, 10)
            create(client)

        else

            if hasItem(client, 721210, 1) then

                text(client, "You are looking for Ryan? Sorry, he sacrificed his life on his first duty. This is his item. Please give it to his relatives.")
                link(client, "Okay.", 1)
                link(client, "Sorry, I do not know him.", 255)
                pic(client, 10)
                create(client)

            else

                text(client, "The life here is too dull and dangerous. I must go crazy if I stay any longer.")
                link(client, "I would rather die.", 255)
                pic(client, 10)
                create(client)

            end

        end

    elseif (idx == 1) then

        awardItem(client, "721211", 1)
        text(client, "Nobody wants to be a soldier, but we must defend our homeland against invasion. Many soldiers have sacrificed their lives.")
        link(client, "What a troubled world!", 255)
        pic(client, 10)
        create(client)

    end

end
