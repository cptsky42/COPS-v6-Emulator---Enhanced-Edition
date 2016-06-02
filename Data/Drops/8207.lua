--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/10/2015 3:10:25 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function Monster8207_OnDie(self, client)
    name = "ThunderApeAide"

    if (rand(client, 100) < 1) then

        action = randomAction(client, 1, 8)
        if action == 1 then
            dropItem(self, client, 450070)
        elseif action == 2 then
            dropItem(self, client, 460070)
        elseif action == 3 then
            dropItem(self, client, 480070)
        elseif action == 4 then
            dropItem(self, client, 481070)
        elseif action == 5 then
            dropItem(self, client, 490070)
        elseif action == 6 then
            dropItem(self, client, 510070)
        elseif action == 7 then
            dropItem(self, client, 530070)
        elseif action == 8 then
            dropItem(self, client, 530070)
        end


    else

        if (rand(client, 100) < 1) then

            action = randomAction(client, 1, 8)
            if action == 1 then
                dropItem(self, client, 540070)
            elseif action == 2 then
                dropItem(self, client, 560070)
            elseif action == 3 then
                dropItem(self, client, 151070)
            elseif action == 4 then
                dropItem(self, client, 120080)
            elseif action == 5 then
                dropItem(self, client, 540070)
            elseif action == 6 then
                dropItem(self, client, 560070)
            elseif action == 7 then
                dropItem(self, client, 151070)
            elseif action == 8 then
                dropItem(self, client, 120080)
            end


        else

            if (rand(client, 100) < 1) then

                dropItem(self, client, 1088001)

            end

        end

    end

end
