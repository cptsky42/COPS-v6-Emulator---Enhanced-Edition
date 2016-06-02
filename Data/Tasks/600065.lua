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

function processTask600065(client, idx)
    name = "William"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "GlitterPowder") and hasTaskItem(client, "SecretLetter") and (getMoney(client) >= 0) then

            text(client, "If you give me the GlitterPowder and the Secret Letter, I can reveal the letter on it.")
            link(client, "Thanks.", 1)
            link(client, "Wait a moment.", 255)
            pic(client, 27)
            create(client)

        else

         text(client, "You see? It looks like a dreamland. I can only see the thick forest and hear the twitter of the happy birds.")
         link(client, "You are in good mood!", 2)
         pic(client, 27)
         create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "GlitterPowder") and hasTaskItem(client, "SecretLetter") and (getMoney(client) >= 0) then

            spendItem(client, 723006, 1)
            spendItem(client, 723008, 1)
            awardItem(client, "723009", 1)
            sendSysMsg(client, "The Moon Letter displayed lines of letters when the GlitterPowder was sprinkled on it.", 2011)
            text(client, "The moon above Ape Mountain and in the midnight? It looks like a poem but it does not tell where the Gem is.")
            text(client, "You can go to ask OldGeneral in ApeMountain for help with the letter.")
            link(client, "Thanks, I must go now.", 255)
            pic(client, 27)
            create(client)

        end

    elseif (idx == 2) then

        if hasItem(client, 723004, 1) then

            if hasItem(client, 723003, 1) then

                text(client, "Yes, indeed. What brings you here, my friend?")
                link(client, "Identify this letter.", 3)
                link(client, "Nothing, just passing by.", 255)
                pic(client, 27)
                create(client)

            else

                text(client, "It is so beautiful here. Why don`t you look around?")
                link(client, "Yes, I will.", 255)
                pic(client, 27)
                create(client)

            end

        else

            if hasItem(client, 723003, 1) then

                text(client, "Oh, Roger recommended you here. But you should get the unidentified letter first so that I can distinguish it.")
                link(client, "I see.", 255)
                pic(client, 27)
                create(client)

            else

                text(client, "It is so beautiful here. Why don`t you look around?")
                link(client, "Yes, I will.", 255)
                pic(client, 27)
                create(client)

            end

        end

    elseif (idx == 3) then

        if (rand(client, 100) < 50) then

            spendItem(client, 723004, 1)
            awardItem(client, "723006", 1)
            spendItem(client, 723003, 1)
            text(client, "This is the real letter. It was written by the celestial wine and only shown its letters when sprinkled with the GlitterPowder.")
            link(client, "How to get GlitterPowder?", 4)
            link(client, "I will give up.", 255)
            pic(client, 27)
            create(client)

        else

            spendItem(client, 723004, 1)
            awardItem(client, "723005", 1)
            spendItem(client, 723003, 1)
            text(client, "Do you come for the Gem? Frankly speaking, it is a FakeLetter.")
            text(client, "It is so emulational that BladeGhost can`t distinguish it. You can sell it to ToughWei outside the MysticCastle.")
            link(client, "Alright.", 255)
            link(client, "Forget it.", 255)
            pic(client, 27)
            create(client)

        end

    elseif (idx == 4) then

        text(client, "GlitterPowder comes from BrilliantStone. Alchemist in this city can make it. StoneMonster in each mine cave has BrilliantStone.")
        text(client, "If I have GlitterPowder, I can make the letter on the MoonLetter shown.")
        link(client, "I will help you.", 255)
        pic(client, 8)
        create(client)

    end

end
