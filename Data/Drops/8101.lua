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

function Monster8101_OnDie(self, client)
    name = "WingedSnakeMsgr"

    if (rand(client, 100) < 5) then

        action = randomAction(client, 1, 8)
        if action == 1 then
            dropItem(self, client, 160050)
        elseif action == 2 then
            dropItem(self, client, 150050)
        elseif action == 3 then
            dropItem(self, client, 410050)
        elseif action == 4 then
            dropItem(self, client, 420050)
        elseif action == 5 then
            dropItem(self, client, 430050)
        elseif action == 6 then
            dropItem(self, client, 440050)
        elseif action == 7 then
            dropItem(self, client, 450050)
        elseif action == 8 then
            dropItem(self, client, 450050)
        end


    else

        if (rand(client, 100) < 5) then

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
