--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:50 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30125(client, idx)
    name = "GreenSnake"
    face = 1

    if (idx == 0) then

        text(client, "Are you leaving the snake array?")
        link(client, "I had better come later.", 1)
        link(client, "No. I never give up.", 255)
        pic(client, 53)
        create(client)

    elseif (idx == 1) then

        move(client, 1015, 717, 577)

    end

end
