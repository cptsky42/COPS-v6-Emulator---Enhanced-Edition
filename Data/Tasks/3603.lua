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

function processTask3603(client, idx)
    name = "Angela"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMetempsychosis(client) == 2 then

                text(client, "Wow! You`ve completed your second rebirth. Congratulations!")
                link(client, "Thanks!", 255)
                pic(client, 153)
                create(client)

            else

                if getMetempsychosis(client) == 0 then

                    sendSysMsg(client, "Sorry, you are not a level 120+ reborn person or haven`t completed the second stage of your tests.", 2005)

                else

                    if getLevel(client) < 120 then

                        sendSysMsg(client, "Sorry, you are not a level 120+ reborn person or haven`t completed the second stage of your tests.", 2005)

                    else

                        if getUserStats(client, 61, 0) >= 2 then

                            if getUserStats(client, 61, 0) == 2 then

                                if getUserStats(client, 61, 21) >= 9 then

                                    text(client, "The BindingCurse has shed all of its blood, but its evil")
                                    text(client, "force is not weakened at all. My fellows can`t be released")
                                    text(client, "until Satan is killed. I lost SquamaBead, the key to open")
                                    text(client, "Satan Seal. That seal protects Satan from being hurt. The")
                                    text(client, "SquamaBead has fallen into hands of evil spirits. Can you")
                                    text(client, "find it for me?")
                                    link(client, "I will try my best.", 1)
                                    link(client, "Sorry, I am busy.", 255)
                                    pic(client, 153)
                                    create(client)

                                else

                                    if getUserStats(client, 61, 21) == 0 then

                                        if getUserStats(client, 61, 2) == 0 then

                                            text(client, "I`ve heard about your merits. Satan is imprisoned in the")
                                            text(client, "depth of this place. I am the warden. I have 8 guards:")
                                            text(client, "Andrew, Peter, Philip, Timothy, Daphne, Victoria, Wayne,")
                                            text(client, "Theodore. They are killed and cursed by Satan. Their souls")
                                            text(client, "can`t be released from suffering. Please kindly help them.")
                                            link(client, "What can I do?", 2)
                                            link(client, "I have no time.", 255)
                                            pic(client, 153)
                                            create(client)

                                        else

                                            text(client, "The Binding Curse is not broken yet. Please go to kill the evil spirits as soon as possible.")
                                            link(client, "I`ve got to go.", 255)
                                            pic(client, 153)
                                            create(client)

                                        end

                                    else

                                        if getUserStats(client, 61, 21) == 1 then

                                            text(client, "The BindingCurse has shed 2400 drops of its blood. It still has 19200 drops of blood. Please try to make it shed ")
                                            text(client, "more blood. My fellows will be very grateful to you.")
                                            link(client, "I will try my best.", 255)
                                            pic(client, 153)
                                            create(client)

                                        else

                                            if getUserStats(client, 61, 21) == 2 then

                                                text(client, "The BindingCurse has shed 4800 drops of its blood. It still has 16800 drops of blood. Please try to make it shed ")
                                                text(client, "more blood. My fellows will be very grateful to you.")
                                                link(client, "I will try my best.", 255)
                                                pic(client, 153)
                                                create(client)

                                            else

                                                if getUserStats(client, 61, 21) == 3 then

                                                    text(client, "The BindingCurse has shed 7200 drops of its blood. It still has 14400 drops of blood. Please try to make it shed ")
                                                    text(client, "more blood. My fellows will be very grateful to you.")
                                                    link(client, "I will try my best.", 255)
                                                    pic(client, 153)
                                                    create(client)

                                                else

                                                    if getUserStats(client, 61, 21) == 4 then

                                                        text(client, "The BindingCurse has shed 9600 drops of its blood. It still has 12000 drops of blood. Please try to make it shed ")
                                                        text(client, "more blood. My fellows will be very grateful to you.")
                                                        link(client, "I will try my best.", 255)
                                                        pic(client, 153)
                                                        create(client)

                                                    else

                                                        if getUserStats(client, 61, 21) == 5 then

                                                            text(client, "The BindingCurse has shed 12000 drops of its blood. It still has 9600 drops of blood. Please try to make it shed ")
                                                            text(client, "more blood. My fellows will be very grateful to you.")
                                                            link(client, "I will try my best.", 255)
                                                            pic(client, 153)
                                                            create(client)

                                                        else

                                                            if getUserStats(client, 61, 21) == 6 then

                                                                text(client, "The BindingCurse has shed 14400 drops of its blood. It still has 7200 drops of blood. Please try to make it shed ")
                                                                text(client, "more blood. My fellows will be very grateful to you.")
                                                                link(client, "I will try my best.", 255)
                                                                pic(client, 153)
                                                                create(client)

                                                            else

                                                                if getUserStats(client, 61, 21) == 7 then

                                                                    text(client, "The BindingCurse has shed 16800 drops of its blood. It still has 4800 drops of blood. Please try to make it shed ")
                                                                    text(client, "more blood. My fellows will be very grateful to you.")
                                                                    link(client, "I will try my best.", 255)
                                                                    pic(client, 153)
                                                                    create(client)

                                                                else

                                                                    if getUserStats(client, 61, 21) == 8 then

                                                                        text(client, "The BindingCurse has shed 19200 drops of its blood. It still has 2400 drops of blood. Please try to make it shed ")
                                                                        text(client, "more blood. My fellows will be very grateful to you.")
                                                                        link(client, "I will try my best.", 255)
                                                                        pic(client, 153)
                                                                        create(client)

                                                                    end

                                                                end

                                                            end

                                                        end

                                                    end

                                                end

                                            end

                                        end

                                    end

                                end

                            else

                                if getUserStats(client, 61, 0) == 4 then

                                    text(client, "You made it. Thank you very much for saving us. You`ve cleansed this hopeless abyss. Now the evil force of Satan ")
                                    text(client, "has gone away, this place will disappear very soon. All souls here will get reborn. You`ve completed your tests. ")
                                    text(client, "You may leave here to take second rebirth now.")
                                    text(client, "")
                                    link(client, "Glad to hear that.", 255)
                                    pic(client, 153)
                                    create(client)

                                else

                                    if getUserStats(client, 61, 0) == 3 then

                                        if hasItem(client, 722727, 1) then

                                            text(client, "It`s the right SquamaBead I`ve lost. You`ve got it! With this key of SatanSeal, you can go to kill Satan now. Satan")
                                            text(client, "will take different forms after death. Be careful. You`ve passed so many tests. I believe you can destroy Satan.")
                                            text(client, "")
                                            text(client, "")
                                            link(client, "I`ve got to kill Satan.", 255)
                                            pic(client, 153)
                                            create(client)

                                        else

                                            text(client, "The BindingCurse has shed all of its blood, but its evil")
                                            text(client, "force is not weakened at all. My fellows can`t be released")
                                            text(client, "until Satan is killed. I lost SquamaBead, the key to open")
                                            text(client, "Satan Seal. That seal protects Santa from being hurt. The")
                                            text(client, "SquamaBead has fallen into hands of evil spirits. Can you find it for me?")
                                            text(client, "")
                                            link(client, "I will try my best.", 3)
                                            link(client, "Sorry, I am busy.", 255)
                                            pic(client, 153)
                                            create(client)

                                        end

                                    end

                                end

                            end

                        else

                            sendSysMsg(client, "Sorry, you are not a level 120+ reborn person or haven`t completed the second stage of your tests.", 2005)

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setUserStats(client, 61, 0, 3, true)
            sendSysMsg(client, "You`ve completed the third stage of your tests!", 2005)

        end

    elseif (idx == 2) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "The 8 curses are closely linked to form a BindingCurse. You")
            text(client, "must break them in this order: Andrew, Peter, Philip,")
            text(client, "Timothy, Daphne, Victoria, Wayne, Theodore.")
            text(client, "The BindingCurse has 21600 drops of blood. You must break")
            text(client, " the 8 curses repeatedly until the BindingCurse sheds all of its blood.")
            text(client, "")
            text(client, "")
            link(client, "I see.", 4)
            link(client, "Too troublesome.", 255)
            pic(client, 153)
            create(client)

        end

    elseif (idx == 3) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "The SquamaBead is probably in the hands of SerpentSpirit, HadesLord, DemonLord, FiendLord, FuryApe, DemonBat, ")
            text(client, "FurySkeleton, FearlessBeast. Please try to get it for me.")
            text(client, "")
            link(client, "OK.", 255)
            pic(client, 153)
            create(client)

        end

    elseif (idx == 4) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "If the BindingCurse has shed all of its blood and the curse can`t be removed, there is only one reason. You may come")
            text(client, "to me by then. I will tell you what to do.")
            link(client, "OK.", 255)
            pic(client, 153)
            create(client)

        end

    end

end
