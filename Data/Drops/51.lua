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

function Monster51_OnDie(self, client)
    name = "RockMonster"

    if (rand(client, 150000) < 1) then

        dropItem(self, client, 1088000)

    else

        if (rand(client, 150000) < 1) then

            dropItem(self, client, 1088001)

        end

    end

end
