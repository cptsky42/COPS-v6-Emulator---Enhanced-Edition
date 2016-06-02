--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:28 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask3010(client, idx)
    name = "AntiqueDealer"
    face = 1

    if (idx == 0) then

        if hasItem(client, 722809, 1) then

            text(client, "Have you got anything else?")
            link(client, "No. Thanks.", 255)
            pic(client, 90)
            create(client)

        else

            if hasItem(client, 722817, 1) then

                if hasItem(client, 722818, 1) then

                    if hasItem(client, 722819, 1) then

                        text(client, "Hi, man, you`re got good stuffs. Are your antiques for sale? I really love them.")
                        link(client, "How much do you offer it?", 1)
                        link(client, "No. I wanna exchange for something.", 2)
                        pic(client, 90)
                        create(client)

                    else

                        text(client, "Hi, man, you`ve got good stuffs. Are your antiques for sale?")
                        link(client, "How much do you offer it?", 3)
                        link(client, "No. I wanna exchange for something.", 4)
                        pic(client, 90)
                        create(client)

                    end

                else

                    if hasItem(client, 722819, 1) then

                        text(client, "Hi, man, you`ve got good stuffs. Are your antiques for sale?")
                        link(client, "How much do you offer it?", 3)
                        link(client, "No. I wanna exchange for something.", 4)
                        pic(client, 90)
                        create(client)

                    else

                        text(client, "Hi, man, you`ve got good stuffs. Are your antiques for sale?")
                        link(client, "How much do you offer it?", 3)
                        link(client, "No. I wanna exchange for something.", 4)
                        pic(client, 90)
                        create(client)

                    end

                end

            else

                if hasItem(client, 722818, 1) then

                    text(client, "Hi, man, you`ve got good stuffs. Are your antiques for sale?")
                    link(client, "How much do you offer it?", 3)
                    link(client, "No. I wanna exchange for something.", 4)
                    pic(client, 90)
                    create(client)

                else

                    if hasItem(client, 722819, 1) then

                        text(client, "Hi, man, you`ve got good stuffs. Are your antiques for sale?")
                        link(client, "How much do you offer it?", 3)
                        link(client, "No. I wanna exchange for something.", 4)
                        pic(client, 90)
                        create(client)

                    else

                        text(client, "What can I do for you?")
                        link(client, "No. Thanks.", 255)
                        pic(client, 90)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 1) then

        text(client, "How about 3000 silvers for one? I will buy whatever you have.")
        link(client, "Ok, it is a deal.", 5)
        link(client, "The price is much too low.", 255)
        pic(client, 90)
        create(client)

    elseif (idx == 2) then

        text(client, "Oh, what are you going to exchange for?")
        link(client, "SaviorPill", 6)
        pic(client, 90)
        create(client)

    elseif (idx == 3) then

        text(client, "How about 3000 silvers for one antique?")
        link(client, "Ok, it is a deal.", 7)
        link(client, "The price is much too low.", 255)
        pic(client, 90)
        create(client)

    elseif (idx == 4) then

        text(client, "Oh, what are you going to exchange for?")
        link(client, "Savior Pill", 8)
        pic(client, 90)
        create(client)

    elseif (idx == 5) then

        text(client, "Great! Then I will take away your antiques and give you the money.")
        link(client, "No problem.", 9)
        link(client, "Wait, I changed my mind.", 255)
        pic(client, 90)
        create(client)

    elseif (idx == 6) then

        text(client, "SaviorPill? I do have one. It is so precious that I will exchange it for three antiques.")
        link(client, "Ok, what are they?", 10)
        pic(client, 90)
        create(client)

    elseif (idx == 7) then

        if hasItem(client, 722818, 1) then

            spendItem(client, 722818, 1)
            gainMoney(client, 3000)
            sendSysMsg(client, "Congratulations! You received 3000 silvers.", 2005)

        else

            if hasItem(client, 722819, 1) then

                spendItem(client, 722819, 1)
                gainMoney(client, 3000)
                sendSysMsg(client, "Congratulations! You received 3000 silvers.", 2005)

            else

                hasItem(client, 722817, 1)
                spendItem(client, 722817, 1)
                gainMoney(client, 3000)
                sendSysMsg(client, "Congratulations! You received 3000 silvers.", 2005)

            end

        end

    elseif (idx == 8) then

        text(client, "Yeah, you know the ropes. It is the most effective panacea. Unless you`ve got TangFresco, KingSword and AmberCup.")
        link(client, "Ok, I will get them soon.", 255)
        pic(client, 90)
        create(client)

    elseif (idx == 9) then

        spendItem(client, 722817, 1)
        spendItem(client, 722818, 1)
        spendItem(client, 722819, 1)
        gainMoney(client, 9000)
        sendSysMsg(client, "Congratulations! You received 9000 silvers.", 2005)

    elseif (idx == 10) then

        text(client, "TangFresco, KingSword and AmberCup.")
        link(client, "Aren`t you asking for too much?", 11)
        link(client, "Ok, it is a deal.", 12)
        pic(client, 90)
        create(client)

    elseif (idx == 11) then

        text(client, "Well. It is up to you whether to exchange it or not.")
        link(client, "Then forget it.", 255)
        pic(client, 90)
        create(client)

    elseif (idx == 12) then

        text(client, "Great! Here is the Savior Pill. And I will take away the antiques.")
        link(client, "Thank you!", 13)
        pic(client, 90)
        create(client)

    elseif (idx == 13) then

        spendItem(client, 722817, 1)
        spendItem(client, 722818, 1)
        spendItem(client, 722819, 1)
        awardItem(client, "722809", 1)
        sendSysMsg(client, "Gained a Savior Pill.", 2005)

    end

end
