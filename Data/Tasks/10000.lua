--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/21/2015 11:50:28 AM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10000(client, idx)
    name = "TaoistMoon"
    face = 1

    if (idx == 0) then

        if getProfession(client) == 100 then

            if getLevel(client) < 15 then

                text(client, "Sorry, you must be level 15 to be promoted. Go seek wisdom in battle.")
                link(client, "I see. Thanks.", 255)
                pic(client, 6)
                create(client)

            else

                text(client, "Taoist spells make them strong in a group or alone.")
                link(client, "Get promoted.", 1)
                link(client, "Let me think it over.", 255)
                pic(client, 6)
                create(client)

            end

        else

            if getProfession(client) == 101 then

                if getLevel(client) < 40 then

                    text(client, "You must attain level 40 to be fire or water Taoist.")
                    link(client, "I see.", 255)
                    link(client, "Learn XP Skill.", 2)
                    pic(client, 6)
                    create(client)

                else

                    if getSoul(client) < 80 then

                        text(client, "Sorry, only Taoists can be promoted to Fire or Water Taoists after they are spirit 80, agility 25.")
                        link(client, "I see.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        if getDexterity(client) < 25 then

                            text(client, "Sorry, only Taoists can be promoted to Fire or Water Taoists after they are spirit 80, agility 25.")
                            link(client, "I see.", 255)
                            pic(client, 6)
                            create(client)

                        else

                            if getProfession(client) == 101 then

                                text(client, "Taoist can be promoted to be Fire or Water Taoist. Which do you prefer?")
                                link(client, "Fire Taoist", 3)
                                link(client, "Water Taoist.", 4)
                                link(client, "Learn XP Skills.", 2)
                                link(client, "Let me think it over.", 255)
                                pic(client, 6)
                                create(client)

                            else

                                text(client, "Sorry, only Taoists can be promoted to Fire or Water Taoists after they are spirit 80, agility 25.")
                                link(client, "I see.", 255)
                                pic(client, 6)
                                create(client)

                            end

                        end

                    end

                end

            else

                if getProfession(client) == 142 then

                    text(client, "Fire Taoists can be promoted to Fire Wizards. If you have not learned XP skills, you can learn it now.")
                    link(client, "Get promoted again.", 5)
                    link(client, "Learn magic.", 6)
                    link(client, "Learn XP Skill.", 2)
                    link(client, "Let me think it over.", 255)
                    pic(client, 6)
                    create(client)

                else

                    if getProfession(client) == 143 then

                        if getLevel(client) < 100 then

                            text(client, "Only Fire Wizards that reach level 100, spirit 205, agility 60 can be Fire Masters!")
                            link(client, "I see.", 255)
                            link(client, "Learn magic.", 6)
                            link(client, "Learn XP Skill.", 2)
                            link(client, "Let me think it over.", 255)
                            pic(client, 6)
                            create(client)

                        else

                            if getDexterity(client) < 60 then

                                text(client, "Only Fire Wizards that reach level 100, spirit 205, agility 60 can be Fire Masters!")
                                link(client, "I see.", 255)
                                link(client, "Learn magic.", 6)
                                link(client, "Learn XP Skill.", 2)
                                link(client, "Let me think it over.", 255)
                                pic(client, 6)
                                create(client)

                            else

                                if getSoul(client) < 205 then

                                    text(client, "Only Fire Wizards that reach level 100, spirit 205, agility 60 can be Fire Masters!")
                                    link(client, "I see.", 255)
                                    link(client, "Learn magic.", 6)
                                    link(client, "Learn XP Skill.", 2)
                                    link(client, "Let me think it over.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    text(client, "If you give me a meteor, I can promote you to Fire Master.")
                                    link(client, "Get promoted.", 7)
                                    link(client, "Learn magic.", 6)
                                    link(client, "Learn XP Skill.", 2)
                                    link(client, "Let me think it over.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        end

                    else

                        if getProfession(client) == 144 then

                            if getLevel(client) < 110 then

                                text(client, "Sorry, you cannot be promoted to Fire Saint before you are level 110, agility 65 and spirit 225.")
                                link(client, "I see.", 255)
                                link(client, "Learn magic.", 6)
                                link(client, "Learn XP Skill.", 2)
                                link(client, "Let me think it over.", 255)
                                pic(client, 6)
                                create(client)

                            else

                                if getDexterity(client) < 65 then

                                    text(client, "Sorry, you cannot be promoted to Fire Saint before you are level 110, agility 65 and spirit 225.")
                                    link(client, "I see.", 255)
                                    link(client, "Learn magic.", 6)
                                    link(client, "Learn XP Skill.", 2)
                                    link(client, "Let me think it over.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    if getSoul(client) < 225 then

                                        text(client, "Sorry, you cannot be promoted to Fire Saint before you are level 110, agility 65 and spirit 225.")
                                        link(client, "I see.", 255)
                                        link(client, "Learn magic.", 6)
                                        link(client, "Learn XP Skill.", 2)
                                        link(client, "Let me think it over.", 255)
                                        pic(client, 6)
                                        create(client)

                                    else

                                        text(client, "If you give me a Moon Box, I can promote you to Fire Saint.")
                                        link(client, "Get promoted.", 8)
                                        link(client, "Learn magic.", 6)
                                        link(client, "Learn XP Skill.", 2)
                                        link(client, "Let me think it over.", 255)
                                        pic(client, 6)
                                        create(client)

                                    end

                                end

                            end

                        else

                            if getProfession(client) == 145 then

                                text(client, "You have been a Fire Saint. Please train harder. I believe you will be a great Saint.")
                                link(client, "Thanks.", 255)
                                link(client, "Learn magic.", 6)
                                link(client, "Learn XP Skill.", 2)
                                link(client, "Let me think it over.", 255)
                                pic(client, 6)
                                create(client)

                            else

                                if getProfession(client) == 132 then

                                    text(client, "Water Taoist can be promoted to Water Wizard. Do you want to get promoted? If you have not learned XP skill, you may learn now.")
                                    link(client, "Get promoted.", 9)
                                    link(client, "Learn Water spells.", 10)
                                    link(client, "Learn XP Skill.", 2)
                                    link(client, "Let me think it over.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    if getProfession(client) == 133 then

                                        if getLevel(client) < 100 then

                                            text(client, "Sorry, you can be promoted to Water Master only after you reach level 100, spirit 205, agility 60. Please train harder.")
                                            link(client, "I see.", 255)
                                            link(client, "Learn Water spells.", 10)
                                            link(client, "Learn XP Skill.", 2)
                                            link(client, "Let me think it over.", 255)
                                            pic(client, 6)
                                            create(client)

                                        else

                                            if getSoul(client) < 205 then

                                                text(client, "Sorry, you can be promoted to Water Master only after you reach level 100, spirit 205, agility 60. Please train harder.")
                                                link(client, "I see.", 255)
                                                link(client, "Learn Water spells.", 10)
                                                link(client, "Learn XP Skill.", 2)
                                                link(client, "Let me think it over.", 255)
                                                pic(client, 6)
                                                create(client)

                                            else

                                                if getDexterity(client) < 60 then

                                                    text(client, "Sorry, you can be promoted to Water Master only after you reach level 100, spirit 205, agility 60. Please train harder.")
                                                    link(client, "I see.", 255)
                                                    link(client, "Learn Water spells.", 10)
                                                    link(client, "Learn XP Skill.", 2)
                                                    link(client, "Let me think it over.", 255)
                                                    pic(client, 6)
                                                    create(client)

                                                else

                                                    text(client, "If you give me a meteor, I can promote you to Water Master.")
                                                    link(client, "Get promoted.", 11)
                                                    link(client, "Learn Water spells.", 10)
                                                    link(client, "Learn XP Skill.", 2)
                                                    link(client, "Let me think it over.", 255)
                                                    pic(client, 6)
                                                    create(client)

                                                end

                                            end

                                        end

                                    else

                                        if getProfession(client) == 134 then

                                            if getLevel(client) < 110 then

                                                text(client, "Sorry, you cannot be promoted to Water Saint before you are level 110, aglity 65, and spirit 225.")
                                                link(client, "I see.", 255)
                                                link(client, "Learn Water spells.", 10)
                                                link(client, "Learn XP Skill.", 2)
                                                link(client, "Let me think it over.", 255)
                                                pic(client, 6)
                                                create(client)

                                            else

                                                if getDexterity(client) < 65 then

                                                    text(client, "Sorry, you cannot be promoted to Water Saint before you are level 110, aglity 65, and spirit 225.")
                                                    link(client, "I see.", 255)
                                                    link(client, "Learn Water spells.", 10)
                                                    link(client, "Learn XP Skill.", 2)
                                                    link(client, "Let me think it over.", 255)
                                                    pic(client, 6)
                                                    create(client)

                                                else

                                                    if getSoul(client) < 225 then

                                                        text(client, "Sorry, you cannot be promoted to Water Saint before you are level 110, aglity 65, and spirit 225.")
                                                        link(client, "I see.", 255)
                                                        link(client, "Learn Water spells.", 10)
                                                        link(client, "Learn XP Skill.", 2)
                                                        link(client, "Let me think it over.", 255)
                                                        pic(client, 6)
                                                        create(client)

                                                    else

                                                        text(client, "If you give me a Moon Box, I can promote you to Water Saint.")
                                                        link(client, "Get promoted.", 12)
                                                        link(client, "Learn Water spells.", 10)
                                                        link(client, "Learn XP Skill.", 2)
                                                        link(client, "Let me think it over.", 255)
                                                        pic(client, 6)
                                                        create(client)

                                                    end

                                                end

                                            end

                                        else

                                            if getProfession(client) == 135 then

                                                text(client, "You have been a Water Saint. Please train harder. I believe you will be a great Saint.")
                                                link(client, "Thanks.", 255)
                                                link(client, "Learn Water spells.", 10)
                                                link(client, "Learn XP Skill.", 2)
                                                link(client, "Let me think it over.", 255)
                                                pic(client, 6)
                                                create(client)

                                            else

                                                text(client, "Sorry. You are not a Taoist. Please consult others.")
                                                link(client, "I see.", 255)
                                                pic(client, 6)
                                                create(client)

                                            end

                                        end

                                    end

                                end

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        if getSoul(client) < 25 then

            text(client, "Sorry! Only intern Taoist can become a Taoist after their Spirit is 25, Agility 10 .")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            if getDexterity(client) < 10 then

                text(client, "Sorry! Only intern Taoist can become a Taoist after their Spirit is 25, Agility 10 .")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                if getProfession(client) == 100 then

                    setProfession(client, 101)
                    if awardMagic(client, 1010, 0) then

                        awardItem(client, "134303", 1)
                        text(client, "Congrats! As a Taoist you have lightning. When your XP is ready you can use it.")
                        link(client, "I see. Thanks.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        awardItem(client, "134303", 1)
                        text(client, "Congrats! As a Taoist you have lightning. When your XP is ready you can use it.")
                        link(client, "I see. Thanks.", 255)
                        pic(client, 6)
                        create(client)

                    end

                else

                    text(client, "Sorry! Only intern Taoist can become a Taoist after their Spirit is 25, Agility 10 .")
                    link(client, "I see.", 255)
                    pic(client, 6)
                    create(client)

                end

            end

        end

    elseif (idx == 2) then

        text(client, "Lighting can hit enemies within a certain range. Water Taoists can learn Water Elf to disguise themselves. Which do you prefer?")
        link(client, "Lightning.", 13)
        link(client, "Volcano.", 14)
        link(client, "Water Elf.", 15)
        link(client, "Learn XP Revive.", 16)
        link(client, "I do not want to learn.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 3) then

        setProfession(client, 142)
        if awardItem(client, "421075", 1) then

            text(client, "Congrats! You are promoted to Fire Taoist. Please train hard and come to get promoted after you reach level 70.")
            link(client, "Thanks.", 255)
            pic(client, 6)
            create(client)

        else

            text(client, "Congrats! You are promoted to Fire Taoist. Please train hard and come to get promoted after you reach level 70.")
            link(client, "Thanks.", 255)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 4) then

        setProfession(client, 132)
        if awardItem(client, "421075", 1) then

            if awardMagic(client, 1050, 0) then

                text(client, "Congrats! You are Water Taoist now and have learned revive. When your XP is full, you may cast revive to resurrect a ghost.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            else

                text(client, "Congrats! You are Water Taoist now and have learned revive. When your XP is full, you may cast revive to resurrect a ghost.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        else

            if awardMagic(client, 1050, 0) then

                text(client, "Congrats! You are Water Taoist now and have learned revive. When your XP is full, you may cast revive to resurrect a ghost.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            else

                text(client, "Congrats! You are Water Taoist now and have learned revive. When your XP is full, you may cast revive to resurrect a ghost.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        end

    elseif (idx == 5) then

        text(client, "Fire Taoists can be promoted to Fire Wizard after they reach level 70, for a mere emerald.")
        link(client, "Ready for promotion.", 17)
        link(client, "I see.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 6) then

        text(client, "What spell do you want to learn?")
        link(client, "Fire Ring.", 18)
        link(client, "Fire Circle.", 19)
        link(client, "FireMeteor.", 20)
        pic(client, 6)
        create(client)

    elseif (idx == 7) then

        if getMetempsychosis(client) == 0 then

            if spendItem(client, 1088001, 1) then

                setProfession(client, 144)
                awardItem(client, "700031", 1)
                if getMetempsychosis(client) == 0 then

                    text(client, "Congrats! You are promoted to Fire Master. I reward you an Rainbow Gem.")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)

                else

                    awardItem(client, "134387 0 0 0 255 0 0 0 0 0 0", 1)
                    text(client, "You are Fire Master. Because you have been reborn, I will give you a RainbowGem and a unique socketed equipment.")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)

                end

            else

                text(client, "Sorry, you do not have a meteor. Please come to get promoted after you have one.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            end

        else

            if (getItemsCount(client) <= 39) then

                if spendItem(client, 1088001, 1) then

                    setProfession(client, 144)
                    awardItem(client, "700031", 1)
                    if getMetempsychosis(client) == 0 then

                        text(client, "Congrats! You are promoted to Fire Master. I reward you an Rainbow Gem.")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        awardItem(client, "134387 0 0 0 255 0 0 0 0 0 0", 1)
                        text(client, "You are Fire Master. Because you have been reborn, I will give you a RainbowGem and a unique socketed equipment.")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)

                    end

                else

                    text(client, "Sorry, you do not have a meteor. Please come to get promoted after you have one.")
                    link(client, "I see.", 255)
                    pic(client, 6)
                    create(client)

                end

            else

                text(client, "You have got reborn. I will send you a gift. Please prepare a slot in your inventory for that.")
                link(client, "Ok. Wait a minute.", 255)
                pic(client, 6)
                create(client)

            end

        end

    elseif (idx == 8) then

        if spendTaskItem(client, "MoonBox") then

            setProfession(client, 145)
            awardItem(client, "1088000", 1)

        else

            text(client, "Sorry, you do not have a Moon Box. You may obtain a Moon Box from Eight Diagram Tactics quest.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 9) then

        text(client, "After Water Taoists reach level 70, they can be promoted to Water Wizard. An emerald will be charged for promotion.")
        link(client, "Ready for promotion.", 21)
        link(client, "I see.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 10) then

        text(client, "Water Taoist can learn many spells. You must reach the required level before you learn. What would you like to learn?")
        link(client, "Healing Rain[level 40]", 22)
        link(client, "Invisibility[level 60]", 23)
        link(client, "Star of Accuracy[level 45]", 24)
        link(client, "Magic Shield[level 50]", 25)
        link(client, "Stigma[level 55]", 26)
        link(client, "Pray[Level 70]", 27)
        link(client, "Advanced Cure[Level 81]", 28)
        link(client, "Nectar[level 94]", 29)
        pic(client, 6)
        create(client)

    elseif (idx == 11) then

        if getMetempsychosis(client) == 0 then

            if spendItem(client, 1088001, 1) then

                setProfession(client, 134)
                awardItem(client, "700031", 1)
                if getMetempsychosis(client) == 0 then

                    text(client, "Congratulations! You are promoted to Water Master. I reward you a Rainbow Gem.")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)

                else

                    awardItem(client, "134387 0 0 0 255 0 0 0 0 0 0", 1)
                    text(client, "You are Water Master. Because you have been reborn, I will give you a RainbowGem and a unique socketed equipment.")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)

                end

            else

                text(client, "Sorry, you do not have a meteor. You may come to get promoted after you have a meteor.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            end

        else

            if (getItemsCount(client) <= 39) then

                if spendItem(client, 1088001, 1) then

                    setProfession(client, 134)
                    awardItem(client, "700031", 1)
                    if getMetempsychosis(client) == 0 then

                        text(client, "Congratulations! You are promoted to Water Master. I reward you a Rainbow Gem.")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        awardItem(client, "134387 0 0 0 255 0 0 0 0 0 0", 1)
                        text(client, "You are Water Master. Because you have been reborn, I will give you a RainbowGem and a unique socketed equipment.")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)

                    end

                else

                    text(client, "Sorry, you do not have a meteor. You may come to get promoted after you have a meteor.")
                    link(client, "I see.", 255)
                    pic(client, 6)
                    create(client)

                end

            else

                text(client, "You have got reborn. I will send you a gift. Please prepare a slot in your inventory for that.")
                link(client, "Ok. Wait a minute.", 255)
                pic(client, 6)
                create(client)

            end

        end

    elseif (idx == 12) then

        if spendTaskItem(client, "MoonBox") then

            if setProfession(client, 135) then

                awardItem(client, "1088000", 1)

            else

                text(client, "Sorry, you do not have a Moon Box. You may obtain a Moon Box from Eight Diagram Tactics quest.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            end

        else

            text(client, "Sorry, you do not have a Moon Box. You may obtain a Moon Box from Eight Diagram Tactics quest.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 13) then

        if hasMagic(client, 1010, -1) then

            text(client, "You have learned this skill.")
            link(client, "Thanks.", 255)
            pic(client, 6)
            create(client)

        else

            awardMagic(client, 1010, 0)
            text(client, "Congrats! You have learned lightning. System will notify you to use when your XP is full.")
            link(client, "Thanks.", 255)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 14) then

        if getLevel(client) < 40 then

            text(client, "Sorry, you cannot learn this spell before you reach level 40.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            awardMagic(client, 1125, 0)
            text(client, "Congrats! You have learned Volcano.")
            link(client, "Thanks.", 255)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 15) then

        if getProfession(client) == 132 then

            if getLevel(client) < 50 then

                text(client, "Sorry, you cannot learn Water Elf before you are level 50. Please train harder.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                if hasMagic(client, 1280, -1) then

                    text(client, "You have learned this spell before.")
                    link(client, "I see.", 255)
                    pic(client, 6)
                    create(client)

                else

                    awardMagic(client, 1280, 0)
                    text(client, "Congrats! You have learned Water Elf.")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)

                end

            end

        else

            if getProfession(client) == 133 then

                if getLevel(client) < 50 then

                    text(client, "Sorry, you cannot learn Water Elf before you are level 50. Please train harder.")
                    link(client, "I see.", 255)
                    pic(client, 6)
                    create(client)

                else

                    if hasMagic(client, 1280, -1) then

                        text(client, "You have learned this spell before.")
                        link(client, "I see.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        awardMagic(client, 1280, 0)
                        text(client, "Congrats! You have learned Water Elf.")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 134 then

                    if getLevel(client) < 50 then

                        text(client, "Sorry, you cannot learn Water Elf before you are level 50. Please train harder.")
                        link(client, "I see.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        if hasMagic(client, 1280, -1) then

                            text(client, "You have learned this spell before.")
                            link(client, "I see.", 255)
                            pic(client, 6)
                            create(client)

                        else

                            awardMagic(client, 1280, 0)
                            text(client, "Congrats! You have learned Water Elf.")
                            link(client, "Thanks.", 255)
                            pic(client, 6)
                            create(client)

                        end

                    end

                else

                    if getProfession(client) == 135 then

                        if getLevel(client) < 50 then

                            text(client, "Sorry, you cannot learn Water Elf before you are level 50. Please train harder.")
                            link(client, "I see.", 255)
                            pic(client, 6)
                            create(client)

                        else

                            if hasMagic(client, 1280, -1) then

                                text(client, "You have learned this spell before.")
                                link(client, "I see.", 255)
                                pic(client, 6)
                                create(client)

                            else

                                awardMagic(client, 1280, 0)
                                text(client, "Congrats! You have learned Water Elf.")
                                link(client, "Thanks.", 255)
                                pic(client, 6)
                                create(client)

                            end

                        end

                    else

                        text(client, "Only Water Taoist can learn Water Elf.")
                        link(client, "I see.", 255)
                        pic(client, 6)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 16) then

        if getProfession(client) == 132 then

            if getLevel(client) < 40 then

                text(client, "Only Water Taoist above level 40 can learn Revive.")
                link(client, "I See.", 255)
                pic(client, 6)
                create(client)

            else

                awardMagic(client, 1050, 0)
                text(client, "You have learned Revive.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        else

            if getProfession(client) == 133 then

                if getLevel(client) < 40 then

                    text(client, "Only Water Taoist above level 40 can learn Revive.")
                    link(client, "I See.", 255)
                    pic(client, 6)
                    create(client)

                else

                    awardMagic(client, 1050, 0)
                    text(client, "You have learned Revive.")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)

                end

            else

                if getProfession(client) == 134 then

                    if getLevel(client) < 40 then

                        text(client, "Only Water Taoist above level 40 can learn Revive.")
                        link(client, "I See.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        awardMagic(client, 1050, 0)
                        text(client, "You have learned Revive.")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)

                    end

                else

                    if getProfession(client) == 135 then

                        if getLevel(client) < 40 then

                            text(client, "Only Water Taoist above level 40 can learn Revive.")
                            link(client, "I See.", 255)
                            pic(client, 6)
                            create(client)

                        else

                            awardMagic(client, 1050, 0)
                            text(client, "You have learned Revive.")
                            link(client, "Thanks.", 255)
                            pic(client, 6)
                            create(client)

                        end

                    else

                        text(client, "Only Water Taoist above level 40 can learn Revive.")
                        link(client, "I See.", 255)
                        pic(client, 6)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 17) then

        if getLevel(client) < 70 then

            text(client, "Sorry, your level is too low. Please train harder and come to get promoted after you reach level 70.")
            link(client, "I see.", 255)
            link(client, "Learn XP Skill.", 2)
            pic(client, 6)
            create(client)

        else

            if getSoul(client) < 140 then

                text(client, "Sorry, only Fire Taoists can be promoted to Fire Wizards after their Spirit is 140, and Agility 45.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                if getDexterity(client) < 45 then

                    text(client, "Sorry, only Fire Taoists can be promoted to Fire Wizards after their Spirit is 140, and Agility 45.")
                    link(client, "I see.", 255)
                    pic(client, 6)
                    create(client)

                else

                    if getProfession(client) == 142 then

                        if spendItem(client, 1080001, 1) then

                            setProfession(client, 143)
                            if awardItem(client, "134365", 1) then

                                text(client, "Congratulations! You have advanced to Fire Wizard. After rebirth, your stamina will counteract 30%  damage.")
                                link(client, "I see.", 255)
                                pic(client, 6)
                                create(client)

                            else

                                text(client, "Congratulations! You have advanced to Fire Wizard. After rebirth, your stamina will counteract 30%  damage.")
                                link(client, "I see.", 255)
                                pic(client, 6)
                                create(client)

                            end

                        else

                            text(client, "Sorry, an emerald is required for promotion. Hill Monsters may drop emeralds. Please come to get promoted after you have one.")
                            link(client, "I see.", 255)
                            pic(client, 6)
                            create(client)

                        end

                    else

                        text(client, "Sorry, only Fire Taoists can be promoted to Fire Wizards after their Spirit is 140, and Agility 45.")
                        link(client, "I see.", 255)
                        pic(client, 6)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 18) then

        if getLevel(client) < 50 then

            text(client, "Sorry, you cannot learn this spell before you reach level 50.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            awardMagic(client, 1150, 0)
            text(client, "Congrats! You have learned Fire Ring.")
            link(client, "Thanks.", 255)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 19) then

        if getLevel(client) < 65 then

            text(client, "Sorry, you cannot learn this spell before you reach level 65.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            awardMagic(client, 1120, 0)
            text(client, "Congrats! You have learned Fire Circle.")
            link(client, "Thanks.", 255)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 20) then

        if getLevel(client) < 52 then

            text(client, "Sorry, you cannot learn this spell before you reach level 52.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            awardMagic(client, 1180, 0)
            text(client, "Congrats! You have learned Fire Meteor.")
            link(client, "Thanks.", 255)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 21) then

        if getLevel(client) < 70 then

            text(client, "Sorry, you cannot get promoted before you reach level 70. Please train harder.")
            link(client, "I see.", 255)
            link(client, "Learn XP skills.", 2)
            pic(client, 6)
            create(client)

        else

            if getSoul(client) < 140 then

                text(client, "Sorry, only Water Taoist can be promoted to Water Wizard. Spirit 140 and agility 45 are required. Please train harder.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                if getDexterity(client) < 45 then

                    text(client, "Sorry, only Water Taoist can be promoted to Water Wizard. Spirit 140 and agility 45 are required. Please train harder.")
                    link(client, "I see.", 255)
                    pic(client, 6)
                    create(client)

                else

                    if getProfession(client) == 132 then

                        if spendItem(client, 1080001, 1) then

                            setProfession(client, 133)
                            if awardItem(client, "134365", 1) then

                                if getMetempsychosis(client) == 1 then

                                    text(client, "Congratulations! You have advanced to Water Wizard. After rebirth, your stamina will counteract 30%  damage.")
                                    link(client, "I see.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    text(client, "Congrats! You are promoted to Water Wizard. Please train hard and come to get promoted after you reach level 100.")
                                    link(client, "I see.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if getMetempsychosis(client) == 1 then

                                    text(client, "Congratulations! You have advanced to Water Wizard. After rebirth, your stamina will counteract 30%  damage.")
                                    link(client, "I see.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    text(client, "Congrats! You are promoted to Water Wizard. Please train hard and come to get promoted after you reach level 100.")
                                    link(client, "I see.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        else

                            text(client, "Sorry, an emerald is required for promotion. Hill Monsters may drop emeralds. Please come to get promoted after you have one.")
                            link(client, "I see.", 255)
                            pic(client, 6)
                            create(client)

                        end

                    else

                        text(client, "Sorry, only Water Taoist can be promoted to Water Wizard. Spirit 140 and agility 45 are required. Please train harder.")
                        link(client, "I see.", 255)
                        pic(client, 6)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 22) then

        if getLevel(client) < 40 then

            text(client, "Sorry, you cannot learn this spell before you reach the required level. Please train harder.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            if hasMagic(client, 1055, -1) then

                text(client, "You have learned this spell.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                awardMagic(client, 1055, 0)
                text(client, "Congrats! You have learned the spell. The power of water exists everywhere.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        end

    elseif (idx == 23) then

        if getLevel(client) < 60 then

            text(client, "Sorry, you cannot learn this spell before you reach the required level. Please train harder.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            if hasMagic(client, 1075, -1) then

                text(client, "You have learned this spell.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                awardMagic(client, 1075, 0)
                text(client, "Congrats! You have learned the spell. The power of water exists everywhere.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        end

    elseif (idx == 24) then

        if getLevel(client) < 45 then

            text(client, "Sorry, you cannot learn this spell before you reach the required level. Please train harder.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            if hasMagic(client, 1085, -1) then

                text(client, "You have learned this spell.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                awardMagic(client, 1085, 0)
                text(client, "Congrats! You have learned the spell. The power of water exists everywhere.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        end

    elseif (idx == 25) then

        if getLevel(client) < 50 then

            text(client, "Sorry, you cannot learn this spell before you reach the required level. Please train harder.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            if hasMagic(client, 1090, -1) then

                text(client, "You have learned this spell.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                awardMagic(client, 1090, 0)
                text(client, "Congrats! You have learned the spell. The power of water exists everywhere.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        end

    elseif (idx == 26) then

        if getLevel(client) < 55 then

            text(client, "Sorry, you cannot learn this spell before you reach the required level. Please train harder.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            if hasMagic(client, 1095, -1) then

                text(client, "You have learned this spell.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                awardMagic(client, 1095, 0)
                text(client, "Congrats! You have learned the spell. The power of water exists everywhere.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        end

    elseif (idx == 27) then

        if getLevel(client) < 70 then

            text(client, "Sorry, you cannot learn this spell before you reach the required level. Please train harder.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            if hasMagic(client, 1100, -1) then

                text(client, "You have learned this spell.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                awardMagic(client, 1100, 0)
                text(client, "Congrats! You have learned this spell.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        end

    elseif (idx == 28) then

        if getLevel(client) < 81 then

            text(client, "Sorry, you cannot learn this spell before you reach the required level. Please train harder.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            if hasMagic(client, 1175, -1) then

                text(client, "You have learned this spell.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                awardMagic(client, 1175, 0)
                text(client, "Congrats! You have learned the spell. The power of water exists everywhere.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        end

    elseif (idx == 29) then

        if getLevel(client) < 94 then

            text(client, "Sorry, you cannot learn this spell before you reach the required level. Please train harder.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        else

            if hasMagic(client, 1170, -1) then

                text(client, "You have learned this spell.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            else

                awardMagic(client, 1170, 0)
                text(client, "Congrats! You have learned the spell. The power of water exists everywhere.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        end

    end

end
