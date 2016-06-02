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

function Monster3149_OnDie(self, client)
    name = "Talon"

    action = randomAction(client, 1, 8)
    if action == 1 then
        dropItem(self, client, 150097)
    elseif action == 2 then
        dropItem(self, client, 152107)
    elseif action == 3 then
        dropItem(self, client, 160097)
    elseif action == 4 then
        dropItem(self, client, 120097)
    elseif action == 5 then
        dropItem(self, client, 900427)
    elseif action == 6 then
        dropItem(self, client, 121097)
    elseif action == 7 then
        dropItem(self, client, 113927)
    elseif action == 8 then
        dropItem(self, client, 721543)
    end

end
