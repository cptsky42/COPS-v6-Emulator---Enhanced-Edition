--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 6/23/2015 7:00:50 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask3602(client, idx)
    name = "Bryan"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMetempsychosis(client) == 2 then

                text(client, "Wow! You have completed your second rebirth. You look so cool. I couldn`t recognize you at the first sight.")
                link(client, "Glad to hear that.", 255)
                pic(client, 54)
                create(client)

            else

                if getMetempsychosis(client) == 0 then

                    text(client, "You haven`t completed your first rebirth yet. How dare you come here?")
                    link(client, "I...I...", 1)
                    pic(client, 54)
                    create(client)

                else

                    if getLevel(client) < 120 then

                        text(client, "You can`t take the second stage of this quest until you completed the first stage. Please work harder.")
                        text(client, "")
                        link(client, "I see.", 255)
                        pic(client, 54)
                        create(client)

                    else

                        if getUserStats(client, 61, 0) >= 1 then

                            if getUserStats(client, 61, 0) == 1 then

                                if getUserStats(client, 61, 1) == 0 then

                                    text(client, "The grievance of the dead souls here has formed a mountain of 40000 meters high. That mountain will sink for a few")
                                    text(client, "meters for every monster here you kill. When that mountain is razed to the ground, you will release many dead souls")
                                    text(client, "from suffering. Your good merits will be infinite and boundless.")
                                    text(client, "")
                                    text(client, "")
                                    link(client, "I will kill those monsters.", 2)
                                    link(client, "Too difficult.", 255)
                                    pic(client, 54)
                                    create(client)

                                else

                                    if getUserStats(client, 61, 1) >= 40000 then

                                        setUserStats(client, 61, 0, 2, true)
                                        setUserStats(client, 61, 2, 0, true)
                                        setUserStats(client, 61, 21, 0, true)

                                    else

                                        if getUserStats(client, 61, 11) == 1 then

                                            text(client, "The mountain of grievance has sunk down, but you still need to work harder to raze it to the ground.")
                                            text(client, "")
                                            link(client, "How high is it now?", 3)
                                            link(client, "I am too tired.", 255)
                                            pic(client, 54)
                                            create(client)

                                        else

                                            text(client, "Those souls of the dead haven`t been completely released from suffering. Please try to kill more monsters.")
                                            link(client, "I will.", 255)
                                            pic(client, 54)
                                            create(client)

                                        end

                                    end

                                end

                            else

                                text(client, "Well done! You`ve completed your quest with good merits. You have a chance to get reborn soon. Angela will guide ")
                                text(client, "you through the next tests.")
                                text(client, "")
                                link(client, "Thanks for your advice.", 255)
                                pic(client, 54)
                                create(client)

                            end

                        else

                            text(client, "You can`t take the second stage of this quest until you completed the first stage. Please work harder.")
                            text(client, "")
                            link(client, "I see.", 255)
                            pic(client, 54)
                            create(client)

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            sendSysMsg(client, "Bryan has wielded a sword at you. You are scared to run away.", 2005)

        end

    elseif (idx == 2) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "I will record your scores. If you want to check how high the mountain of grievance remains, you may return to check")
            text(client, "at any time.")
            link(client, "I see.", 255)
            pic(client, 54)
            create(client)

        end

    elseif (idx == 3) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setUserStats(client, 61, 11, 2, true)
            setRegister(client, 6, 0)

        end

    end

end
