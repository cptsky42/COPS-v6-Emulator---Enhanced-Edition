--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:52 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600000(client, idx)
    name = "Milly"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "GuardianStar") and (getMoney(client) >= 0) then

            text(client, "Have you seen my sister? She cannot cheer up until her Joe returns to her.")
            link(client, "I must help her.", 255)
            pic(client, 4)
            create(client)

        elseif hasTaskItem(client, "Milly`sLetter") and (getMoney(client) >= 0) then

            text(client, "My sister is in Bird Island. She fell in love with Joe there. Since Joe left her with no news, she stays there to wait for him.")
            link(client, "I see.", 255)
            pic(client, 4)
            create(client)

        else

         text(client, "I cannot get near you now. You have got me going crazy. Wherever you go, whatever you do, I will be right here waiting for you.")
         link(client, "Why are you so sad?", 1)
         link(client, "I got to go.", 255)
         pic(client, 4)
         create(client)

        end

    elseif (idx == 1) then

        text(client, "My sister taught me that song. .Her lover, Joe, is a brave and kind man. All said they make a good couple, but he left her.")
        link(client, "What a pity.", 2)
        link(client, "Another old love story.", 255)
        pic(client, 4)
        create(client)

    elseif (idx == 2) then

        text(client, "My sister believes that Joe still loves her. She stays in Bird Island waiting for him. Can you take my letter to her?")
        link(client, "OK. I shall visit her.", 3)
        link(client, "Just passing by.", 255)
        pic(client, 4)
        create(client)

    elseif (idx == 3) then

        awardItem(client, "721000", 1)

    end

end
