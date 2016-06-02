--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:13 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask1500(client, idx)
    name = "Matchmaker"
    face = 1

    if (idx == 0) then

        text(client, "It`s a good place to watch the moon. You can usually come to enjoy the moon together. Have a good day!")
        link(client, "Thank you.", 255)
        pic(client, 116)
        create(client)

    end

end
