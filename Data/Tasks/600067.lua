--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:56 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600067(client, idx)
    name = "Emily"
    face = 1

    if (idx == 0) then

        if getSex(client) == 1 then

            text(client, "I am on my way home alone. But the path is steep and dangerous animals are lurking ahead. Could you please escort me home?")
            link(client, "No problom.", 1)
            link(client, "It is doubtful that a young lady walks alone at midnight.", 2)
            pic(client, 134)
            create(client)

        else

            text(client, "It is dangerous that you go by yourself. Are you lonely? Let us have a chat, and then you will not feel lonely.")
            link(client, "That is great.", 1)
            link(client, "I doubt it.", 2)
            pic(client, 134)
            create(client)

        end

    elseif (idx == 1) then

        addLife(client, -100)
        addLife(client, 1)
        text(client, "LOL! You fell for my small tricks easily.")
        link(client, "Help me.", 255)
        pic(client, 134)
        create(client)

    elseif (idx == 2) then

        if getLevel(client) < 60 then

            text(client, "Do not bother me, newbie, or I will take you down.")
            link(client, "It is ok.", 255)
            pic(client, 134)
            create(client)

        else

            if hasItem(client, 723010, 1) then

                text(client, "Oh, you are sent by Old General. You are too weak to oppose us.")
                text(client, "Since you select to enter the cave, I will sent you there. Let`s see how you handle yourself against my boss. Do you have the g")
                link(client, "I would like to have a try.", 3)
                link(client, "No. I will give up.", 255)
                pic(client, 134)
                create(client)

            else

                text(client, "Yeah. You are right. I advise you mind your own business.")
                link(client, "Everybody can say so.", 4)
                link(client, "Ok. I got to go.", 255)
                pic(client, 134)
                create(client)

            end

        end

    elseif (idx == 3) then

        move(client, 1070, 194, 185)

    elseif (idx == 4) then

        text(client, "You do get some guts. I will send you to my boss`s. Hope you won`t regret on what you said.")
        link(client, "I would like to have a try.", 3)
        link(client, "No. I will give up.", 255)
        pic(client, 134)
        create(client)

    end

end
