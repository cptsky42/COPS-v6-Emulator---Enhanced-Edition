--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/10/2015 3:10:24 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function Monster8106_OnDie(self, client)
    name = "GiantApeMsgr"

    if (rand(client, 100) < 4) then

        action = randomAction(client, 1, 8)
        if action == 1 then
            dropItem(self, client, 150070)
        elseif action == 2 then
            dropItem(self, client, 160070)
        elseif action == 3 then
            dropItem(self, client, 410070)
        elseif action == 4 then
            dropItem(self, client, 420070)
        elseif action == 5 then
            dropItem(self, client, 421070)
        elseif action == 6 then
            dropItem(self, client, 430070)
        elseif action == 7 then
            dropItem(self, client, 440070)
        elseif action == 8 then
            dropItem(self, client, 440070)
        end


    else

        if (rand(client, 100) < 3) then

            action = randomAction(client, 1, 8)
            if action == 1 then
                dropItem(self, client, 900300)
            elseif action == 2 then
                dropItem(self, client, 130440)
            elseif action == 3 then
                dropItem(self, client, 131340)
            elseif action == 4 then
                dropItem(self, client, 134340)
            elseif action == 5 then
                dropItem(self, client, 900300)
            elseif action == 6 then
                dropItem(self, client, 130440)
            elseif action == 7 then
                dropItem(self, client, 131340)
            elseif action == 8 then
                dropItem(self, client, 134340)
            end


        else

            if (rand(client, 100) < 1) then

                dropItem(self, client, 1088001)

            end

        end

    end

end
