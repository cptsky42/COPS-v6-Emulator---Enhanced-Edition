--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:13 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask1611(client, idx)
    name = "GeneralJudd"
    face = 1

    if (idx == 0) then

        if hasItem(client, 722515, 1) then

            text(client, "This Secret Command is of great importance to me. Please return it to me, I will give you ten Meteors as the rewards.")
            link(client, "Great! Take it.", 1)
            link(client, "But I need it too.", 255)
            pic(client, 95)
            create(client)

        else

            if hasItem(client, 722512, 1) then

                if hasItem(client, 722513, 1) then

                    if hasItem(client, 722514, 1) then

                        text(client, "Those three items ware awarded by the King. Were they not retrieved, we would be sentenced to death. Please give")
                        text(client, "them to me. I will repay your kindness with our precious treasures.")
                        link(client, "Here you are.", 2)
                        link(client, "I`d like to play for a while.", 255)
                        pic(client, 95)
                        create(client)

                    else

                        text(client, "Thanks a lot for retrieving those two precious items. Please take my awards.")
                        link(client, "Thank you.", 3)
                        link(client, "I`d rather keep them.", 255)
                        pic(client, 95)
                        create(client)

                    end

                else

                    if hasItem(client, 722514, 1) then

                        text(client, "Thanks a lot for retrieving those two precious items. Please take my awards.")
                        link(client, "Thank you.", 4)
                        link(client, "I`d rather keep them.", 255)
                        pic(client, 95)
                        create(client)

                    else

                        text(client, "This AsterNecklace is of great importance to me. Please return it to me, I will pay you.")
                        link(client, "Ok, just take it.", 5)
                        link(client, "Wait. I wll get the other two.", 255)
                        pic(client, 95)
                        create(client)

                    end

                end

            else

                if hasItem(client, 722513, 1) then

                    if hasItem(client, 722514, 1) then

                        text(client, "Thanks a lot for retrieving those two precious items. Please take my awards.")
                        link(client, "Thank you.", 6)
                        link(client, "I`d rather keep them.", 255)
                        pic(client, 95)
                        create(client)

                    else

                        text(client, "The picture was awarded by the king. Please return it to me, I will send you a meteor as a token of my gratitude.")
                        link(client, "Ok, here you are.", 7)
                        link(client, "Wait. I wll get the other two.", 255)
                        pic(client, 95)
                        create(client)

                    end

                else

                    if hasItem(client, 722514, 1) then

                        text(client, "This RoyalSword is of great importance to the country. Please give it to me, I will send you 5 meteors as a token of gratitude.")
                        link(client, "Ok, take it.", 8)
                        link(client, "Wait. I wll get the other two.", 255)
                        pic(client, 95)
                        create(client)

                    else

                        text(client, "Sigh! The damn thieves stole the most precious items of the General Mansion: AsterNecklace,PinetumPicture,Royal")
                        text(client, "Sword, together with the SecretCommand sent by the king. We have sent soldiers to trace the lost items.")
                        link(client, "Did you get any clues?", 9)
                        link(client, "Poor guy.", 255)
                        pic(client, 95)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 1) then

        if (getItemsCount(client) <= 31) then

            spendItem(client, 722515, 1)
            if (rand(client, 200) < 1) then

                awardItem(client, "1088000", 1)

            else

                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                sendSysMsg(client, "Congratulations! You got 10 Meteors.", 2005)

            end

        else

            sendSysMsg(client, "You do not have enough space in your bag.", 2005)

        end

    elseif (idx == 2) then

        if (getItemsCount(client) <= 34) then

            spendItem(client, 722512, 1)
            spendItem(client, 722513, 1)
            spendItem(client, 722514, 1)
            if (rand(client, 200) < 1) then

                awardItem(client, "1088000", 1)

            else

                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                awardItem(client, "1088001", 1)
                sendSysMsg(client, "Congratulations! You got 9 Meteors.", 2005)

            end

        else

            sendSysMsg(client, "You do not have enough space in your bag.", 2005)

        end

    elseif (idx == 3) then

        spendItem(client, 722513, 1)
        spendItem(client, 722512, 1)
        awardItem(client, "1088001", 1)
        if (rand(client, 10) < 6) then

            gainMoney(client, 50000)
            sendSysMsg(client, "Congratulations! You got 50000 silvers.", 2005)
            sendSysMsg(client, "Congratulations! You got a Meteor.", 2005)

        else

            awardItem(client, "1088001", 1)
            sendSysMsg(client, "Congratulations! You got 2 Meteors.", 2005)

        end

    elseif (idx == 4) then

        if (getItemsCount(client) <= 36) then

            spendItem(client, 722512, 1)
            spendItem(client, 722514, 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            if (rand(client, 10) < 6) then

                gainMoney(client, 20000)
                sendSysMsg(client, "Congratulations! You got 20000 silvers.", 2005)
                sendSysMsg(client, "Congratulations! You got 5 Meteors.", 2005)

            else

                awardItem(client, "1088001", 1)
                sendSysMsg(client, "Congratulations! You got 6 Meteors.", 2005)

            end

        else

            sendSysMsg(client, "You do not have enough space in your bag.", 2005)

        end

    elseif (idx == 5) then

        spendItem(client, 722512, 1)
        if (rand(client, 10) < 6) then

            gainMoney(client, 50000)
            sendSysMsg(client, "Congratulations! You got 50000 silvers.", 2005)

        else

            awardItem(client, "1088001", 1)
            sendSysMsg(client, "Congratulations! You got a Meteor.", 2005)

        end

    elseif (idx == 6) then

        if (getItemsCount(client) <= 36) then

            spendItem(client, 722513, 1)
            spendItem(client, 722514, 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            sendSysMsg(client, "Congratulations! You got 6 Meteors.", 2005)

        else

            sendSysMsg(client, "You do not have enough space in your bag.", 2005)

        end

    elseif (idx == 7) then

        spendItem(client, 722513, 1)
        awardItem(client, "1088001", 1)
        sendSysMsg(client, "Congratulations! You got a Meteor.", 2005)

    elseif (idx == 8) then

        if (getItemsCount(client) <= 36) then

            spendItem(client, 722514, 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            awardItem(client, "1088001", 1)
            sendSysMsg(client, "Congratulations! You got 5 Meteors.", 2005)

        else

            sendSysMsg(client, "You do not have enough space in your bag.", 2005)

        end

    elseif (idx == 9) then

        text(client, "Yeah. The thieves were captured. But we did not find out the lost items. They finally confessed that those items")
        text(client, "were stolen by Blue Mouse when they passed the forest. Nobody ever saw a Blue Mouse except OldMiner outside the forest mine.")
        link(client, "Why not ask him?", 10)
        link(client, "Oh, I see.", 255)
        pic(client, 95)
        create(client)

    elseif (idx == 10) then

        text(client, "I did. But he does not know either. The King promises big rewards for those who can retrieve the lost items. Do you")
        text(client, "want to give it a try?")
        link(client, "Sure. I do.", 11)
        link(client, "I am quite busy now.", 255)
        pic(client, 95)
        create(client)

    elseif (idx == 11) then

        text(client, "Good! OldMiner has a kind of special needle to catch the mouse. Hope you find out the lost items soon.")
        link(client, "Ok. I will visit OldMiner now.", 12)
        link(client, "Sorry that I can`t help you.", 255)
        pic(client, 95)
        create(client)

    elseif (idx == 12) then

        text(client, "Thank you very much for you kindness. For any treasure you retrieve, I will repay you something. Good luck.")
        link(client, "Thanks. Bye.", 255)
        pic(client, 95)
        create(client)

    end

end
