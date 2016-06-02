--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 4/1/2015 7:44:06 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask3604(client, idx)
    name = "Bruce"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Another wronged person! What a poor guy! You should have been in the Heaven, unfortunately, you`ve been driven to ")
            text(client, "this terrible place where nobody will take care.")
            text(client, "")
            link(client, "Where am I now?", 1)
            pic(client, 71)
            create(client)

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Here is not the fairyland or hell. This place is called Evil Abyss. If you want to get reborn and start a fresh ")
            text(client, "life, you need to go through a lot of tests.")
            text(client, "")
            text(client, "")
            text(client, "")
            link(client, "What tests?", 2)
            pic(client, 71)
            create(client)

        end

    elseif (idx == 2) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "The journey to second rebirth is very long and dangerous. There are many vicious demons and fiends roaming around. ")
            text(client, "I advise you think it over. If you have made up your mind, you may have a talk with my fellows. They will tell you what to do.")
            text(client, "")
            text(client, "")
            text(client, "")
            link(client, "I see.", 255)
            pic(client, 71)
            create(client)

        end

    end

end
