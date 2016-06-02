--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/21/2015 2:05:13 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function useItem1060080(self, client)
    name = "RedDye"
    face = 1

    setHair(client, getHair(client) - (math.floor((getHair(client) % 1000) / 100) * 100) + (5 * 100))

end
