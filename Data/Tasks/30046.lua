--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:45 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30046(client, idx)
    name = "StageGuard"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721104, 1) then

            if (rand(client, 6) < 1) then

                text(client, "Congrats! You have passed the sky pass. I am pleased to teleport you to claim the prize.")
                link(client, "Thanks.", 1)
                pic(client, 7)
                create(client)

            else

                spendItem(client, 721104, 1)
                move(client, 1040, 394, 181)
                text(client, "I am so sorry that you will be sent back to challenge again.")
                link(client, "What a bad luck.", 255)
                pic(client, 7)
                create(client)

            end

        else

            text(client, "Keep up with the good job! I may send you to claim the prizes if you obtain the Pass Token.")
            link(client, "I will try my best.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "PassTokenL80") and (getMoney(client) >= 0) then

            spendItem(client, 721104, 1)
            awardItem(client, "721109", 1)
            move(client, 1040, 192, 250)

        end

    end

end
