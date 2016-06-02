--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:52 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600001(client, idx)
    name = "Minner"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "MeteorTear") and (getMoney(client) >= 0) then

            text(client, "Thanks for your news. Please take this meteor tear. I hope you are happy all your life.")
            link(client, "Thanks.", 255)
            pic(client, 2)
            create(client)

        elseif hasTaskItem(client, "SadMeteor") and (getMoney(client) >= 0) then

            text(client, "I cannot help crying. Where did you get this sad meteor? What happened?")
            link(client, "It is from Joe.", 1)
            link(client, "Sorry, I cannot tell you.", 255)
            pic(client, 2)
            create(client)

        elseif hasTaskItem(client, "GuardianStar") and (getMoney(client) >= 0) then

            text(client, "You have not found him yet? Wherever he goes and whatever he does, I shall be right here waiting for him.")
            link(client, "I am trying to find him.", 255)
            pic(client, 2)
            create(client)

        else

         text(client, "Should auld acquaintance be forgotten and never brought to mind? Should auld acquaintance be forgotten and auld lang syne?")
         link(client, "Why are you so sad?", 2)
         link(client, "Nice song.", 255)
         pic(client, 2)
         create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "SadMeteor") and (getMoney(client) >= 0) then

            text(client, "Joe! You have seen him? Where is he now? Is everything going on well with him?")
            link(client, "Do not cry.", 3)
            link(client, "Do not ruin the meteor!", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 2) then

        if hasTaskItem(client, "Milly`sLetter") and (getMoney(client) >= 0) then

            text(client, "Thanks. I am so pleased to hear from my sister. I have not seen her for a long time. It is very thoughtful of her. Sigh!")
            link(client, "Why are you so sad?", 4)
            link(client, "I had better leave now.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 3) then

        if hasTaskItem(client, "SadMeteor") and (getMoney(client) >= 0) then

            sendSysMsg(client, "Joe loves and misses you very much. He engraved his love on this meteor. He said you will understand after you see it.", 2011)
            text(client, "It is said that true love can move the meteor to cry and turn it to meteor tear. I am so glad that Joe does love me.")
            link(client, "All will be fine.", 5)
            link(client, "Return my meteor.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 4) then

        if hasTaskItem(client, "Milly`sLetter") and (getMoney(client) >= 0) then

            text(client, "My lover has left me without any reason. I tried to find him but got no news since he left. Can you help me to find him?")
            link(client, "OK. I shall help you.", 6)
            link(client, "Sorry, I am too busy.", 7)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 5) then

        if hasTaskItem(client, "SadMeteor") and (getMoney(client) >= 0) then

            text(client, "Joe, Can you hear me? I love you. wherever you go, whatever you do, I will be right here waiting for you.")
            link(client, "Sad love story. Sigh.", 8)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 6) then

        if hasTaskItem(client, "Milly`sLetter") and (getMoney(client) >= 0) then

            spendItem(client, 721000, 1)
            text(client, "Thanks. Joe gave me this bag as our love token. Give it to him when you find him and tell him I am always here waiting for him.")
            link(client, "I shall try my best!", 9)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 7) then

        if hasTaskItem(client, "Milly`sLetter") and (getMoney(client) >= 0) then

            spendItem(client, 721000, 1)
            text(client, "Thank you for the letter. Good bye.")
            link(client, "See you.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 8) then

        if hasTaskItem(client, "SadMeteor") and (getMoney(client) >= 0) then

            text(client, "Since Joe enjoys traveling round the world, I only wish he is happy. I shall be very delighted Whenever he thinks of me.")
            link(client, "How about you then?", 10)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 9) then

        awardItem(client, "721001", 1)

    elseif (idx == 10) then

        if hasTaskItem(client, "SadMeteor") and (getMoney(client) >= 0) then

            text(client, "It is said a star in the sky represents a person on the earth. When I miss him, I can look at his star and it will twinkle.")
            link(client, "I hope Joe will return.", 11)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 11) then

        if hasTaskItem(client, "SadMeteor") and (getMoney(client) >= 0) then

            spendItem(client, 721002, 1)
            awardItem(client, "1088002", 1)

        end

    end

end
