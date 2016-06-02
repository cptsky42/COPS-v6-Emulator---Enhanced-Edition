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

function Monster3102_OnDie(self, client)
    name = "SnakeKing"

    if (rand(client, 96) < 1) then

        dropItem(self, client, 160076)

    else

        if (rand(client, 96) < 1) then

            dropItem(self, client, 1088000)

        else

            if (rand(client, 1000) < 1) then

                action = randomAction(client, 1, 8)
                if action == 1 then
                    dropItem(self, client, 700013)
                elseif action == 2 then
                    dropItem(self, client, 700023)
                elseif action == 3 then
                    dropItem(self, client, 700033)
                elseif action == 4 then
                    dropItem(self, client, 700043)
                elseif action == 5 then
                    dropItem(self, client, 700053)
                elseif action == 6 then
                    dropItem(self, client, 700063)
                elseif action == 7 then
                    dropItem(self, client, 700003)
                elseif action == 8 then
                    dropItem(self, client, 700023)
                end


            else

                if (rand(client, 300) < 1) then

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        dropItem(self, client, 700022)
                    elseif action == 2 then
                        dropItem(self, client, 700012)
                    elseif action == 3 then
                        dropItem(self, client, 700022)
                    elseif action == 4 then
                        dropItem(self, client, 700032)
                    elseif action == 5 then
                        dropItem(self, client, 700042)
                    elseif action == 6 then
                        dropItem(self, client, 700052)
                    elseif action == 7 then
                        dropItem(self, client, 700062)
                    elseif action == 8 then
                        dropItem(self, client, 700002)
                    end


                else

                    if (rand(client, 8) < 1) then

                        dropItem(self, client, 1088001)
                        dropItem(self, client, 1088001)
                        dropItem(self, client, 1088001)
                        dropItem(self, client, 1088001)
                        dropItem(self, client, 1088001)
                        dropItem(self, client, 1088001)

                    else

                        dropItem(self, client, 1088001)

                    end

                end

            end

        end

    end

end
