--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:46 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30066(client, idx)
    name = "ProtectingKid"
    face = 1

    if (idx == 0) then

        text(client, "You have done a good job to make it so far. I will send you out. Wish you good luck next time.")
        link(client, "Well. I have no choice.", 1)
        pic(client, 10)
        create(client)

    elseif (idx == 1) then

        if spendItem(client, 721110, 1) then

            move(client, 1020, 566, 565)

        else

            if spendItem(client, 721111, 1) then

                move(client, 1020, 566, 565)

            else

                if spendItem(client, 721112, 1) then

                    move(client, 1020, 566, 565)

                else

                    move(client, 1020, 566, 565)

                end

            end

        end

    end

end
