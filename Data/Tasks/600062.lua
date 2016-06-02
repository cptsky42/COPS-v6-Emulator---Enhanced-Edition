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

function processTask600062(client, idx)
    name = "GossiperWang"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "Hankerchief") and (getMoney(client) >= 0) then

            text(client, "What a nice day. What brings you here?")
            link(client, "Return your hankerchief.", 1)
            link(client, "Nothing.", 255)
            pic(client, 28)
            create(client)

        else

         text(client, "LOL. It is ridiculous that cheesy Rogger can not fall asleep because of a woman.")
         link(client, "You are a real gossiper!", 255)
         link(client, "Here is a letter for you.", 2)
         pic(client, 28)
         create(client)

        end

    elseif (idx == 1) then

        text(client, "Handkerchief? I do not need it. Take it away.")
        link(client, "Alright.", 255)
        pic(client, 28)
        create(client)

    elseif (idx == 2) then

        if hasItem(client, 723000, 1) then

            text(client, "Mr. Pine again! He got a pair of shoes from me and then went saying nothing. Now come for the Moon Gem. I have no idea!")
            link(client, "Do you know everything!", 3)
            link(client, "Oh, I come here in vain.", 255)
            pic(client, 28)
            create(client)

        else

            text(client, "Ha-ha, you are kidding.")
            link(client, "It is only a joke.", 255)
            pic(client, 28)
            create(client)

        end

    elseif (idx == 3) then

        text(client, "Ha-ha. You are sweet. I do know something. But it is not in Bird Island. Dr. Rogger in this city may know it exactly.")
        link(client, "Dr. Rogger?", 4)
        link(client, "You are flamming again!", 255)
        pic(client, 28)
        create(client)

    elseif (idx == 4) then

        text(client, "Yes, Rogger is a famous tea doctor. People from different places come to visit him, and nothing escapes his knowledge.")
        text(client, "I hear that he is distressed by insomnia recently. If you find a Balsamine to cure him, he may tell you where the Gem is.")
        link(client, "Balsamine? How to get it?", 5)
        link(client, "I will give up.", 255)
        pic(client, 28)
        create(client)

    elseif (idx == 5) then

        text(client, "A herborist said that Balsamine is dominated by Lonely Tyrant, the bandit king in Maple Forest. I will give you a sketch map.")
        text(client, "This is my sister Rachel`s handkerchief. I draw the map in it. But you must return after you get it, or she will blame me.")
        link(client, "Ok, I will go.", 6)
        link(client, "I got to go.", 255)
        pic(client, 28)
        create(client)

    elseif (idx == 6) then

        spendItem(client, 723000, 1)
        awardItem(client, "723001", 1)

    end

end
