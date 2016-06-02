--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/10/2015 3:10:26 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function Monster8319_OnDie(self, client)
    name = "EvilLord"

    if (rand(client, 100) < 2) then

        dropItem(self, client, 1088001)

    else

        if (rand(client, 100) < 2) then

            action = randomAction(client, 1, 8)
            if action == 1 then
                dropItem(self, client, 1088001)
                dropItem(self, client, 1088001)
                dropItem(self, client, 1088001)
            elseif action == 2 then
                dropItem(self, client, 1088001)
                dropItem(self, client, 1088001)
            elseif action == 3 then
                dropItem(self, client, 1088001)
                dropItem(self, client, 1088001)
                dropItem(self, client, 1088001)
            elseif action == 4 then
                dropItem(self, client, 1088001)
                dropItem(self, client, 1088001)
            elseif action == 5 then
                dropItem(self, client, 1088001)
            elseif action == 6 then
                dropItem(self, client, 1088001)
            elseif action == 7 then
                dropItem(self, client, 1088001)
            elseif action == 8 then
                dropItem(self, client, 1088001)
            end


        else

            if (rand(client, 25000) < 1) then

                dropItem(self, client, 1088000)

            end

        end

    end

end
