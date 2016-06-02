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

function processTask30114(client, idx)
    name = "GeneralWu"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721133, 1) then

            text(client, "Do not tell the General that I am hunting. Okay?")
            link(client, "Okay.", 255)
            pic(client, 63)
            create(client)

        else

            if hasItem(client, 721135, 1) then

                if hasItem(client, 721134, 1) then

                    text(client, "Oh, it is the official letter from the General. Give it to me, let me take a look first.")
                    link(client, "Here you are.", 1)
                    pic(client, 63)
                    create(client)

                else

                    text(client, "It is annoying. Tell my men that I will be back soon and do not send a stranger to disturb me for nothing.")
                    link(client, "Ok.", 255)
                    pic(client, 63)
                    create(client)

                end

            else

                text(client, "Get out of here. I`ll never forgive you if you scare away the game.")
                link(client, "Ok.", 255)
                pic(client, 63)
                create(client)

            end

        end

    elseif (idx == 1) then

        text(client, "I have read the official letter. Here is my letter in reply. Please give it to the General.")
        link(client, "I see.", 2)
        pic(client, 63)
        create(client)

    elseif (idx == 2) then

        spendItem(client, 721135, 1)
        spendItem(client, 721134, 1)
        awardItem(client, "721133", 1)
        action = randomAction(client, 1, 8)
        if action == 1 then
            moveNpc(client, 30114, 1011, 800, 471)
        elseif action == 2 then
            moveNpc(client, 30114, 1011, 728, 490)
        elseif action == 3 then
            moveNpc(client, 30114, 1011, 685, 426)
        elseif action == 4 then
            moveNpc(client, 30114, 1011, 720, 375)
        elseif action == 5 then
            moveNpc(client, 30114, 1011, 800, 471)
        elseif action == 6 then
            moveNpc(client, 30114, 1011, 728, 490)
        elseif action == 7 then
            moveNpc(client, 30114, 1011, 685, 426)
        elseif action == 8 then
            moveNpc(client, 30114, 1011, 720, 375)
        end


    end

end
