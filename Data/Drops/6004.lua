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

function Monster6004_OnDie(self, client)
    name = "ToughHorn"

    if (rand(client, 65) < 1) then

        dropItem(self, client, 721014)
        spendItem(client, 721064, 1)

    end

end
