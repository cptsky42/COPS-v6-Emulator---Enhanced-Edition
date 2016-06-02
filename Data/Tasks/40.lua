--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:10 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask40(client, idx)
    name = "Alchemist"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "BrilliantStone") and (getMoney(client) >= 0) then

            text(client, "The Stone is hard-won. I would like to spend $2,000 buying it, or I can help to grind it into powder for a fee of $800.")
            link(client, "Ok, I will sell it to you.", 1)
            link(client, "Grind it to GlitterPowder.", 2)
            link(client, "Let me think it over.", 255)
            pic(client, 10)
            create(client)

        else

         if hasItem(client, 1072031, 1) then

             text(client, "Do you come for inquirying about alchemy?")
             link(client, "Exactly.", 3)
             link(client, "Just passing by.", 255)
             pic(client, 10)
             create(client)

         else

             if hasItems(client, 1072050, 1072059, 1) then

                 text(client, "Have you got the gold ores yet?")
                 link(client, "Yeah.", 4)
                 link(client, "How can I get ores?", 5)
                 pic(client, 10)
                 create(client)

             else

                 text(client, "The miner providing material for my experiment got hurt, while the experiment is in the turning point. How can I carry on?")
                 link(client, "What material do you need?", 6)
                 link(client, "Just passing by.", 255)
                 pic(client, 10)
                 create(client)

             end

         end

        end

    elseif (idx == 1) then

        spendItem(client, 723007, 1)
        gainMoney(client, 2000)

    elseif (idx == 2) then

        if getMoney(client) < 799 then

            text(client, "You do not have enough money. Come when you have $800.")
            link(client, "Oh.", 255)
            pic(client, 10)
            create(client)

        else

            spendItem(client, 723007, 1)
            spendMoney(client, 800)
            awardItem(client, "723008", 1)
            text(client, "The powder is ok,and I will take the money away.")
            link(client, "Thank you.", 255)
            pic(client, 10)
            create(client)

        end

    elseif (idx == 3) then

        text(client, "I hear people often dig out mysterious euxenite ore not knowing their usage.")
        link(client, "What is it for?", 7)
        link(client, "Do not disturb me.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 4) then

        if hasItem(client, 1072059, 3) then

            text(client, "You have three rate 10 gold ores? That is great. I can do the experiment with them. May I exchange them with a Kylin Gem?")
            link(client, "Ok, it is a deal.", 8)
            link(client, "I need those ores too.", 255)
            pic(client, 10)
            create(client)

        else

            if hasItems(client, 1072052, 1072059, 3) then

                spendItems(client, 1072052, 1072059, 3)
                text(client, "Thanks a lot for your great help. This is a refined bag for Taoist. May it bring good luck to you.")
                link(client, "Thank you.", 9)
                pic(client, 10)
                create(client)

            else

                text(client, "Sorry.You do not have three gold ores above rate 3.")
                link(client, "I will bring ores soon.", 255)
                pic(client, 10)
                create(client)

            end

        end

    elseif (idx == 5) then

        text(client, "Do not stick to the same place, change position from time to time. If you can see much more dust, then there are more ores.")
        link(client, "I shall try again.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 6) then

        text(client, "I need three gold ores above rate 3. Could you help me?")
        link(client, "No problem.", 10)
        link(client, "Sorry, I am helpless.", 11)
        pic(client, 10)
        create(client)

    elseif (idx == 7) then

        text(client, "Based on the recipe handed down from my ancestors, I need them to refine a super armor. If you have, I shall buy from you.")
        link(client, "How much do you offer?", 12)
        link(client, "I have no ores.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 8) then

        spendItem(client, 1072059, 3)
        awardItem(client, "700041", 1)

    elseif (idx == 9) then

        awardItem(client, "121126", 1)

    elseif (idx == 10) then

        text(client, "If you can bring me the gold ores, I shall offer a good reward.")
        link(client, "Thanks in advance.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 11) then

        text(client, "That is ok if you can bring me more ores.")
        link(client, "OK.", 10)
        pic(client, 10)
        create(client)

    elseif (idx == 12) then

        text(client, "I shall offer 3000 silvers for five euxenite ores. How about that?")
        link(client, "OK. A deal.", 13)
        link(client, "Too cheap.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 13) then

        if hasItem(client, 1072031, 5) then

            if spendItem(client, 1072031, 5) then

                gainMoney(client, 3000)

            else

                text(client, "Sorry, you do not have five ores. 0")
                link(client, "OK.", 255)
                pic(client, 10)
                create(client)

            end

        else

            text(client, "Sorry, you do not have five ores. 0")
            link(client, "OK.", 255)
            pic(client, 10)
            create(client)

        end

    end

end
