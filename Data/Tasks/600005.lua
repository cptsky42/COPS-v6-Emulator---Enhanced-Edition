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

function processTask600005(client, idx)
    name = "Ghost"
    face = 1

    if (idx == 0) then

        text(client, "I have been trapped here for many years. Many challengers have come but failed. When can I see a person conquer the tactics?")
        link(client, "How can I conquer?", 1)
        link(client, "What nonsense!", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 1) then

        if hasItem(client, 721010, 1) then

            text(client, "Wow! You have a token for Peace Tactic. My only wish is to see a person break through this tactic. I hope you can succeed.")
            link(client, "Why not leave here?", 2)
            pic(client, 9)
            create(client)

        else

            text(client, "You are so weak. I am afraid you will die from this tactic.")
            link(client, "I must prove my ability.", 255)
            pic(client, 9)
            create(client)

        end

    elseif (idx == 2) then

        text(client, "I do not want to get revived until I see a person break through the tactics. Perhaps you can make my dreams come true.")
        link(client, "How can I break through?", 3)
        pic(client, 9)
        create(client)

    elseif (idx == 3) then

        text(client, "According to my experience, the key tactic is Death, but I do not know how it works. You may try the other tactics first.")
        link(client, "How can I leave?", 4)
        pic(client, 9)
        create(client)

    elseif (idx == 4) then

        text(client, "Although I cannot help you break through this tactic, but I can help you leave here. I hope you can conquer the tactics soon.")
        link(client, "I must try my best.", 5)
        pic(client, 9)
        create(client)

    elseif (idx == 5) then

        text(client, "If you are ready to leave this tactic, I am going to teleport you out right now.")
        link(client, "I am ready.", 6)
        link(client, "I shall leave later.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 6) then

        move(client, 1042, 23, 24)

    end

end
