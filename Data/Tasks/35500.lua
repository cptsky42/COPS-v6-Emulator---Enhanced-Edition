--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:51 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask35500(client, idx)
    name = "MightyTao"
    face = 1

    if (idx == 0) then

        text(client, "Sigh, it is a pity that no one can inherit my skill.")
        link(client, "What kind of skill?", 1)
        link(client, "I am just passing by.", 255)
        pic(client, 56)
        create(client)

    elseif (idx == 1) then

        if getMetempsychosis(client) == 0 then

            text(client, "You are intelligent. But I can`t pass the skill to you for you have not been reborn yet.")
            link(client, "It is a pity.", 255)
            pic(client, 56)
            create(client)

        else

            text(client, "You have been reborn successfully. I have a spell to summon the guards or the monsters to your aid. Which one do you prefer?")
            link(client, "SummonGuard[level 15]", 2)
            link(client, "SummonMob[level 40]", 3)
            link(client, "Let me think it over.", 255)
            pic(client, 56)
            create(client)

        end

    elseif (idx == 2) then

        if hasMagic(client, 4000, -1) then

            text(client, "You have mastered the spell of SummonGuard. Please put in more efforts.")
            link(client, "Thanks.", 255)
            pic(client, 56)
            create(client)

        else

            if hasItem(client, 1072031, 1) then

                text(client, "Since you have got EuxeniteOre, I will teach you SummonGuard. Please put in more efforts.")
                link(client, "Thanks.", 4)
                pic(client, 56)
                create(client)

            else

                text(client, "If you want to learn SummonGuard, you should get a EuxeniteOre.")
                link(client, "Thanks.", 255)
                pic(client, 56)
                create(client)

            end

        end

    elseif (idx == 3) then

        if getLevel(client) < 40 then

            text(client, "You have  reborn, but your level is not high enough to master SummonMob.")
            link(client, "Thanks.", 255)
            pic(client, 56)
            create(client)

        else

            text(client, "Each profession has its own skills. please choose a suitable skill according to your profession.")
            link(client, "Warrior Magic", 5)
            link(client, "Trojan Magic", 6)
            link(client, "Archer Magic", 7)
            link(client, "Water Taoist Magic", 8)
            link(client, "Fire Taoist Magic", 9)
            link(client, "Let me see.", 255)
            pic(client, 56)
            create(client)

        end

    elseif (idx == 4) then

        spendItem(client, 1072031, 1)
        awardMagic(client, 4000, 0)

    elseif (idx == 5) then

        if getProfession(client) == 20 then

            if hasMagic(client, 4060, -1) then

                text(client, "You have mastered FireEvil, you should take more practice and carry it forward.")
                link(client, "Thanks.", 255)
                pic(client, 56)
                create(client)

            else

                if hasItems(client, 1072050, 1072059, 1) then

                    text(client, "Since you have got GoldOre, I will teach you FireEvil for Warrior. You should carry it forward.")
                    link(client, "Thanks.", 10)
                    pic(client, 56)
                    create(client)

                else

                    text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                    link(client, "Thanks.", 255)
                    pic(client, 56)
                    create(client)

                end

            end

        else

            if getProfession(client) == 21 then

                if hasMagic(client, 4060, -1) then

                    text(client, "You have mastered FireEvil, you should take more practice and carry it forward.")
                    link(client, "Thanks.", 255)
                    pic(client, 56)
                    create(client)

                else

                    if hasItems(client, 1072050, 1072059, 1) then

                        text(client, "Since you have got GoldOre, I will teach you FireEvil for Warrior. You should carry it forward.")
                        link(client, "Thanks.", 10)
                        pic(client, 56)
                        create(client)

                    else

                        text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                        link(client, "Thanks.", 255)
                        pic(client, 56)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 22 then

                    if hasMagic(client, 4060, -1) then

                        text(client, "You have mastered FireEvil, you should take more practice and carry it forward.")
                        link(client, "Thanks.", 255)
                        pic(client, 56)
                        create(client)

                    else

                        if hasItems(client, 1072050, 1072059, 1) then

                            text(client, "Since you have got GoldOre, I will teach you FireEvil for Warrior. You should carry it forward.")
                            link(client, "Thanks.", 10)
                            pic(client, 56)
                            create(client)

                        else

                            text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                            link(client, "Thanks.", 255)
                            pic(client, 56)
                            create(client)

                        end

                    end

                else

                    if getProfession(client) == 23 then

                        if hasMagic(client, 4060, -1) then

                            text(client, "You have mastered FireEvil, you should take more practice and carry it forward.")
                            link(client, "Thanks.", 255)
                            pic(client, 56)
                            create(client)

                        else

                            if hasItems(client, 1072050, 1072059, 1) then

                                text(client, "Since you have got GoldOre, I will teach you FireEvil for Warrior. You should carry it forward.")
                                link(client, "Thanks.", 10)
                                pic(client, 56)
                                create(client)

                            else

                                text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                                link(client, "Thanks.", 255)
                                pic(client, 56)
                                create(client)

                            end

                        end

                    else

                        if getProfession(client) == 24 then

                            if hasMagic(client, 4060, -1) then

                                text(client, "You have mastered FireEvil, you should take more practice and carry it forward.")
                                link(client, "Thanks.", 255)
                                pic(client, 56)
                                create(client)

                            else

                                if hasItems(client, 1072050, 1072059, 1) then

                                    text(client, "Since you have got GoldOre, I will teach you FireEvil for Warrior. You should carry it forward.")
                                    link(client, "Thanks.", 10)
                                    pic(client, 56)
                                    create(client)

                                else

                                    text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 56)
                                    create(client)

                                end

                            end

                        else

                            if getProfession(client) == 25 then

                                if hasMagic(client, 4060, -1) then

                                    text(client, "You have mastered FireEvil, you should take more practice and carry it forward.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 56)
                                    create(client)

                                else

                                    if hasItems(client, 1072050, 1072059, 1) then

                                        text(client, "Since you have got GoldOre, I will teach you FireEvil for Warrior. You should carry it forward.")
                                        link(client, "Thanks.", 10)
                                        pic(client, 56)
                                        create(client)

                                    else

                                        text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                                        link(client, "Thanks.", 255)
                                        pic(client, 56)
                                        create(client)

                                    end

                                end

                            else

                                text(client, "Sorry, you are not a warrior.")
                                link(client, "Warrior Magic", 5)
                                link(client, "Trojan Magic", 6)
                                link(client, "Archer Magic", 7)
                                link(client, "Water Taoist Magic", 8)
                                link(client, "Fire Taoist Magic", 9)
                                link(client, "Let me see.", 255)
                                pic(client, 56)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 6) then

        if getProfession(client) == 10 then

            if hasMagic(client, 4050, -1) then

                text(client, "You have mastered BloodyBat, you should take more practice and carry it forward.")
                link(client, "Thanks.", 255)
                pic(client, 56)
                create(client)

            else

                if hasItems(client, 1072050, 1072059, 1) then

                    text(client, "You have got GoldOre. I will teach you BloodyBat Magic for Trojan. You should carry it forward.")
                    link(client, "Thanks.", 11)
                    pic(client, 56)
                    create(client)

                else

                    text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                    link(client, "Thanks.", 255)
                    pic(client, 56)
                    create(client)

                end

            end

        else

            if getProfession(client) == 11 then

                if hasMagic(client, 4050, -1) then

                    text(client, "You have mastered BloodyBat, you should take more practice and carry it forward.")
                    link(client, "Thanks.", 255)
                    pic(client, 56)
                    create(client)

                else

                    if hasItems(client, 1072050, 1072059, 1) then

                        text(client, "You have got GoldOre. I will teach you BloodyBat Magic for Trojan. You should carry it forward.")
                        link(client, "Thanks.", 11)
                        pic(client, 56)
                        create(client)

                    else

                        text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                        link(client, "Thanks.", 255)
                        pic(client, 56)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 12 then

                    if hasMagic(client, 4050, -1) then

                        text(client, "You have mastered BloodyBat, you should take more practice and carry it forward.")
                        link(client, "Thanks.", 255)
                        pic(client, 56)
                        create(client)

                    else

                        if hasItems(client, 1072050, 1072059, 1) then

                            text(client, "You have got GoldOre. I will teach you BloodyBat Magic for Trojan. You should carry it forward.")
                            link(client, "Thanks.", 11)
                            pic(client, 56)
                            create(client)

                        else

                            text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                            link(client, "Thanks.", 255)
                            pic(client, 56)
                            create(client)

                        end

                    end

                else

                    if getProfession(client) == 13 then

                        if hasMagic(client, 4050, -1) then

                            text(client, "You have mastered BloodyBat, you should take more practice and carry it forward.")
                            link(client, "Thanks.", 255)
                            pic(client, 56)
                            create(client)

                        else

                            if hasItems(client, 1072050, 1072059, 1) then

                                text(client, "You have got GoldOre. I will teach you BloodyBat Magic for Trojan. You should carry it forward.")
                                link(client, "Thanks.", 11)
                                pic(client, 56)
                                create(client)

                            else

                                text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                                link(client, "Thanks.", 255)
                                pic(client, 56)
                                create(client)

                            end

                        end

                    else

                        if getProfession(client) == 14 then

                            if hasMagic(client, 4050, -1) then

                                text(client, "You have mastered BloodyBat, you should take more practice and carry it forward.")
                                link(client, "Thanks.", 255)
                                pic(client, 56)
                                create(client)

                            else

                                if hasItems(client, 1072050, 1072059, 1) then

                                    text(client, "You have got GoldOre. I will teach you BloodyBat Magic for Trojan. You should carry it forward.")
                                    link(client, "Thanks.", 11)
                                    pic(client, 56)
                                    create(client)

                                else

                                    text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 56)
                                    create(client)

                                end

                            end

                        else

                            if getProfession(client) == 15 then

                                if hasMagic(client, 4050, -1) then

                                    text(client, "You have mastered BloodyBat, you should take more practice and carry it forward.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 56)
                                    create(client)

                                else

                                    if hasItems(client, 1072050, 1072059, 1) then

                                        text(client, "You have got GoldOre. I will teach you BloodyBat Magic for Trojan. You should carry it forward.")
                                        link(client, "Thanks.", 11)
                                        pic(client, 56)
                                        create(client)

                                    else

                                        text(client, "If you want to learn HeavenEvil, you should get a GoldOre of any rate. This is our rule.")
                                        link(client, "Thanks.", 255)
                                        pic(client, 56)
                                        create(client)

                                    end

                                end

                            else

                                text(client, "Each profession has its own skill. You are not a Trojon, how can you learn Trojan`s skill?")
                                link(client, "Warrior Magic", 5)
                                link(client, "Trojan Magic", 6)
                                link(client, "Archer Magic", 7)
                                link(client, "Water Taoist Magic", 8)
                                link(client, "Fire Taoist Magic", 9)
                                link(client, "Let me see.", 255)
                                pic(client, 56)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 7) then

        if getProfession(client) == 40 then

            if hasMagic(client, 4070, -1) then

                text(client, "You have mastered Skeleton. You should take more practice.")
                link(client, "Thanks.", 255)
                pic(client, 56)
                create(client)

            else

                if hasItems(client, 1072050, 1072059, 1) then

                    text(client, "Since you have got a GoldOre, I will teach you Skeleton. You should take more practice and carry it forward.")
                    link(client, "Thanks.", 12)
                    pic(client, 56)
                    create(client)

                else

                    text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                    link(client, "Thanks.", 255)
                    pic(client, 56)
                    create(client)

                end

            end

        else

            if getProfession(client) == 41 then

                if hasMagic(client, 4070, -1) then

                    text(client, "You have mastered Skeleton. You should take more practice.")
                    link(client, "Thanks.", 255)
                    pic(client, 56)
                    create(client)

                else

                    if hasItems(client, 1072050, 1072059, 1) then

                        text(client, "Since you have got a GoldOre, I will teach you Skeleton. You should take more practice and carry it forward.")
                        link(client, "Thanks.", 12)
                        pic(client, 56)
                        create(client)

                    else

                        text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                        link(client, "Thanks.", 255)
                        pic(client, 56)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 42 then

                    if hasMagic(client, 4070, -1) then

                        text(client, "You have mastered Skeleton. You should take more practice.")
                        link(client, "Thanks.", 255)
                        pic(client, 56)
                        create(client)

                    else

                        if hasItems(client, 1072050, 1072059, 1) then

                            text(client, "Since you have got a GoldOre, I will teach you Skeleton. You should take more practice and carry it forward.")
                            link(client, "Thanks.", 12)
                            pic(client, 56)
                            create(client)

                        else

                            text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                            link(client, "Thanks.", 255)
                            pic(client, 56)
                            create(client)

                        end

                    end

                else

                    if getProfession(client) == 43 then

                        if hasMagic(client, 4070, -1) then

                            text(client, "You have mastered Skeleton. You should take more practice.")
                            link(client, "Thanks.", 255)
                            pic(client, 56)
                            create(client)

                        else

                            if hasItems(client, 1072050, 1072059, 1) then

                                text(client, "Since you have got a GoldOre, I will teach you Skeleton. You should take more practice and carry it forward.")
                                link(client, "Thanks.", 12)
                                pic(client, 56)
                                create(client)

                            else

                                text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                                link(client, "Thanks.", 255)
                                pic(client, 56)
                                create(client)

                            end

                        end

                    else

                        if getProfession(client) == 44 then

                            if hasMagic(client, 4070, -1) then

                                text(client, "You have mastered Skeleton. You should take more practice.")
                                link(client, "Thanks.", 255)
                                pic(client, 56)
                                create(client)

                            else

                                if hasItems(client, 1072050, 1072059, 1) then

                                    text(client, "Since you have got a GoldOre, I will teach you Skeleton. You should take more practice and carry it forward.")
                                    link(client, "Thanks.", 12)
                                    pic(client, 56)
                                    create(client)

                                else

                                    text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 56)
                                    create(client)

                                end

                            end

                        else

                            if getProfession(client) == 45 then

                                if hasMagic(client, 4070, -1) then

                                    text(client, "You have mastered Skeleton. You should take more practice.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 56)
                                    create(client)

                                else

                                    if hasItems(client, 1072050, 1072059, 1) then

                                        text(client, "Since you have got a GoldOre, I will teach you Skeleton. You should take more practice and carry it forward.")
                                        link(client, "Thanks.", 12)
                                        pic(client, 56)
                                        create(client)

                                    else

                                        text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                                        link(client, "Thanks.", 255)
                                        pic(client, 56)
                                        create(client)

                                    end

                                end

                            else

                                text(client, "Each profession has its magic. You are not Archer, how can you learn Archer??s Magic?")
                                link(client, "Warrior Magic", 5)
                                link(client, "Trojan Magic", 6)
                                link(client, "Archer Magic", 7)
                                link(client, "Water Taoist Magic", 8)
                                link(client, "Fire Taoist Magic", 9)
                                link(client, "Let me see.", 255)
                                pic(client, 56)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 8) then

        if getProfession(client) == 100 then

            if hasMagic(client, 4020, -1) then

                text(client, "You have mastered BatBoss, you should take more practice and carry it forward.")
                link(client, "Thanks.", 255)
                pic(client, 56)
                create(client)

            else

                if hasItems(client, 1072050, 1072059, 1) then

                    text(client, "Since you have got a GoldOre, I will teach you BatBoss for Water Taoist. You should carry it forward.")
                    link(client, "Thanks.", 13)
                    pic(client, 56)
                    create(client)

                else

                    text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                    link(client, "Thanks.", 255)
                    pic(client, 56)
                    create(client)

                end

            end

        else

            if getProfession(client) == 101 then

                if hasMagic(client, 4020, -1) then

                    text(client, "You have mastered BatBoss, you should take more practice and carry it forward.")
                    link(client, "Thanks.", 255)
                    pic(client, 56)
                    create(client)

                else

                    if hasItems(client, 1072050, 1072059, 1) then

                        text(client, "Since you have got a GoldOre, I will teach you BatBoss for Water Taoist. You should carry it forward.")
                        link(client, "Thanks.", 13)
                        pic(client, 56)
                        create(client)

                    else

                        text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                        link(client, "Thanks.", 255)
                        pic(client, 56)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 132 then

                    if hasMagic(client, 4020, -1) then

                        text(client, "You have mastered BatBoss, you should take more practice and carry it forward.")
                        link(client, "Thanks.", 255)
                        pic(client, 56)
                        create(client)

                    else

                        if hasItems(client, 1072050, 1072059, 1) then

                            text(client, "Since you have got a GoldOre, I will teach you BatBoss for Water Taoist. You should carry it forward.")
                            link(client, "Thanks.", 13)
                            pic(client, 56)
                            create(client)

                        else

                            text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                            link(client, "Thanks.", 255)
                            pic(client, 56)
                            create(client)

                        end

                    end

                else

                    if getProfession(client) == 133 then

                        if hasMagic(client, 4020, -1) then

                            text(client, "You have mastered BatBoss, you should take more practice and carry it forward.")
                            link(client, "Thanks.", 255)
                            pic(client, 56)
                            create(client)

                        else

                            if hasItems(client, 1072050, 1072059, 1) then

                                text(client, "Since you have got a GoldOre, I will teach you BatBoss for Water Taoist. You should carry it forward.")
                                link(client, "Thanks.", 13)
                                pic(client, 56)
                                create(client)

                            else

                                text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                                link(client, "Thanks.", 255)
                                pic(client, 56)
                                create(client)

                            end

                        end

                    else

                        if getProfession(client) == 134 then

                            if hasMagic(client, 4020, -1) then

                                text(client, "You have mastered BatBoss, you should take more practice and carry it forward.")
                                link(client, "Thanks.", 255)
                                pic(client, 56)
                                create(client)

                            else

                                if hasItems(client, 1072050, 1072059, 1) then

                                    text(client, "Since you have got a GoldOre, I will teach you BatBoss for Water Taoist. You should carry it forward.")
                                    link(client, "Thanks.", 13)
                                    pic(client, 56)
                                    create(client)

                                else

                                    text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 56)
                                    create(client)

                                end

                            end

                        else

                            if getProfession(client) == 135 then

                                if hasMagic(client, 4020, -1) then

                                    text(client, "You have mastered BatBoss, you should take more practice and carry it forward.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 56)
                                    create(client)

                                else

                                    if hasItems(client, 1072050, 1072059, 1) then

                                        text(client, "Since you have got a GoldOre, I will teach you BatBoss for Water Taoist. You should carry it forward.")
                                        link(client, "Thanks.", 13)
                                        pic(client, 56)
                                        create(client)

                                    else

                                        text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                                        link(client, "Thanks.", 255)
                                        pic(client, 56)
                                        create(client)

                                    end

                                end

                            else

                                text(client, "Each profession has its magic. You are not Water Taoist, how can you learn Water Taoist??s Magic?")
                                link(client, "Warrior Magic", 5)
                                link(client, "Trojan Magic", 6)
                                link(client, "Archer Magic", 7)
                                link(client, "Water Taoist Magic", 8)
                                link(client, "Fire Taoist Magic", 9)
                                link(client, "Let me see.", 255)
                                pic(client, 56)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 9) then

        if getProfession(client) == 100 then

            if hasMagic(client, 4020, -1) then

                text(client, "You have mastered the skill of SummonBat, you must do more practice to strengthen it.")
                link(client, "Thanks.", 255)
                pic(client, 56)
                create(client)

            else

                if hasItems(client, 1072050, 1072059, 1) then

                    text(client, "Since you have got GoldOre, I will teach you SummonBat. You should do more practice and carry forward it.")
                    link(client, "Thanks.", 14)
                    pic(client, 56)
                    create(client)

                else

                    text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                    link(client, "Thanks.", 255)
                    pic(client, 56)
                    create(client)

                end

            end

        else

            if getProfession(client) == 101 then

                if hasMagic(client, 4020, -1) then

                    text(client, "You have mastered the skill of SummonBat, you must do more practice to strengthen it.")
                    link(client, "Thanks.", 255)
                    pic(client, 56)
                    create(client)

                else

                    if hasItems(client, 1072050, 1072059, 1) then

                        text(client, "Since you have got GoldOre, I will teach you SummonBat. You should do more practice and carry forward it.")
                        link(client, "Thanks.", 14)
                        pic(client, 56)
                        create(client)

                    else

                        text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                        link(client, "Thanks.", 255)
                        pic(client, 56)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 142 then

                    if hasMagic(client, 4020, -1) then

                        text(client, "You have mastered the skill of SummonBat, you must do more practice to strengthen it.")
                        link(client, "Thanks.", 255)
                        pic(client, 56)
                        create(client)

                    else

                        if hasItems(client, 1072050, 1072059, 1) then

                            text(client, "Since you have got GoldOre, I will teach you SummonBat. You should do more practice and carry forward it.")
                            link(client, "Thanks.", 14)
                            pic(client, 56)
                            create(client)

                        else

                            text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                            link(client, "Thanks.", 255)
                            pic(client, 56)
                            create(client)

                        end

                    end

                else

                    if getProfession(client) == 143 then

                        if hasMagic(client, 4020, -1) then

                            text(client, "You have mastered the skill of SummonBat, you must do more practice to strengthen it.")
                            link(client, "Thanks.", 255)
                            pic(client, 56)
                            create(client)

                        else

                            if hasItems(client, 1072050, 1072059, 1) then

                                text(client, "Since you have got GoldOre, I will teach you SummonBat. You should do more practice and carry forward it.")
                                link(client, "Thanks.", 14)
                                pic(client, 56)
                                create(client)

                            else

                                text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                                link(client, "Thanks.", 255)
                                pic(client, 56)
                                create(client)

                            end

                        end

                    else

                        if getProfession(client) == 144 then

                            if hasMagic(client, 4020, -1) then

                                text(client, "You have mastered the skill of SummonBat, you must do more practice to strengthen it.")
                                link(client, "Thanks.", 255)
                                pic(client, 56)
                                create(client)

                            else

                                if hasItems(client, 1072050, 1072059, 1) then

                                    text(client, "Since you have got GoldOre, I will teach you SummonBat. You should do more practice and carry forward it.")
                                    link(client, "Thanks.", 14)
                                    pic(client, 56)
                                    create(client)

                                else

                                    text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 56)
                                    create(client)

                                end

                            end

                        else

                            if getProfession(client) == 145 then

                                if hasMagic(client, 4020, -1) then

                                    text(client, "You have mastered the skill of SummonBat, you must do more practice to strengthen it.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 56)
                                    create(client)

                                else

                                    if hasItems(client, 1072050, 1072059, 1) then

                                        text(client, "Since you have got GoldOre, I will teach you SummonBat. You should do more practice and carry forward it.")
                                        link(client, "Thanks.", 14)
                                        pic(client, 56)
                                        create(client)

                                    else

                                        text(client, "If you want to learn HeavenEvil Magic, you should get a GoldOre of any rate. This is our rule.")
                                        link(client, "Thanks.", 255)
                                        pic(client, 56)
                                        create(client)

                                    end

                                end

                            else

                                text(client, "Only Fire Taoist can learn SummonBat.")
                                link(client, "Warrior Magic", 5)
                                link(client, "Trojan Magic", 6)
                                link(client, "Archer Magic", 7)
                                link(client, "Water Taoist Magic", 8)
                                link(client, "Fire Taoist Magic", 9)
                                link(client, "Let me see.", 255)
                                pic(client, 56)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 10) then

        spendItems(client, 1072050, 1072059, 1)
        awardMagic(client, 4060, 0)

    elseif (idx == 11) then

        spendItems(client, 1072050, 1072059, 1)
        awardMagic(client, 4050, 0)

    elseif (idx == 12) then

        spendItems(client, 1072050, 1072059, 1)
        awardMagic(client, 4070, 0)

    elseif (idx == 13) then

        spendItems(client, 1072050, 1072059, 1)
        awardMagic(client, 4020, 0)

    elseif (idx == 14) then

        spendItems(client, 1072050, 1072059, 1)
        awardMagic(client, 4010, 0)

    end

end
