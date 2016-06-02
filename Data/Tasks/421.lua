--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:11 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask421(client, idx)
    name = "Norbert"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721262, 1) then

            if hasItem(client, 721262, 5) then

                if hasItem(client, 721263, 1) then

                    if getMoney(client) < 30000 then

                        text(client, "Five pieces of Saltpeter and one piece of Sulphur are required to make the Bomb. And I charge a fee of 30,000 silvers.")
                        link(client, "What is the Bomb?", 1)
                        link(client, "I see.", 255)
                        pic(client, 67)
                        create(client)

                    else

                        text(client, "Hi, you have got the meterial I want. Do you come for the Bomb?")
                        link(client, "Yeah.", 2)
                        link(client, "Just passing by.", 255)
                        pic(client, 67)
                        create(client)

                    end

                else

                    text(client, "Five pieces of Saltpeter and one piece of Sulphur are required to make the Bomb. And I charge a fee of 30,000 silvers.")
                    link(client, "What is the Bomb?", 1)
                    link(client, "I see.", 255)
                    pic(client, 67)
                    create(client)

                end

            else

                text(client, "Five pieces of Saltpeter and one piece of Sulphur are required to make the Bomb. And I charge a fee of 30,000 silvers.")
                link(client, "What is the Bomb?", 1)
                link(client, "I see.", 255)
                pic(client, 67)
                create(client)

            end

        else

            if hasItem(client, 721263, 1) then

                text(client, "Five pieces of Saltpeter and one piece of Sulphur are required to make the Bomb. And I charge a fee of 30,000 silvers.")
                link(client, "What is the Bomb?", 1)
                link(client, "I see.", 255)
                pic(client, 67)
                create(client)

            else

                text(client, "Saltpeter, Sulphur, charcoal, and now make a fire...")
                link(client, "What are you doing?", 3)
                link(client, "I`d better not bother you.", 255)
                pic(client, 67)
                create(client)

            end

        end

    elseif (idx == 1) then

        text(client, "Blow up the guild gate! I attended the guild war and witnessed lots of casualties in attacking the gate. Ever")
        text(client, "since, I devoted myself to inventing a kind of powerful bomb which can smash the guild gate easily. I finally work it out.")
        link(client, "Can you make one for me?", 4)
        link(client, "Oh. I see.", 255)
        pic(client, 67)
        create(client)

    elseif (idx == 2) then

        if hasItem(client, 721262, 5) then

            if hasItem(client, 721263, 1) then

                if getMoney(client) < 30000 then

                    text(client, "But you do not take the materials I need.")
                    link(client, "Oh, I will get them ready.", 255)
                    pic(client, 67)
                    create(client)

                else

                    spendItem(client, 721262, 5)
                    spendItem(client, 721263, 1)
                    spendMoney(client, 30000)
                    awardItem(client, "721261", 1)
                    setUserStats(client, 3, 0, getUserStats(3, 0) + 1, true)

                end

            else

                text(client, "But you do not take the materials I need.")
                link(client, "Oh, I will get them ready.", 255)
                pic(client, 67)
                create(client)

            end

        else

            text(client, "But you do not take the materials I need.")
            link(client, "Oh, I will get them ready.", 255)
            pic(client, 67)
            create(client)

        end

    elseif (idx == 3) then

        text(client, "I`m refining a bomb which is very powerful but dangerous.")
        link(client, "What is it used for?", 1)
        link(client, "Wow, I`d better leave here.", 255)
        pic(client, 67)
        create(client)

    elseif (idx == 4) then

        text(client, "I`d be glad to. Mind you that this bomb is so powerful that it will exert enormous damage to the gate and kill the Bomb user.")
        link(client, "Thanks. I need it badly.", 5)
        link(client, "Then forget it.", 255)
        pic(client, 67)
        create(client)

    elseif (idx == 5) then

        text(client, "Good. You prepare five pieces of Saltpeter and a piece of Sulphur. And I will charge 30000 silvers as fees and other materials.")
        link(client, "Where do I get the materials?", 6)
        link(client, "I`ll give up.", 255)
        pic(client, 67)
        create(client)

    elseif (idx == 6) then

        text(client, "You can get Saltpeter from my friend OldQuarrier in Ape City and get Sulphur from the Bandits nearby.")
        link(client, "Ok, I`ll get them ready.", 255)
        pic(client, 67)
        create(client)

    end

end
