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

function Monster8116_OnDie(self, client)
    name = "BatMessenger"

    if (rand(client, 100) < 3) then

        action = randomAction(client, 1, 8)
        if action == 1 then
            dropItem(self, client, 150090)
        elseif action == 2 then
            dropItem(self, client, 160090)
        elseif action == 3 then
            dropItem(self, client, 410090)
        elseif action == 4 then
            dropItem(self, client, 420090)
        elseif action == 5 then
            dropItem(self, client, 430090)
        elseif action == 6 then
            dropItem(self, client, 440090)
        elseif action == 7 then
            dropItem(self, client, 450090)
        elseif action == 8 then
            dropItem(self, client, 450090)
        end


    else

        if (rand(client, 100) < 2) then

            action = randomAction(client, 1, 8)
            if action == 1 then
                dropItem(self, client, 111350)
            elseif action == 2 then
                dropItem(self, client, 114650)
            elseif action == 3 then
                dropItem(self, client, 117350)
            elseif action == 4 then
                dropItem(self, client, 118350)
            elseif action == 5 then
                dropItem(self, client, 120090)
            elseif action == 6 then
                dropItem(self, client, 121090)
            elseif action == 7 then
                dropItem(self, client, 151090)
            elseif action == 8 then
                dropItem(self, client, 151090)
            end


        else

            if (rand(client, 100) < 1) then

                dropItem(self, client, 1088001)

            end

        end

    end

end
