--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/21/2015 11:50:34 AM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10022(client, idx)
    name = "TrojanStar"
    face = 1

    if (idx == 0) then

        if getProfession(client) == 10 then

            if getLevel(client) < 15 then

                text(client, "Sorry, you cannot get promoted before you reach level 15. Please train harder.")
                link(client, "I see.", 255)
                pic(client, 5)
                create(client)

            else

                text(client, "Trojan has more HP than any other class. Only Trojan can use two handed weapons which will give much more damage to enemies.")
                link(client, "Get promoted.", 1)
                link(client, "Let me think it over.", 255)
                pic(client, 5)
                create(client)

            end

        else

            if getProfession(client) == 11 then

                if getLevel(client) < 40 then

                    text(client, "Sorry, you cannot get promoted before you are level 40. Please train harder.")
                    link(client, "I see.", 255)
                    link(client, "Learn skills.", 2)
                    link(client, "Learn XP skills.", 3)
                    pic(client, 5)
                    create(client)

                else

                    if getForce(client) < 60 then

                        text(client, "To be promoted to Veteran Trojan, you should be strength 60, agility 25 and stamina 25. Please train harder.")
                        link(client, "I see.", 255)
                        pic(client, 5)
                        create(client)

                    else

                        if getDexterity(client) < 25 then

                            text(client, "To be promoted to Veteran Trojan, you should be strength 60, agility 25 and stamina 25. Please train harder.")
                            link(client, "I see.", 255)
                            pic(client, 5)
                            create(client)

                        else

                            if getHealth(client) < 25 then

                                text(client, "To be promoted to Veteran Trojan, you should be strength 60, agility 25 and stamina 25. Please train harder.")
                                link(client, "I see.", 255)
                                pic(client, 5)
                                create(client)

                            else

                                if getProfession(client) == 11 then

                                    text(client, "Trojan can be promoted to Veteran Trojan. Do you want to get promoted? If you have not learned XP skills, you may learn now.")
                                    link(client, "Get promoted.", 4)
                                    link(client, "Learn skills.", 2)
                                    link(client, "Learn XP skills.", 3)
                                    link(client, "Let me think it over.", 255)
                                    pic(client, 5)
                                    create(client)

                                else

                                    link(client, "Let me think it over.", 255)
                                    pic(client, 5)
                                    create(client)

                                end

                            end

                        end

                    end

                end

            else

                if getProfession(client) == 12 then

                    text(client, "Veteran Trojan can be promoted to Tiger Trojan. Do you wanna get promoted? If you have not learned XP skill, you may learn now.")
                    link(client, "Get promoted.", 5)
                    link(client, "Learn skills.", 2)
                    link(client, "Learn XP skills.", 3)
                    link(client, "Let me think it over.", 255)
                    pic(client, 5)
                    create(client)

                else

                    if getProfession(client) == 13 then

                        if getLevel(client) < 100 then

                            text(client, "You can be promoted to DragonTrojan after you reach level 100, strength 155, agility 60 and stamina 92. Please train harder.")
                            link(client, "Learn skills.", 2)
                            link(client, "Learn XP skills.", 3)
                            link(client, "Let me think it over.", 255)
                            pic(client, 5)
                            create(client)

                        else

                            if getForce(client) < 155 then

                                text(client, "You can be promoted to DragonTrojan after you reach level 100, strength 155, agility 60 and stamina 92. Please train harder.")
                                link(client, "Learn skills.", 2)
                                link(client, "Learn XP skills.", 3)
                                link(client, "Let me think it over.", 255)
                                pic(client, 5)
                                create(client)

                            else

                                if getDexterity(client) < 60 then

                                    text(client, "You can be promoted to DragonTrojan after you reach level 100, strength 155, agility 60 and stamina 92. Please train harder.")
                                    link(client, "Learn skills.", 2)
                                    link(client, "Learn XP skills.", 3)
                                    link(client, "Let me think it over.", 255)
                                    pic(client, 5)
                                    create(client)

                                else

                                    if getHealth(client) < 92 then

                                        text(client, "You can be promoted to DragonTrojan after you reach level 100, strength 155, agility 60 and stamina 92. Please train harder.")
                                        link(client, "Learn skills.", 2)
                                        link(client, "Learn XP skills.", 3)
                                        link(client, "Let me think it over.", 255)
                                        pic(client, 5)
                                        create(client)

                                    else

                                        text(client, "If you give me a meteor, I can promote you to Dragon Trojan.")
                                        link(client, "Get promoted.", 6)
                                        link(client, "Learn skills.", 2)
                                        link(client, "Learn XP skills.", 3)
                                        link(client, "Let me think it over.", 255)
                                        pic(client, 5)
                                        create(client)

                                    end

                                end

                            end

                        end

                    else

                        if getProfession(client) == 14 then

                            if getLevel(client) < 110 then

                                text(client, "Sorry, you cannot be promoted to Trojan King before you are level 110, strength 170, agility 65, and stamina 100.")
                                link(client, "I see.", 255)
                                link(client, "Learn skills.", 2)
                                link(client, "Learn XP skills.", 3)
                                link(client, "Let me think it over.", 255)
                                pic(client, 5)
                                create(client)

                            else

                                if getForce(client) < 170 then

                                    text(client, "Sorry, you cannot be promoted to Trojan King before you are level 110, strength 170, agility 65, and stamina 100.")
                                    link(client, "I see.", 255)
                                    link(client, "Learn skills.", 2)
                                    link(client, "Learn XP skills.", 3)
                                    link(client, "Let me think it over.", 255)
                                    pic(client, 5)
                                    create(client)

                                else

                                    if getDexterity(client) < 65 then

                                        text(client, "Sorry, you cannot be promoted to Trojan King before you are level 110, strength 170, agility 65, and stamina 100.")
                                        link(client, "I see.", 255)
                                        link(client, "Learn skills.", 2)
                                        link(client, "Learn XP skills.", 3)
                                        link(client, "Let me think it over.", 255)
                                        pic(client, 5)
                                        create(client)

                                    else

                                        if getHealth(client) < 100 then

                                            text(client, "Sorry, you cannot be promoted to Trojan King before you are level 110, strength 170, agility 65, and stamina 100.")
                                            link(client, "I see.", 255)
                                            link(client, "Learn skills.", 2)
                                            link(client, "Learn XP skills.", 3)
                                            link(client, "Let me think it over.", 255)
                                            pic(client, 5)
                                            create(client)

                                        else

                                            text(client, "If you give me a Moon Box, I can promote you to Trojan King.")
                                            link(client, "Get promoted.", 7)
                                            link(client, "Learn skills.", 2)
                                            link(client, "Learn XP skills.", 3)
                                            link(client, "Let me think it over.", 255)
                                            pic(client, 5)
                                            create(client)

                                        end

                                    end

                                end

                            end

                        else

                            if getProfession(client) == 15 then

                                text(client, "You have been a Trojan King. Please train harder. I believe you will be a great King.")
                                link(client, "Thanks.", 255)
                                link(client, "Learn skills.", 2)
                                link(client, "Learn XP skills.", 3)
                                link(client, "Let me think it over.", 255)
                                pic(client, 5)
                                create(client)

                            else

                                text(client, "Sorry, you are not Trojan. please ask other people for help.")
                                link(client, "I see.", 255)
                                pic(client, 5)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        if getForce(client) < 28 then

            text(client, "Sorry, Intern Trojans can be promoted to Trojans after their Strength is 28, Agility is 10 and Vitality is 10.")
            link(client, "I see.", 255)
            pic(client, 5)
            create(client)

        else

            if getDexterity(client) < 10 then

                text(client, "Sorry, Intern Trojans can be promoted to Trojans after their Strength is 28, Agility is 10 and Vitality is 10.")
                link(client, "I see.", 255)
                pic(client, 5)
                create(client)

            else

                if getHealth(client) < 10 then

                    text(client, "Sorry, Intern Trojans can be promoted to Trojans after their Strength is 28, Agility is 10 and Vitality is 10.")
                    link(client, "I see.", 255)
                    pic(client, 5)
                    create(client)

                else

                    if getProfession(client) == 10 then

                        setProfession(client, 11)
                        if awardMagic(client, 1015, 0) then

                            text(client, "Congrats! You are promoted to Trojan and have learned XP skills, system will notify you to use XP skills when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 5)
                            create(client)

                        else

                            text(client, "Congrats! You are promoted to Trojan and have learned XP skills, system will notify you to use XP skills when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 5)
                            create(client)

                        end

                    else

                        text(client, "Sorry, Intern Trojans can be promoted to Trojans after their Strength is 28, Agility is 10 and Vitality is 10.")
                        link(client, "I see.", 255)
                        pic(client, 5)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 2) then

        text(client, "You may learn Hercules. This spell is very powerful. You may cast it to kill enemies easily.")
        link(client, "Learn Hercules.", 8)
        link(client, "SpiritualHealing[level 40]", 9)
        link(client, "I changed my mind.", 255)
        pic(client, 5)
        create(client)

    elseif (idx == 3) then

        text(client, "You can learn accuracy, cyclone and robot. Accuracy triples hit rate, cyclone boosts speed. Robot is used to disguise as robot.")
        link(client, "Learn accuracy and cyclone.", 10)
        link(client, "Learn Robot.", 11)
        link(client, "I do not want to learn.", 255)
        pic(client, 5)
        create(client)

    elseif (idx == 4) then

        setProfession(client, 12)
        if awardItem(client, "410075", 1) then

            text(client, "Congrats! You are promoted to Veteran Trojan. Please train hard and come to get promoted after you reach level 70.")
            link(client, "Thanks.", 255)
            pic(client, 5)
            create(client)

        else

            text(client, "Congrats! You are promoted to Veteran Trojan. Please train hard and come to get promoted after you reach level 70.")
            link(client, "Thanks.", 255)
            pic(client, 5)
            create(client)

        end

    elseif (idx == 5) then

        text(client, "After Veteran Trojans reach level 70, they can be promoted to Tiger Trojan. An emerald will be charged for promotion.")
        link(client, "Ready for promotion.", 12)
        link(client, "I see.", 255)
        pic(client, 5)
        create(client)

    elseif (idx == 6) then

        if getMetempsychosis(client) == 0 then

            if spendItem(client, 1088001, 1) then

                setProfession(client, 14)
                awardItem(client, "700031", 1)
                if getMetempsychosis(client) == 0 then

                    text(client, "Congratulations! You are promoted to Dragon Trojan. I reward you a Rainbow Gem.")
                    link(client, "Thanks.", 255)
                    pic(client, 5)
                    create(client)

                else

                    awardItem(client, "130487 0 0 0 255 0 0 0 0 0 0", 1)
                    text(client, "You are Dragon Trojan. Because you have been reborn, I will give you a RainbowGem and a unique socketed equipment.")
                    link(client, "Thanks.", 255)
                    pic(client, 5)
                    create(client)

                end

            else

                text(client, "Sorry, you do not have a meteor. You may come to get promoted after you have a meteor.")
                link(client, "I see.", 255)
                pic(client, 5)
                create(client)

            end

        else

            if (getItemsCount(client) <= 39) then

                if spendItem(client, 1088001, 1) then

                    setProfession(client, 14)
                    awardItem(client, "700031", 1)
                    if getMetempsychosis(client) == 0 then

                        text(client, "Congratulations! You are promoted to Dragon Trojan. I reward you a Rainbow Gem.")
                        link(client, "Thanks.", 255)
                        pic(client, 5)
                        create(client)

                    else

                        awardItem(client, "130487 0 0 0 255 0 0 0 0 0 0", 1)
                        text(client, "You are Dragon Trojan. Because you have been reborn, I will give you a RainbowGem and a unique socketed equipment.")
                        link(client, "Thanks.", 255)
                        pic(client, 5)
                        create(client)

                    end

                else

                    text(client, "Sorry, you do not have a meteor. You may come to get promoted after you have a meteor.")
                    link(client, "I see.", 255)
                    pic(client, 5)
                    create(client)

                end

            else

                text(client, "You have got reborn. I will send you a gift. Please prepare a slot in your inventory for that.")
                link(client, "Ok. Wait a minute.", 255)
                pic(client, 5)
                create(client)

            end

        end

    elseif (idx == 7) then

        if spendTaskItem(client, "MoonBox") then

            setProfession(client, 15)
            awardItem(client, "1088000", 1)

        else

            text(client, "Sorry, you do not have a Moon Box. You may obtain a Moon Box from Eight Diagram Tactics quest.")
            link(client, "I see.", 255)
            pic(client, 5)
            create(client)

        end

    elseif (idx == 8) then

        if getLevel(client) < 40 then

            text(client, "Sorry, only Trojans can learn this skill after they reach level 40.")
            link(client, "I see.", 255)
            pic(client, 5)
            create(client)

        else

            awardMagic(client, 1115, 0)
            text(client, "Congrats! You have learned Hercules. Please practice it hard.")
            link(client, "Thanks.", 255)
            pic(client, 5)
            create(client)

        end

    elseif (idx == 9) then

        if getLevel(client) < 40 then

            text(client, "Sorry, you cannot learn this spell before you reach the required level.")
            link(client, "I see.", 255)
            pic(client, 5)
            create(client)

        else

            awardMagic(client, 1190, 0)
            text(client, "Congrats! You have learned spiritual healing. Please practice it hard.")
            link(client, "Thanks.", 255)
            pic(client, 5)
            create(client)

        end

    elseif (idx == 10) then

        if awardMagic(client, 1015, 0) then

            awardMagic(client, 1110, 0)
            text(client, "Congrats! You have learned Accuracy and Cyclone. System will notify you to use when your XP is full.")
            link(client, "Thanks.", 255)
            pic(client, 5)
            create(client)

        else

            awardMagic(client, 1110, 0)
            text(client, "Congrats! You have learned Accuracy and Cyclone. System will notify you to use when your XP is full.")
            link(client, "Thanks.", 255)
            pic(client, 5)
            create(client)

        end

    elseif (idx == 11) then

        if getLevel(client) < 40 then

            text(client, "Sorry, you cannot learn this spell before you are level 40.")
            link(client, "I see.", 255)
            link(client, "I do not want to learn.", 255)
            pic(client, 5)
            create(client)

        else

            if hasMagic(client, 1270, -1) then

                text(client, "You have learned this spell before.")
                link(client, "I see.", 255)
                link(client, "I do not want to learn.", 255)
                pic(client, 5)
                create(client)

            else

                awardMagic(client, 1270, 0)
                text(client, "Congrats! You have learned Robot. When your XP is full, you may cast it to disguise as a Robot.")
                link(client, "Thanks.", 255)
                link(client, "I do not want to learn.", 255)
                pic(client, 5)
                create(client)

            end

        end

    elseif (idx == 12) then

        if getLevel(client) < 70 then

            text(client, "Sorry, you cannot get promoted before you reach level 70. Please train harder.")
            link(client, "I see.", 255)
            link(client, "Learn skills.", 2)
            link(client, "Learn XP skills.", 3)
            pic(client, 5)
            create(client)

        else

            if getForce(client) < 110 then

                text(client, "Sorry, only Veteran Trojan can be promoted to Tiger Trojan. Strength 110, agility 42 and stamina 45 are required.")
                link(client, "I see.", 255)
                pic(client, 5)
                create(client)

            else

                if getDexterity(client) < 42 then

                    text(client, "Sorry, only Veteran Trojan can be promoted to Tiger Trojan. Strength 110, agility 42 and stamina 45 are required.")
                    link(client, "I see.", 255)
                    pic(client, 5)
                    create(client)

                else

                    if getHealth(client) < 45 then

                        text(client, "Sorry, only Veteran Trojan can be promoted to Tiger Trojan. Strength 110, agility 42 and stamina 45 are required.")
                        link(client, "I see.", 255)
                        pic(client, 5)
                        create(client)

                    else

                        if getProfession(client) == 12 then

                            if spendItem(client, 1080001, 1) then

                                setProfession(client, 13)
                                if awardItem(client, "130365", 1) then

                                    if getMetempsychosis(client) == 1 then

                                        text(client, "Congratulations! You have advanced to Tiger Trojan. After rebirth, your stamina will counteract 30%  damage.")
                                        link(client, "Thanks.", 255)
                                        pic(client, 5)
                                        create(client)

                                    else

                                        text(client, "Congrats! You are promoted to Tiger Trojan. Please train hard and come to get promoted after you reach level 100.")
                                        link(client, "Thanks.", 255)
                                        pic(client, 5)
                                        create(client)

                                    end

                                else

                                    if getMetempsychosis(client) == 1 then

                                        text(client, "Congratulations! You have advanced to Tiger Trojan. After rebirth, your stamina will counteract 30%  damage.")
                                        link(client, "Thanks.", 255)
                                        pic(client, 5)
                                        create(client)

                                    else

                                        text(client, "Congrats! You are promoted to Tiger Trojan. Please train hard and come to get promoted after you reach level 100.")
                                        link(client, "Thanks.", 255)
                                        pic(client, 5)
                                        create(client)

                                    end

                                end

                            else

                                text(client, "Sorry, an emerald is required for promotion. Hill Monsters may drop emeralds. Please come to get promoted after you have one.")
                                link(client, "I see.", 255)
                                pic(client, 5)
                                create(client)

                            end

                        else

                            text(client, "Sorry, only Veteran Trojan can be promoted to Tiger Trojan. Strength 110, agility 42 and stamina 45 are required.")
                            link(client, "I see.", 255)
                            pic(client, 5)
                            create(client)

                        end

                    end

                end

            end

        end

    end

end
