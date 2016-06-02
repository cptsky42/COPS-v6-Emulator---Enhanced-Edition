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

function processTask30120(client, idx)
    name = "DevineArtisan"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721150, 1) then

            text(client, "Thank you for retrieve WaterRing. Here is my token of gratitude. Please take it.")
            link(client, "Thanks a lot.", 1)
            pic(client, 48)
            create(client)

        else

            if hasItem(client, 721152, 1) then

                text(client, "Hades made so bold as to change the Roll of Metempsychosis. But that is none of my business. Take my Seal to fetch WaterRing.")
                link(client, "Ok, I will be right back.", 2)
                pic(client, 48)
                create(client)

            else

                if hasItem(client, 721151, 1) then

                    text(client, "Hades will know why you come to him at the sight of my letter.")
                    link(client, "Oh, I see.", 255)
                    pic(client, 48)
                    create(client)

                else

                    text(client, "There are five worlds since the beginning: human world, ghostdom, spirit world, kingdom of god and devildom.")
                    text(client, "As ages passed, and things changed, the communication among the five worlds died out gradually.")
                    link(client, "And then?", 3)
                    link(client, "Oh, nice story.", 255)
                    pic(client, 48)
                    create(client)

                end

            end

        end

    elseif (idx == 1) then

        spendItem(client, 721150, 1)
        if (rand(client, 2) < 1) then

            if (rand(client, 5) < 1) then

                awardItem(client, "150137", 1)

            else

                awardItem(client, "150136", 1)

            end

        else

            if (rand(client, 5) < 1) then

                awardItem(client, "152127", 1)

            else

                awardItem(client, "152126", 1)

            end

        end

    elseif (idx == 2) then

        spendItem(client, 721152, 1)
        awardItem(client, "721153", 1)

    elseif (idx == 3) then

        text(client, "I conjured all the power of the universe and made five mighty treasures: HeartMirror, KeyWand, LoveLoop,")
        text(client, "UniverseSword and WaterRing, in the hope that they will unite the five worlds again.")
        link(client, "Isn`t that good?", 4)
        pic(client, 48)
        create(client)

    elseif (idx == 4) then

        text(client, "But things went out of my control. The five worlds coveted the treasures and launched wars to snatch them.")
        link(client, "Pity! I got to go.", 255)
        link(client, "How do you deal with them?", 5)
        pic(client, 48)
        create(client)

    elseif (idx == 5) then

        text(client, "Retrieve the 5 treasures and seal them first. Then I`ll unseal them and give them to the 5 worlds, so they will reunite again.")
        link(client, "It won`t be hard for you.", 6)
        pic(client, 48)
        create(client)

    elseif (idx == 6) then

        text(client, "Yeah. I`ve taken back four. But WaterRing, having been held by Hades, disappeared from the world. He must be looking")
        text(client, "for it too.I was wondering if you could help me find WaterRing back?")
        link(client, "No problem.", 7)
        pic(client, 48)
        create(client)

    elseif (idx == 7) then

        awardItem(client, "721151", 1)
        text(client, "Thanks very much. Hades lives in the Love Canyon. You may get some clues from him.")
        link(client, "Ok. I will try my best.", 255)
        pic(client, 48)
        create(client)

    end

end
