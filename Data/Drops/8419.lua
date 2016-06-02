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

function Monster8419_OnDie(self, client)
    name = "WaterDevil"

    if (rand(client, 300) < 1) then

        if (rand(client, 2) < 1) then

            action = randomAction(client, 1, 8)
            if action == 1 then
                dropItem(self, client, 150097)
            elseif action == 2 then
                dropItem(self, client, 160097)
            elseif action == 3 then
                dropItem(self, client, 120097)
            elseif action == 4 then
                dropItem(self, client, 131847)
            elseif action == 5 then
                dropItem(self, client, 111347)
            elseif action == 6 then
                dropItem(self, client, 134447)
            elseif action == 7 then
                dropItem(self, client, 133627)
            elseif action == 8 then
                dropItem(self, client, 114947)
            end


        else

            action = randomAction(client, 1, 8)
            if action == 1 then
                dropItem(self, client, 900717)
            elseif action == 2 then
                dropItem(self, client, 151097)
            elseif action == 3 then
                dropItem(self, client, 130647)
            elseif action == 4 then
                dropItem(self, client, 118547)
            elseif action == 5 then
                dropItem(self, client, 117357)
            elseif action == 6 then
                dropItem(self, client, 113827)
            elseif action == 7 then
                dropItem(self, client, 121097)
            elseif action == 8 then
                dropItem(self, client, 152087)
            end


        end

    else

        if (rand(client, 100) < 1) then

            if (rand(client, 2) < 1) then

                action = randomAction(client, 1, 8)
                if action == 1 then
                    dropItem(self, client, 150096)
                elseif action == 2 then
                    dropItem(self, client, 160096)
                elseif action == 3 then
                    dropItem(self, client, 120096)
                elseif action == 4 then
                    dropItem(self, client, 131846)
                elseif action == 5 then
                    dropItem(self, client, 111346)
                elseif action == 6 then
                    dropItem(self, client, 134446)
                elseif action == 7 then
                    dropItem(self, client, 133626)
                elseif action == 8 then
                    dropItem(self, client, 114946)
                end


            else

                action = randomAction(client, 1, 8)
                if action == 1 then
                    dropItem(self, client, 900716)
                elseif action == 2 then
                    dropItem(self, client, 151096)
                elseif action == 3 then
                    dropItem(self, client, 130646)
                elseif action == 4 then
                    dropItem(self, client, 118546)
                elseif action == 5 then
                    dropItem(self, client, 117356)
                elseif action == 6 then
                    dropItem(self, client, 113826)
                elseif action == 7 then
                    dropItem(self, client, 121096)
                elseif action == 8 then
                    dropItem(self, client, 152086)
                end


            end

        end

    end

end
