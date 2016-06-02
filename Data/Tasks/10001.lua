--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/21/2015 11:50:32 AM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10001(client, idx)
    name = "WarriorGod"
    face = 1

    if (idx == 0) then

        if getProfession(client) == 20 then

            if getLevel(client) < 15 then

                text(client, "Sorry, you cannot get promoted before you reach level 15. Please train harder.")
                link(client, "I see.", 255)
                pic(client, 8)
                create(client)

            else

                text(client, "Warriors are good at attack and defense. Their physical defense is the strongest. Only warriors can wear shields and helmets.")
                link(client, "Get promoted.", 1)
                link(client, "Let me think it over.", 255)
                pic(client, 8)
                create(client)

            end

        else

            if getProfession(client) == 21 then

                if getLevel(client) < 40 then

                    text(client, "Sorry, you cannot get promoted before you reach level 40. Please train harder.")
                    link(client, "I see.", 255)
                    link(client, "Learn XP Skill.", 2)
                    link(client, "Learn weapon skills.", 3)
                    pic(client, 8)
                    create(client)

                else

                    if getForce(client) < 80 then

                        text(client, "Sorry, only Warrior can be promoted to Brass Warrior after their Strength is 80 and Agility 25.")
                        link(client, "I see.", 255)
                        pic(client, 8)
                        create(client)

                    else

                        if getDexterity(client) < 25 then

                            text(client, "Sorry, only Warrior can be promoted to Brass Warrior after their Strength is 80 and Agility 25.")
                            link(client, "I see.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            if getProfession(client) == 21 then

                                text(client, "Warrior can be promoted to Brass Warrior. Are you here for promotion? If you haven`t learned XP skills, you can learn them now.")
                                link(client, "Get promoted.", 4)
                                link(client, "Learn XP Skills.", 2)
                                link(client, "Let me think it over.", 255)
                                pic(client, 8)
                                create(client)

                            else

                                text(client, "Sorry, only Warrior can be promoted to Brass Warrior after their Strength is 80 and Agility 25.")
                                link(client, "I see.", 255)
                                pic(client, 8)
                                create(client)

                            end

                        end

                    end

                end

            else

                if getProfession(client) == 22 then

                    text(client, "Brass Warriors can be promoted to Silver Warriors. If you have not learned XP skills, you can learn now.")
                    link(client, "Get promoted again.", 5)
                    link(client, "Learn XP skills.", 2)
                    link(client, "Learn Dash.", 6)
                    link(client, "Let me think it over.", 255)
                    pic(client, 8)
                    create(client)

                else

                    if getProfession(client) == 23 then

                        if getLevel(client) < 100 then

                            text(client, "You can be promoted to Gold Warrior after you are level 100, strength 205, agility 60.")
                            text(client, "Sorry, you do not meet the requirements. Please train harder and come to get promoted later.")
                            link(client, "OK.", 255)
                            link(client, "Learn XP skills.", 2)
                            link(client, "Learn Dash.", 6)
                            link(client, "Let me think it over.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            if getForce(client) < 205 then

                                text(client, "You can be promoted to Gold Warrior after you are level 100, strength 205, agility 60.")
                                text(client, "Sorry, you do not meet the requirements. Please train harder and come to get promoted later.")
                                link(client, "OK.", 255)
                                link(client, "Learn XP skills.", 2)
                                link(client, "Learn Dash.", 6)
                                link(client, "Let me think it over.", 255)
                                pic(client, 8)
                                create(client)

                            else

                                if getDexterity(client) < 60 then

                                    text(client, "You can be promoted to Gold Warrior after you are level 100, strength 205, agility 60.")
                                    text(client, "Sorry, you do not meet the requirements. Please train harder and come to get promoted later.")
                                    link(client, "OK.", 255)
                                    link(client, "Learn XP skills.", 2)
                                    link(client, "Learn Dash.", 6)
                                    link(client, "Let me think it over.", 255)
                                    pic(client, 8)
                                    create(client)

                                else

                                    text(client, "If you give me a meteor, I can promote you to Gold Warrior.")
                                    link(client, "Get promoted.", 7)
                                    link(client, "Learn XP skills.", 2)
                                    link(client, "Learn Dash.", 6)
                                    link(client, "Let me think it over.", 255)
                                    pic(client, 8)
                                    create(client)

                                end

                            end

                        end

                    else

                        if getProfession(client) == 24 then

                            if getLevel(client) < 110 then

                                text(client, "Sorry, you cannot be promoted to Warrior King before you are level 110, strength 225, and agility 65.")
                                link(client, "I see.", 255)
                                link(client, "Learn XP skills.", 2)
                                link(client, "Learn Dash.", 6)
                                link(client, "Let me think it over.", 255)
                                pic(client, 8)
                                create(client)

                            else

                                if getForce(client) < 225 then

                                    text(client, "Sorry, you cannot be promoted to Warrior King before you are level 110, strength 225, and agility 65.")
                                    link(client, "I see.", 255)
                                    link(client, "Learn XP skills.", 2)
                                    link(client, "Learn Dash.", 6)
                                    link(client, "Let me think it over.", 255)
                                    pic(client, 8)
                                    create(client)

                                else

                                    if getDexterity(client) < 65 then

                                        text(client, "Sorry, you cannot be promoted to Warrior King before you are level 110, strength 225, and agility 65.")
                                        link(client, "I see.", 255)
                                        link(client, "Learn XP skills.", 2)
                                        link(client, "Learn Dash.", 6)
                                        link(client, "Let me think it over.", 255)
                                        pic(client, 8)
                                        create(client)

                                    else

                                        text(client, "If you give me a Moon Box, I may promote you to Warrior King.")
                                        link(client, "Get promoted.", 8)
                                        link(client, "Learn XP skills.", 2)
                                        link(client, "Learn Dash.", 6)
                                        link(client, "Let me think it over.", 255)
                                        pic(client, 8)
                                        create(client)

                                    end

                                end

                            end

                        else

                            if getProfession(client) == 25 then

                                text(client, "You have been a Warrior King. Please train harder. I believe you will be a great King.")
                                link(client, "Thanks.", 255)
                                link(client, "Learn XP skills.", 2)
                                link(client, "Learn Dash.", 6)
                                link(client, "Let me think it over.", 255)
                                pic(client, 8)
                                create(client)

                            else

                                text(client, "Sorry. You are not a warrior. Please consult others.")
                                link(client, "I see.", 255)
                                pic(client, 8)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        if getForce(client) < 28 then

            text(client, "Sorry, Intern Warriors can be promoted to Warriors after their Strength is 28, Agility is 10 and Vitality is 10.")
            link(client, "I see.", 255)
            pic(client, 8)
            create(client)

        else

            if getDexterity(client) < 10 then

                text(client, "Sorry, Intern Warriors can be promoted to Warriors after their Strength is 28, Agility is 10 and Vitality is 10.")
                link(client, "I see.", 255)
                pic(client, 8)
                create(client)

            else

                if getHealth(client) < 10 then

                    text(client, "Sorry, Intern Warriors can be promoted to Warriors after their Strength is 28, Agility is 10 and Vitality is 10.")
                    link(client, "I see.", 255)
                    pic(client, 8)
                    create(client)

                else

                    if getProfession(client) == 20 then

                        setProfession(client, 21)
                        awardMagic(client, 1015, 0)
                        awardMagic(client, 1020, 0)
                        if awardMagic(client, 1025, 0) then

                            awardMagic(client, 1040, 0)
                            awardItem(client, "131303", 1)
                            text(client, "Congrats! You are promoted to Warrior and have learned XP skills. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            awardMagic(client, 1040, 0)
                            awardItem(client, "131303", 1)
                            text(client, "Congrats! You are promoted to Warrior and have learned XP skills. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        end

                    else

                        text(client, "Sorry, Intern Warriors can be promoted to Warriors after their Strength is 28, Agility is 10 and Vitality is 10.")
                        link(client, "I see.", 255)
                        pic(client, 8)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 2) then

        text(client, "Accuracy triples hit rate, Shield triples defense, Superman decuples attack, and Roar adds XP to the teammates.")
        link(client, "I want to learn.", 9)
        link(client, "I do not care.", 255)
        pic(client, 8)
        create(client)

    elseif (idx == 3) then

        if hasSkill(client, 410, 5) then

            if awardMagic(client, 1045, 0) then

                if hasSkill(client, 420, 5) then

                    awardMagic(client, 1046, 0)
                    text(client, "Congrats! You have learned fast blade and scent sword.")
                    link(client, "Thanks.", 255)
                    pic(client, 8)
                    create(client)

                else

                    text(client, "Congrats! You have learned fast blade.")
                    link(client, "Thanks.", 255)
                    pic(client, 8)
                    create(client)

                end

            else

                if hasSkill(client, 420, 5) then

                    awardMagic(client, 1046, 0)
                    text(client, "Congrats! You have learned scent sword.")
                    link(client, "Thanks.", 255)
                    pic(client, 8)
                    create(client)

                else

                    text(client, "I can teach you fast blade and scent sword. Please come to learn after your blade or sword reaches level 5.")
                    link(client, "Ok.", 255)
                    pic(client, 8)
                    create(client)

                end

            end

        else

            if hasSkill(client, 420, 5) then

                awardMagic(client, 1046, 0)
                text(client, "Congrats! You have learned scent sword.")
                link(client, "Thanks.", 255)
                pic(client, 8)
                create(client)

            else

                text(client, "I can teach you fast blade and scent sword. Please come to learn after your blade or sword reaches level 5.")
                link(client, "Ok.", 255)
                pic(client, 8)
                create(client)

            end

        end

    elseif (idx == 4) then

        setProfession(client, 22)
        awardItem(client, "900305", 1)
        text(client, "Congrats! You are promoted to Brass Warrior. Please train hard and come to get promoted after you reach level 70.")
        link(client, "Thanks.", 255)
        pic(client, 8)
        create(client)

    elseif (idx == 5) then

        text(client, "After Brass Warriors reach level 70, they can be promoted to Silver Warriors for the mere price of an emerald.")
        link(client, "Ready for promotion.", 10)
        link(client, "I see.", 255)
        pic(client, 8)
        create(client)

    elseif (idx == 6) then

        if getLevel(client) < 63 then

            text(client, "Only Warriors can learn this skill after they reach level 63.")
            link(client, "Thanks.", 255)
            pic(client, 8)
            create(client)

        else

            if awardMagic(client, 1051, 0) then

                text(client, "Congrats! You have learned Dash.")
                link(client, "I see.", 255)
                pic(client, 8)
                create(client)

            else

                text(client, "Congrats! You have learned Dash.")
                link(client, "I see.", 255)
                pic(client, 8)
                create(client)

            end

        end

    elseif (idx == 7) then

        if getMetempsychosis(client) == 0 then

            if spendItem(client, 1088001, 1) then

                setProfession(client, 24)
                awardItem(client, "700031", 1)
                if getMetempsychosis(client) == 0 then

                    text(client, "Congratulations! You are promoted to Gold Warrior. I reward you a Rainbow Gem.")
                    link(client, "Thanks.", 255)
                    pic(client, 8)
                    create(client)

                else

                    awardItem(client, "131387 0 0 0 255 0 0 0 0 0 0", 1)
                    text(client, "You are Gold Warrior. Because you have been reborn, I will give you a RainbowGem and a unique socketed equipment.")
                    link(client, "Thanks.", 255)
                    pic(client, 8)
                    create(client)

                end

            else

                text(client, "Sorry, you do not have a meteor. You may come to get promoted after you have a meteor.")
                link(client, "I see.", 255)
                pic(client, 8)
                create(client)

            end

        else

            if (getItemsCount(client) <= 39) then

                if spendItem(client, 1088001, 1) then

                    setProfession(client, 24)
                    awardItem(client, "700031", 1)
                    if getMetempsychosis(client) == 0 then

                        text(client, "Congratulations! You are promoted to Gold Warrior. I reward you a Rainbow Gem.")
                        link(client, "Thanks.", 255)
                        pic(client, 8)
                        create(client)

                    else

                        awardItem(client, "131387 0 0 0 255 0 0 0 0 0 0", 1)
                        text(client, "You are Gold Warrior. Because you have been reborn, I will give you a RainbowGem and a unique socketed equipment.")
                        link(client, "Thanks.", 255)
                        pic(client, 8)
                        create(client)

                    end

                else

                    text(client, "Sorry, you do not have a meteor. You may come to get promoted after you have a meteor.")
                    link(client, "I see.", 255)
                    pic(client, 8)
                    create(client)

                end

            else

                text(client, "You have got reborn. I will send you a gift. Please prepare a slot in your inventory for that.")
                link(client, "Ok. Wait a minute.", 255)
                pic(client, 8)
                create(client)

            end

        end

    elseif (idx == 8) then

        if spendTaskItem(client, "MoonBox") then

            setProfession(client, 25)
            awardItem(client, "1088000", 1)

        else

            text(client, "Sorry, you do not have a Moon Box. You may obtain a Moon Box from Eight Diagram Tactics quest.")
            link(client, "I see.", 255)
            pic(client, 8)
            create(client)

        end

    elseif (idx == 9) then

        if hasMagic(client, 1040, -1) then

            text(client, "You have learned two skills.")
            link(client, "Thanks.", 255)
            pic(client, 8)
            create(client)

        else

            if awardMagic(client, 1015, 0) then

                if awardMagic(client, 1020, 0) then

                    if awardMagic(client, 1025, 0) then

                        if awardMagic(client, 1040, 0) then

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        end

                    else

                        if awardMagic(client, 1040, 0) then

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        end

                    end

                else

                    if awardMagic(client, 1025, 0) then

                        if awardMagic(client, 1040, 0) then

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        end

                    else

                        if awardMagic(client, 1040, 0) then

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        end

                    end

                end

            else

                if awardMagic(client, 1020, 0) then

                    if awardMagic(client, 1025, 0) then

                        if awardMagic(client, 1040, 0) then

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        end

                    else

                        if awardMagic(client, 1040, 0) then

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        end

                    end

                else

                    if awardMagic(client, 1025, 0) then

                        if awardMagic(client, 1040, 0) then

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        end

                    else

                        if awardMagic(client, 1040, 0) then

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            text(client, "Congrats! You have learned Accuracy, Shield, Superman and Defense. System will notify you when your XP is full.")
                            link(client, "Thanks.", 255)
                            pic(client, 8)
                            create(client)

                        end

                    end

                end

            end

        end

    elseif (idx == 10) then

        if getLevel(client) < 70 then

            text(client, "Sorry, you cannot get promoted before you reach level 70. Please train harder.")
            link(client, "I see.", 255)
            link(client, "Learn XP skills.", 2)
            pic(client, 8)
            create(client)

        else

            if getForce(client) < 140 then

                text(client, "Sorry, only Brass Warriors can be promoted to Silver Warriors. Strength 140 and Agility 45 are required. Please train harder.")
                link(client, "I see.", 255)
                pic(client, 8)
                create(client)

            else

                if getDexterity(client) < 45 then

                    text(client, "Sorry, only Brass Warriors can be promoted to Silver Warriors. Strength 140 and Agility 45 are required. Please train harder.")
                    link(client, "I see.", 255)
                    pic(client, 8)
                    create(client)

                else

                    if getProfession(client) == 22 then

                        if spendItem(client, 1080001, 1) then

                            setProfession(client, 23)
                            if awardItem(client, "131365", 1) then

                                if getMetempsychosis(client) == 1 then

                                    text(client, "Congratulations! You have advanced to Silver Warrior. After rebirth, your stamina will counteract 30%  damage.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 8)
                                    create(client)

                                else

                                    text(client, "Congrats! You have been promoted to Silver Warrior. Please train hard and come to get promoted after you reach level 100.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 8)
                                    create(client)

                                end

                            else

                                if getMetempsychosis(client) == 1 then

                                    text(client, "Congratulations! You have advanced to Silver Warrior. After rebirth, your stamina will counteract 30%  damage.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 8)
                                    create(client)

                                else

                                    text(client, "Congrats! You have been promoted to Silver Warrior. Please train hard and come to get promoted after you reach level 100.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 8)
                                    create(client)

                                end

                            end

                        else

                            text(client, "Sorry, an emerald is required for promotion. Hill Monsters may drop emeralds. Please come to get promoted after you have one.")
                            link(client, "I see.", 255)
                            pic(client, 8)
                            create(client)

                        end

                    else

                        text(client, "Sorry, only Brass Warriors can be promoted to Silver Warriors. Strength 140 and Agility 45 are required. Please train harder.")
                        link(client, "I see.", 255)
                        pic(client, 8)
                        create(client)

                    end

                end

            end

        end

    end

end
