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

function processTask30124(client, idx)
    name = "Lauren"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721153, 1) then

            text(client, "Thank goodness. I have not seen a single person for long. Did you come all the way to rescue me?")
            link(client, "Yes, I did.", 1)
            pic(client, 154)
            create(client)

        else

            text(client, "I wish I could get out of here soon.")
            link(client, "Good luck.", 255)
            pic(client, 154)
            create(client)

        end

    elseif (idx == 1) then

        text(client, "But WaterRing sticks to me and I can`t take it off.")
        link(client, "I can do it.", 2)
        pic(client, 154)
        create(client)

    elseif (idx == 2) then

        text(client, "Oh, you got the seal. Give it to me please and take away WaterRing at once. I want to be free.")
        link(client, "Ok. One moment.", 3)
        pic(client, 154)
        create(client)

    elseif (idx == 3) then

        spendItem(client, 721153, 1)
        awardItem(client, "721150", 1)

    end

end
