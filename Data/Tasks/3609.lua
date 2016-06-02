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

function processTask3609(client, idx)
    name = "CleansingStove"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMetempsychosis(client) == 2 then

                sendSysMsg(client, "Sorry, you`re ineligible to use the CleansingStove.", 2005)

            else

                if getMetempsychosis(client) == 0 then

                    sendSysMsg(client, "Sorry, you`re ineligible to use the CleansingStove.", 2005)

                else

                    if getLevel(client) < 120 then

                        sendSysMsg(client, "Sorry, you`re ineligible to use the CleansingStove.", 2005)

                    else

                        if getUserStats(client, 61, 0) >= 1 then

                            sendSysMsg(client, "Sorry, you`re ineligible to use the CleansingStove.", 2005)

                        else

                            if hasItem(client, 722730, 1) then

                                text(client, "There is floating light in the stove. You can`t see it clearly. What are you coming for?")
                                link(client, "Cleanse my ImpureVigor.", 1)
                                link(client, "Just passing by.", 255)
                                create(client)

                            else

                                sendSysMsg(client, "You can`t open the CleansingStove. Please bring an ImpureVigor to open it.", 2005)

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if countMonsterByName(client, "CleansingDevil", 1700) == 1 then

                sendSysMsg(client, "This stove is already open.", 2005)

            else

                spendItem(client, 722730, 1)
                spawnMonster(client, 3635, "CleansingDevil", 0, 0, 1700, 717, 738, 5603, 0)
                sendSysMsg(client, "A CleansingDevil has come out of the stove.", 2005)
                setUserStats(client, 61, 17, 1, true)

            end

        end

    end

end
