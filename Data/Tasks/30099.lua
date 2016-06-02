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

function processTask30099(client, idx)
    name = "GeneralLong"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "ToMountainKing") and (getMoney(client) >= 0) then

            text(client, "Please take care of it.")
            link(client, "All right.", 255)
            pic(client, 7)
            create(client)

        else

         if getLevel(client) < 50 then

             text(client, "Do not bother me!")
             link(client, "Okay.", 255)
             pic(client, 7)
             create(client)

         else

             text(client, "You are very excellent. May I ask you for a help?")
             link(client, "What can I do?", 1)
             link(client, "I am busy.", 255)
             pic(client, 7)
             create(client)

         end

        end

    elseif (idx == 1) then

        text(client, "My brother Mountain King in Ape Mountain is in trouble, but I do not have soldiers to help him. Can you help him?")
        link(client, "I shall have a try.", 2)
        link(client, "Let me think it over.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 2) then

        awardItem(client, "721122", 1)
        text(client, "Take this letter with you, and he will know that you are on his side.")
        link(client, "Ok.", 255)
        pic(client, 7)
        create(client)

    end

end
