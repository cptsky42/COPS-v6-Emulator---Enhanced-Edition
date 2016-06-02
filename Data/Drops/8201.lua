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

function Monster8201_OnDie(self, client)
    name = "WingedSnakeAide"

    if (rand(client, 100) < 2) then

        action = randomAction(client, 1, 8)
        if action == 1 then
            dropItem(self, client, 460050)
        elseif action == 2 then
            dropItem(self, client, 480050)
        elseif action == 3 then
            dropItem(self, client, 481050)
        elseif action == 4 then
            dropItem(self, client, 490050)
        elseif action == 5 then
            dropItem(self, client, 510050)
        elseif action == 6 then
            dropItem(self, client, 530050)
        elseif action == 7 then
            dropItem(self, client, 540050)
        elseif action == 8 then
            dropItem(self, client, 540050)
        end


    else

        if (rand(client, 100) < 2) then

            action = randomAction(client, 1, 8)
            if action == 1 then
                dropItem(self, client, 460050)
            elseif action == 2 then
                dropItem(self, client, 130320)
            elseif action == 3 then
                dropItem(self, client, 131320)
            elseif action == 4 then
                dropItem(self, client, 134320)
            elseif action == 5 then
                dropItem(self, client, 460050)
            elseif action == 6 then
                dropItem(self, client, 130320)
            elseif action == 7 then
                dropItem(self, client, 131320)
            elseif action == 8 then
                dropItem(self, client, 134320)
            end


        else

            if (rand(client, 100) < 1) then

                dropItem(self, client, 1088001)

            end

        end

    end

end
