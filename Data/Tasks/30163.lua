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

function processTask30163(client, idx)
    name = "Joy"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721220, 1) then

            text(client, "Thanks a lot. I give my elite coat to you as a token of my gratitude.")
            link(client, "Thanks.", 1)
            link(client, "Do not mention it.", 255)
            pic(client, 118)
            create(client)

        else

            if hasItem(client, 721221, 1) then

                text(client, "Thanks.")
                link(client, "My pleasure.", 255)
                pic(client, 118)
                create(client)

            else

                text(client, "I feel very sad these days.")
                link(client, "What happened.", 2)
                link(client, "That is life.", 255)
                pic(client, 118)
                create(client)

            end

        end

    elseif (idx == 1) then

        spendItem(client, 721220, 1)
        awardItem(client, "133538", 1)

    elseif (idx == 2) then

        text(client, "My father was fond of collecting bows and arrows. He has not touched them since I lost his magic bow.")
        link(client, "Why not make one for him.", 3)
        link(client, "What a pity.", 255)
        pic(client, 118)
        create(client)

    elseif (idx == 3) then

        text(client, "My good friend, Mike, is skilled in making magic bows. Would you please take my letter to him and bring his bow back to me.")
        link(client, "Okay.", 4)
        link(client, "Sorry, I am helpless.", 255)
        pic(client, 118)
        create(client)

    elseif (idx == 4) then

        awardItem(client, "721221", 1)

    end

end
