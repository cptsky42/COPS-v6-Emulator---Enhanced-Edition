--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:53 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600008(client, idx)
    name = "GuardianGod"
    face = 1

    if (idx == 0) then

        text(client, "I have watched this tactic for many years and seen many challengers. Do you have full confidence that you will succeed?")
        link(client, "What tactic is this?", 1)
        link(client, "Of course.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 1) then

        if hasItem(client, 721012, 1) then

            text(client, "Since you have got a token, I am glad to tell you that this is Deserted Tactic. You may ask Ghost to teleport you to Maggie.")
            link(client, "Thanks.", 255)
            pic(client, 9)
            create(client)

        else

            text(client, "It is a secret. I cannot tell you before you have a token for this tactic.")
            link(client, "I see.", 255)
            pic(client, 9)
            create(client)

        end

    end

end
