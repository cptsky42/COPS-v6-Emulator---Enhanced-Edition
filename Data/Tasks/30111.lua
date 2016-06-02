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

function processTask30111(client, idx)
    name = "BlacksmithLi"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721136, 1) then

            text(client, "Have you got the stone spirit?")
            link(client, "Yes, here you are.", 1)
            link(client, "Not yet.", 255)
            pic(client, 34)
            create(client)

        else

            if hasItem(client, 721131, 1) then

                text(client, "Oh, You are introduced by Mr. Leisure . He is not a reliable man. He has introduced many people, but no one can be helpful.")
                link(client, "I am different.", 2)
                link(client, "You have prejudice.", 255)
                pic(client, 34)
                create(client)

            else

                text(client, "Nowadays, you cannot count on a person to do a good job. Nobody can take care of such a small case.")
                link(client, "You should do it yourself.", 255)
                pic(client, 34)
                create(client)

            end

        end

    elseif (idx == 1) then

        if hasItem(client, 721132, 5) then

            spendItem(client, 721136, 1)
            spendItem(client, 721132, 5)
            text(client, "Well done. This is what I have made. I am glad to give it to you as a reward.")
            link(client, "Thanks a lot.", 3)
            pic(client, 34)
            create(client)

        else

            text(client, "Sorry, you do not have 5 stone spirits.")
            link(client, "I see.", 255)
            pic(client, 34)
            create(client)

        end

    elseif (idx == 2) then

        text(client, "Someone ordered from me some high quality weapons. However, I run out of the special material. I need you to get me some more.")
        link(client, "What material do you need?", 4)
        pic(client, 34)
        create(client)

    elseif (idx == 3) then

        action = randomAction(client, 1, 8)
        if action == 1 then
            awardItem(client, "900306", 1)
        elseif action == 2 then
            awardItem(client, "900406", 1)
        elseif action == 3 then
            awardItem(client, "900506", 1)
        elseif action == 4 then
            awardItem(client, "900606", 1)
        elseif action == 5 then
            awardItem(client, "900706", 1)
        elseif action == 6 then
            awardItem(client, "900806", 1)
        elseif action == 7 then
            awardItem(client, "900906", 1)
        elseif action == 8 then
            awardItem(client, "900306", 1)
        end


    elseif (idx == 4) then

        text(client, "It is Stone spirit. You may get some from rock monsters in the mine cave of southeastern Maple Forest. Five spirits will do.")
        link(client, "I can make it.", 5)
        pic(client, 34)
        create(client)

    elseif (idx == 5) then

        spendItem(client, 721131, 1)
        awardItem(client, "721136", 1)

    end

end
