--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 6/23/2015 6:56:02 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask42(client, idx)
    name = "Warden"
    face = 1

    if (idx == 0) then

        text(client, "What can I do for you?")
        link(client, "Let me out, please.", 1)
        link(client, "Lend me a hoe, ok?", 2)
        link(client, "Just passing by.", 255)
        pic(client, 2)
        create(client)

    elseif (idx == 1) then

        if getPkPoints(client) < 100 then

            text(client, "You do not look like an evildoer. You may leave now.")
            link(client, "Thanks.", 3)
            pic(client, 2)
            create(client)

        else

            if getPkPoints(client) < 500 then

                text(client, "Sorry, you cannot leave here. I shall not release you until you are in red name or give me two gold ores.")
                link(client, "Here are gold ores.", 4)
                pic(client, 2)
                create(client)

            else

                if getPkPoints(client) < 1000 then

                    text(client, "You have killed too many people. I shall not let you out until your PK points are lower than 100 or give me 3 gold ores.")
                    link(client, "Here are 3 gold ores.", 5)
                    pic(client, 2)
                    create(client)

                else

                    if getPkPoints(client) < 3000 then

                        text(client, "You have killed too many people. I shall not let you out until your PK points are lower than 100 or give me 4 gold ores.")
                        link(client, "Here are 4 gold ores.", 6)
                        pic(client, 2)
                        create(client)

                    else

                        if getPkPoints(client) < 10000 then

                            text(client, "You have killed too many people. I shall not let you out until your PK points are lower than 100 or give me 5 gold ores.")
                            link(client, "Here are 5 gold ores.", 7)
                            pic(client, 2)
                            create(client)

                        else

                            if getPkPoints(client) < 40000 then

                                text(client, "You have killed too many people. I shall not let you out until your PK points are lower than 100 or give me 6 gold ores.")
                                link(client, "Here are 6 gold ores.", 8)
                                pic(client, 2)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 2) then

        text(client, "Take this hoe. Work hard. If you can get gold ores and give two to me, I may let you out.")
        link(client, "Thanks, sir.", 9)
        pic(client, 2)
        create(client)

    elseif (idx == 3) then

        move(client, 1002, 518, 356)

    elseif (idx == 4) then

        if hasItems(client, 1072050, 1072059, 2) then

            text(client, "Since you have given me two gold ores, you may leave now.")
            link(client, "Thanks, sir.", 10)
            pic(client, 2)
            create(client)

        else

            text(client, "Sorry, you do not have the required gold ores. How dare you cheat me?")
            link(client, "I dare not.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 5) then

        if hasItems(client, 1072050, 1072059, 3) then

            text(client, "Since you have given me 3 gold ores, you may leave now.")
            link(client, "Thanks, sir.", 11)
            pic(client, 2)
            create(client)

        else

            text(client, "Sorry, you do not have the required gold ores. How dare you cheat me?")
            link(client, "I dare not.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 6) then

        if hasItems(client, 1072050, 1072059, 4) then

            text(client, "Since you have given me 4 gold ores, you may leave now.")
            link(client, "Thanks, sir.", 12)
            pic(client, 2)
            create(client)

        else

            text(client, "Sorry, you do not have the required gold ores. How dare you cheat me?")
            link(client, "I dare not.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 7) then

        if hasItems(client, 1072050, 1072059, 5) then

            text(client, "Since you have given me 5 gold ores, you may leave now.")
            link(client, "Thanks, sir.", 13)
            pic(client, 2)
            create(client)

        else

            text(client, "Sorry, you do not have the required gold ores. How dare you cheat me?")
            link(client, "I dare not.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 8) then

        if hasItems(client, 1072050, 1072059, 6) then

            text(client, "Since you have given me 6 gold ores, you may leave now.")
            link(client, "Thanks, sir.", 14)
            pic(client, 2)
            create(client)

        else

            text(client, "Sorry, you do not have the required gold ores. How dare you cheat me?")
            link(client, "I dare not.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 9) then

        awardItem(client, "562001", 1)

    elseif (idx == 10) then

        spendItems(client, 1072050, 1072059, 2)
        action = randomAction(client, 1, 8)
        if action == 1 then
            move(client, 1000, 567, 764)
        elseif action == 2 then
            move(client, 1002, 738, 784)
        elseif action == 3 then
            move(client, 1002, 414, 8)
        elseif action == 4 then
            move(client, 1020, 455, 167)
        elseif action == 5 then
            move(client, 1000, 274, 436)
        elseif action == 6 then
            move(client, 1015, 674, 785)
        elseif action == 7 then
            move(client, 1015, 166, 189)
        elseif action == 8 then
            move(client, 1011, 382, 752)
        end


    elseif (idx == 11) then

        spendItems(client, 1072050, 1072059, 3)
        action = randomAction(client, 1, 8)
        if action == 1 then
            move(client, 1000, 567, 764)
        elseif action == 2 then
            move(client, 1002, 738, 784)
        elseif action == 3 then
            move(client, 1002, 414, 8)
        elseif action == 4 then
            move(client, 1020, 455, 167)
        elseif action == 5 then
            move(client, 1000, 274, 436)
        elseif action == 6 then
            move(client, 1015, 674, 785)
        elseif action == 7 then
            move(client, 1015, 166, 189)
        elseif action == 8 then
            move(client, 1011, 382, 752)
        end


    elseif (idx == 12) then

        spendItems(client, 1072050, 1072059, 4)
        action = randomAction(client, 1, 8)
        if action == 1 then
            move(client, 1000, 567, 764)
        elseif action == 2 then
            move(client, 1002, 738, 784)
        elseif action == 3 then
            move(client, 1002, 414, 8)
        elseif action == 4 then
            move(client, 1020, 455, 167)
        elseif action == 5 then
            move(client, 1000, 274, 436)
        elseif action == 6 then
            move(client, 1015, 674, 785)
        elseif action == 7 then
            move(client, 1015, 166, 189)
        elseif action == 8 then
            move(client, 1011, 382, 752)
        end


    elseif (idx == 13) then

        spendItems(client, 1072050, 1072059, 5)
        action = randomAction(client, 1, 8)
        if action == 1 then
            move(client, 1000, 567, 764)
        elseif action == 2 then
            move(client, 1002, 738, 784)
        elseif action == 3 then
            move(client, 1002, 414, 8)
        elseif action == 4 then
            move(client, 1020, 455, 167)
        elseif action == 5 then
            move(client, 1000, 274, 436)
        elseif action == 6 then
            move(client, 1015, 674, 785)
        elseif action == 7 then
            move(client, 1015, 166, 189)
        elseif action == 8 then
            move(client, 1011, 382, 752)
        end


    elseif (idx == 14) then

        spendItems(client, 1072050, 1072059, 6)
        action = randomAction(client, 1, 8)
        if action == 1 then
            move(client, 1000, 567, 764)
        elseif action == 2 then
            move(client, 1002, 738, 784)
        elseif action == 3 then
            move(client, 1002, 414, 8)
        elseif action == 4 then
            move(client, 1020, 455, 167)
        elseif action == 5 then
            move(client, 1000, 274, 436)
        elseif action == 6 then
            move(client, 1015, 674, 785)
        elseif action == 7 then
            move(client, 1015, 166, 189)
        elseif action == 8 then
            move(client, 1011, 382, 752)
        end


    end

end
