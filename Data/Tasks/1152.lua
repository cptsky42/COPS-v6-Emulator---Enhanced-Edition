--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:12 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask1152(client, idx)
    name = "Simon"
    face = 1

    if (idx == 0) then

        text(client, "Great rewards will attract many brave people. I am looking for brave people to help me take my patrimony back.")
        text(client, "Can you help me? The rewards are handsome.")
        link(client, "Please tell me more.", 1)
        link(client, "What rewards?", 2)
        link(client, "I got SunDiamonds.", 3)
        link(client, "I got MoonDiamonds.", 4)
        link(client, "I got StarDiamonds.", 5)
        link(client, "I got CloudDiamonds.", 6)
        pic(client, 63)
        create(client)

    elseif (idx == 1) then

        text(client, "My ancestors built a Labyrinth long before. Many treasures were stored there like SunDiamonds, MoonDiamonds, ")
        text(client, "StarDiamond and so on. But it was occupied by fiece monsters soon. They expelled our clansmen and kept the treasure.")
        link(client, "It`s a pity.", 7)
        link(client, "I have no interest.", 255)
        pic(client, 63)
        create(client)

    elseif (idx == 2) then

        text(client, "SunDiamond, MoonDiamond, StarDiamond and CloudDiamond are kept by different monsters.")
        text(client, "If You get them for me, I will give you some rewards.")
        link(client, "What rewards?", 8)
        pic(client, 63)
        create(client)

    elseif (idx == 3) then

        if hasItem(client, 721533, 17) then

            spendItem(client, 721533, 17)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            text(client, "Well done. Here is the rewards I promised to you.")
            link(client, "Thanks.", 255)
            pic(client, 63)
            create(client)

        else

            text(client, "You are kidding on me. How dare you come here to claim prize with so few SunDiamonds?")
            link(client, "Wait. I will get more.", 255)
            pic(client, 63)
            create(client)

        end

    elseif (idx == 4) then

        if hasItem(client, 721534, 17) then

            spendItem(client, 721534, 17)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            text(client, "Well done. Here is the rewards I promised to you.")
            link(client, "Thanks.", 255)
            pic(client, 63)
            create(client)

        else

            text(client, "You are kidding on me. How dare you come here to claim prize with so few MoonDiamonds?")
            link(client, "Wait. I will get more.", 255)
            pic(client, 63)
            create(client)

        end

    elseif (idx == 5) then

        if hasItem(client, 721535, 17) then

            spendItem(client, 721535, 17)
            action = randomAction(client, 1, 8)
            if action == 1 then
                awardItem(client, "700051", 1)
                text(client, "Well done. Here is the rewards I promised to you.")
                link(client, "Thanks.", 255)
                pic(client, 63)
                create(client)
            elseif action == 2 then
                awardItem(client, "700061", 1)
                text(client, "Well done. Here is the rewards I promised to you.")
                link(client, "Thanks.", 255)
                pic(client, 63)
                create(client)
            elseif action == 3 then
                awardItem(client, "700031", 1)
                text(client, "Well done. Here is the rewards I promised to you.")
                link(client, "Thanks.", 255)
                pic(client, 63)
                create(client)
            elseif action == 4 then
                awardItem(client, "700041", 1)
                text(client, "Well done. Here is the rewards I promised to you.")
                link(client, "Thanks.", 255)
                pic(client, 63)
                create(client)
            elseif action == 5 then
                awardItem(client, "700001", 1)
                text(client, "Well done. Here is the rewards I promised to you.")
                link(client, "Thanks.", 255)
                pic(client, 63)
                create(client)
            elseif action == 6 then
                awardItem(client, "700011", 1)
                text(client, "Well done. Here is the rewards I promised to you.")
                link(client, "Thanks.", 255)
                pic(client, 63)
                create(client)
            elseif action == 7 then
                awardItem(client, "700021", 1)
                text(client, "Well done. Here is the rewards I promised to you.")
                link(client, "Thanks.", 255)
                pic(client, 63)
                create(client)
            elseif action == 8 then
                awardItem(client, "700051", 1)
                text(client, "Well done. Here is the rewards I promised to you.")
                link(client, "Thanks.", 255)
                pic(client, 63)
                create(client)
            end


        else

            text(client, "You are kidding on me. How dare you come here to claim prize with so few StarDiamonds?")
            link(client, "Wait. I will get more.", 255)
            pic(client, 63)
            create(client)

        end

    elseif (idx == 6) then

        if hasItem(client, 721536, 17) then

            spendItem(client, 721536, 17)
            awardItem(client, "721540", 1)
            text(client, "Well done. Here is the rewards I promised to you.")
            link(client, "Thanks.", 255)
            pic(client, 63)
            create(client)

        else

            text(client, "You are kidding on me. How dare you come here to claim prize with so few CloudDiamonds?")
            link(client, "Wait. I will get more.", 255)
            pic(client, 63)
            create(client)

        end

    elseif (idx == 7) then

        text(client, "I have always been here waiting for brave people to help me. Of course I can`t trust in those who do not")
        text(client, "have 2000 virtue points.")
        link(client, "How about me?", 9)
        link(client, "Sorry. That is too tough for me.", 255)
        pic(client, 63)
        create(client)

    elseif (idx == 8) then

        text(client, "2 meteors for 17 SunDiamonds, 4 meteors for 17 MoonDiamonds, a normal gem for 17 StarDiamonds and a AncestorBox")
        text(client, "for 17 CloudDiamonds. If you are lucky enough, you will get a big surprise from the box.")
        link(client, "I see. Thank you.", 255)
        pic(client, 63)
        create(client)

    elseif (idx == 9) then

        if getVirtue(client) < 2000 then

            text(client, "Sorry, you don`t have enough virtue points.")
            link(client, "Sigh...", 255)
            pic(client, 63)
            create(client)

        else

            text(client, "Great. You are kind-hearted. I believe you are able to help me out. Let me tell you something about the Labyrinth.")
            text(client, "Then you can have a good preparation.")
            link(client, "Thank you.", 10)
            pic(client, 63)
            create(client)

        end

    elseif (idx == 10) then

        text(client, "The Labyrinth contains 4 floors guarded by different monsters. If you can find the treasures and give them to me, I will reward")
        link(client, "Please continue.", 11)
        pic(client, 63)
        create(client)

    elseif (idx == 11) then

        text(client, "There are 3 kinds of monsters on each floor. The more monsters, the weaker they are. The ones in lesser")
        text(client, "quantities are strong. The fiercest monsters are always moving around. And they drop different tokens.")
        link(client, "What do they drop?", 12)
        pic(client, 63)
        create(client)

    elseif (idx == 12) then

        text(client, "SkyToken, EarthToken and SoulToken you need to enter the next floor. While the weak monsters usually drop treasure.")
        text(client, "After You get a token, find a general who will send you to the next floor. Some boss monsters drop rare items.")
        link(client, "I see. Thanks.", 13)
        link(client, "I must leave now.", 255)
        pic(client, 63)
        create(client)

    elseif (idx == 13) then

        text(client, "Let me teleport you there, but I will deduct 2000 of your virtue points. Are you ready?")
        link(client, "Yeah.", 14)
        pic(client, 63)
        create(client)

    elseif (idx == 14) then

        if spendVirtue(client, 2000) then

            move(client, 1351, 16, 128)
            setRecordPos(client, 1002, 430, 380)

        else

            text(client, "Sorry, you don`t have enough virtue points.")
            link(client, "Sigh...", 255)
            pic(client, 63)
            create(client)

        end

    end

end
