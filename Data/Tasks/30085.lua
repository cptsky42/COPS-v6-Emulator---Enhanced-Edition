--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:48 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30085(client, idx)
    name = "FoodCarrier"
    face = 1

    if (idx == 0) then

        text(client, "Something unexpected may happen at any time. We were attacked by monsters when we passed Phoenix Castle. I`m the only survivor.")
        link(client, "You were too careless.", 255)
        pic(client, 7)
        create(client)

    end

end
