--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/21/2015 11:49:58 AM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask400(client, idx)
    name = "ArcherGod"
    face = 1

    if (idx == 0) then

        text(client, "I am the famous Archer God. What can I do for you?")
        link(client, "Learn Archer skills.", 1)
        link(client, "Get promoted.", 2)
        pic(client, 10)
        create(client)

    elseif (idx == 1) then

        text(client, "I am proficient in all Archer skills. What do you want to learn?")
        link(client, "ElementaryFly[Level 15]", 3)
        link(client, "Scatter[Level 23]", 4)
        link(client, "RapidFire[Level 46]", 5)
        link(client, "Improved Fly[Level 70]", 6)
        link(client, "ArrowRain[Level 70]", 7)
        link(client, "Intensify[Level 71]", 8)
        link(client, "Advanced Fly[Level 100]", 9)
        pic(client, 10)
        create(client)

    elseif (idx == 2) then

        if getProfession(client) == 40 then

            if getLevel(client) < 15 then

                text(client, "Sorry, you cannot get promoted before you reach level 15. Please train harder.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            else

                text(client, "Archers have the advantage of fast speed and distance shoot. They can dodge physical attack while they are flying.")
                link(client, "Get promoted.", 10)
                link(client, "Let me think it over.", 255)
                pic(client, 10)
                create(client)

            end

        else

            if getProfession(client) == 41 then

                if getLevel(client) < 40 then

                    text(client, "Sorry, you cannot get promoted before you reach level 40. Please train harder.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                else

                    if getForce(client) < 25 then

                        text(client, "Sorry, only Archer can be promoted to Eagle Archer. Strength 25 and agility 90 are required. Please train harder.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        if getDexterity(client) < 90 then

                            text(client, "Sorry, only Archer can be promoted to Eagle Archer. Strength 25 and agility 90 are required. Please train harder.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            if getProfession(client) == 41 then

                                text(client, "If you give me 5 Euxenite Ores, I can promote you to Eagle Archer and teach you more skills.")
                                link(client, "Get promoted.", 11)
                                link(client, "Let me think it over.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                text(client, "Sorry, only Archer can be promoted to Eagle Archer. Strength 25 and agility 90 are required. Please train harder.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            end

                        end

                    end

                end

            else

                if getProfession(client) == 42 then

                    text(client, "Eagle Archers can be promoted to Tiger Archers.")
                    link(client, "Get promoted.", 12)
                    link(client, "Let me think it over.", 255)
                    pic(client, 10)
                    create(client)

                else

                    if getProfession(client) == 43 then

                        if getLevel(client) < 100 then

                            text(client, "Sorry, only Tiger Archer can be promoted to Dragon Archer. Strength 60 and agility 215 are required. Please train harder.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            if getForce(client) < 60 then

                                text(client, "Sorry, only Tiger Archer can be promoted to Dragon Archer. Strength 60 and agility 215 are required. Please train harder.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                if getDexterity(client) < 215 then

                                    text(client, "Sorry, only Tiger Archer can be promoted to Dragon Archer. Strength 60 and agility 215 are required. Please train harder.")
                                    link(client, "I see.", 255)
                                    pic(client, 10)
                                    create(client)

                                else

                                    text(client, "If you give me a meteor, I can promote you to Dragon Archer.")
                                    link(client, "Get promoted.", 13)
                                    pic(client, 10)
                                    create(client)

                                end

                            end

                        end

                    else

                        if getProfession(client) == 44 then

                            if getForce(client) < 68 then

                                text(client, "Sorry, only Dragon Archers can be promoted to Archer Kings after they are level 110, agility 235 and strength 68.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                if getDexterity(client) < 235 then

                                    text(client, "Sorry, only Dragon Archers can be promoted to Archer Kings after they are level 110, agility 235 and strength 68.")
                                    link(client, "I see.", 255)
                                    pic(client, 10)
                                    create(client)

                                else

                                    text(client, "If you give me a Moon Box, I can promote you to Archer King.")
                                    link(client, "Get promoted.", 14)
                                    pic(client, 10)
                                    create(client)

                                end

                            end

                        else

                            if getProfession(client) == 45 then

                                text(client, "Congrats! You are promoted to Archer King. Please train hard. I believe you will be a great Archer King.")
                                link(client, "Thanks.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                text(client, "Sorry, you are not an Archer. I only promote and teach Archers.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 3) then

        if getProfession(client) == 41 then

            if getLevel(client) < 15 then

                text(client, "Sorry, Only Archers can learn this skill after they reach level 15.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            else

                if awardMagic(client, 8002, 0) then

                    text(client, "Congrats! You have learned Junior Fly. This spell can make you dodge the physical attack.")
                    link(client, "Thanks.", 255)
                    pic(client, 10)
                    create(client)

                else

                    text(client, "You have learned this skill.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                end

            end

        else

            if getProfession(client) == 42 then

                if getLevel(client) < 15 then

                    text(client, "Sorry, Only Archers can learn this skill after they reach level 15.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                else

                    if awardMagic(client, 8002, 0) then

                        text(client, "Congrats! You have learned Junior Fly. This spell can make you dodge the physical attack.")
                        link(client, "Thanks.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        text(client, "You have learned this skill.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 43 then

                    if getLevel(client) < 15 then

                        text(client, "Sorry, Only Archers can learn this skill after they reach level 15.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        if awardMagic(client, 8002, 0) then

                            text(client, "Congrats! You have learned Junior Fly. This spell can make you dodge the physical attack.")
                            link(client, "Thanks.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            text(client, "You have learned this skill.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    end

                else

                    if getProfession(client) == 44 then

                        if getLevel(client) < 15 then

                            text(client, "Sorry, Only Archers can learn this skill after they reach level 15.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            if awardMagic(client, 8002, 0) then

                                text(client, "Congrats! You have learned Junior Fly. This spell can make you dodge the physical attack.")
                                link(client, "Thanks.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                text(client, "You have learned this skill.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            end

                        end

                    else

                        if getProfession(client) == 45 then

                            if getLevel(client) < 15 then

                                text(client, "Sorry, Only Archers can learn this skill after they reach level 15.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                if awardMagic(client, 8002, 0) then

                                    text(client, "Congrats! You have learned Junior Fly. This spell can make you dodge the physical attack.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 10)
                                    create(client)

                                else

                                    text(client, "You have learned this skill.")
                                    link(client, "I see.", 255)
                                    pic(client, 10)
                                    create(client)

                                end

                            end

                        else

                            text(client, "Sorry, Only Archers can learn this skill after they reach level 15.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    end

                end

            end

        end

    elseif (idx == 4) then

        if getProfession(client) == 41 then

            if getLevel(client) < 23 then

                text(client, "Sorry, only Archers can learn this skill after they reach level 23.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            else

                if awardMagic(client, 8001, 0) then

                    text(client, "Congrats! You have learned Scatter.")
                    link(client, "Thanks.", 255)
                    pic(client, 10)
                    create(client)

                else

                    text(client, "You have learned this skill.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                end

            end

        else

            if getProfession(client) == 42 then

                if getLevel(client) < 23 then

                    text(client, "Sorry, only Archers can learn this skill after they reach level 23.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                else

                    if awardMagic(client, 8001, 0) then

                        text(client, "Congrats! You have learned Scatter.")
                        link(client, "Thanks.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        text(client, "You have learned this skill.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 43 then

                    if getLevel(client) < 23 then

                        text(client, "Sorry, only Archers can learn this skill after they reach level 23.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        if awardMagic(client, 8001, 0) then

                            text(client, "Congrats! You have learned Scatter.")
                            link(client, "Thanks.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            text(client, "You have learned this skill.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    end

                else

                    if getProfession(client) == 44 then

                        if getLevel(client) < 23 then

                            text(client, "Sorry, only Archers can learn this skill after they reach level 23.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            if awardMagic(client, 8001, 0) then

                                text(client, "Congrats! You have learned Scatter.")
                                link(client, "Thanks.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                text(client, "You have learned this skill.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            end

                        end

                    else

                        if getProfession(client) == 45 then

                            if getLevel(client) < 23 then

                                text(client, "Sorry, only Archers can learn this skill after they reach level 23.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                if awardMagic(client, 8001, 0) then

                                    text(client, "Congrats! You have learned Scatter.")
                                    link(client, "Thanks.", 255)
                                    pic(client, 10)
                                    create(client)

                                else

                                    text(client, "You have learned this skill.")
                                    link(client, "I see.", 255)
                                    pic(client, 10)
                                    create(client)

                                end

                            end

                        else

                            text(client, "Sorry, only Archers can learn this skill after they reach level 23.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    end

                end

            end

        end

    elseif (idx == 5) then

        if getProfession(client) == 42 then

            if getLevel(client) < 46 then

                text(client, "You can learn it after you reach level 46.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            else

                if awardMagic(client, 8000, 0) then

                    text(client, "Congrats! You have learned Rapid Fire.")
                    link(client, "Thanks.", 255)
                    pic(client, 10)
                    create(client)

                else

                    text(client, "You have learned this skill.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                end

            end

        else

            if getProfession(client) == 43 then

                if getLevel(client) < 46 then

                    text(client, "You can learn it after you reach level 46.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                else

                    if awardMagic(client, 8000, 0) then

                        text(client, "Congrats! You have learned Rapid Fire.")
                        link(client, "Thanks.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        text(client, "You have learned this skill.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 44 then

                    if getLevel(client) < 46 then

                        text(client, "You can learn it after you reach level 46.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        if awardMagic(client, 8000, 0) then

                            text(client, "Congrats! You have learned Rapid Fire.")
                            link(client, "Thanks.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            text(client, "You have learned this skill.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    end

                else

                    if getProfession(client) == 45 then

                        if getLevel(client) < 46 then

                            text(client, "You can learn it after you reach level 46.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            if awardMagic(client, 8000, 0) then

                                text(client, "Congrats! You have learned Rapid Fire.")
                                link(client, "Thanks.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                text(client, "You have learned this skill.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            end

                        end

                    else

                        text(client, "Sorry, Only Eagle Archers can learn this spell after they reach level 46.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 6) then

        if getProfession(client) == 43 then

            if getLevel(client) < 70 then

                text(client, "Sorry, only Tiger Archers can learn this skill after they reach level 70.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            else

                if hasMagic(client, 8002, 0) then

                    if awardMagic(client, 8003, 0) then

                        text(client, "Congrats! You have learned Improved Fly.")
                        link(client, "Thanks.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        text(client, "You have learned this skill.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    end

                else

                    if hasMagic(client, 8003, -1) then

                        text(client, "You have learned this skill.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        if hasMagic(client, 8004, -1) then

                            text(client, "Sorry, you have learned advanced Fly, you need not learn Improved Fly.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            text(client, "Sorry, please learn Junior Fly first.")
                            link(client, "OK.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    end

                end

            end

        else

            if getProfession(client) == 44 then

                if getLevel(client) < 70 then

                    text(client, "Sorry, only Tiger Archers can learn this skill after they reach level 70.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                else

                    if hasMagic(client, 8002, 0) then

                        if awardMagic(client, 8003, 0) then

                            text(client, "Congrats! You have learned Improved Fly.")
                            link(client, "Thanks.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            text(client, "You have learned this skill.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    else

                        if hasMagic(client, 8003, -1) then

                            text(client, "You have learned this skill.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            if hasMagic(client, 8004, -1) then

                                text(client, "Sorry, you have learned advanced Fly, you need not learn Improved Fly.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                text(client, "Sorry, please learn Junior Fly first.")
                                link(client, "OK.", 255)
                                pic(client, 10)
                                create(client)

                            end

                        end

                    end

                end

            else

                if getProfession(client) == 45 then

                    if getLevel(client) < 70 then

                        text(client, "Sorry, only Tiger Archers can learn this skill after they reach level 70.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        if hasMagic(client, 8002, 0) then

                            if awardMagic(client, 8003, 0) then

                                text(client, "Congrats! You have learned Improved Fly.")
                                link(client, "Thanks.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                text(client, "You have learned this skill.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            end

                        else

                            if hasMagic(client, 8003, -1) then

                                text(client, "You have learned this skill.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                if hasMagic(client, 8004, -1) then

                                    text(client, "Sorry, you have learned advanced Fly, you need not learn Improved Fly.")
                                    link(client, "I see.", 255)
                                    pic(client, 10)
                                    create(client)

                                else

                                    text(client, "Sorry, please learn Junior Fly first.")
                                    link(client, "OK.", 255)
                                    pic(client, 10)
                                    create(client)

                                end

                            end

                        end

                    end

                else

                    text(client, "Sorry, only Tiger Archers can learn this skill after they reach level 70.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                end

            end

        end

    elseif (idx == 7) then

        if getProfession(client) == 43 then

            if getLevel(client) < 70 then

                text(client, "Sorry, only Tiger Archers can learn this skill after they reach level 70.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            else

                if awardMagic(client, 8030, 0) then

                    text(client, "Congrats! You have learned Arrow Rain.")
                    link(client, "Thanks.", 255)
                    pic(client, 10)
                    create(client)

                else

                    text(client, "You have learned this skill.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                end

            end

        else

            if getProfession(client) == 44 then

                if getLevel(client) < 70 then

                    text(client, "Sorry, only Tiger Archers can learn this skill after they reach level 70.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                else

                    if awardMagic(client, 8030, 0) then

                        text(client, "Congrats! You have learned Arrow Rain.")
                        link(client, "Thanks.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        text(client, "You have learned this skill.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 45 then

                    if getLevel(client) < 70 then

                        text(client, "Sorry, only Tiger Archers can learn this skill after they reach level 70.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        if awardMagic(client, 8030, 0) then

                            text(client, "Congrats! You have learned Arrow Rain.")
                            link(client, "Thanks.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            text(client, "You have learned this skill.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    end

                else

                    text(client, "Sorry, only Tiger Archers can learn this skill after they reach level 70.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                end

            end

        end

    elseif (idx == 8) then

        if getProfession(client) == 43 then

            if getLevel(client) < 71 then


            else

                if awardMagic(client, 9000, 0) then

                    text(client, "Congrats! You have learned Intensify.")
                    link(client, "Thanks.", 255)
                    pic(client, 10)
                    create(client)

                else

                    text(client, "You have learned this skill.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                end

            end

        else

            if getProfession(client) == 44 then

                if getLevel(client) < 71 then


                else

                    if awardMagic(client, 9000, 0) then

                        text(client, "Congrats! You have learned Intensify.")
                        link(client, "Thanks.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        text(client, "You have learned this skill.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    end

                end

            else

                if getProfession(client) == 45 then

                    if getLevel(client) < 71 then


                    else

                        if awardMagic(client, 9000, 0) then

                            text(client, "Congrats! You have learned Intensify.")
                            link(client, "Thanks.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            text(client, "You have learned this skill.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    end

                else

                    text(client, "Sorry, only Tiger Archers can learn this skill after they reach level 71.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                end

            end

        end

    elseif (idx == 9) then

        if getProfession(client) == 44 then

            if getLevel(client) < 100 then

                text(client, "Sorry, only Dragon Archers can learn this skill after they reach level 100.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            else

                if hasMagic(client, 8003, 1) then

                    text(client, "You have learned this skill.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                else

                    if hasMagic(client, 8003, 0) then

                        if upMagicLevel(client, 8003) then

                            text(client, "Congrats! You have learned Advanced Fly.")
                            link(client, "Thanks.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            text(client, "You have learned this skill.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    else

                        if hasMagic(client, 8002, -1) then

                            text(client, "Sorry, please learn Improved Fly first.")
                            link(client, "OK.", 255)
                            pic(client, 10)
                            create(client)

                        else

                            text(client, "Sorry, please learn Junior Fly first.")
                            link(client, "OK.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    end

                end

            end

        else

            if getProfession(client) == 45 then

                if getLevel(client) < 100 then

                    text(client, "Sorry, only Dragon Archers can learn this skill after they reach level 100.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                else

                    if hasMagic(client, 8003, 1) then

                        text(client, "You have learned this skill.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        if hasMagic(client, 8003, 0) then

                            if upMagicLevel(client, 8003) then

                                text(client, "Congrats! You have learned Advanced Fly.")
                                link(client, "Thanks.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                text(client, "You have learned this skill.")
                                link(client, "I see.", 255)
                                pic(client, 10)
                                create(client)

                            end

                        else

                            if hasMagic(client, 8002, -1) then

                                text(client, "Sorry, please learn Improved Fly first.")
                                link(client, "OK.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                text(client, "Sorry, please learn Junior Fly first.")
                                link(client, "OK.", 255)
                                pic(client, 10)
                                create(client)

                            end

                        end

                    end

                end

            else

                text(client, "Sorry, only Dragon Archers can learn this skill after they reach level 100.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            end

        end

    elseif (idx == 10) then

        if getForce(client) < 12 then

            text(client, "Sorry, only Intern Archers can be promoted to Archer. Strength 12 and agility 35 are required. Please consult other people.")
            link(client, "I see.", 255)
            pic(client, 10)
            create(client)

        else

            if getDexterity(client) < 35 then

                text(client, "Sorry, only Intern Archers can be promoted to Archer. Strength 12 and agility 35 are required. Please consult other people.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            else

                if getProfession(client) == 40 then

                    setProfession(client, 41)
                    if awardMagic(client, 8002, 0) then

                        awardItem(client, "133305", 1)
                        text(client, "Congrats! You are promoted to Archer and have learned flying. When your XP is full, system will notify you to use.")
                        link(client, "Thanks.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        awardItem(client, "133305", 1)
                        text(client, "Congrats! You are promoted to Archer and have learned flying. When your XP is full, system will notify you to use.")
                        link(client, "Thanks.", 255)
                        pic(client, 10)
                        create(client)

                    end

                else

                    text(client, "Sorry, only Intern Archers can be promoted to Archer. Strength 12 and agility 35 are required. Please consult other people.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                end

            end

        end

    elseif (idx == 11) then

        if hasItem(client, 1072031, 5) then

            spendItem(client, 1072031, 5)
            setProfession(client, 42)
            awardItem(client, "500077 0 0 0 255 0 0 0 0 0 0", 1)
            text(client, "Congrats! You are promoted to Eagle Archer. I rewarded you a unique Horn Bow. Please train hard.")
            link(client, "Thanks.", 255)
            pic(client, 10)
            create(client)

        else

            text(client, "Sorry, you do not have 5 Euxenite Ores. You may dig Euxenite Ores in mine cave.")
            link(client, "I see.", 255)
            pic(client, 10)
            create(client)

        end

    elseif (idx == 12) then

        text(client, "Eagle Archer can be promoted to Tiger Archer after they reach level 70 and have an emerald.")
        link(client, "Ready for promotion.", 15)
        link(client, "I see.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 13) then

        if hasTaskItem(client, "Meteor") and (getMoney(client) >= 0) then

            if getMetempsychosis(client) == 0 then

                setProfession(client, 44)
                spendItem(client, 1088001, 1)
                awardItem(client, "700031", 1)
                if getMetempsychosis(client) == 0 then

                    text(client, "Congratulations! You are promoted to Dragon Archer. I reward you an Rainbow Gem.")
                    link(client, "Thanks.", 255)
                    pic(client, 10)
                    create(client)

                else

                    awardItem(client, "133377 0 0 0 255 0 0 0 0 0 0", 1)
                    text(client, "You are Dragon Archer. Because you have been reborn, I will give you a RainbowGem and a unique socketed equipment.")
                    link(client, "Thanks.", 255)
                    pic(client, 10)
                    create(client)

                end

            else

                if (getItemsCount(client) <= 39) then

                    setProfession(client, 44)
                    spendItem(client, 1088001, 1)
                    awardItem(client, "700031", 1)
                    if getMetempsychosis(client) == 0 then

                        text(client, "Congratulations! You are promoted to Dragon Archer. I reward you an Rainbow Gem.")
                        link(client, "Thanks.", 255)
                        pic(client, 10)
                        create(client)

                    else

                        awardItem(client, "133377 0 0 0 255 0 0 0 0 0 0", 1)
                        text(client, "You are Dragon Archer. Because you have been reborn, I will give you a RainbowGem and a unique socketed equipment.")
                        link(client, "Thanks.", 255)
                        pic(client, 10)
                        create(client)

                    end

                else

                    text(client, "You have got reborn. I will send you a gift. Please prepare a slot in your inventory for that.")
                    link(client, "Ok. Wait a minute.", 255)
                    pic(client, 10)
                    create(client)

                end

            end

        end

    elseif (idx == 14) then

        if spendTaskItem(client, "MoonBox") then

            setProfession(client, 45)
            awardItem(client, "1088000", 1)

        else

            text(client, "Sorry, you do not have Moon Box. You may obtain one from Eight Diagram Tactics quest. Come to get promoted after you get one.")
            link(client, "I see.", 255)
            pic(client, 10)
            create(client)

        end

    elseif (idx == 15) then

        if getLevel(client) < 70 then

            text(client, "Sorry, you cannot get promoted before you reach level 70. Please train harder.")
            link(client, "I see.", 255)
            pic(client, 10)
            create(client)

        else

            if getForce(client) < 45 then

                text(client, "Sorry, only Eagle Archer can be promoted to Tiger Archer. Strength 45 and agility 150 are required. Please train harder.")
                link(client, "I see.", 255)
                pic(client, 10)
                create(client)

            else

                if getDexterity(client) < 150 then

                    text(client, "Sorry, only Eagle Archer can be promoted to Tiger Archer. Strength 45 and agility 150 are required. Please train harder.")
                    link(client, "I see.", 255)
                    pic(client, 10)
                    create(client)

                else

                    if getProfession(client) == 42 then

                        if spendItem(client, 1080001, 1) then

                            setProfession(client, 43)
                            if getMetempsychosis(client) == 1 then

                                text(client, "Congratulations! You have advanced to Tiger Archer. After rebirth, your stamina will counteract 30%  damage.")
                                link(client, "Thanks.", 255)
                                pic(client, 10)
                                create(client)

                            else

                                text(client, "Congrats! You are promoted to Tiger Archer. You may come to get promoted after you reach level 100.")
                                link(client, "Thanks.", 255)
                                pic(client, 10)
                                create(client)

                            end

                        else

                            text(client, "Sorry, an emerald is required. Hill Monsters may drop emeralds. Please come to get promoted after you have one.")
                            link(client, "I see.", 255)
                            pic(client, 10)
                            create(client)

                        end

                    else

                        text(client, "Sorry, only Eagle Archer can be promoted to Tiger Archer. Strength 45 and agility 150 are required. Please train harder.")
                        link(client, "I see.", 255)
                        pic(client, 10)
                        create(client)

                    end

                end

            end

        end

    end

end
