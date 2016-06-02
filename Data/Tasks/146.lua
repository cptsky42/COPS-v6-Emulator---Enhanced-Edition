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

function processTask146(client, idx)
    name = "GeneralWinner"
    face = 1

    if (idx == 0) then

        if checkTime(client, 2, "1 21:10 1 21:27") then

            if alivePlayersOnMap(client, 1518) == 1 then

                text(client, "Congratulations! You are the winner of the PK War. You will be awarded 2 million or a super gem. What do you prefer?")
                link(client, "2,000,000 silver.", 1)
                link(client, "Super Phoenix Gem", 2)
                link(client, "Super Dragon Gem", 3)
                pic(client, 7)
                create(client)

            else

                text(client, "When you defeat all the challengers in the arena, you can come to claim your prize within the tournament period.")
                link(client, "Oh, I see.", 255)
                link(client, "Please send me out.", 4)
                pic(client, 7)
                create(client)

            end

        else

            if checkTime(client, 3, "6 21:10 6 21:27") then

                if alivePlayersOnMap(client, 1518) == 1 then

                    text(client, "Congrats! You are the champion of the PK tournament today. What prize would you like, 500,000 silver or a dragon ball?")
                    link(client, "500,000 silver", 5)
                    link(client, "A Dragon Ball", 6)
                    pic(client, 7)
                    create(client)

                else

                    text(client, "When you defeat all the challengers in the arena, you can come to claim your prize within the tournament period.")
                    link(client, "Oh, I see.", 255)
                    link(client, "Please send me out.", 4)
                    pic(client, 7)
                    create(client)

                end

            else

                if checkTime(client, 2, "1 21:30 1 22:27") then

                    if alivePlayersOnMap(client, 1092) == 1 then

                        text(client, "Congratulations! You are the winner of the PK War. You will be awarded 2 million or a super gem. What do you prefer?")
                        link(client, "2,000,000 silver.", 1)
                        link(client, "Super Phoenix Gem", 2)
                        link(client, "Super Dragon Gem", 3)
                        pic(client, 7)
                        create(client)

                    else

                        text(client, "When you defeat all the challengers in the arena, you can come to claim your prize within the tournament period.")
                        link(client, "Oh, I see.", 255)
                        link(client, "Please send me out.", 4)
                        pic(client, 7)
                        create(client)

                    end

                else

                    if checkTime(client, 3, "6 21:30 6 22:27") then

                        if alivePlayersOnMap(client, 1092) == 1 then

                            text(client, "Congrats! You are the champion of the PK tournament today. What prize would you like, 500,000 silver or a dragon ball?")
                            link(client, "500,000 silver", 5)
                            link(client, "A Dragon Ball", 6)
                            pic(client, 7)
                            create(client)

                        else

                            text(client, "When you defeat all the challengers in the arena, you can come to claim your prize within the tournament period.")
                            link(client, "Oh, I see.", 255)
                            link(client, "Please send me out.", 4)
                            pic(client, 7)
                            create(client)

                        end

                    else

                        if checkTime(client, 2, "1 22:30 1 23:59") then

                            if alivePlayersOnMap(client, 1093) == 1 then

                                text(client, "Congratulations! You are the winner of the PK War. You will be awarded 2 million or a super gem. What do you prefer?")
                                link(client, "2,000,000 silver.", 1)
                                link(client, "Super Phoenix Gem", 2)
                                link(client, "Super Dragon Gem", 3)
                                pic(client, 7)
                                create(client)

                            else

                                text(client, "When you defeat all the challengers in the arena, you can come to claim your prize within the tournament period.")
                                link(client, "Oh, I see.", 255)
                                link(client, "Please send me out.", 4)
                                pic(client, 7)
                                create(client)

                            end

                        else

                            checkTime(client, 3, "6 22:30 6 23:59")
                            if alivePlayersOnMap(client, 1093) == 1 then

                                text(client, "Congrats! You are the champion of the PK tournament today. What prize would you like, 500,000 silver or a dragon ball?")
                                link(client, "500,000 silver", 5)
                                link(client, "A Dragon Ball", 6)
                                pic(client, 7)
                                create(client)

                            else

                                text(client, "When you defeat all the challengers in the arena, you can come to claim your prize within the tournament period.")
                                link(client, "Oh, I see.", 255)
                                link(client, "Please send me out.", 4)
                                pic(client, 7)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        gainMoney(client, 2000000)
        broadcastMapMsg(client, 1002, "Congratulations! " .. getName(client) .. " wins this month`s PK Tournament.", 2005)

        if move(client, 1002, 430, 380) then

            moveNpc(client, 146, 5000, 35, 78)

        else

            moveNpc(client, 146, 5000, 35, 78)

        end

    elseif (idx == 2) then

        awardItem(client, "700003", 1)
        broadcastMapMsg(client, 1002, "Congratulations! " .. getName(client) .. " wins this month`s PK Tournament.", 2005)

        if move(client, 1002, 430, 380) then

            moveNpc(client, 146, 5000, 35, 78)

        else

            moveNpc(client, 146, 5000, 35, 78)

        end

    elseif (idx == 3) then

        awardItem(client, "700013", 1)
        broadcastMapMsg(client, 1002, "Congratulations! " .. getName(client) .. " wins this month`s PK Tournament.", 2005)

        if move(client, 1002, 430, 380) then

            moveNpc(client, 146, 5000, 35, 78)

        else

            moveNpc(client, 146, 5000, 35, 78)

        end

    elseif (idx == 4) then

        text(client, "Are you sure to quit the PK war?")
        link(client, "Yes.", 7)
        pic(client, 7)
        create(client)

    elseif (idx == 5) then

        gainMoney(client, 500000)
        broadcastMapMsg(client, 1002, "Congratulations! " .. getName(client) .. " wins this week`s PK Tournament.", 2005)

        if move(client, 1002, 430, 380) then

            moveNpc(client, 146, 5000, 35, 78)

        else

            moveNpc(client, 146, 5000, 35, 78)

        end

    elseif (idx == 6) then

        awardItem(client, "1088000", 1)

    elseif (idx == 7) then

        move(client, 1002, 430, 380)

    end

end
