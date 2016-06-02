--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:18 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask1614(client, idx)
    name = "BlueMouse"
    face = 1

    if (idx == 0) then

        action = randomAction(client, 1, 8)
        if action == 1 then
            moveNpc(client, 1614, 2000, 81, 81)
            if hasItem(client, 722511, 1) then

                if (rand(client, 10) < 7) then

                    text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                    link(client, "Damn!", 255)
                    create(client)

                else

                    if (getItemsCount(client) <= 38) then

                        spendItem(client, 722511, 1)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 2 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 3 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 4 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 5 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 6 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 7 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 8 then
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                awardItem(client, "722515", 1)
                                sendSysMsg(client, "Congratulations! You got SecretCommand", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            end

                        end


                    else

                        sendSysMsg(client, "You do not have enough space in your bag.", 2005)

                    end

                end

            else

                if hasItem(client, 722510, 1) then

                    if (rand(client, 10) < 7) then

                        text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                        link(client, "Damn!", 255)
                        create(client)

                    else

                        if (getItemsCount(client) <= 38) then

                            spendItem(client, 722510, 1)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                action = randomAction(client, 1, 8)
                                if action == 1 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 2 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 3 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 4 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 5 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 6 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 7 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 8 then
                                    awardItem(client, "722514", 1)
                                    sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                end

                            end


                        else

                            sendSysMsg(client, "Congratulations! You got", 2005)

                        end

                    end

                else

                    text(client, "You should at least bring a needle to catch me. Goodbye!")
                    link(client, "Damn!", 255)
                    create(client)

                end

            end
        elseif action == 2 then
            moveNpc(client, 1614, 2001, 98, 143)
            if hasItem(client, 722511, 1) then

                if (rand(client, 10) < 7) then

                    text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                    link(client, "Damn!", 255)
                    create(client)

                else

                    if (getItemsCount(client) <= 38) then

                        spendItem(client, 722511, 1)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 2 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 3 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 4 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 5 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 6 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 7 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 8 then
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                awardItem(client, "722515", 1)
                                sendSysMsg(client, "Congratulations! You got SecretCommand", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            end

                        end


                    else

                        sendSysMsg(client, "You do not have enough space in your bag.", 2005)

                    end

                end

            else

                if hasItem(client, 722510, 1) then

                    if (rand(client, 10) < 7) then

                        text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                        link(client, "Damn!", 255)
                        create(client)

                    else

                        if (getItemsCount(client) <= 38) then

                            spendItem(client, 722510, 1)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                action = randomAction(client, 1, 8)
                                if action == 1 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 2 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 3 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 4 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 5 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 6 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 7 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 8 then
                                    awardItem(client, "722514", 1)
                                    sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                end

                            end


                        else

                            sendSysMsg(client, "Congratulations! You got", 2005)

                        end

                    end

                else

                    text(client, "You should at least bring a needle to catch me. Goodbye!")
                    link(client, "Damn!", 255)
                    create(client)

                end

            end
        elseif action == 3 then
            moveNpc(client, 1614, 2002, 83, 132)
            if hasItem(client, 722511, 1) then

                if (rand(client, 10) < 7) then

                    text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                    link(client, "Damn!", 255)
                    create(client)

                else

                    if (getItemsCount(client) <= 38) then

                        spendItem(client, 722511, 1)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 2 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 3 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 4 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 5 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 6 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 7 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 8 then
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                awardItem(client, "722515", 1)
                                sendSysMsg(client, "Congratulations! You got SecretCommand", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            end

                        end


                    else

                        sendSysMsg(client, "You do not have enough space in your bag.", 2005)

                    end

                end

            else

                if hasItem(client, 722510, 1) then

                    if (rand(client, 10) < 7) then

                        text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                        link(client, "Damn!", 255)
                        create(client)

                    else

                        if (getItemsCount(client) <= 38) then

                            spendItem(client, 722510, 1)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                action = randomAction(client, 1, 8)
                                if action == 1 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 2 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 3 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 4 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 5 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 6 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 7 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 8 then
                                    awardItem(client, "722514", 1)
                                    sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                end

                            end


                        else

                            sendSysMsg(client, "Congratulations! You got", 2005)

                        end

                    end

                else

                    text(client, "You should at least bring a needle to catch me. Goodbye!")
                    link(client, "Damn!", 255)
                    create(client)

                end

            end
        elseif action == 4 then
            moveNpc(client, 1614, 2004, 111, 134)
            if hasItem(client, 722511, 1) then

                if (rand(client, 10) < 7) then

                    text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                    link(client, "Damn!", 255)
                    create(client)

                else

                    if (getItemsCount(client) <= 38) then

                        spendItem(client, 722511, 1)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 2 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 3 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 4 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 5 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 6 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 7 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 8 then
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                awardItem(client, "722515", 1)
                                sendSysMsg(client, "Congratulations! You got SecretCommand", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            end

                        end


                    else

                        sendSysMsg(client, "You do not have enough space in your bag.", 2005)

                    end

                end

            else

                if hasItem(client, 722510, 1) then

                    if (rand(client, 10) < 7) then

                        text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                        link(client, "Damn!", 255)
                        create(client)

                    else

                        if (getItemsCount(client) <= 38) then

                            spendItem(client, 722510, 1)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                action = randomAction(client, 1, 8)
                                if action == 1 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 2 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 3 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 4 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 5 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 6 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 7 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 8 then
                                    awardItem(client, "722514", 1)
                                    sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                end

                            end


                        else

                            sendSysMsg(client, "Congratulations! You got", 2005)

                        end

                    end

                else

                    text(client, "You should at least bring a needle to catch me. Goodbye!")
                    link(client, "Damn!", 255)
                    create(client)

                end

            end
        elseif action == 5 then
            moveNpc(client, 1614, 2005, 83, 132)
            if hasItem(client, 722511, 1) then

                if (rand(client, 10) < 7) then

                    text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                    link(client, "Damn!", 255)
                    create(client)

                else

                    if (getItemsCount(client) <= 38) then

                        spendItem(client, 722511, 1)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 2 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 3 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 4 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 5 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 6 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 7 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 8 then
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                awardItem(client, "722515", 1)
                                sendSysMsg(client, "Congratulations! You got SecretCommand", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            end

                        end


                    else

                        sendSysMsg(client, "You do not have enough space in your bag.", 2005)

                    end

                end

            else

                if hasItem(client, 722510, 1) then

                    if (rand(client, 10) < 7) then

                        text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                        link(client, "Damn!", 255)
                        create(client)

                    else

                        if (getItemsCount(client) <= 38) then

                            spendItem(client, 722510, 1)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                action = randomAction(client, 1, 8)
                                if action == 1 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 2 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 3 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 4 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 5 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 6 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 7 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 8 then
                                    awardItem(client, "722514", 1)
                                    sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                end

                            end


                        else

                            sendSysMsg(client, "Congratulations! You got", 2005)

                        end

                    end

                else

                    text(client, "You should at least bring a needle to catch me. Goodbye!")
                    link(client, "Damn!", 255)
                    create(client)

                end

            end
        elseif action == 6 then
            moveNpc(client, 1614, 2007, 98, 103)
            if hasItem(client, 722511, 1) then

                if (rand(client, 10) < 7) then

                    text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                    link(client, "Damn!", 255)
                    create(client)

                else

                    if (getItemsCount(client) <= 38) then

                        spendItem(client, 722511, 1)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 2 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 3 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 4 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 5 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 6 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 7 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 8 then
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                awardItem(client, "722515", 1)
                                sendSysMsg(client, "Congratulations! You got SecretCommand", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            end

                        end


                    else

                        sendSysMsg(client, "You do not have enough space in your bag.", 2005)

                    end

                end

            else

                if hasItem(client, 722510, 1) then

                    if (rand(client, 10) < 7) then

                        text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                        link(client, "Damn!", 255)
                        create(client)

                    else

                        if (getItemsCount(client) <= 38) then

                            spendItem(client, 722510, 1)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                action = randomAction(client, 1, 8)
                                if action == 1 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 2 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 3 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 4 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 5 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 6 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 7 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 8 then
                                    awardItem(client, "722514", 1)
                                    sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                end

                            end


                        else

                            sendSysMsg(client, "Congratulations! You got", 2005)

                        end

                    end

                else

                    text(client, "You should at least bring a needle to catch me. Goodbye!")
                    link(client, "Damn!", 255)
                    create(client)

                end

            end
        elseif action == 7 then
            moveNpc(client, 1614, 2009, 83, 132)
            if hasItem(client, 722511, 1) then

                if (rand(client, 10) < 7) then

                    text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                    link(client, "Damn!", 255)
                    create(client)

                else

                    if (getItemsCount(client) <= 38) then

                        spendItem(client, 722511, 1)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 2 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 3 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 4 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 5 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 6 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 7 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 8 then
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                awardItem(client, "722515", 1)
                                sendSysMsg(client, "Congratulations! You got SecretCommand", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            end

                        end


                    else

                        sendSysMsg(client, "You do not have enough space in your bag.", 2005)

                    end

                end

            else

                if hasItem(client, 722510, 1) then

                    if (rand(client, 10) < 7) then

                        text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                        link(client, "Damn!", 255)
                        create(client)

                    else

                        if (getItemsCount(client) <= 38) then

                            spendItem(client, 722510, 1)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                action = randomAction(client, 1, 8)
                                if action == 1 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 2 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 3 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 4 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 5 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 6 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 7 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 8 then
                                    awardItem(client, "722514", 1)
                                    sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                end

                            end


                        else

                            sendSysMsg(client, "Congratulations! You got", 2005)

                        end

                    end

                else

                    text(client, "You should at least bring a needle to catch me. Goodbye!")
                    link(client, "Damn!", 255)
                    create(client)

                end

            end
        elseif action == 8 then
            moveNpc(client, 1614, 2011, 111, 134)
            if hasItem(client, 722511, 1) then

                if (rand(client, 10) < 7) then

                    text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                    link(client, "Damn!", 255)
                    create(client)

                else

                    if (getItemsCount(client) <= 38) then

                        spendItem(client, 722511, 1)
                        action = randomAction(client, 1, 8)
                        if action == 1 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 2 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 3 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 4 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 5 then
                            awardItem(client, "722514", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 6 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 7 then
                            awardItem(client, "722514", 1)
                            awardItem(client, "722513", 1)
                            sendSysMsg(client, "Congratulations! You got RoyalSword and PinetumPicture", 2005)
                            text(client, "Let me go! I will give you something.")
                            link(client, "Go away.", 255)
                            create(client)
                        elseif action == 8 then
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722514", 1)
                                awardItem(client, "722513", 1)
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got RoyalSword, PinetumPicture and AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                awardItem(client, "722515", 1)
                                sendSysMsg(client, "Congratulations! You got SecretCommand", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            end

                        end


                    else

                        sendSysMsg(client, "You do not have enough space in your bag.", 2005)

                    end

                end

            else

                if hasItem(client, 722510, 1) then

                    if (rand(client, 10) < 7) then

                        text(client, "Oh, my god! Narrowly escaped! Bye, guy!")
                        link(client, "Damn!", 255)
                        create(client)

                    else

                        if (getItemsCount(client) <= 38) then

                            spendItem(client, 722510, 1)
                            action = randomAction(client, 1, 8)
                            if action == 1 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 2 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 3 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 4 then
                                awardItem(client, "722512", 1)
                                sendSysMsg(client, "Congratulations! You got AsterNecklace", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 5 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 6 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 7 then
                                awardItem(client, "722513", 1)
                                sendSysMsg(client, "Congratulations! You got PinetumPicture", 2005)
                                text(client, "Let me go! I will give you something.")
                                link(client, "Go away.", 255)
                                create(client)
                            elseif action == 8 then
                                action = randomAction(client, 1, 8)
                                if action == 1 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 2 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 3 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 4 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 5 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 6 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 7 then
                                    awardItem(client, "722512", 1)
                                    awardItem(client, "722513", 1)
                                    sendSysMsg(client, "Congratulations! You got AsterNecklace and PinetumPicture", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                elseif action == 8 then
                                    awardItem(client, "722514", 1)
                                    sendSysMsg(client, "Congratulations! You got RoyalSword", 2005)
                                    text(client, "Let me go! I will give you something.")
                                    link(client, "Go away.", 255)
                                    create(client)
                                end

                            end


                        else

                            sendSysMsg(client, "Congratulations! You got", 2005)

                        end

                    end

                else

                    text(client, "You should at least bring a needle to catch me. Goodbye!")
                    link(client, "Damn!", 255)
                    create(client)

                end

            end
        end


    end

end
