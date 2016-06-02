--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/10/2015 3:10:14 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function Monster18_OnDie(self, client)
    name = "Birdman"

    if (rand(client, 100000) < 1) then

        dropItem(self, client, 1060100)

    else

        if (rand(client, 100000) < 1) then

            dropItem(self, client, 1060101)

        end

    end

end
