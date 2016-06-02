--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/10/2015 3:10:15 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function Monster1411_OnDie(self, client)
    name = "Serpent"

    if (rand(client, 10) < 1) then

        action = randomAction(client, 1, 8)
        if action == 1 then
            dropItem(self, client, 1000010)
        elseif action == 2 then
            dropItem(self, client, 1000020)
        elseif action == 3 then
            dropItem(self, client, 1000030)
        elseif action == 4 then
            dropItem(self, client, 1001020)
        elseif action == 5 then
            dropItem(self, client, 1001010)
        elseif action == 6 then
            dropItem(self, client, 1001000)
        elseif action == 7 then
            dropItem(self, client, 1002000)
        elseif action == 8 then
            dropItem(self, client, 1002010)
        end


    else

        if (rand(client, 1000) < 1) then

            dropItem(self, client, 1060102)

        else

            if (rand(client, 100) < 1) then

                dropItem(self, client, 1060020)

            end

        end

    end

end
