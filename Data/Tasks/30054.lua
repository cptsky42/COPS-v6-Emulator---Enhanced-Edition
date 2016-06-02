--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:45 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30054(client, idx)
    name = "GodCloud"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "SkyPrizeToken") and (getMoney(client) >= 0) then

            text(client, "Well done! You have passed the 5 tough floors in one time. You have showed your great competence and perseverance.")
            link(client, "Thanks.", 1)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "SkyPrizeToken") and (getMoney(client) >= 0) then

            text(client, "There are two Treasure Boxes here, 4 Meteors in the first box, 10 Meteors or some money in the second one. Which do you like?")
            link(client, "Treasure Box 1", 2)
            link(client, "Treasure Box 2", 3)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 2) then

        if hasTaskItem(client, "SkyPrizeToken") and (getMoney(client) >= 0) then

            spendItem(client, 721109, 1)
            awardItem(client, "1088001", 1)
            if awardItem(client, "1088001", 1) then

                if awardItem(client, "1088001", 1) then

                    if awardItem(client, "1088001", 1) then

                        move(client, 1002, 430, 380)

                    else

                        move(client, 1002, 430, 380)

                    end

                else

                    move(client, 1002, 430, 380)

                end

            else

                move(client, 1002, 430, 380)

            end

        end

    elseif (idx == 3) then

        if hasTaskItem(client, "SkyPrizeToken") and (getMoney(client) >= 0) then

            if (rand(client, 100) < 20) then

                spendItem(client, 721109, 1)
                if awardItem(client, "1088001", 1) then

                    if awardItem(client, "1088001", 1) then

                        if awardItem(client, "1088001", 1) then

                            if awardItem(client, "1088001", 1) then

                                if awardItem(client, "1088001", 1) then

                                    if awardItem(client, "1088001", 1) then

                                        if awardItem(client, "1088001", 1) then

                                            if awardItem(client, "1088001", 1) then

                                                if awardItem(client, "1088001", 1) then

                                                    if awardItem(client, "1088001", 1) then

                                                        move(client, 1002, 430, 380)

                                                    else

                                                        move(client, 1002, 430, 380)

                                                    end

                                                else

                                                    move(client, 1002, 430, 380)

                                                end

                                            else

                                                move(client, 1002, 430, 380)

                                            end

                                        else

                                            move(client, 1002, 430, 380)

                                        end

                                    else

                                        move(client, 1002, 430, 380)

                                    end

                                else

                                    move(client, 1002, 430, 380)

                                end

                            else

                                move(client, 1002, 430, 380)

                            end

                        else

                            move(client, 1002, 430, 380)

                        end

                    else

                        move(client, 1002, 430, 380)

                    end

                else

                    move(client, 1002, 430, 380)

                end

            else

                action = randomAction(client, 1, 8)
                if action == 1 then
                    spendItem(client, 721109, 1)
                    gainMoney(client, 1000)
                    move(client, 1002, 430, 380)
                elseif action == 2 then
                    spendItem(client, 721109, 1)
                    gainMoney(client, 1000)
                    move(client, 1002, 430, 380)
                elseif action == 3 then
                    spendItem(client, 721109, 1)
                    awardItem(client, "1088001", 1)
                    if awardItem(client, "1088001", 1) then

                        if awardItem(client, "1088001", 1) then

                            if awardItem(client, "1088001", 1) then

                                move(client, 1002, 430, 380)

                            else

                                move(client, 1002, 430, 380)

                            end

                        else

                            move(client, 1002, 430, 380)

                        end

                    else

                        move(client, 1002, 430, 380)

                    end
                elseif action == 4 then
                    spendItem(client, 721109, 1)
                    awardItem(client, "1088001", 1)
                    if awardItem(client, "1088001", 1) then

                        if awardItem(client, "1088001", 1) then

                            if awardItem(client, "1088001", 1) then

                                move(client, 1002, 430, 380)

                            else

                                move(client, 1002, 430, 380)

                            end

                        else

                            move(client, 1002, 430, 380)

                        end

                    else

                        move(client, 1002, 430, 380)

                    end
                elseif action == 5 then
                    spendItem(client, 721109, 1)
                    awardItem(client, "1088001", 1)
                    if awardItem(client, "1088001", 1) then

                        if awardItem(client, "1088001", 1) then

                            if awardItem(client, "1088001", 1) then

                                move(client, 1002, 430, 380)

                            else

                                move(client, 1002, 430, 380)

                            end

                        else

                            move(client, 1002, 430, 380)

                        end

                    else

                        move(client, 1002, 430, 380)

                    end
                elseif action == 6 then
                    spendItem(client, 721109, 1)
                    awardItem(client, "1088001", 1)
                    if awardItem(client, "1088001", 1) then

                        if awardItem(client, "1088001", 1) then

                            if awardItem(client, "1088001", 1) then

                                move(client, 1002, 430, 380)

                            else

                                move(client, 1002, 430, 380)

                            end

                        else

                            move(client, 1002, 430, 380)

                        end

                    else

                        move(client, 1002, 430, 380)

                    end
                elseif action == 7 then
                    spendItem(client, 721109, 1)
                    gainMoney(client, 10000)
                    move(client, 1002, 430, 380)
                elseif action == 8 then
                    spendItem(client, 721109, 1)
                    gainMoney(client, 10000)
                    move(client, 1002, 430, 380)
                end

                spendItem(client, 721109, 1)
                awardItem(client, "1088001", 1)
                if awardItem(client, "1088001", 1) then

                    if awardItem(client, "1088001", 1) then

                        if awardItem(client, "1088001", 1) then

                            move(client, 1002, 430, 380)

                        else

                            move(client, 1002, 430, 380)

                        end

                    else

                        move(client, 1002, 430, 380)

                    end

                else

                    move(client, 1002, 430, 380)

                end

            end

        end

    end

end
