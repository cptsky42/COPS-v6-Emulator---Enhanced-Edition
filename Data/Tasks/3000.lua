--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:27 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask3000(client, idx)
    name = "DoctorHolt"
    face = 1

    if (idx == 0) then

        if hasItem(client, 722809, 1) then

            if hasItem(client, 722807, 1) then

                if hasItem(client, 722808, 1) then

                    text(client, "With those ingredients, I will be able to make the medicine to control the plague. Could you send them to me?")
                    link(client, "Sure. That is what I am coming for.", 1)
                    link(client, "I`d rather keep SaviorPill to myself.", 255)
                    pic(client, 15)
                    create(client)

                else

                    text(client, "SaviorPill is rare and invaluable. You must have racked your brains to get it.")
                    link(client, "Yeah. I know that you need it badly.", 2)
                    link(client, "It is no sweat at all.", 255)
                    pic(client, 15)
                    create(client)

                end

            else

                text(client, "SaviorPill is rare and invaluable. You must have racked your brains to get it.")
                link(client, "Yeah. I know that you need it badly.", 2)
                link(client, "It is no sweat at all.", 255)
                pic(client, 15)
                create(client)

            end

        else

            if hasItem(client, 722817, 1) then

                if hasItem(client, 722818, 1) then

                    if hasItem(client, 722819, 1) then

                        text(client, "Haven`t you found out the Antique Dealer?")
                        link(client, "It won`t be long.", 255)
                        pic(client, 15)
                        create(client)

                    else

                        if hasItem(client, 722807, 1) then

                            if hasItem(client, 722808, 1) then

                                text(client, "You are marvelous to get those two items in such a short notice.")
                                link(client, "I am concerned with the villagers too.", 3)
                                pic(client, 15)
                                create(client)

                            else

                                text(client, "FireSpiritFang and Aweto must be used together as the ingredients.")
                                link(client, "Oh, I see.", 255)
                                pic(client, 15)
                                create(client)

                            end

                        else

                            if hasItem(client, 722808, 1) then

                                text(client, "FireSpiritFang and Aweto must be used together as the ingredients.")
                                link(client, "Oh, I see.", 255)
                                pic(client, 15)
                                create(client)

                            else

                                text(client, "May I stop you for a minute? It gets on my nerves. I really need your help.")
                                link(client, "Take it easy. I am glad to be of help.", 4)
                                link(client, "I am sorry, I`ve got something important to do.", 255)
                                pic(client, 15)
                                create(client)

                            end

                        end

                    end

                else

                    if hasItem(client, 722807, 1) then

                        if hasItem(client, 722808, 1) then

                            text(client, "You are marvelous to get those two items in such a short notice.")
                            link(client, "I am concerned with the villagers too.", 3)
                            pic(client, 15)
                            create(client)

                        else

                            text(client, "FireSpiritFang and Aweto must be used together as the ingredients.")
                            link(client, "Oh, I see.", 255)
                            pic(client, 15)
                            create(client)

                        end

                    else

                        if hasItem(client, 722808, 1) then

                            text(client, "FireSpiritFang and Aweto must be used together as the ingredients.")
                            link(client, "Oh, I see.", 255)
                            pic(client, 15)
                            create(client)

                        else

                            text(client, "May I stop you for a minute? It gets on my nerves. I really need your help.")
                            link(client, "Take it easy. I am glad to be of help.", 4)
                            link(client, "I am sorry, I`ve got something important to do.", 255)
                            pic(client, 15)
                            create(client)

                        end

                    end

                end

            else

                if hasItem(client, 722807, 1) then

                    if hasItem(client, 722808, 1) then

                        text(client, "You are marvelous to get those two items in such a short notice.")
                        link(client, "I am concerned with the villagers too.", 3)
                        pic(client, 15)
                        create(client)

                    else

                        text(client, "FireSpiritFang and Aweto must be used together as the ingredients.")
                        link(client, "Oh, I see.", 255)
                        pic(client, 15)
                        create(client)

                    end

                else

                    if hasItem(client, 722808, 1) then

                        text(client, "FireSpiritFang and Aweto must be used together as the ingredients.")
                        link(client, "Oh, I see.", 255)
                        pic(client, 15)
                        create(client)

                    else

                        text(client, "May I stop you for a minute? It gets on my nerves. I really need your help.")
                        link(client, "Take it easy. I am glad to be of help.", 4)
                        link(client, "I am sorry, I`ve got something important to do.", 255)
                        pic(client, 15)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 1) then

        text(client, "Thanks to your kindness, the villagers will survive the plague. Please take this meteor as my token of thankfulness.")
        link(client, "Thanks a lot.", 5)
        pic(client, 15)
        create(client)

    elseif (idx == 2) then

        text(client, "But having SaviorPill is not enough. I need FireSpiritFang and Aweto, together with it, to make medicine to cure the villagers.")
        link(client, "Don`t worry. I will fetch them for you.", 6)
        pic(client, 15)
        create(client)

    elseif (idx == 3) then

        text(client, "Thanks. Those two items can relieve the illness although they can not cure it. SaviorPill is an indispensable ingredient.")
        link(client, "How to get it?", 7)
        link(client, "I will set off to get it right now.", 8)
        pic(client, 15)
        create(client)

    elseif (idx == 4) then

        text(client, "Plague is prevailing in the Desert. I am frustrated after seeing so many people being tortured by the disease.")
        link(client, "Have you come up with any solution?", 9)
        link(client, "I am sorry to hear that.", 255)
        pic(client, 15)
        create(client)

    elseif (idx == 5) then

        spendItem(client, 722807, 1)
        spendItem(client, 722808, 1)
        spendItem(client, 722809, 1)
        awardItem(client, "1088001", 1)
        sendSysMsg(client, "Congratulations! You have received a meteor.", 2005)
        action = randomAction(client, 1, 8)
        if action == 1 then
            moveNpc(client, 3000, 1000, 496, 578)
        elseif action == 2 then
            moveNpc(client, 3000, 1000, 302, 433)
        elseif action == 3 then
            moveNpc(client, 3000, 1213, 474, 255)
        elseif action == 4 then
            moveNpc(client, 3000, 1000, 53, 283)
        elseif action == 5 then
            moveNpc(client, 3000, 1213, 442, 258)
        elseif action == 6 then
            moveNpc(client, 3000, 1000, 496, 578)
        elseif action == 7 then
            moveNpc(client, 3000, 1213, 474, 255)
        elseif action == 8 then
            moveNpc(client, 3000, 1213, 442, 258)
        end


    elseif (idx == 6) then

        text(client, "You are really kind. I believe the villagers will recover from the diseases soon with your persistent help.")
        link(client, "It is my pleasure.", 255)
        pic(client, 15)
        create(client)

    elseif (idx == 7) then

        text(client, "I know that the Antique Dealer in Bird Island has it. He is fond of collecting all kinds of curios.")
        link(client, "Oh, but I do not have any curio.", 10)
        pic(client, 15)
        create(client)

    elseif (idx == 8) then

        text(client, "That is great! Here are my antiques. The Antique Dealer will be interested and give you the pill.")
        link(client, "Alright. I will go right now.", 11)
        pic(client, 15)
        create(client)

    elseif (idx == 9) then

        text(client, "Yeah, I worked out a formula to make the remedy for the disease after I have studied a great deal of books and")
        text(client, "done a lot of experiments. What bothers me is that three ingredients are necessary: FireSpiritFang, Aweto and SaviorPill.")
        link(client, "How to get those ingredients?", 12)
        link(client, "Are you sure of the effect?", 255)
        pic(client, 15)
        create(client)

    elseif (idx == 10) then

        text(client, "Do not worry. I have some. But, you know? I do not have time to pay him a visit.")
        link(client, "I can run an errand for you.", 8)
        pic(client, 15)
        create(client)

    elseif (idx == 11) then

        spendItem(client, 722807, 1)
        spendItem(client, 722808, 1)
        action = randomAction(client, 1, 8)
        if action == 1 then
            awardItem(client, "722817", 1)
            sendSysMsg(client, "Congratulations! You have received a TangFresco.", 2005)
        elseif action == 2 then
            awardItem(client, "722818", 1)
            sendSysMsg(client, "Congratulations! You have received a King Sword.", 2005)
        elseif action == 3 then
            awardItem(client, "722819", 1)
            sendSysMsg(client, "Congratulations! You have received an Amber Cup.", 2005)
        elseif action == 4 then
            awardItem(client, "722817", 1)
            sendSysMsg(client, "Congratulations! You have received a TangFresco.", 2005)
        elseif action == 5 then
            awardItem(client, "722818", 1)
            sendSysMsg(client, "Congratulations! You have received a King Sword.", 2005)
        elseif action == 6 then
            awardItem(client, "722819", 1)
            sendSysMsg(client, "Congratulations! You have received an Amber Cup.", 2005)
        elseif action == 7 then
            awardItem(client, "722817", 1)
            sendSysMsg(client, "Congratulations! You have received a TangFresco.", 2005)
        elseif action == 8 then
            awardItem(client, "722818", 1)
            sendSysMsg(client, "Congratulations! You have received a King Sword.", 2005)
        end


    elseif (idx == 12) then

        text(client, "You can get FireSpiritFang from the FireSpirit in the forest, and Aweto from the Macaque in the canyon.")
        text(client, "As to SaviorPill, it is a rather rare panacea.")
        link(client, "How to get it?", 13)
        link(client, "Oh, I see.", 255)
        pic(client, 15)
        create(client)

    elseif (idx == 13) then

        text(client, "I heard that an Antique Dealer in Bird Island has it. And he will probably give it away if you send him some rare")
        text(client, "curios.I have TankFrestco, KingSword and AmberCup. If you bring me the FireSpiritFang and Aweto, I will entrust them to you.")
        link(client, "Ok. I will go to find them.", 255)
        pic(client, 15)
        create(client)

    end

end
