--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:12 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask1060(client, idx)
    name = "Guide"
    face = 1

    if (idx == 0) then

        text(client, "Hi, are you new to Conquer Online? What can I do for you?")
        link(client, "Where am I?", 1)
        pic(client, 6)
        create(client)

    elseif (idx == 1) then

        text(client, "This is the way to Birth Village, where people can teach you some basic knowledge of the game and your profession.")
        text(client, "And they may also teach you useful spells.")
        link(client, "How to go to the village?", 2)
        link(client, "I want to skip the Birth Village and start playing.", 3)
        pic(client, 6)
        create(client)

    elseif (idx == 2) then

        text(client, "Follow the stone path all the way to the right. You can switch to run by click on the Walk/Run button on the left")
        text(client, "bottom of this screen. You can also jump by pressing Ctrl on you keyboard. I can give you a ride if you like.")
        link(client, "Cool. Please send me to the village.", 4)
        link(client, "Thanks. I`d rather go by myself.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 3) then

        if getProfession(client) == 100 then

            text(client, "You`re a Taoist! I`ll recommend you to TaoistStar. He can teach you some basic spells.")
            link(client, "Thanks a lot.", 4)
            pic(client, 6)
            create(client)

        else

            if awardItem(client, "410301", 1) then

                if (rand(client, 100) < 50) then

                    if awardItem(client, "132305", 1) then

                        if awardItem(client, "1000000", 1) then

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        else

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        end

                    else

                        if awardItem(client, "1000000", 1) then

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        else

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        end

                    end

                else

                    if awardItem(client, "132405", 1) then

                        if awardItem(client, "1000000", 1) then

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        else

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        end

                    else

                        if awardItem(client, "1000000", 1) then

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        else

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        end

                    end

                end

            else

                if (rand(client, 100) < 50) then

                    if awardItem(client, "132305", 1) then

                        if awardItem(client, "1000000", 1) then

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        else

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        end

                    else

                        if awardItem(client, "1000000", 1) then

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        else

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        end

                    end

                else

                    if awardItem(client, "132405", 1) then

                        if awardItem(client, "1000000", 1) then

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        else

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        end

                    else

                        if awardItem(client, "1000000", 1) then

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        else

                            if awardItem(client, "1000000", 1) then

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            else

                                if awardItem(client, "1000000", 1) then

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                else

                                    move(client, 1002, 440, 390)
                                    text(client, "I give you a Coat, Wooden Sword and some healing potions. Wish you a pleasant journey.")
                                    link(client, "I see. Thanks.", 255)
                                    pic(client, 6)
                                    create(client)

                                end

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 4) then

        move(client, 1010, 83, 59)
        text(client, "Here it is. You see the flickering PK in you lower screen? It means you are in PK mode. Switch it to Peace. By the")
        text(client, "way, KnowItAll can send you to Twin City where you can start playing. And he will also give you some starting gears and money.")
        link(client, "Thanks a lot.", 255)
        pic(client, 6)
        create(client)

    end

end
