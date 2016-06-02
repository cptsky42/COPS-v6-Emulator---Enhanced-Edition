--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:49 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30104(client, idx)
    name = "GeneralShou"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721127, 1) then

            text(client, "Ok, we will deal with it when we are free.")
            link(client, "So mean.", 255)
            pic(client, 30)
            create(client)

        else

            if hasItem(client, 721125, 1) then

                text(client, "I know it`s VillagerHead asked you to come here. Go to tell him we will deal with it once we are free. We are so busy now.")
                link(client, "I can`t believe that you are so irresponsible.", 1)
                pic(client, 30)
                create(client)

            else

                text(client, "I am in charge of the security of the city. No bad guys can escape here.")
                link(client, "Wow, it is wonderful.", 255)
                pic(client, 30)
                create(client)

            end

        end

    elseif (idx == 1) then

        spendItem(client, 721125, 1)
        awardItem(client, "721127", 1)

    end

end
