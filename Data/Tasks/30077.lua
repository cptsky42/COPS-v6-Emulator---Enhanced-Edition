--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:47 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30077(client, idx)
    name = "KindTaoist"
    face = 1

    if (idx == 0) then

        text(client, "You are great to make it finally. I will send you a gift. Hope you like it.")
        link(client, "Thanks a lot.", 1)
        pic(client, 6)
        create(client)

    elseif (idx == 1) then

        if (rand(client, 1000) < 1) then

            if awardItem(client, "150016 1698 1698 0 255 0 0 0 0 0 0", 1) then

                move(client, 1020, 566, 565)
                action = randomAction(client, 1, 8)
                if action == 1 then
                    moveNpc(client, 30077, 1041, 301, 151)
                elseif action == 2 then
                    moveNpc(client, 30077, 1041, 301, 151)
                elseif action == 3 then
                    moveNpc(client, 30077, 1041, 266, 126)
                elseif action == 4 then
                    moveNpc(client, 30077, 1041, 266, 126)
                elseif action == 5 then
                    moveNpc(client, 30077, 1041, 309, 126)
                elseif action == 6 then
                    moveNpc(client, 30077, 1041, 309, 126)
                elseif action == 7 then
                    moveNpc(client, 30077, 1041, 293, 92)
                elseif action == 8 then
                    moveNpc(client, 30077, 1041, 253, 79)
                end


            else

                move(client, 1020, 566, 565)
                action = randomAction(client, 1, 8)
                if action == 1 then
                    moveNpc(client, 30077, 1041, 301, 151)
                elseif action == 2 then
                    moveNpc(client, 30077, 1041, 301, 151)
                elseif action == 3 then
                    moveNpc(client, 30077, 1041, 266, 126)
                elseif action == 4 then
                    moveNpc(client, 30077, 1041, 266, 126)
                elseif action == 5 then
                    moveNpc(client, 30077, 1041, 309, 126)
                elseif action == 6 then
                    moveNpc(client, 30077, 1041, 309, 126)
                elseif action == 7 then
                    moveNpc(client, 30077, 1041, 293, 92)
                elseif action == 8 then
                    moveNpc(client, 30077, 1041, 253, 79)
                end


            end

        else

            if (rand(client, 1000) < 1) then

                if awardItem(client, "1088000", 1) then


                else

                    move(client, 1020, 566, 565)
                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        moveNpc(client, 30077, 1041, 301, 151)
                    elseif action == 2 then
                        moveNpc(client, 30077, 1041, 301, 151)
                    elseif action == 3 then
                        moveNpc(client, 30077, 1041, 266, 126)
                    elseif action == 4 then
                        moveNpc(client, 30077, 1041, 266, 126)
                    elseif action == 5 then
                        moveNpc(client, 30077, 1041, 309, 126)
                    elseif action == 6 then
                        moveNpc(client, 30077, 1041, 309, 126)
                    elseif action == 7 then
                        moveNpc(client, 30077, 1041, 293, 92)
                    elseif action == 8 then
                        moveNpc(client, 30077, 1041, 253, 79)
                    end


                end

            else

                if (rand(client, 100) < 30) then

                    if awardItem(client, "1088001", 1) then

                        if awardItem(client, "1088001", 1) then

                            if awardItem(client, "1088001", 1) then

                                move(client, 1020, 566, 565)
                                action = randomAction(client, 1, 8)
                                if action == 1 then
                                    moveNpc(client, 30077, 1041, 301, 151)
                                elseif action == 2 then
                                    moveNpc(client, 30077, 1041, 301, 151)
                                elseif action == 3 then
                                    moveNpc(client, 30077, 1041, 266, 126)
                                elseif action == 4 then
                                    moveNpc(client, 30077, 1041, 266, 126)
                                elseif action == 5 then
                                    moveNpc(client, 30077, 1041, 309, 126)
                                elseif action == 6 then
                                    moveNpc(client, 30077, 1041, 309, 126)
                                elseif action == 7 then
                                    moveNpc(client, 30077, 1041, 293, 92)
                                elseif action == 8 then
                                    moveNpc(client, 30077, 1041, 253, 79)
                                end


                            else

                                move(client, 1020, 566, 565)
                                action = randomAction(client, 1, 8)
                                if action == 1 then
                                    moveNpc(client, 30077, 1041, 301, 151)
                                elseif action == 2 then
                                    moveNpc(client, 30077, 1041, 301, 151)
                                elseif action == 3 then
                                    moveNpc(client, 30077, 1041, 266, 126)
                                elseif action == 4 then
                                    moveNpc(client, 30077, 1041, 266, 126)
                                elseif action == 5 then
                                    moveNpc(client, 30077, 1041, 309, 126)
                                elseif action == 6 then
                                    moveNpc(client, 30077, 1041, 309, 126)
                                elseif action == 7 then
                                    moveNpc(client, 30077, 1041, 293, 92)
                                elseif action == 8 then
                                    moveNpc(client, 30077, 1041, 253, 79)
                                end


                            end

                        else

                            move(client, 1020, 566, 565)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                moveNpc(client, 30077, 1041, 301, 151)
                            elseif action == 2 then
                                moveNpc(client, 30077, 1041, 301, 151)
                            elseif action == 3 then
                                moveNpc(client, 30077, 1041, 266, 126)
                            elseif action == 4 then
                                moveNpc(client, 30077, 1041, 266, 126)
                            elseif action == 5 then
                                moveNpc(client, 30077, 1041, 309, 126)
                            elseif action == 6 then
                                moveNpc(client, 30077, 1041, 309, 126)
                            elseif action == 7 then
                                moveNpc(client, 30077, 1041, 293, 92)
                            elseif action == 8 then
                                moveNpc(client, 30077, 1041, 253, 79)
                            end


                        end

                    else

                        move(client, 1020, 566, 565)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 2 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 3 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 4 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 5 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 6 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 7 then
                            moveNpc(client, 30077, 1041, 293, 92)
                        elseif action == 8 then
                            moveNpc(client, 30077, 1041, 253, 79)
                        end


                    end

                else

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        gainMoney(client, 1000)
                        move(client, 1020, 566, 565)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 2 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 3 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 4 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 5 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 6 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 7 then
                            moveNpc(client, 30077, 1041, 293, 92)
                        elseif action == 8 then
                            moveNpc(client, 30077, 1041, 253, 79)
                        end

                    elseif action == 2 then
                        gainMoney(client, 3000)
                        move(client, 1020, 566, 565)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 2 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 3 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 4 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 5 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 6 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 7 then
                            moveNpc(client, 30077, 1041, 293, 92)
                        elseif action == 8 then
                            moveNpc(client, 30077, 1041, 253, 79)
                        end

                    elseif action == 3 then
                        if awardItem(client, "121066", 1) then

                            move(client, 1020, 566, 565)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                moveNpc(client, 30077, 1041, 301, 151)
                            elseif action == 2 then
                                moveNpc(client, 30077, 1041, 301, 151)
                            elseif action == 3 then
                                moveNpc(client, 30077, 1041, 266, 126)
                            elseif action == 4 then
                                moveNpc(client, 30077, 1041, 266, 126)
                            elseif action == 5 then
                                moveNpc(client, 30077, 1041, 309, 126)
                            elseif action == 6 then
                                moveNpc(client, 30077, 1041, 309, 126)
                            elseif action == 7 then
                                moveNpc(client, 30077, 1041, 293, 92)
                            elseif action == 8 then
                                moveNpc(client, 30077, 1041, 253, 79)
                            end


                        else

                            move(client, 1020, 566, 565)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                moveNpc(client, 30077, 1041, 301, 151)
                            elseif action == 2 then
                                moveNpc(client, 30077, 1041, 301, 151)
                            elseif action == 3 then
                                moveNpc(client, 30077, 1041, 266, 126)
                            elseif action == 4 then
                                moveNpc(client, 30077, 1041, 266, 126)
                            elseif action == 5 then
                                moveNpc(client, 30077, 1041, 309, 126)
                            elseif action == 6 then
                                moveNpc(client, 30077, 1041, 309, 126)
                            elseif action == 7 then
                                moveNpc(client, 30077, 1041, 293, 92)
                            elseif action == 8 then
                                moveNpc(client, 30077, 1041, 253, 79)
                            end


                        end
                    elseif action == 4 then
                        if awardItem(client, "121066", 1) then

                            move(client, 1020, 566, 565)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                moveNpc(client, 30077, 1041, 301, 151)
                            elseif action == 2 then
                                moveNpc(client, 30077, 1041, 301, 151)
                            elseif action == 3 then
                                moveNpc(client, 30077, 1041, 266, 126)
                            elseif action == 4 then
                                moveNpc(client, 30077, 1041, 266, 126)
                            elseif action == 5 then
                                moveNpc(client, 30077, 1041, 309, 126)
                            elseif action == 6 then
                                moveNpc(client, 30077, 1041, 309, 126)
                            elseif action == 7 then
                                moveNpc(client, 30077, 1041, 293, 92)
                            elseif action == 8 then
                                moveNpc(client, 30077, 1041, 253, 79)
                            end


                        else

                            move(client, 1020, 566, 565)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                moveNpc(client, 30077, 1041, 301, 151)
                            elseif action == 2 then
                                moveNpc(client, 30077, 1041, 301, 151)
                            elseif action == 3 then
                                moveNpc(client, 30077, 1041, 266, 126)
                            elseif action == 4 then
                                moveNpc(client, 30077, 1041, 266, 126)
                            elseif action == 5 then
                                moveNpc(client, 30077, 1041, 309, 126)
                            elseif action == 6 then
                                moveNpc(client, 30077, 1041, 309, 126)
                            elseif action == 7 then
                                moveNpc(client, 30077, 1041, 293, 92)
                            elseif action == 8 then
                                moveNpc(client, 30077, 1041, 253, 79)
                            end


                        end
                    elseif action == 5 then
                        gainMoney(client, 5000)
                        move(client, 1020, 566, 565)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 2 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 3 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 4 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 5 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 6 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 7 then
                            moveNpc(client, 30077, 1041, 293, 92)
                        elseif action == 8 then
                            moveNpc(client, 30077, 1041, 253, 79)
                        end

                    elseif action == 6 then
                        gainMoney(client, 8000)
                        move(client, 1020, 566, 565)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 2 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 3 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 4 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 5 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 6 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 7 then
                            moveNpc(client, 30077, 1041, 293, 92)
                        elseif action == 8 then
                            moveNpc(client, 30077, 1041, 253, 79)
                        end

                    elseif action == 7 then
                        gainMoney(client, 1000)
                        move(client, 1020, 566, 565)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 2 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 3 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 4 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 5 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 6 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 7 then
                            moveNpc(client, 30077, 1041, 293, 92)
                        elseif action == 8 then
                            moveNpc(client, 30077, 1041, 253, 79)
                        end

                    elseif action == 8 then
                        gainMoney(client, 10000)
                        move(client, 1020, 566, 565)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 2 then
                            moveNpc(client, 30077, 1041, 301, 151)
                        elseif action == 3 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 4 then
                            moveNpc(client, 30077, 1041, 266, 126)
                        elseif action == 5 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 6 then
                            moveNpc(client, 30077, 1041, 309, 126)
                        elseif action == 7 then
                            moveNpc(client, 30077, 1041, 293, 92)
                        elseif action == 8 then
                            moveNpc(client, 30077, 1041, 253, 79)
                        end

                    end


                end

            end

        end

    end

end
