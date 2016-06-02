--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:43 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask20005(client, idx)
    name = "Celestine"
    face = 1

    if (idx == 0) then

        if hasItem(client, 700001, 1) then

            if hasItem(client, 700011, 1) then

                if hasItem(client, 700021, 1) then

                    if hasItem(client, 700031, 1) then

                        if hasItem(client, 700041, 1) then

                            if hasItem(client, 700051, 1) then

                                if hasItem(client, 700061, 1) then

                                    if hasItem(client, 721258, 1) then

                                        if spendItem(client, 700001, 1) then

                                            if spendItem(client, 700011, 1) then

                                                if spendItem(client, 700021, 1) then

                                                    if spendItem(client, 700031, 1) then

                                                        if spendItem(client, 700041, 1) then

                                                            if spendItem(client, 700051, 1) then

                                                                if spendItem(client, 700061, 1) then

                                                                    if spendItem(client, 721258, 1) then

                                                                        awardItem(client, "721259", 1)
                                                                        text(client, "The Celestial Stone is ready. Please take it to Eternity in SkyPass ,he will tell you more.")
                                                                        link(client, "Ok, Thank you.", 255)
                                                                        pic(client, 15)
                                                                        create(client)

                                                                    else

                                                                        text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                                        link(client, "I will get it ready.", 255)
                                                                        pic(client, 15)
                                                                        create(client)

                                                                    end

                                                                else

                                                                    text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                                    link(client, "I will get it ready.", 255)
                                                                    pic(client, 15)
                                                                    create(client)

                                                                end

                                                            else

                                                                text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                                link(client, "I will get it ready.", 255)
                                                                pic(client, 15)
                                                                create(client)

                                                            end

                                                        else

                                                            text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                            link(client, "I will get it ready.", 255)
                                                            pic(client, 15)
                                                            create(client)

                                                        end

                                                    else

                                                        text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                        link(client, "I will get it ready.", 255)
                                                        pic(client, 15)
                                                        create(client)

                                                    end

                                                else

                                                    text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                    link(client, "I will get it ready.", 255)
                                                    pic(client, 15)
                                                    create(client)

                                                end

                                            else

                                                text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                link(client, "I will get it ready.", 255)
                                                pic(client, 15)
                                                create(client)

                                            end

                                        else

                                            text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                            link(client, "I will get it ready.", 255)
                                            pic(client, 15)
                                            create(client)

                                        end

                                    else

                                        text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                        link(client, "I will get it ready.", 255)
                                        pic(client, 15)
                                        create(client)

                                    end

                                else

                                    text(client, "People are pursuing greater achievement during their lives, but none can make it due to the limit of human constitution.")
                                    link(client, "What does it mean?", 1)
                                    link(client, "I don`t believe it.", 255)
                                    pic(client, 15)
                                    create(client)

                                end

                            else

                                text(client, "People are pursuing greater achievement during their lives, but none can make it due to the limit of human constitution.")
                                link(client, "What does it mean?", 1)
                                link(client, "I don`t believe it.", 255)
                                pic(client, 15)
                                create(client)

                            end

                        else

                            text(client, "People are pursuing greater achievement during their lives, but none can make it due to the limit of human constitution.")
                            link(client, "What does it mean?", 1)
                            link(client, "I don`t believe it.", 255)
                            pic(client, 15)
                            create(client)

                        end

                    else

                        text(client, "People are pursuing greater achievement during their lives, but none can make it due to the limit of human constitution.")
                        link(client, "What does it mean?", 1)
                        link(client, "I don`t believe it.", 255)
                        pic(client, 15)
                        create(client)

                    end

                else

                    text(client, "People are pursuing greater achievement during their lives, but none can make it due to the limit of human constitution.")
                    link(client, "What does it mean?", 1)
                    link(client, "I don`t believe it.", 255)
                    pic(client, 15)
                    create(client)

                end

            else

                text(client, "People are pursuing greater achievement during their lives, but none can make it due to the limit of human constitution.")
                link(client, "What does it mean?", 1)
                link(client, "I don`t believe it.", 255)
                pic(client, 15)
                create(client)

            end

        else

            text(client, "People are pursuing greater achievement during their lives, but none can make it due to the limit of human constitution.")
            link(client, "What does it mean?", 1)
            link(client, "I don`t believe it.", 255)
            pic(client, 15)
            create(client)

        end

    elseif (idx == 1) then

        text(client, "Mortals are mundane. Only getting rid of it can help them accomplish their aims.")
        text(client, "If you are high level enough, you can get reborn to learn more and stronger skills.")
        link(client, "I am satisfied.", 255)
        link(client, "How to get reborn?", 2)
        pic(client, 15)
        create(client)

    elseif (idx == 2) then

        text(client, "It is difficult. First, you should reach certain level. Second, you need a Celestial Stone.")
        link(client, "How to get CelestialStone?", 3)
        link(client, "Forget it.", 255)
        pic(client, 15)
        create(client)

    elseif (idx == 3) then

        text(client, "CelestialStone is made of 7 gems: VioletGem, KylinGem, RainbowGem, MoonGem, PhoenixGem, FuryGem, DragonGem, and CleanWater.")
        link(client, "What is Clean Water?", 4)
        link(client, "It is difficult.", 255)
        pic(client, 15)
        create(client)

    elseif (idx == 4) then

        text(client, "It is used to get rid of your mundaneness, and then you won`t be affected by the environment.")
        text(client, "By the way, Clean Water comes from celestial rinsing.")
        link(client, "What are gems used for?", 5)
        pic(client, 15)
        create(client)

    elseif (idx == 5) then

        text(client, "Only the seven gems can protect you during the rebirth.")
        link(client, "I will collect them now.", 6)
        link(client, "I changed my mind.", 255)
        pic(client, 15)
        create(client)

    elseif (idx == 6) then

        text(client, "It is easy to get the gems. But CleanWater??")
        link(client, "But what?", 7)
        pic(client, 15)
        create(client)

    elseif (idx == 7) then

        text(client, "The Adventure island is the headstream of CleanWater. But it is occupied by WaterEvil and he uses spell to hide the stream.")
        link(client, "What can I do?", 8)
        link(client, "I will give up.", 255)
        pic(client, 15)
        create(client)

    elseif (idx == 8) then

        text(client, "WaterEvilElder will go to get the water every certain time. If you defeat him, you may get the water.")
        text(client, "But he is very hard to deal with.")
        link(client, "I see. Thank you.", 255)
        link(client, "Anything else?", 9)
        pic(client, 15)
        create(client)

    elseif (idx == 9) then

        if hasItem(client, 700001, 1) then

            if hasItem(client, 700011, 1) then

                if hasItem(client, 700021, 1) then

                    if hasItem(client, 700031, 1) then

                        if hasItem(client, 700041, 1) then

                            if hasItem(client, 700051, 1) then

                                if hasItem(client, 700061, 1) then

                                    if hasItem(client, 721258, 1) then

                                        if spendItem(client, 700001, 1) then

                                            if spendItem(client, 700011, 1) then

                                                if spendItem(client, 700021, 1) then

                                                    if spendItem(client, 700031, 1) then

                                                        if spendItem(client, 700041, 1) then

                                                            if spendItem(client, 700051, 1) then

                                                                if spendItem(client, 700061, 1) then

                                                                    if spendItem(client, 721258, 1) then

                                                                        awardItem(client, "721259", 1)
                                                                        text(client, "The Celestial Stone is ready. Please take it to Eternity in SkyPass ,he will tell you more.")
                                                                        link(client, "Ok, Thank you.", 255)
                                                                        pic(client, 15)
                                                                        create(client)

                                                                    else

                                                                        text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                                        link(client, "I will get it ready.", 255)
                                                                        pic(client, 15)
                                                                        create(client)

                                                                    end

                                                                else

                                                                    text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                                    link(client, "I will get it ready.", 255)
                                                                    pic(client, 15)
                                                                    create(client)

                                                                end

                                                            else

                                                                text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                                link(client, "I will get it ready.", 255)
                                                                pic(client, 15)
                                                                create(client)

                                                            end

                                                        else

                                                            text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                            link(client, "I will get it ready.", 255)
                                                            pic(client, 15)
                                                            create(client)

                                                        end

                                                    else

                                                        text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                        link(client, "I will get it ready.", 255)
                                                        pic(client, 15)
                                                        create(client)

                                                    end

                                                else

                                                    text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                    link(client, "I will get it ready.", 255)
                                                    pic(client, 15)
                                                    create(client)

                                                end

                                            else

                                                text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                                link(client, "I will get it ready.", 255)
                                                pic(client, 15)
                                                create(client)

                                            end

                                        else

                                            text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                            link(client, "I will get it ready.", 255)
                                            pic(client, 15)
                                            create(client)

                                        end

                                    else

                                        text(client, "Although you get seven normal gems, I cannot refine the CelestialStone for you without the CleanWater. ")
                                        link(client, "I will get it ready.", 255)
                                        pic(client, 15)
                                        create(client)

                                    end

                                else

                                    text(client, "Come here to refine Celestial Stone when you get the seven gems and Clean Water.")
                                    link(client, "That`s ok.", 255)
                                    pic(client, 15)
                                    create(client)

                                end

                            else

                                text(client, "Come here to refine Celestial Stone when you get the seven gems and Clean Water.")
                                link(client, "That`s ok.", 255)
                                pic(client, 15)
                                create(client)

                            end

                        else

                            text(client, "Come here to refine Celestial Stone when you get the seven gems and Clean Water.")
                            link(client, "That`s ok.", 255)
                            pic(client, 15)
                            create(client)

                        end

                    else

                        text(client, "Come here to refine Celestial Stone when you get the seven gems and Clean Water.")
                        link(client, "That`s ok.", 255)
                        pic(client, 15)
                        create(client)

                    end

                else

                    text(client, "Come here to refine Celestial Stone when you get the seven gems and Clean Water.")
                    link(client, "That`s ok.", 255)
                    pic(client, 15)
                    create(client)

                end

            else

                text(client, "Come here to refine Celestial Stone when you get the seven gems and Clean Water.")
                link(client, "That`s ok.", 255)
                pic(client, 15)
                create(client)

            end

        else

            text(client, "Come here to refine Celestial Stone when you get the seven gems and Clean Water.")
            link(client, "That`s ok.", 255)
            pic(client, 15)
            create(client)

        end

    end

end
