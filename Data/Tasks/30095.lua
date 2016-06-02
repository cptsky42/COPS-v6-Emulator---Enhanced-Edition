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

function processTask30095(client, idx)
    name = "John"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "Letter.John") and (getMoney(client) >= 0) then

            text(client, "Please take care of it.")
            link(client, "I shall try my best.", 255)
            pic(client, 7)
            create(client)

        else

         text(client, "Hi, can you do me a favor?")
         link(client, "Sure.", 1)
         link(client, "I am busy.", 255)
         pic(client, 7)
         create(client)

        end

    elseif (idx == 1) then

        text(client, "My brother Richard enjoys roaming around the city. Can you deliver this letter to him for me? He will reward you handsomely.")
        link(client, "OK.", 2)
        link(client, "I am busy.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 2) then

        awardItem(client, "721121", 1)
        text(client, "But he is an odd fellow. He will run away as soon as a stranger speaks to him. It is a little difficult to find him.")
        link(client, "I shall try my best!", 255)
        pic(client, 7)
        create(client)

    end

end
