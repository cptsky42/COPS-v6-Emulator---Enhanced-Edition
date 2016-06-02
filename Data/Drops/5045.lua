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

function Monster5045_OnDie(self, client)
    name = "UndeadSoldier"

    if getProfession(client) <= 15 then

        if (rand(client, 60) < 1) then

            dropItem(self, client, 723085)

        end

    else

        if getProfession(client) <= 25 then

            if (rand(client, 60) < 1) then

                dropItem(self, client, 723085)

            end

        else

            if getProfession(client) <= 45 then

                if (rand(client, 100) < 1) then

                    dropItem(self, client, 723085)

                end

            else

                if getProfession(client) <= 135 then

                    if (rand(client, 50) < 1) then

                        dropItem(self, client, 723085)

                    end

                else

                    if (rand(client, 80) < 1) then

                        dropItem(self, client, 723085)

                    end

                end

            end

        end

    end

end
