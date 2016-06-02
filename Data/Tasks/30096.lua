--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:49 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30096(client, idx)
    name = "Richard"
    face = 1

    if (idx == 0) then

        if getMoney(client) < 1 then

            action = randomAction(client, 1, 8)
            if action == 1 then
                moveNpc(client, 30096, 1002, 439, 290)
                text(client, "Do not bother me!")
                link(client, "Well.", 255)
                pic(client, 8)
                create(client)
            elseif action == 2 then
                moveNpc(client, 30096, 1002, 432, 330)
                text(client, "Do not bother me!")
                link(client, "Well.", 255)
                pic(client, 8)
                create(client)
            elseif action == 3 then
                moveNpc(client, 30096, 1002, 455, 394)
                text(client, "Do not bother me!")
                link(client, "Well.", 255)
                pic(client, 8)
                create(client)
            elseif action == 4 then
                moveNpc(client, 30096, 1002, 458, 375)
                text(client, "Do not bother me!")
                link(client, "Well.", 255)
                pic(client, 8)
                create(client)
            elseif action == 5 then
                moveNpc(client, 30096, 1002, 387, 363)
                text(client, "Do not bother me!")
                link(client, "Well.", 255)
                pic(client, 8)
                create(client)
            elseif action == 6 then
                moveNpc(client, 30096, 1002, 398, 357)
                text(client, "Do not bother me!")
                link(client, "Well.", 255)
                pic(client, 8)
                create(client)
            elseif action == 7 then
                moveNpc(client, 30096, 1002, 446, 221)
                text(client, "Do not bother me!")
                link(client, "Well.", 255)
                pic(client, 8)
                create(client)
            elseif action == 8 then
                moveNpc(client, 30096, 1002, 422, 295)
                text(client, "Do not bother me!")
                link(client, "Well.", 255)
                pic(client, 8)
                create(client)
            end


        else

            if getCurHP(client) < 93 then

                action = randomAction(client, 1, 8)
                if action == 1 then
                    moveNpc(client, 30096, 1002, 439, 290)
                    text(client, "Do not bother me!")
                    link(client, "Well.", 255)
                    pic(client, 8)
                    create(client)
                elseif action == 2 then
                    moveNpc(client, 30096, 1002, 432, 330)
                    text(client, "Do not bother me!")
                    link(client, "Well.", 255)
                    pic(client, 8)
                    create(client)
                elseif action == 3 then
                    moveNpc(client, 30096, 1002, 455, 394)
                    text(client, "Do not bother me!")
                    link(client, "Well.", 255)
                    pic(client, 8)
                    create(client)
                elseif action == 4 then
                    moveNpc(client, 30096, 1002, 458, 375)
                    text(client, "Do not bother me!")
                    link(client, "Well.", 255)
                    pic(client, 8)
                    create(client)
                elseif action == 5 then
                    moveNpc(client, 30096, 1002, 387, 363)
                    text(client, "Do not bother me!")
                    link(client, "Well.", 255)
                    pic(client, 8)
                    create(client)
                elseif action == 6 then
                    moveNpc(client, 30096, 1002, 398, 357)
                    text(client, "Do not bother me!")
                    link(client, "Well.", 255)
                    pic(client, 8)
                    create(client)
                elseif action == 7 then
                    moveNpc(client, 30096, 1002, 446, 221)
                    text(client, "Do not bother me!")
                    link(client, "Well.", 255)
                    pic(client, 8)
                    create(client)
                elseif action == 8 then
                    moveNpc(client, 30096, 1002, 422, 295)
                    text(client, "Do not bother me!")
                    link(client, "Well.", 255)
                    pic(client, 8)
                    create(client)
                end


            else

                if (rand(client, 3) < 1) then

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        moveNpc(client, 30096, 1002, 439, 290)
                        text(client, "Do not bother me!")
                        link(client, "Well.", 255)
                        pic(client, 8)
                        create(client)
                    elseif action == 2 then
                        moveNpc(client, 30096, 1002, 432, 330)
                        text(client, "Do not bother me!")
                        link(client, "Well.", 255)
                        pic(client, 8)
                        create(client)
                    elseif action == 3 then
                        moveNpc(client, 30096, 1002, 455, 394)
                        text(client, "Do not bother me!")
                        link(client, "Well.", 255)
                        pic(client, 8)
                        create(client)
                    elseif action == 4 then
                        moveNpc(client, 30096, 1002, 458, 375)
                        text(client, "Do not bother me!")
                        link(client, "Well.", 255)
                        pic(client, 8)
                        create(client)
                    elseif action == 5 then
                        moveNpc(client, 30096, 1002, 387, 363)
                        text(client, "Do not bother me!")
                        link(client, "Well.", 255)
                        pic(client, 8)
                        create(client)
                    elseif action == 6 then
                        moveNpc(client, 30096, 1002, 398, 357)
                        text(client, "Do not bother me!")
                        link(client, "Well.", 255)
                        pic(client, 8)
                        create(client)
                    elseif action == 7 then
                        moveNpc(client, 30096, 1002, 446, 221)
                        text(client, "Do not bother me!")
                        link(client, "Well.", 255)
                        pic(client, 8)
                        create(client)
                    elseif action == 8 then
                        moveNpc(client, 30096, 1002, 422, 295)
                        text(client, "Do not bother me!")
                        link(client, "Well.", 255)
                        pic(client, 8)
                        create(client)
                    end


                else

                    if hasItem(client, 721121, 1) then

                        text(client, "Oh, John sent you to find me? You see the situation is very bad to me due to the jewellery stolen case. What a pity.")
                        link(client, "What did you mean?", 1)
                        link(client, "Everything is over. Bye.", 255)
                        pic(client, 8)
                        create(client)

                    else

                        text(client, "Money makes the mare to go.")
                        link(client, "A moneygrubber.", 255)
                        link(client, "Man should not do so.", 2)
                        pic(client, 8)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 1) then

        text(client, "Tell you a secret, I am the famous thief Richard. I have pried into the palace for a long time, but cannot find a chance.")
        link(client, "Guard! Here is a thief.", 3)
        link(client, "I got to go.", 255)
        pic(client, 8)
        create(client)

    elseif (idx == 2) then

        text(client, "That is quite right.")
        link(client, "Yes.", 255)
        pic(client, 8)
        create(client)

    elseif (idx == 3) then

        spendItem(client, 721121, 1)
        if (rand(client, 3) < 1) then

            addLife(client, -500)
            addLife(client, 1)
            text(client, "How dare you reveal the inside story? I must teach you a lesson.")
            link(client, "You are a cruel guy.", 255)
            pic(client, 8)
            create(client)

        else

            if (rand(client, 2) < 1) then

                text(client, "Be quiet. Here is 1000 silvers. Take it and do not say anything.")
                link(client, "Guard! Guard!", 4)
                link(client, "It is a deal.", 5)
                pic(client, 8)
                create(client)

            else

                gainMoney(client, 250)
                sendSysMsg(client, "Justice overcomes evil. Thief Richard was put into the jail and you received a reward of 250 silvers.", 2007)
                text(client, "Oh, my god! When can I be set free?")
                link(client, "You must pay for that.", 255)
                pic(client, 8)
                create(client)

            end

        end

    elseif (idx == 4) then

        if (rand(client, 2) < 1) then

            text(client, "How dare you reveal the inside story? I must teach you a lesson.")
            link(client, "You are a cruel guy.", 255)
            pic(client, 8)
            create(client)

        else

            sendSysMsg(client, "Justice overcomes evil. Thief Richard was put into the jail and you received a reward of 250 silvers.", 2007)
            text(client, "Oh, my god! When can I be set free?")
            link(client, "You must pay for that.", 255)
            pic(client, 8)
            create(client)

        end

    elseif (idx == 5) then

        if (rand(client, 2) < 1) then

            gainMoney(client, 1000)

        else

            if spendMoney(client, 200) then

                sendSysMsg(client, "You are too greedy and were cheated by Richard. He has stolen all your money.", 2007)
                text(client, "Haha, you fool.")
                link(client, "You evil.", 255)
                pic(client, 8)
                create(client)

            else

                addLife(client, -500)
                addLife(client, 1)
                text(client, "It is ashamed for you to carry so little money with you. You just waste my time and I shall give you some suffering.")
                link(client, "You will pay for that.", 255)
                pic(client, 8)
                create(client)

            end

        end

    end

end
