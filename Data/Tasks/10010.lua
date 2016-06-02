--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:41 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10010(client, idx)
    name = "KnowItAll"
    face = 1

    if (idx == 0) then

        if getProfession(client) == 100 then

            if hasMagic(client, 1000, -1) then

                if getLevel(client) < 15 then

                    text(client, "This is the way to Twin City. Many people are gathering there. Shall I give you some advice before I teleport you there.")
                    link(client, "Yes, please.", 1)
                    link(client, "Teleport me to Twin City.", 2)
                    link(client, "Consult others.", 255)
                    pic(client, 6)
                    create(client)

                else

                    text(client, "Welcome back! Is everything going on well with you? My advice must have been of great help to you.")
                    link(client, "Teleport me to Twin City.", 2)
                    pic(client, 6)
                    create(client)

                end

            else

                text(client, "The circle is very dangerous. You had better ask Taoist Star to teach you elementary spells before you leave Birth Village.")
                link(client, "I see. Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        else

            if getProfession(client) == 10 then

                if checkUserTask(client, 1) then

                    if getLevel(client) < 15 then

                        text(client, "This is the way to Twin City. Many people are gathering there. Shall I give you some advice before I teleport you there.")
                        link(client, "Yes, please.", 1)
                        link(client, "Teleport me to Twin City.", 2)
                        link(client, "Consult others.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        text(client, "Welcome back! Is everything going on well with you? My advice must have been of great help to you.")
                        link(client, "Teleport me to Twin City.", 2)
                        pic(client, 6)
                        create(client)

                    end

                else

                    text(client, "Firstly you`d better visit OldGeneralYang nearby who is a warmhearted man and is ready to help.")
                    link(client, "OK, I`m going.", 255)
                    pic(client, 6)
                    create(client)

                end

            else

                if getProfession(client) == 20 then

                    if checkUserTask(client, 1) then

                        if getLevel(client) < 15 then

                            text(client, "This is the way to Twin City. Many people are gathering there. Shall I give you some advice before I teleport you there.")
                            link(client, "Yes, please.", 1)
                            link(client, "Teleport me to Twin City.", 2)
                            link(client, "Consult others.", 255)
                            pic(client, 6)
                            create(client)

                        else

                            text(client, "Welcome back! Is everything going on well with you? My advice must have been of great help to you.")
                            link(client, "Teleport me to Twin City.", 2)
                            pic(client, 6)
                            create(client)

                        end

                    else

                        text(client, "Firstly you`d better visit OldGeneralYang nearby who is a warmhearted man and is ready to help.")
                        link(client, "OK, I`m going.", 255)
                        pic(client, 6)
                        create(client)

                    end

                else

                    if getLevel(client) < 15 then

                        text(client, "This is the way to Twin City. Many people are gathering there. Shall I give you some advice before I teleport you there.")
                        link(client, "Yes, please.", 1)
                        link(client, "Teleport me to Twin City.", 2)
                        link(client, "Consult others.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        text(client, "Welcome back! Is everything going on well with you? My advice must have been of great help to you.")
                        link(client, "Teleport me to Twin City.", 2)
                        pic(client, 6)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 1) then

        text(client, "Make good use of Hot Key (F1-F10) will save you a lot of troubles. You can drag potions and spells to F1-F10. Then")
        text(client, "you just press on the corresponding key on your keyboard to use it. You like PK? Be careful. If you get blue even")
        text(client, "black name after you kill other players, you will lose the equipments you are wearing. Remember to switch PK to Peace.")
        text(client, "That is all. Have a good journey.")
        link(client, "Thanks.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 2) then

        if getProfession(client) == 10 then

            awardItem(client, "410301 0 0 0 0 0 0 0 0 0 0", 1)
            action = randomAction(client, 1, 7)
            if action == 1 then
                awardItem(client, "132304 0 0 0 0 0 0 0 0 0 0", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                clearUserTask(client, 1)
                move(client, 1002, 440, 390)
                text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)
            elseif action == 2 then
                awardItem(client, "132404 0 0 0 0 0 0 0 0 0 0", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                clearUserTask(client, 1)
                move(client, 1002, 440, 390)
                text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)
            elseif action == 3 then
                awardItem(client, "132504 0 0 0 0 0 0 0 0 0 0", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                clearUserTask(client, 1)
                move(client, 1002, 440, 390)
                text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)
            elseif action == 4 then
                awardItem(client, "132604 0 0 0 0 0 0 0 0 0 0", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                clearUserTask(client, 1)
                move(client, 1002, 440, 390)
                text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)
            elseif action == 5 then
                awardItem(client, "132704 0 0 0 0 0 0 0 0 0 0", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                clearUserTask(client, 1)
                move(client, 1002, 440, 390)
                text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)
            elseif action == 6 then
                awardItem(client, "132804 0 0 0 0 0 0 0 0 0 0", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                clearUserTask(client, 1)
                move(client, 1002, 440, 390)
                text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)
            elseif action == 7 then
                awardItem(client, "132904 0 0 0 0 0 0 0 0 0 0", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                awardItem(client, "1000000", 1)
                clearUserTask(client, 1)
                move(client, 1002, 440, 390)
                text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)
            end


        else

            if getProfession(client) == 20 then

                awardItem(client, "410301 0 0 0 0 0 0 0 0 0 0", 1)
                action = randomAction(client, 1, 7)
                if action == 1 then
                    awardItem(client, "132304 0 0 0 0 0 0 0 0 0 0", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    clearUserTask(client, 1)
                    move(client, 1002, 440, 390)
                    text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)
                elseif action == 2 then
                    awardItem(client, "132404 0 0 0 0 0 0 0 0 0 0", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    clearUserTask(client, 1)
                    move(client, 1002, 440, 390)
                    text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)
                elseif action == 3 then
                    awardItem(client, "132504 0 0 0 0 0 0 0 0 0 0", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    clearUserTask(client, 1)
                    move(client, 1002, 440, 390)
                    text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)
                elseif action == 4 then
                    awardItem(client, "132604 0 0 0 0 0 0 0 0 0 0", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    clearUserTask(client, 1)
                    move(client, 1002, 440, 390)
                    text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)
                elseif action == 5 then
                    awardItem(client, "132704 0 0 0 0 0 0 0 0 0 0", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    clearUserTask(client, 1)
                    move(client, 1002, 440, 390)
                    text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)
                elseif action == 6 then
                    awardItem(client, "132804 0 0 0 0 0 0 0 0 0 0", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    clearUserTask(client, 1)
                    move(client, 1002, 440, 390)
                    text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)
                elseif action == 7 then
                    awardItem(client, "132904 0 0 0 0 0 0 0 0 0 0", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    awardItem(client, "1000000", 1)
                    clearUserTask(client, 1)
                    move(client, 1002, 440, 390)
                    text(client, "I will give you some weapons and drugs. Don`t forget to use them. Enjoy Conquer online!")
                    link(client, "Thanks.", 255)
                    pic(client, 6)
                    create(client)
                end


            else

                if getProfession(client) == 40 then

                    awardItem(client, "500301 0 0 0 0 0 0 0 0 0 0", 1)
                    action = randomAction(client, 1, 7)
                    if action == 1 then
                        awardItem(client, "132304 0 0 0 0 0 0 0 0 0 0", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        move(client, 1002, 440, 390)
                        text(client, "I will give you some weapons and drugs. Remember that WoodArrow can`t be used until you arm it. Enjoy Conquer online!")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)
                    elseif action == 2 then
                        awardItem(client, "132404 0 0 0 0 0 0 0 0 0 0", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        move(client, 1002, 440, 390)
                        text(client, "I will give you some weapons and drugs. Remember that WoodArrow can`t be used until you arm it. Enjoy Conquer online!")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)
                    elseif action == 3 then
                        awardItem(client, "132504 0 0 0 0 0 0 0 0 0 0", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        move(client, 1002, 440, 390)
                        text(client, "I will give you some weapons and drugs. Remember that WoodArrow can`t be used until you arm it. Enjoy Conquer online!")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)
                    elseif action == 4 then
                        awardItem(client, "132604 0 0 0 0 0 0 0 0 0 0", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        move(client, 1002, 440, 390)
                        text(client, "I will give you some weapons and drugs. Remember that WoodArrow can`t be used until you arm it. Enjoy Conquer online!")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)
                    elseif action == 5 then
                        awardItem(client, "132704 0 0 0 0 0 0 0 0 0 0", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        move(client, 1002, 440, 390)
                        text(client, "I will give you some weapons and drugs. Remember that WoodArrow can`t be used until you arm it. Enjoy Conquer online!")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)
                    elseif action == 6 then
                        awardItem(client, "132804 0 0 0 0 0 0 0 0 0 0", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        move(client, 1002, 440, 390)
                        text(client, "I will give you some weapons and drugs. Remember that WoodArrow can`t be used until you arm it. Enjoy Conquer online!")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)
                    elseif action == 7 then
                        awardItem(client, "132904 0 0 0 0 0 0 0 0 0 0", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1000000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        awardItem(client, "1050000", 1)
                        move(client, 1002, 440, 390)
                        text(client, "I will give you some weapons and drugs. Remember that WoodArrow can`t be used until you arm it. Enjoy Conquer online!")
                        link(client, "Thanks.", 255)
                        pic(client, 6)
                        create(client)
                    end


                else

                    if getProfession(client) == 100 then

                        awardItem(client, "421301 0 0 0 0 0 0 0 0 0 0", 1)
                        action = randomAction(client, 1, 7)
                        if action == 1 then
                            awardItem(client, "132304 0 0 0 0 0 0 0 0 0 0", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            move(client, 1002, 440, 390)
                            text(client, "I will give you some weapons and drugs. Remember to click on the Skill button to select a spell to cast. You can")
                            text(client, "also drag the skill icon to the hot key F1-F10 to use it expediently. Enjoy Conquer online!")
                            link(client, "Thanks a lot!", 255)
                            pic(client, 6)
                            create(client)
                        elseif action == 2 then
                            awardItem(client, "132404 0 0 0 0 0 0 0 0 0 0", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            move(client, 1002, 440, 390)
                            text(client, "I will give you some weapons and drugs. Remember to click on the Skill button to select a spell to cast. You can")
                            text(client, "also drag the skill icon to the hot key F1-F10 to use it expediently. Enjoy Conquer online!")
                            link(client, "Thanks a lot!", 255)
                            pic(client, 6)
                            create(client)
                        elseif action == 3 then
                            awardItem(client, "132504 0 0 0 0 0 0 0 0 0 0", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            move(client, 1002, 440, 390)
                            text(client, "I will give you some weapons and drugs. Remember to click on the Skill button to select a spell to cast. You can")
                            text(client, "also drag the skill icon to the hot key F1-F10 to use it expediently. Enjoy Conquer online!")
                            link(client, "Thanks a lot!", 255)
                            pic(client, 6)
                            create(client)
                        elseif action == 4 then
                            awardItem(client, "132604 0 0 0 0 0 0 0 0 0 0", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            move(client, 1002, 440, 390)
                            text(client, "I will give you some weapons and drugs. Remember to click on the Skill button to select a spell to cast. You can")
                            text(client, "also drag the skill icon to the hot key F1-F10 to use it expediently. Enjoy Conquer online!")
                            link(client, "Thanks a lot!", 255)
                            pic(client, 6)
                            create(client)
                        elseif action == 5 then
                            awardItem(client, "132704 0 0 0 0 0 0 0 0 0 0", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            move(client, 1002, 440, 390)
                            text(client, "I will give you some weapons and drugs. Remember to click on the Skill button to select a spell to cast. You can")
                            text(client, "also drag the skill icon to the hot key F1-F10 to use it expediently. Enjoy Conquer online!")
                            link(client, "Thanks a lot!", 255)
                            pic(client, 6)
                            create(client)
                        elseif action == 6 then
                            awardItem(client, "132804 0 0 0 0 0 0 0 0 0 0", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            move(client, 1002, 440, 390)
                            text(client, "I will give you some weapons and drugs. Remember to click on the Skill button to select a spell to cast. You can")
                            text(client, "also drag the skill icon to the hot key F1-F10 to use it expediently. Enjoy Conquer online!")
                            link(client, "Thanks a lot!", 255)
                            pic(client, 6)
                            create(client)
                        elseif action == 7 then
                            awardItem(client, "132904 0 0 0 0 0 0 0 0 0 0", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1000000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            awardItem(client, "1001000", 1)
                            move(client, 1002, 440, 390)
                            text(client, "I will give you some weapons and drugs. Remember to click on the Skill button to select a spell to cast. You can")
                            text(client, "also drag the skill icon to the hot key F1-F10 to use it expediently. Enjoy Conquer online!")
                            link(client, "Thanks a lot!", 255)
                            pic(client, 6)
                            create(client)
                        end

                    end

                end

            end

        end

    end

end
