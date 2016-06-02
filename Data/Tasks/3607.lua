--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 4/1/2015 7:44:06 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask3607(client, idx)
    name = "FireSeal"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMetempsychosis(client) == 2 then

                sendSysMsg(client, "Sorry, you`re ineligible to use the FireSeal.", 2005)

            else

                if getMetempsychosis(client) == 0 then

                    sendSysMsg(client, "Sorry, you`re ineligible to use the FireSeal.", 2005)

                else

                    if getLevel(client) < 120 then

                        sendSysMsg(client, "Sorry, you`re ineligible to use the FireSeal.", 2005)

                    else

                        if getUserStats(client, 61, 0) >= 1 then

                            sendSysMsg(client, "Sorry, you`re ineligible to use the FireSeal.", 2005)

                        else

                            if hasItem(client, 722723, 1) then

                                if hasItem(client, 722724, 1) then

                                    if hasItem(client, 722725, 1) then

                                        if hasItem(client, 722728, 1) then

                                            text(client, "If you want me to release a Banshee, you must bring an ImmortalStone, Moss, DreamGrass and SoulAroma. Have you got all ready?")
                                            text(client, "")
                                            link(client, "Of course.", 1)
                                            link(client, "Not yet.", 255)
                                            create(client)

                                        else

                                            sendSysMsg(client, "You can`t open FireSeal. Please bring an ImmortalStone, Moss, DreamGrass and SoulAroma to open it.", 2005)

                                        end

                                    else

                                        sendSysMsg(client, "You can`t open FireSeal. Please bring an ImmortalStone, Moss, DreamGrass and SoulAroma to open it.", 2005)

                                    end

                                else

                                    sendSysMsg(client, "You can`t open FireSeal. Please bring an ImmortalStone, Moss, DreamGrass and SoulAroma to open it.", 2005)

                                end

                            else

                                sendSysMsg(client, "You can`t open FireSeal. Please bring an ImmortalStone, Moss, DreamGrass and SoulAroma to open it.", 2005)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if countMonsterByName(client, "Banshee", 1700) == 1 then

                sendSysMsg(client, "This seal is already open.", 2005)

            else

                if hasItem(client, 722723, 1) then

                    if hasItem(client, 722724, 1) then

                        if hasItem(client, 722725, 1) then

                            if hasItem(client, 722728, 1) then

                                spendItem(client, 722723, 1)
                                spendItem(client, 722724, 1)
                                spendItem(client, 722725, 1)
                                spendItem(client, 722728, 1)
                                spawnMonster(client, 3634, "Banshee", 0, 0, 1700, 925, 934, 5602, 0)
                                sendSysMsg(client, "A Banshee has come out of the FireSeal.", 2005)
                                setUserStats(client, 61, 16, 1, true)

                            else

                                sendSysMsg(client, "You can`t open FireSeal. Please bring an ImmortalStone, Moss, DreamGrass and SoulAroma to open it.", 2005)

                            end

                        else

                            sendSysMsg(client, "You can`t open FireSeal. Please bring an ImmortalStone, Moss, DreamGrass and SoulAroma to open it.", 2005)

                        end

                    else

                        sendSysMsg(client, "You can`t open FireSeal. Please bring an ImmortalStone, Moss, DreamGrass and SoulAroma to open it.", 2005)

                    end

                else

                    sendSysMsg(client, "You can`t open FireSeal. Please bring an ImmortalStone, Moss, DreamGrass and SoulAroma to open it.", 2005)

                end

            end

        end

    end

end
