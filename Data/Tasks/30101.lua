--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:49 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30101(client, idx)
    name = "SnakemanLeader"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "ChallengeLetter") and (getMoney(client) >= 0) then

            text(client, "We are indifferent to the human being, and we never harry them.")
            link(client, "Who did it?", 1)
            pic(client, 10)
            create(client)

        else

         text(client, "Man, you are lucky. You would be my dinner if it was not for my satisfactory stomach.")
         link(client, "Are you scaring me?", 255)
         pic(client, 10)
         create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "ChallengeLetter") and (getMoney(client) >= 0) then

            text(client, "The other day, heresy snakemen came. They never talk to us. I reckon they did it. Their color is different to ours.")
            link(client, "I shall go to find them.", 255)
            pic(client, 10)
            create(client)

        end

    end

end
