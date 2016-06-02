--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/21/2015 11:50:19 AM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask3608(client, idx)
    name = "SatanSeal"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMetempsychosis(client) == 2 then

                sendSysMsg(client, "Sorry, you`re ineligible to use the SatanSeal.", 2005)

            else

                if getMetempsychosis(client) == 0 then

                    sendSysMsg(client, "Sorry, you`re ineligible to use the SatanSeal.", 2005)

                else

                    if getLevel(client) < 120 then

                        sendSysMsg(client, "Sorry, you`re ineligible to use the SatanSeal.", 2005)

                    else

                        if getUserStats(client, 61, 0) == 3 then

                            if hasItem(client, 722727, 1) then

                                text(client, "Satan is imprisoned here. Only Squama Bead can open the seal to summon a Satan. Do you want to open the seal now?")
                                link(client, "Yeah.", 1)
                                link(client, "I`ve got to go.", 255)
                                create(client)

                            else

                                sendSysMsg(client, "You can`t open the SatanSeal. Please bring a SquamaBead to open it.", 2005)

                            end

                        else

                            sendSysMsg(client, "Sorry, you`re ineligible to use the SatanSeal.", 2005)

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if countMonsterByName(client, "Satan", 1700) == 1 then

                sendSysMsg(client, "The SatanSeal is already open.", 2005)

            else

                if countMonsterByName(client, "BeastSatan", 1700) == 1 then

                    sendSysMsg(client, "The SatanSeal is already open.", 2005)

                else

                    if countMonsterByName(client, "FurySatan", 1700) == 1 then

                        sendSysMsg(client, "The SatanSeal is already open.", 2005)

                    else

                        spendItem(client, 722727, 1)
                        spawnMonster(client, 3644, "Satan", 0, 0, 1700, 337, 341, 5604, 0)
                        broadcastMapMsg(client, 1700, "" .. getName(client) .. " has opened the SatanSeal! A Satan has come out of the seal.", 2005)

                    end

                end

            end

        end

    end

end
